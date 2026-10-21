using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Lab06A.WinForms;

namespace Bai01_QuanLyMonHoc;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmQuanLyMonHoc());
    }
}

/// <summary>Bài tại lớp: thêm, xóa, sửa và hiển thị dữ liệu bảng MonHoc.</summary>
public sealed class FrmQuanLyMonHoc : DatabaseFormBase
{
    private readonly TextBox _maMonHoc = Ui.TextBox(170);
    private readonly TextBox _tenMonHoc = Ui.TextBox(300);

    public FrmQuanLyMonHoc()
    {
        FlowLayoutPanel editor = Ui.Row(
            Ui.Label("Mã môn học:"), _maMonHoc,
            Ui.Label("Tên môn học:"), _tenMonHoc,
            Ui.Button("Thêm", (_, _) => Them()),
            Ui.Button("Xóa", (_, _) => Xoa()),
            Ui.Button("Sửa", (_, _) => Sua()),
            Ui.Button("Làm trống", (_, _) => LamTrong()));

        InitializeDatabaseForm("BÀI 1 - QUẢN LÝ MÔN HỌC", "MonHoc", editor);
        Grid.SelectionChanged += (_, _) => NapDongDangChon();
    }

    private bool DuThongTin() => Require(
        (_maMonHoc.Text, "Mã môn học không được để trống."),
        (_tenMonHoc.Text, "Tên môn học không được để trống."));

    private void Them()
    {
        if (!DuThongTin()) return;
        Execute(
            "INSERT dbo.MonHoc(MaMonHoc, TenMonHoc) VALUES(@maMonHoc, @tenMonHoc)",
            new SqlParameter("@maMonHoc", _maMonHoc.Text.Trim()),
            new SqlParameter("@tenMonHoc", _tenMonHoc.Text.Trim()));
    }

    private void Xoa()
    {
        if (!Require((_maMonHoc.Text, "Hãy nhập mã môn học cần xóa."))) return;
        if (_tenMonHoc.TextLength > 0)
        {
            MessageBox.Show("Khi xóa, tên môn học phải để trống.");
            return;
        }

        Execute("DELETE dbo.MonHoc WHERE MaMonHoc=@maMonHoc", new SqlParameter("@maMonHoc", _maMonHoc.Text.Trim()));
    }

    private void Sua()
    {
        if (!DuThongTin()) return;
        Execute(
            "UPDATE dbo.MonHoc SET TenMonHoc=@tenMonHoc WHERE MaMonHoc=@maMonHoc",
            new SqlParameter("@tenMonHoc", _tenMonHoc.Text.Trim()),
            new SqlParameter("@maMonHoc", _maMonHoc.Text.Trim()));
    }

    private void NapDongDangChon()
    {
        if (Grid.CurrentRow == null) return;
        _maMonHoc.Text = Convert.ToString(Grid.CurrentRow.Cells["MaMonHoc"].Value);
        _tenMonHoc.Text = Convert.ToString(Grid.CurrentRow.Cells["TenMonHoc"].Value);
    }

    private void LamTrong()
    {
        _maMonHoc.Clear();
        _tenMonHoc.Clear();
        _maMonHoc.Focus();
    }
}
