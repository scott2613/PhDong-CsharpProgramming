using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Lab05D.WinForms;

namespace ADO_Bai01_QuanLyMonHoc;

internal static class Program
{
    [STAThread] private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmQuanLyMonHoc());
    }
}

public sealed class FrmQuanLyMonHoc : DatabaseFormBase
{
    private readonly TextBox _code = Ui.TextBox(170);
    private readonly TextBox _name = Ui.TextBox(300);

    public FrmQuanLyMonHoc()
    {
        FlowLayoutPanel editor = Ui.Row(Ui.Label("Mã môn học:"), _code, Ui.Label("Tên môn học:"), _name,
            Ui.Button("Thêm", (_, _) => Add()), Ui.Button("Sửa", (_, _) => Edit()), Ui.Button("Xóa", (_, _) => Delete()));
        InitializeDatabaseForm("BÀI 1 - QUẢN LÝ MÔN HỌC", "MonHoc", editor);
        Grid.SelectionChanged += (_, _) => LoadSelected();
    }

    private bool ValidateInput() => Require((_code.Text, "Mã môn học không được trống."), (_name.Text, "Tên môn học không được trống."));
    private void Add()
    {
        if (!ValidateInput()) return;
        Execute("MonHoc", "INSERT dbo.MonHoc(MaMonHoc,TenMonHoc) VALUES(@code,@name)", new SqlParameter("@code", _code.Text.Trim()), new SqlParameter("@name", _name.Text.Trim()));
    }
    private void Edit()
    {
        if (!ValidateInput()) return;
        Execute("MonHoc", "UPDATE dbo.MonHoc SET TenMonHoc=@name WHERE MaMonHoc=@code", new SqlParameter("@name", _name.Text.Trim()), new SqlParameter("@code", _code.Text.Trim()));
    }
    private void Delete()
    {
        if (!Require((_code.Text, "Hãy nhập mã môn học cần xóa."))) return;
        Execute("MonHoc", "DELETE dbo.MonHoc WHERE MaMonHoc=@code", new SqlParameter("@code", _code.Text.Trim()));
    }
    private void LoadSelected()
    {
        if (Grid.CurrentRow == null) return;
        _code.Text = Convert.ToString(Grid.CurrentRow.Cells["MaMonHoc"].Value);
        _name.Text = Convert.ToString(Grid.CurrentRow.Cells["TenMonHoc"].Value);
    }
}
