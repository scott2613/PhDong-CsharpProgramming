using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Lab06B.Core;
using Lab06B.WinForms;

namespace Bai02_QuanLyDiem;

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

public sealed class FrmQuanLyDiem : DatabaseFormBase
{
    private readonly ComboBox _maSinhVien = Ui.ComboBox(175);
    private readonly ComboBox _maMonHoc = Ui.ComboBox(155);
    private readonly TextBox _diem = Ui.TextBox(110);
    private readonly DataGridView _grid = Ui.Grid();

    public FrmQuanLyDiem()
    {
        Ui.Configure(this, "BÀI 2 - QUẢN LÝ ĐIỂM");
        FlowLayoutPanel editor = Ui.Row(
            Ui.Label("Sinh viên"), _maSinhVien, Ui.Label("Môn học"), _maMonHoc, Ui.Label("Điểm"), _diem,
            Ui.Button("Thêm", (_, _) => Them()), Ui.Button("Xóa", (_, _) => Xoa()), Ui.Button("Sửa", (_, _) => Sua()));
        Controls.Add(_grid);
        Controls.Add(Ui.Hint());
        Controls.Add(editor);
        Controls.Add(Ui.Title("BÀI 2 - QUẢN LÝ ĐIỂM"));
        _grid.CellClick += (_, _) => ChonDong();
        Shown += (_, _) => SafeRun(() => { DatabaseInitializer.EnsureUniversity(); NapKhoaNgoai(); NapDuLieu(); });
    }

    private void NapKhoaNgoai()
    {
        _maSinhVien.Items.Clear();
        _maMonHoc.Items.Clear();
        using SqlConnection connection = DataAccess.OpenUniversity();
        using (SqlCommand command = new("SELECT MaSinhVien FROM dbo.SinhVien ORDER BY MaSinhVien", connection))
        using (SqlDataReader reader = command.ExecuteReader())
            while (reader.Read()) _maSinhVien.Items.Add(reader.GetString(0));
        using (SqlCommand command = new("SELECT MaMonHoc FROM dbo.MonHoc ORDER BY MaMonHoc", connection))
        using (SqlDataReader reader = command.ExecuteReader())
            while (reader.Read()) _maMonHoc.Items.Add(reader.GetString(0));
        if (_maSinhVien.Items.Count > 0) _maSinhVien.SelectedIndex = 0;
        if (_maMonHoc.Items.Count > 0) _maMonHoc.SelectedIndex = 0;
    }

    private void NapDuLieu() => _grid.DataSource = DataAccess.QueryUniversity("SELECT MaSinhVien,MaMonHoc,Diem FROM dbo.Diem ORDER BY MaSinhVien,MaMonHoc");

    private bool DocDiem(out decimal score)
    {
        if (!Require((Convert.ToString(_maSinhVien.SelectedItem) ?? "", "Hãy chọn sinh viên."), (Convert.ToString(_maMonHoc.SelectedItem) ?? "", "Hãy chọn môn học."), (_diem.Text, "Điểm không được để trống.")))
        {
            score = 0;
            return false;
        }
        if (!InputValidator.TryReadScore(_diem.Text, out score))
        {
            MessageBox.Show("Điểm phải lớn hơn 0 và không vượt quá 10.");
            return false;
        }
        return true;
    }

    private SqlParameter[] KhoaGhep() => new[]
    {
        new SqlParameter("@sv", Convert.ToString(_maSinhVien.SelectedItem)),
        new SqlParameter("@mh", Convert.ToString(_maMonHoc.SelectedItem))
    };

    private void Them()
    {
        if (!DocDiem(out decimal score)) return;
        SafeRun(() =>
        {
            if (DataAccess.ExistsUniversity("SELECT 1 FROM dbo.Diem WHERE MaSinhVien=@sv AND MaMonHoc=@mh", KhoaGhep()))
            {
                MessageBox.Show("Sinh viên đã có điểm ở môn học này.");
                return;
            }
            ShowResult(DataAccess.ExecuteUniversity("INSERT dbo.Diem(MaSinhVien,MaMonHoc,Diem) VALUES(@sv,@mh,@diem)",
                new SqlParameter("@sv", Convert.ToString(_maSinhVien.SelectedItem)), new SqlParameter("@mh", Convert.ToString(_maMonHoc.SelectedItem)),
                new SqlParameter("@diem", SqlDbType.Decimal) { Precision = 4, Scale = 2, Value = score }));
            NapDuLieu();
        });
    }

    private void Xoa() => SafeRun(() =>
    {
        ShowResult(DataAccess.ExecuteUniversity("DELETE dbo.Diem WHERE MaSinhVien=@sv AND MaMonHoc=@mh", KhoaGhep()));
        NapDuLieu();
    });

    private void Sua()
    {
        if (!DocDiem(out decimal score)) return;
        SafeRun(() =>
        {
            ShowResult(DataAccess.ExecuteUniversity("UPDATE dbo.Diem SET Diem=@diem WHERE MaSinhVien=@sv AND MaMonHoc=@mh",
                new SqlParameter("@diem", SqlDbType.Decimal) { Precision = 4, Scale = 2, Value = score },
                new SqlParameter("@sv", Convert.ToString(_maSinhVien.SelectedItem)), new SqlParameter("@mh", Convert.ToString(_maMonHoc.SelectedItem))));
            NapDuLieu();
        });
    }

    private void ChonDong()
    {
        if (_grid.CurrentRow == null) return;
        _maSinhVien.SelectedItem = Convert.ToString(_grid.CurrentRow.Cells["MaSinhVien"].Value);
        _maMonHoc.SelectedItem = Convert.ToString(_grid.CurrentRow.Cells["MaMonHoc"].Value);
        _diem.Text = Convert.ToString(_grid.CurrentRow.Cells["Diem"].Value);
    }
}
