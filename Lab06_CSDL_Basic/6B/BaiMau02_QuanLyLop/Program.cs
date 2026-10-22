using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Lab06B.Core;
using Lab06B.WinForms;

namespace BaiMau02_QuanLyLop;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmQuanLyLop());
    }
}

public sealed class FrmQuanLyLop : DatabaseFormBase
{
    private readonly ComboBox _maKhoa = Ui.ComboBox(150);
    private readonly TextBox _maLop = Ui.TextBox(170);
    private readonly TextBox _tenLop = Ui.TextBox(250);
    private readonly DataGridView _grid = Ui.Grid();

    public FrmQuanLyLop()
    {
        Ui.Configure(this, "BÀI MẪU 2 - QUẢN LÝ LỚP");
        FlowLayoutPanel editor = Ui.Row(
            Ui.Label("Khoa"), _maKhoa, Ui.Label("Mã lớp"), _maLop, Ui.Label("Tên lớp"), _tenLop,
            Ui.Button("Thêm", (_, _) => Them()), Ui.Button("Xóa", (_, _) => Xoa()),
            Ui.Button("Sửa", (_, _) => Sua()), Ui.Button("Làm trống", (_, _) => LamTrong()));
        Controls.Add(_grid);
        Controls.Add(Ui.Hint());
        Controls.Add(editor);
        Controls.Add(Ui.Title("BÀI MẪU 2 - QUẢN LÝ LỚP"));
        _grid.CellClick += (_, _) => ChonDong();
        Shown += (_, _) => SafeRun(() => { DatabaseInitializer.EnsureUniversity(); NapKhoa(); NapDuLieu(); });
    }

    private void NapKhoa()
    {
        _maKhoa.Items.Clear();
        using SqlConnection connection = DataAccess.OpenUniversity();
        using SqlCommand command = new("SELECT MaKhoa FROM dbo.Khoa ORDER BY MaKhoa", connection);
        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read()) _maKhoa.Items.Add(reader.GetString(0));
        if (_maKhoa.Items.Count > 0) _maKhoa.SelectedIndex = 0;
    }

    private void NapDuLieu() => _grid.DataSource = DataAccess.QueryUniversity("SELECT MaLop,TenLop,MaKhoa FROM dbo.Lop ORDER BY MaLop");

    private bool DuThongTin() => Require((_maLop.Text, "Mã lớp không được để trống."), (_tenLop.Text, "Tên lớp không được để trống."), (Convert.ToString(_maKhoa.SelectedItem) ?? "", "Hãy chọn khoa."));

    private void Them()
    {
        if (!DuThongTin()) return;
        SafeRun(() =>
        {
            if (DataAccess.ExistsUniversity("SELECT 1 FROM dbo.Lop WHERE MaLop=@ma", new SqlParameter("@ma", _maLop.Text.Trim())))
            {
                MessageBox.Show("Mã lớp đã tồn tại.");
                return;
            }
            ShowResult(DataAccess.ExecuteUniversity("INSERT dbo.Lop(MaLop,TenLop,MaKhoa) VALUES(@ma,@ten,@khoa)",
                new SqlParameter("@ma", _maLop.Text.Trim()), new SqlParameter("@ten", _tenLop.Text.Trim()),
                new SqlParameter("@khoa", Convert.ToString(_maKhoa.SelectedItem))));
            NapDuLieu();
        });
    }

    private void Xoa() => SafeRun(() =>
    {
        if (!Require((_maLop.Text, "Hãy nhập mã lớp cần xóa."))) return;
        if (DataAccess.ExistsUniversity("SELECT 1 FROM dbo.SinhVien WHERE MaLop=@ma", new SqlParameter("@ma", _maLop.Text.Trim())))
        {
            MessageBox.Show("Không thể xóa vì lớp đang có sinh viên.");
            return;
        }
        ShowResult(DataAccess.ExecuteUniversity("DELETE dbo.Lop WHERE MaLop=@ma", new SqlParameter("@ma", _maLop.Text.Trim())));
        NapDuLieu();
    });

    private void Sua()
    {
        if (!DuThongTin()) return;
        SafeRun(() =>
        {
            ShowResult(DataAccess.ExecuteUniversity("UPDATE dbo.Lop SET TenLop=@ten,MaKhoa=@khoa WHERE MaLop=@ma",
                new SqlParameter("@ten", _tenLop.Text.Trim()), new SqlParameter("@khoa", Convert.ToString(_maKhoa.SelectedItem)),
                new SqlParameter("@ma", _maLop.Text.Trim())));
            NapDuLieu();
        });
    }

    private void ChonDong()
    {
        if (_grid.CurrentRow == null) return;
        _maLop.Text = Convert.ToString(_grid.CurrentRow.Cells["MaLop"].Value);
        _tenLop.Text = Convert.ToString(_grid.CurrentRow.Cells["TenLop"].Value);
        _maKhoa.SelectedItem = Convert.ToString(_grid.CurrentRow.Cells["MaKhoa"].Value);
    }

    private void LamTrong() { _maLop.Clear(); _tenLop.Clear(); _maLop.Focus(); }
}
