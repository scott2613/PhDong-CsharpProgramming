using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Lab06A.WinForms;

namespace BaiNangCao01_QuanLySinhVien;

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

/// <summary>Bài nâng cao 1: CRUD sinh viên với quy tắc cập nhật từng trường tùy chọn.</summary>
public sealed class FrmQuanLySinhVien : DatabaseFormBase
{
    private readonly TextBox _maLop = Ui.TextBox(135);
    private readonly TextBox _maSinhVien = Ui.TextBox(145);
    private readonly TextBox _hoTen = Ui.TextBox(225);
    private readonly DateTimePicker _ngaySinh = new()
    {
        Width = 150,
        Format = DateTimePickerFormat.Short,
        ShowCheckBox = true,
        Checked = false,
        Margin = new Padding(7)
    };

    public FrmQuanLySinhVien()
    {
        FlowLayoutPanel editor = Ui.Row(
            Ui.Label("Mã lớp:"), _maLop,
            Ui.Label("Mã sinh viên:"), _maSinhVien,
            Ui.Label("Họ tên:"), _hoTen,
            Ui.Label("Ngày sinh:"), _ngaySinh,
            Ui.Button("Thêm", (_, _) => Them()),
            Ui.Button("Xóa", (_, _) => Xoa()),
            Ui.Button("Sửa", (_, _) => Sua()),
            Ui.Button("Làm trống", (_, _) => LamTrong()));

        InitializeDatabaseForm("BÀI NÂNG CAO 1 - QUẢN LÝ SINH VIÊN", "SinhVien", editor, 1220);
        Grid.SelectionChanged += (_, _) => NapDongDangChon();
    }

    private void Them()
    {
        if (!Require(
                (_maLop.Text, "Mã lớp không được để trống."),
                (_maSinhVien.Text, "Mã sinh viên không được để trống."),
                (_hoTen.Text, "Họ tên không được để trống."))) return;

        if (!_ngaySinh.Checked)
        {
            MessageBox.Show("Hãy chọn ngày sinh.");
            return;
        }

        Execute(
            "INSERT dbo.SinhVien(MaSinhVien, HoTen, NgaySinh, MaLop) VALUES(@maSinhVien, @hoTen, @ngaySinh, @maLop)",
            new SqlParameter("@maSinhVien", _maSinhVien.Text.Trim()),
            new SqlParameter("@hoTen", _hoTen.Text.Trim()),
            NgaySinhParameter(),
            new SqlParameter("@maLop", _maLop.Text.Trim()));
    }

    private void Xoa()
    {
        // Đề yêu cầu xóa theo đồng thời mã sinh viên và họ tên.
        if (!Require((_maSinhVien.Text, "Hãy nhập mã sinh viên."), (_hoTen.Text, "Hãy nhập họ tên."))) return;
        if (_maLop.TextLength > 0 || _ngaySinh.Checked)
        {
            MessageBox.Show("Khi xóa, mã lớp và ngày sinh phải để trống.");
            return;
        }

        Execute(
            "DELETE dbo.SinhVien WHERE MaSinhVien=@maSinhVien AND HoTen=@hoTen",
            new SqlParameter("@maSinhVien", _maSinhVien.Text.Trim()),
            new SqlParameter("@hoTen", _hoTen.Text.Trim()));
    }

    private void Sua()
    {
        if (!Require((_maSinhVien.Text, "Hãy nhập mã sinh viên cần sửa."))) return;

        // Chỉ đưa các trường người dùng đã nhập vào mệnh đề SET.
        List<string> assignments = new();
        List<SqlParameter> parameters = new();
        if (_maLop.TextLength > 0)
        {
            assignments.Add("MaLop=@maLop");
            parameters.Add(new SqlParameter("@maLop", _maLop.Text.Trim()));
        }
        if (_hoTen.TextLength > 0)
        {
            assignments.Add("HoTen=@hoTen");
            parameters.Add(new SqlParameter("@hoTen", _hoTen.Text.Trim()));
        }
        if (_ngaySinh.Checked)
        {
            assignments.Add("NgaySinh=@ngaySinh");
            parameters.Add(NgaySinhParameter());
        }
        if (assignments.Count == 0)
        {
            MessageBox.Show("Hãy nhập ít nhất một thông tin cần cập nhật.");
            return;
        }

        parameters.Add(new SqlParameter("@maSinhVien", _maSinhVien.Text.Trim()));
        Execute("UPDATE dbo.SinhVien SET " + string.Join(", ", assignments) + " WHERE MaSinhVien=@maSinhVien", parameters.ToArray());
    }

    private SqlParameter NgaySinhParameter() => new("@ngaySinh", SqlDbType.Date) { Value = _ngaySinh.Value.Date };

    private void NapDongDangChon()
    {
        if (Grid.CurrentRow == null) return;
        _maSinhVien.Text = Convert.ToString(Grid.CurrentRow.Cells["MaSinhVien"].Value);
        _hoTen.Text = Convert.ToString(Grid.CurrentRow.Cells["HoTen"].Value);
        _ngaySinh.Value = Convert.ToDateTime(Grid.CurrentRow.Cells["NgaySinh"].Value);
        _ngaySinh.Checked = true;
        _maLop.Text = Convert.ToString(Grid.CurrentRow.Cells["MaLop"].Value);
    }

    private void LamTrong()
    {
        _maLop.Clear();
        _maSinhVien.Clear();
        _hoTen.Clear();
        _ngaySinh.Checked = false;
        _maSinhVien.Focus();
    }
}
