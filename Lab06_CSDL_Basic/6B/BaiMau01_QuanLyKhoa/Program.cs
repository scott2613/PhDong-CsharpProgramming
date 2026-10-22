using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Lab06B.Core;
using Lab06B.WinForms;

namespace BaiMau01_QuanLyKhoa;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmQuanLyKhoa());
    }
}

public sealed class FrmQuanLyKhoa : DatabaseFormBase
{
    private readonly TextBox _maKhoa = Ui.TextBox(170);
    private readonly TextBox _tenKhoa = Ui.TextBox(260);
    private readonly DataGridView _grid = Ui.Grid();

    public FrmQuanLyKhoa()
    {
        Ui.Configure(this, "BÀI MẪU 1 - QUẢN LÝ KHOA");
        FlowLayoutPanel editor = Ui.Row(
            Ui.Label("Mã khoa"), _maKhoa, Ui.Label("Tên khoa"), _tenKhoa,
            Ui.Button("Thêm", (_, _) => Them()), Ui.Button("Xóa", (_, _) => Xoa()),
            Ui.Button("Sửa", (_, _) => Sua()), Ui.Button("Làm trống", (_, _) => LamTrong()));
        Controls.Add(_grid);
        Controls.Add(Ui.Hint());
        Controls.Add(editor);
        Controls.Add(Ui.Title("BÀI MẪU 1 - QUẢN LÝ KHOA"));
        _grid.CellClick += (_, _) => ChonDong();
        Shown += (_, _) => SafeRun(() => { DatabaseInitializer.EnsureUniversity(); NapDuLieu(); });
    }

    private void NapDuLieu() => _grid.DataSource = DataAccess.QueryUniversity("SELECT MaKhoa,TenKhoa FROM dbo.Khoa ORDER BY MaKhoa");

    private void Them()
    {
        if (!Require((_maKhoa.Text, "Mã khoa không được để trống."), (_tenKhoa.Text, "Tên khoa không được để trống."))) return;
        SafeRun(() =>
        {
            // ExecuteScalar kiểm tra khóa chính trước khi thêm, đúng yêu cầu của đề.
            if (DataAccess.ExistsUniversity("SELECT 1 FROM dbo.Khoa WHERE MaKhoa=@ma", new SqlParameter("@ma", _maKhoa.Text.Trim())))
            {
                MessageBox.Show("Mã khoa đã tồn tại.");
                return;
            }
            ShowResult(DataAccess.ExecuteUniversity("INSERT dbo.Khoa(MaKhoa,TenKhoa) VALUES(@ma,@ten)",
                new SqlParameter("@ma", _maKhoa.Text.Trim()), new SqlParameter("@ten", _tenKhoa.Text.Trim())));
            NapDuLieu();
        });
    }

    private void Xoa() => SafeRun(() =>
    {
        if (!Require((_maKhoa.Text, "Hãy nhập mã khoa cần xóa."))) return;
        if (DataAccess.ExistsUniversity("SELECT 1 FROM dbo.Lop WHERE MaKhoa=@ma", new SqlParameter("@ma", _maKhoa.Text.Trim())))
        {
            MessageBox.Show("Không thể xóa vì khoa đang được sử dụng trong bảng Lớp.");
            return;
        }
        ShowResult(DataAccess.ExecuteUniversity("DELETE dbo.Khoa WHERE MaKhoa=@ma", new SqlParameter("@ma", _maKhoa.Text.Trim())));
        NapDuLieu();
    });

    private void Sua() => SafeRun(() =>
    {
        if (!Require((_maKhoa.Text, "Hãy nhập mã khoa cần sửa."), (_tenKhoa.Text, "Tên khoa mới không được để trống."))) return;
        ShowResult(DataAccess.ExecuteUniversity("UPDATE dbo.Khoa SET TenKhoa=@ten WHERE MaKhoa=@ma",
            new SqlParameter("@ten", _tenKhoa.Text.Trim()), new SqlParameter("@ma", _maKhoa.Text.Trim())));
        NapDuLieu();
    });

    private void ChonDong()
    {
        if (_grid.CurrentRow == null) return;
        _maKhoa.Text = Convert.ToString(_grid.CurrentRow.Cells["MaKhoa"].Value);
        _tenKhoa.Text = Convert.ToString(_grid.CurrentRow.Cells["TenKhoa"].Value);
    }

    private void LamTrong() { _maKhoa.Clear(); _tenKhoa.Clear(); _maKhoa.Focus(); }
}
