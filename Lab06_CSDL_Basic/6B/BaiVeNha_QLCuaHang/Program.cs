using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Lab06B.Core;
using Lab06B.WinForms;

namespace BaiVeNha_QLCuaHang;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmCuaHang());
    }
}

public sealed class FrmCuaHang : DatabaseFormBase
{
    private readonly DataGridView _grid = Ui.Grid();

    public FrmCuaHang()
    {
        Ui.Configure(this, "BÀI VỀ NHÀ - QUẢN LÝ CỬA HÀNG", 1050, 650);
        MenuStrip menu = new() { Dock = DockStyle.Top };
        ToolStripMenuItem nghiepVu = new("Nghiệp vụ");
        nghiepVu.DropDownItems.Add("Tạo tồn kho hôm nay", null, (_, _) => TaoTonKho());
        nghiepVu.DropDownItems.Add("Thêm sản phẩm mới", null, (_, _) => new FrmSanPham().ShowDialog(this));
        nghiepVu.DropDownItems.Add("Bán hàng", null, (_, _) => new FrmBanHang().ShowDialog(this));
        nghiepVu.DropDownItems.Add("Thoát", null, (_, _) => Close());
        menu.Items.Add(nghiepVu);

        FlowLayoutPanel actions = Ui.Row(
            Ui.Button("Tạo tồn kho", (_, _) => TaoTonKho()),
            Ui.Button("Thêm sản phẩm", (_, _) => { new FrmSanPham().ShowDialog(this); NapTonKho(); }),
            Ui.Button("Bán hàng", (_, _) => { new FrmBanHang().ShowDialog(this); NapTonKho(); }),
            Ui.Button("Làm mới", (_, _) => NapTonKho()));
        Controls.Add(_grid);
        Controls.Add(Ui.Hint("QLCuaHang"));
        Controls.Add(actions);
        Controls.Add(Ui.Title("BÀI VỀ NHÀ - QUẢN LÝ CỬA HÀNG"));
        Controls.Add(menu);
        MainMenuStrip = menu;
        Shown += (_, _) => SafeRun(() => { DatabaseInitializer.EnsureStore(); NapTonKho(); });
    }

    private void NapTonKho()
    {
        _grid.DataSource = DataAccess.QueryStore(@"SELECT sp.MaSP,sp.TenSP,tk.NgayTon,tk.SLDau,tk.SLNhap,tk.SLBan,
            tk.SLDau+tk.SLNhap-tk.SLBan AS SLCon FROM dbo.SANPHAM sp
            LEFT JOIN dbo.TONKHO tk ON tk.MaSP=sp.MaSP AND tk.NgayTon=CONVERT(date,GETDATE()) ORDER BY sp.MaSP");
    }

    private void TaoTonKho() => SafeRun(() =>
    {
        // Tồn đầu hôm nay bằng tồn cuối của ngày gần nhất; mỗi ngày chỉ tạo một lần.
        int count = DataAccess.ExecuteStore(@"INSERT dbo.TONKHO(MaSP,NgayTon,SLDau,SLNhap,SLBan)
            SELECT sp.MaSP,CONVERT(date,GETDATE()),
                   ISNULL((SELECT TOP 1 t.SLDau+t.SLNhap-t.SLBan FROM dbo.TONKHO t
                           WHERE t.MaSP=sp.MaSP AND t.NgayTon<CONVERT(date,GETDATE()) ORDER BY t.NgayTon DESC),0),0,0
            FROM dbo.SANPHAM sp
            WHERE NOT EXISTS(SELECT 1 FROM dbo.TONKHO x WHERE x.MaSP=sp.MaSP AND x.NgayTon=CONVERT(date,GETDATE()));");
        MessageBox.Show(count > 0 ? $"Đã tạo tồn kho cho {count} sản phẩm." : "Tồn kho hôm nay đã được tạo.");
        NapTonKho();
    });
}

public sealed class FrmSanPham : DatabaseFormBase
{
    private readonly TextBox _tenSanPham = Ui.TextBox(300);
    private readonly DataGridView _grid = Ui.Grid();

    public FrmSanPham()
    {
        Ui.Configure(this, "THÊM SẢN PHẨM MỚI", 820, 540);
        FlowLayoutPanel editor = Ui.Row(Ui.Label("Tên sản phẩm"), _tenSanPham,
            Ui.Button("Thêm", (_, _) => Them()), Ui.Button("Thoát", (_, _) => Close()));
        Controls.Add(_grid);
        Controls.Add(Ui.Hint("QLCuaHang"));
        Controls.Add(editor);
        Controls.Add(Ui.Title("THÊM SẢN PHẨM MỚI"));
        Shown += (_, _) => SafeRun(() => { DatabaseInitializer.EnsureStore(); NapDuLieu(); });
    }

    private void NapDuLieu() => _grid.DataSource = DataAccess.QueryStore("SELECT MaSP,TenSP FROM dbo.SANPHAM ORDER BY MaSP");

    private void Them()
    {
        if (!Require((_tenSanPham.Text, "Tên sản phẩm không được để trống."))) return;
        SafeRun(() =>
        {
            using SqlConnection connection = DataAccess.OpenStore();
            using SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                // OUTPUT INSERTED lấy mã tự tăng vừa tạo để thêm dòng tồn kho tương ứng.
                using SqlCommand product = new("INSERT dbo.SANPHAM(TenSP) OUTPUT INSERTED.MaSP VALUES(@ten)", connection, transaction);
                product.Parameters.AddWithValue("@ten", _tenSanPham.Text.Trim());
                int id = Convert.ToInt32(product.ExecuteScalar());
                using SqlCommand stock = new("INSERT dbo.TONKHO(MaSP,NgayTon,SLDau,SLNhap,SLBan) VALUES(@ma,CONVERT(date,GETDATE()),0,0,0)", connection, transaction);
                stock.Parameters.AddWithValue("@ma", id);
                stock.ExecuteNonQuery();
                transaction.Commit();
                _tenSanPham.Clear();
                NapDuLieu();
                MessageBox.Show("Đã thêm sản phẩm và dòng tồn kho đầu tiên.");
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        });
    }
}

public sealed class FrmBanHang : DatabaseFormBase
{
    private readonly TextBox _maHoaDon = Ui.TextBox(120);
    private readonly ComboBox _sanPham = Ui.ComboBox(260);
    private readonly TextBox _soLuong = Ui.TextBox(120);
    private readonly DataGridView _grid = Ui.Grid();
    private readonly Button _them;
    private readonly Button _huy;
    private readonly Button _thanhToan;
    private int _invoiceId;

    public FrmBanHang()
    {
        Ui.Configure(this, "BÁN HÀNG", 980, 620);
        _maHoaDon.ReadOnly = true;
        _them = Ui.Button("Thêm chi tiết", (_, _) => ThemChiTiet());
        _huy = Ui.Button("Hủy hóa đơn", (_, _) => HuyHoaDon());
        _thanhToan = Ui.Button("Thanh toán", (_, _) => ThanhToan());
        FlowLayoutPanel editor = Ui.Row(
            Ui.Button("Tạo hóa đơn mới", (_, _) => TaoHoaDon()), Ui.Label("Mã hóa đơn"), _maHoaDon,
            Ui.Label("Sản phẩm"), _sanPham, Ui.Label("Số lượng"), _soLuong, _them, _huy, _thanhToan);
        Controls.Add(_grid);
        Controls.Add(Ui.Hint("QLCuaHang"));
        Controls.Add(editor);
        Controls.Add(Ui.Title("BÁN HÀNG"));
        Shown += (_, _) => SafeRun(() => { DatabaseInitializer.EnsureStore(); NapSanPham(); DatTrangThai(false); });
    }

    private void NapSanPham()
    {
        _sanPham.Items.Clear();
        using SqlConnection connection = DataAccess.OpenStore();
        using SqlCommand command = new("SELECT MaSP,TenSP FROM dbo.SANPHAM ORDER BY MaSP", connection);
        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read()) _sanPham.Items.Add(new ProductItem(reader.GetInt32(0), reader.GetString(1)));
        if (_sanPham.Items.Count > 0) _sanPham.SelectedIndex = 0;
    }

    private void DatTrangThai(bool enabled)
    {
        _sanPham.Enabled = enabled;
        _soLuong.Enabled = enabled;
        _them.Enabled = enabled;
        _huy.Enabled = enabled;
        _thanhToan.Enabled = enabled;
    }

    private void TaoHoaDon() => SafeRun(() =>
    {
        // Mỗi hóa đơn mới có ngày hiện tại và trạng thái chưa thanh toán.
        _invoiceId = Convert.ToInt32(DataAccess.ScalarStore(
            "INSERT dbo.HOADON(NgayTao,TinhTrangThanhToan) OUTPUT INSERTED.MaHD VALUES(CONVERT(date,GETDATE()),0)"));
        _maHoaDon.Text = _invoiceId.ToString();
        DatTrangThai(true);
        NapChiTiet();
    });

    private void ThemChiTiet()
    {
        if (_invoiceId == 0 || _sanPham.SelectedItem is not ProductItem product || !InputValidator.TryReadPositiveInteger(_soLuong.Text, out int quantity))
        {
            MessageBox.Show("Hãy tạo hóa đơn, chọn sản phẩm và nhập số lượng nguyên dương.");
            return;
        }

        SafeRun(() =>
        {
            using SqlConnection connection = DataAccess.OpenStore();
            using SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                using SqlCommand stock = new(@"SELECT SLDau+SLNhap-SLBan FROM dbo.TONKHO WITH(UPDLOCK,ROWLOCK)
                    WHERE MaSP=@sp AND NgayTon=CONVERT(date,GETDATE())", connection, transaction);
                stock.Parameters.AddWithValue("@sp", product.Id);
                object availableValue = stock.ExecuteScalar();
                int available = availableValue == null ? 0 : Convert.ToInt32(availableValue);
                if (quantity > available) throw new InvalidOperationException($"Số lượng tồn chỉ còn {available}.");

                using SqlCommand detail = new("INSERT dbo.CHITIET_HOADON(MaHD,MaSP,SoLuong) VALUES(@hd,@sp,@sl)", connection, transaction);
                detail.Parameters.AddWithValue("@hd", _invoiceId);
                detail.Parameters.AddWithValue("@sp", product.Id);
                detail.Parameters.AddWithValue("@sl", quantity);
                detail.ExecuteNonQuery();

                // SLBan tăng làm số lượng còn lại giảm theo công thức tồn kho.
                using SqlCommand updateStock = new("UPDATE dbo.TONKHO SET SLBan=SLBan+@sl WHERE MaSP=@sp AND NgayTon=CONVERT(date,GETDATE())", connection, transaction);
                updateStock.Parameters.AddWithValue("@sl", quantity);
                updateStock.Parameters.AddWithValue("@sp", product.Id);
                updateStock.ExecuteNonQuery();
                transaction.Commit();
                _soLuong.Clear();
                NapChiTiet();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        });
    }

    private void HuyHoaDon() => SafeRun(() =>
    {
        if (_invoiceId == 0) return;
        using SqlConnection connection = DataAccess.OpenStore();
        using SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            // Trả lại số lượng tồn trước khi xóa chi tiết và hóa đơn chưa thanh toán.
            using SqlCommand restore = new(@"UPDATE tk SET tk.SLBan=tk.SLBan-ct.SoLuong FROM dbo.TONKHO tk
                JOIN dbo.CHITIET_HOADON ct ON ct.MaSP=tk.MaSP
                WHERE ct.MaHD=@hd AND tk.NgayTon=CONVERT(date,GETDATE())", connection, transaction);
            restore.Parameters.AddWithValue("@hd", _invoiceId);
            restore.ExecuteNonQuery();
            using SqlCommand details = new("DELETE dbo.CHITIET_HOADON WHERE MaHD=@hd", connection, transaction);
            details.Parameters.AddWithValue("@hd", _invoiceId);
            details.ExecuteNonQuery();
            using SqlCommand invoice = new("DELETE dbo.HOADON WHERE MaHD=@hd AND TinhTrangThanhToan=0", connection, transaction);
            invoice.Parameters.AddWithValue("@hd", _invoiceId);
            invoice.ExecuteNonQuery();
            transaction.Commit();
            KetThucHoaDon();
        }
        catch { transaction.Rollback(); throw; }
    });

    private void ThanhToan() => SafeRun(() =>
    {
        if (_invoiceId == 0) return;
        int affected = DataAccess.ExecuteStore("UPDATE dbo.HOADON SET TinhTrangThanhToan=1 WHERE MaHD=@hd AND EXISTS(SELECT 1 FROM dbo.CHITIET_HOADON WHERE MaHD=@hd)", new SqlParameter("@hd", _invoiceId));
        if (affected == 0) { MessageBox.Show("Hóa đơn chưa có sản phẩm."); return; }
        MessageBox.Show("Thanh toán thành công.");
        KetThucHoaDon();
    });

    private void NapChiTiet()
    {
        _grid.DataSource = DataAccess.QueryStore(@"SELECT ct.MaHD,ct.MaSP,sp.TenSP,ct.SoLuong
            FROM dbo.CHITIET_HOADON ct JOIN dbo.SANPHAM sp ON sp.MaSP=ct.MaSP WHERE ct.MaHD=@hd",
            new SqlParameter("@hd", _invoiceId));
    }

    private void KetThucHoaDon()
    {
        _invoiceId = 0;
        _maHoaDon.Clear();
        _soLuong.Clear();
        _grid.DataSource = null;
        DatTrangThai(false);
    }

    private sealed class ProductItem
    {
        public ProductItem(int id, string name) { Id = id; Name = name; }
        public int Id { get; }
        public string Name { get; }
        public override string ToString() => $"{Id} - {Name}";
    }
}
