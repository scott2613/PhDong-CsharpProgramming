using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using Lab06A.WinForms;

namespace BaiVeNha_QuanLyLop;

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

/// <summary>Bài về nhà: CRUD bảng Lop, cho phép sửa một phần mã khoa hoặc tên lớp.</summary>
public sealed class FrmQuanLyLop : DatabaseFormBase
{
    private readonly TextBox _maKhoa = Ui.TextBox(145);
    private readonly TextBox _maLop = Ui.TextBox(145);
    private readonly TextBox _tenLop = Ui.TextBox(240);

    public FrmQuanLyLop()
    {
        FlowLayoutPanel editor = Ui.Row(
            Ui.Label("Mã khoa:"), _maKhoa,
            Ui.Label("Mã lớp:"), _maLop,
            Ui.Label("Tên lớp:"), _tenLop,
            Ui.Button("Thêm", (_, _) => Them()),
            Ui.Button("Xóa", (_, _) => Xoa()),
            Ui.Button("Sửa", (_, _) => Sua()),
            Ui.Button("Làm trống", (_, _) => LamTrong()));

        InitializeDatabaseForm("BÀI VỀ NHÀ - QUẢN LÝ LỚP", "Lop", editor, 1140);
        Grid.SelectionChanged += (_, _) => NapDongDangChon();
    }

    private void Them()
    {
        if (!Require(
                (_maKhoa.Text, "Mã khoa không được để trống."),
                (_maLop.Text, "Mã lớp không được để trống."),
                (_tenLop.Text, "Tên lớp không được để trống."))) return;

        Execute(
            "INSERT dbo.Lop(MaLop, TenLop, MaKhoa) VALUES(@maLop, @tenLop, @maKhoa)",
            new SqlParameter("@maLop", _maLop.Text.Trim()),
            new SqlParameter("@tenLop", _tenLop.Text.Trim()),
            new SqlParameter("@maKhoa", _maKhoa.Text.Trim()));
    }

    private void Xoa()
    {
        if (!Require((_maLop.Text, "Hãy nhập mã lớp cần xóa."))) return;
        if (_maKhoa.TextLength > 0 || _tenLop.TextLength > 0)
        {
            MessageBox.Show("Khi xóa, chỉ nhập mã lớp; mã khoa và tên lớp phải để trống.");
            return;
        }

        Execute("DELETE dbo.Lop WHERE MaLop=@maLop", new SqlParameter("@maLop", _maLop.Text.Trim()));
    }

    private void Sua()
    {
        if (!Require((_maLop.Text, "Hãy nhập mã lớp cần sửa."))) return;

        // Cho phép cập nhật riêng mã khoa, riêng tên lớp hoặc cả hai trường.
        List<string> assignments = new();
        List<SqlParameter> parameters = new();
        if (_maKhoa.TextLength > 0)
        {
            assignments.Add("MaKhoa=@maKhoa");
            parameters.Add(new SqlParameter("@maKhoa", _maKhoa.Text.Trim()));
        }
        if (_tenLop.TextLength > 0)
        {
            assignments.Add("TenLop=@tenLop");
            parameters.Add(new SqlParameter("@tenLop", _tenLop.Text.Trim()));
        }
        if (assignments.Count == 0)
        {
            MessageBox.Show("Hãy nhập ít nhất mã khoa hoặc tên lớp mới.");
            return;
        }

        parameters.Add(new SqlParameter("@maLop", _maLop.Text.Trim()));
        Execute("UPDATE dbo.Lop SET " + string.Join(", ", assignments) + " WHERE MaLop=@maLop", parameters.ToArray());
    }

    private void NapDongDangChon()
    {
        if (Grid.CurrentRow == null) return;
        _maLop.Text = Convert.ToString(Grid.CurrentRow.Cells["MaLop"].Value);
        _tenLop.Text = Convert.ToString(Grid.CurrentRow.Cells["TenLop"].Value);
        _maKhoa.Text = Convert.ToString(Grid.CurrentRow.Cells["MaKhoa"].Value);
    }

    private void LamTrong()
    {
        _maKhoa.Clear();
        _maLop.Clear();
        _tenLop.Clear();
        _maLop.Focus();
    }
}
