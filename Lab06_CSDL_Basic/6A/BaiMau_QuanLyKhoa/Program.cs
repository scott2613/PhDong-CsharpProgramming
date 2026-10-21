using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Lab06A.WinForms;

namespace BaiMau_QuanLyKhoa;

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

/// <summary>Bài mẫu CRUD bảng Khoa bằng SqlConnection và SqlCommand.</summary>
public sealed class FrmQuanLyKhoa : DatabaseFormBase
{
    private readonly TextBox _maKhoa = Ui.TextBox(160);
    private readonly TextBox _tenKhoa = Ui.TextBox(260);

    public FrmQuanLyKhoa()
    {
        FlowLayoutPanel editor = Ui.Row(
            Ui.Label("Mã khoa:"), _maKhoa,
            Ui.Label("Tên khoa:"), _tenKhoa,
            Ui.Button("Thêm", (_, _) => Them()),
            Ui.Button("Xóa", (_, _) => Xoa()),
            Ui.Button("Sửa", (_, _) => Sua()),
            Ui.Button("Làm trống", (_, _) => LamTrong()));

        InitializeDatabaseForm("BÀI MẪU - QUẢN LÝ KHOA", "Khoa", editor);
        Grid.SelectionChanged += (_, _) => NapDongDangChon();
    }

    private void Them()
    {
        if (!Require((_maKhoa.Text, "Mã khoa không được để trống."), (_tenKhoa.Text, "Tên khoa không được để trống."))) return;

        // Dùng tham số thay vì ghép chuỗi để tránh lỗi dấu nháy và SQL Injection.
        Execute(
            "INSERT dbo.Khoa(MaKhoa, TenKhoa) VALUES(@maKhoa, @tenKhoa)",
            new SqlParameter("@maKhoa", _maKhoa.Text.Trim()),
            new SqlParameter("@tenKhoa", _tenKhoa.Text.Trim()));
    }

    private void Xoa()
    {
        if (!Require((_maKhoa.Text, "Hãy nhập mã khoa cần xóa."))) return;
        if (_tenKhoa.TextLength > 0)
        {
            MessageBox.Show("Khi xóa, chỉ nhập mã khoa; tên khoa phải để trống.");
            return;
        }

        Execute("DELETE dbo.Khoa WHERE MaKhoa=@maKhoa", new SqlParameter("@maKhoa", _maKhoa.Text.Trim()));
    }

    private void Sua()
    {
        if (!Require((_maKhoa.Text, "Mã khoa không được để trống."), (_tenKhoa.Text, "Tên khoa không được để trống."))) return;
        Execute(
            "UPDATE dbo.Khoa SET TenKhoa=@tenKhoa WHERE MaKhoa=@maKhoa",
            new SqlParameter("@tenKhoa", _tenKhoa.Text.Trim()),
            new SqlParameter("@maKhoa", _maKhoa.Text.Trim()));
    }

    private void NapDongDangChon()
    {
        if (Grid.CurrentRow == null) return;
        _maKhoa.Text = Convert.ToString(Grid.CurrentRow.Cells["MaKhoa"].Value);
        _tenKhoa.Text = Convert.ToString(Grid.CurrentRow.Cells["TenKhoa"].Value);
    }

    private void LamTrong()
    {
        _maKhoa.Clear();
        _tenKhoa.Clear();
        _maKhoa.Focus();
    }
}
