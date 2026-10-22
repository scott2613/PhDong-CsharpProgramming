using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Lab06B.Core;
using Lab06B.WinForms;

namespace Bai01_QuanLySinhVien;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmQuanLySinhVien());
    }
}

public sealed class FrmQuanLySinhVien : DatabaseFormBase
{
    private readonly ComboBox _maLop = Ui.ComboBox(155);
    private readonly TextBox _maSinhVien = Ui.TextBox(165);
    private readonly TextBox _hoTen = Ui.TextBox(235);
    private readonly DateTimePicker _ngaySinh = new() { Width = 155, Format = DateTimePickerFormat.Short, Margin = new Padding(7) };
    private readonly DataGridView _grid = Ui.Grid();

    public FrmQuanLySinhVien()
    {
        Ui.Configure(this, "BÀI 1 - QUẢN LÝ SINH VIÊN", 1180);
        FlowLayoutPanel editor = Ui.Row(
            Ui.Label("Lớp"), _maLop, Ui.Label("Mã SV"), _maSinhVien, Ui.Label("Họ tên"), _hoTen,
            Ui.Label("Ngày sinh"), _ngaySinh, Ui.Button("Thêm", (_, _) => Them()),
            Ui.Button("Xóa", (_, _) => Xoa()), Ui.Button("Sửa", (_, _) => Sua()));
        Controls.Add(_grid);
        Controls.Add(Ui.Hint());
        Controls.Add(editor);
        Controls.Add(Ui.Title("BÀI 1 - QUẢN LÝ SINH VIÊN"));
        _grid.CellClick += (_, _) => ChonDong();
        Shown += (_, _) => SafeRun(() => { DatabaseInitializer.EnsureUniversity(); NapLop(); NapDuLieu(); });
    }

    private void NapLop()
    {
        _maLop.Items.Clear();
        // SqlDataReader đọc khóa ngoại Mã lớp và đưa vào ComboBox.
        using SqlConnection connection = DataAccess.OpenUniversity();
        using SqlCommand command = new("SELECT MaLop FROM dbo.Lop ORDER BY MaLop", connection);
        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read()) _maLop.Items.Add(reader.GetString(0));
        if (_maLop.Items.Count > 0) _maLop.SelectedIndex = 0;
    }

    private void NapDuLieu() => _grid.DataSource = DataAccess.QueryUniversity("SELECT MaSinhVien,HoTen,NgaySinh,MaLop FROM dbo.SinhVien ORDER BY MaSinhVien");

    private bool DuThongTin()
    {
        if (!Require((_maSinhVien.Text, "Mã sinh viên không được để trống."), (_hoTen.Text, "Họ tên không được để trống."), (Convert.ToString(_maLop.SelectedItem) ?? "", "Hãy chọn lớp."))) return false;
        if (!InputValidator.IsAtLeast17(_ngaySinh.Value.Date, DateTime.Today))
        {
            MessageBox.Show("Sinh viên phải đủ 17 tuổi trở lên (tính theo năm sinh).");
            return false;
        }
        return true;
    }

    private void Them()
    {
        if (!DuThongTin()) return;
        SafeRun(() =>
        {
            if (DataAccess.ExistsUniversity("SELECT 1 FROM dbo.SinhVien WHERE MaSinhVien=@ma", new SqlParameter("@ma", _maSinhVien.Text.Trim())))
            {
                MessageBox.Show("Mã sinh viên đã tồn tại.");
                return;
            }
            ShowResult(DataAccess.ExecuteUniversity(
                "INSERT dbo.SinhVien(MaSinhVien,HoTen,NgaySinh,MaLop) VALUES(@ma,@ten,@ngay,@lop)",
                new SqlParameter("@ma", _maSinhVien.Text.Trim()), new SqlParameter("@ten", _hoTen.Text.Trim()),
                new SqlParameter("@ngay", _ngaySinh.Value.Date), new SqlParameter("@lop", Convert.ToString(_maLop.SelectedItem))));
            NapDuLieu();
        });
    }

    private void Xoa() => SafeRun(() =>
    {
        if (!Require((_maSinhVien.Text, "Hãy nhập mã sinh viên cần xóa."))) return;
        if (DataAccess.ExistsUniversity("SELECT 1 FROM dbo.Diem WHERE MaSinhVien=@ma", new SqlParameter("@ma", _maSinhVien.Text.Trim())))
        {
            MessageBox.Show("Không thể xóa vì sinh viên đã có điểm.");
            return;
        }
        ShowResult(DataAccess.ExecuteUniversity("DELETE dbo.SinhVien WHERE MaSinhVien=@ma", new SqlParameter("@ma", _maSinhVien.Text.Trim())));
        NapDuLieu();
    });

    private void Sua()
    {
        if (!DuThongTin()) return;
        SafeRun(() =>
        {
            ShowResult(DataAccess.ExecuteUniversity("UPDATE dbo.SinhVien SET HoTen=@ten,NgaySinh=@ngay,MaLop=@lop WHERE MaSinhVien=@ma",
                new SqlParameter("@ten", _hoTen.Text.Trim()), new SqlParameter("@ngay", _ngaySinh.Value.Date),
                new SqlParameter("@lop", Convert.ToString(_maLop.SelectedItem)), new SqlParameter("@ma", _maSinhVien.Text.Trim())));
            NapDuLieu();
        });
    }

    private void ChonDong()
    {
        if (_grid.CurrentRow == null) return;
        _maSinhVien.Text = Convert.ToString(_grid.CurrentRow.Cells["MaSinhVien"].Value);
        _hoTen.Text = Convert.ToString(_grid.CurrentRow.Cells["HoTen"].Value);
        _ngaySinh.Value = Convert.ToDateTime(_grid.CurrentRow.Cells["NgaySinh"].Value);
        _maLop.SelectedItem = Convert.ToString(_grid.CurrentRow.Cells["MaLop"].Value);
    }
}
