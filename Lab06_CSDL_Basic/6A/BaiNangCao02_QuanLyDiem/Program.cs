using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using Lab06A.Core;
using Lab06A.WinForms;

namespace BaiNangCao02_QuanLyDiem;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmQuanLyDiem());
    }
}

/// <summary>Bài nâng cao 2: CRUD bảng Diem có khóa ghép và kiểm tra miền điểm 0-10.</summary>
public sealed class FrmQuanLyDiem : DatabaseFormBase
{
    private readonly TextBox _maSinhVien = Ui.TextBox(155);
    private readonly TextBox _maMonHoc = Ui.TextBox(150);
    private readonly TextBox _diem = Ui.TextBox(100);

    public FrmQuanLyDiem()
    {
        FlowLayoutPanel editor = Ui.Row(
            Ui.Label("Mã sinh viên:"), _maSinhVien,
            Ui.Label("Mã môn học:"), _maMonHoc,
            Ui.Label("Điểm:"), _diem,
            Ui.Button("Thêm", (_, _) => Them()),
            Ui.Button("Xóa", (_, _) => Xoa()),
            Ui.Button("Sửa", (_, _) => Sua()),
            Ui.Button("Làm trống", (_, _) => LamTrong()));

        InitializeDatabaseForm("BÀI NÂNG CAO 2 - QUẢN LÝ ĐIỂM", "Diem", editor);
        Grid.SelectionChanged += (_, _) => NapDongDangChon();
    }

    private bool DocDiem(out decimal score)
    {
        bool valid = InputValidator.TryReadScore(_diem.Text, out score);
        if (!valid)
        {
            MessageBox.Show("Điểm phải là số trong khoảng từ 0 đến 10.", "Dữ liệu chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        return valid;
    }

    private void Them()
    {
        if (!Require(
                (_maSinhVien.Text, "Mã sinh viên không được để trống."),
                (_maMonHoc.Text, "Mã môn học không được để trống."),
                (_diem.Text, "Điểm không được để trống.")) || !DocDiem(out decimal score)) return;

        Execute(
            "INSERT dbo.Diem(MaSinhVien, MaMonHoc, Diem) VALUES(@maSinhVien, @maMonHoc, @diem)",
            new SqlParameter("@maSinhVien", _maSinhVien.Text.Trim()),
            new SqlParameter("@maMonHoc", _maMonHoc.Text.Trim()),
            TaoThamSoDiem(score));
    }

    private void Xoa()
    {
        if (!Require((_maSinhVien.Text, "Hãy nhập mã sinh viên."), (_maMonHoc.Text, "Hãy nhập mã môn học."))) return;
        if (_diem.TextLength > 0)
        {
            MessageBox.Show("Khi xóa, ô điểm phải để trống.");
            return;
        }

        Execute(
            "DELETE dbo.Diem WHERE MaSinhVien=@maSinhVien AND MaMonHoc=@maMonHoc",
            new SqlParameter("@maSinhVien", _maSinhVien.Text.Trim()),
            new SqlParameter("@maMonHoc", _maMonHoc.Text.Trim()));
    }

    private void Sua()
    {
        if (!Require(
                (_maSinhVien.Text, "Mã sinh viên không được để trống."),
                (_maMonHoc.Text, "Mã môn học không được để trống."),
                (_diem.Text, "Điểm không được để trống.")) || !DocDiem(out decimal score)) return;

        Execute(
            "UPDATE dbo.Diem SET Diem=@diem WHERE MaSinhVien=@maSinhVien AND MaMonHoc=@maMonHoc",
            TaoThamSoDiem(score),
            new SqlParameter("@maSinhVien", _maSinhVien.Text.Trim()),
            new SqlParameter("@maMonHoc", _maMonHoc.Text.Trim()));
    }

    private static SqlParameter TaoThamSoDiem(decimal score) => new("@diem", SqlDbType.Decimal)
    {
        Precision = 4,
        Scale = 2,
        Value = score
    };

    private void NapDongDangChon()
    {
        if (Grid.CurrentRow == null) return;
        _maSinhVien.Text = Convert.ToString(Grid.CurrentRow.Cells["MaSinhVien"].Value);
        _maMonHoc.Text = Convert.ToString(Grid.CurrentRow.Cells["MaMonHoc"].Value);
        _diem.Text = Convert.ToString(Grid.CurrentRow.Cells["Diem"].Value, CultureInfo.InvariantCulture);
    }

    private void LamTrong()
    {
        _maSinhVien.Clear();
        _maMonHoc.Clear();
        _diem.Clear();
        _maSinhVien.Focus();
    }
}
