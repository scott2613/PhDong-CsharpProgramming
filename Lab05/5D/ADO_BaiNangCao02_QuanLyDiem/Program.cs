using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using Lab05D.WinForms;

namespace ADO_BaiNangCao02_QuanLyDiem;

internal static class Program
{
    [STAThread] private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmQuanLyDiem());
    }
}

public sealed class FrmQuanLyDiem : DatabaseFormBase
{
    private readonly TextBox _student = Ui.TextBox(150);
    private readonly TextBox _course = Ui.TextBox(150);
    private readonly TextBox _score = Ui.TextBox(100);

    public FrmQuanLyDiem()
    {
        FlowLayoutPanel editor = Ui.Row(Ui.Label("MSSV:"), _student, Ui.Label("Mã môn:"), _course, Ui.Label("Điểm:"), _score,
            Ui.Button("Thêm", (_, _) => Add()), Ui.Button("Sửa", (_, _) => Edit()), Ui.Button("Xóa", (_, _) => Delete()));
        InitializeDatabaseForm("BÀI NÂNG CAO 2 - QUẢN LÝ ĐIỂM", "Diem", editor);
        Grid.SelectionChanged += (_, _) => LoadSelected();
    }

    private bool TryReadScore(out decimal score)
    {
        bool valid = decimal.TryParse(_score.Text.Trim().Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out score) && score >= 0 && score <= 10;
        if (!valid) MessageBox.Show("Điểm phải là số trong khoảng từ 0 đến 10.", "Dữ liệu chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return valid;
    }

    private SqlParameter ScoreParameter(decimal score) => new("@score", SqlDbType.Decimal) { Precision = 4, Scale = 2, Value = score };
    private void Add()
    {
        if (!Require((_student.Text, "Hãy nhập MSSV."), (_course.Text, "Hãy nhập mã môn."), (_score.Text, "Hãy nhập điểm.")) || !TryReadScore(out decimal score)) return;
        Execute("Diem", "INSERT dbo.Diem(MaSinhVien,MaMonHoc,Diem) VALUES(@student,@course,@score)", new SqlParameter("@student", _student.Text.Trim()), new SqlParameter("@course", _course.Text.Trim()), ScoreParameter(score));
    }
    private void Delete()
    {
        if (!Require((_student.Text, "Hãy nhập MSSV."), (_course.Text, "Hãy nhập mã môn."))) return;
        if (_score.TextLength > 0) { MessageBox.Show("Khi xóa, ô điểm phải để trống."); return; }
        Execute("Diem", "DELETE dbo.Diem WHERE MaSinhVien=@student AND MaMonHoc=@course", new SqlParameter("@student", _student.Text.Trim()), new SqlParameter("@course", _course.Text.Trim()));
    }
    private void Edit()
    {
        if (!Require((_student.Text, "Hãy nhập MSSV."), (_course.Text, "Hãy nhập mã môn."), (_score.Text, "Hãy nhập điểm.")) || !TryReadScore(out decimal score)) return;
        Execute("Diem", "UPDATE dbo.Diem SET Diem=@score WHERE MaSinhVien=@student AND MaMonHoc=@course", ScoreParameter(score), new SqlParameter("@student", _student.Text.Trim()), new SqlParameter("@course", _course.Text.Trim()));
    }
    private void LoadSelected()
    {
        if (Grid.CurrentRow == null) return;
        _student.Text = Convert.ToString(Grid.CurrentRow.Cells["MaSinhVien"].Value);
        _course.Text = Convert.ToString(Grid.CurrentRow.Cells["MaMonHoc"].Value);
        _score.Text = Convert.ToString(Grid.CurrentRow.Cells["Diem"].Value, CultureInfo.InvariantCulture);
    }
}
