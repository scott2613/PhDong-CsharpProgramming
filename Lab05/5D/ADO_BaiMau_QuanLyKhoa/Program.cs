using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Lab05D.WinForms;

namespace ADO_BaiMau_QuanLyKhoa;

internal static class Program
{
    [STAThread] private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmQuanLyKhoa());
    }
}

public sealed class FrmQuanLyKhoa : DatabaseFormBase
{
    private readonly TextBox _code = Ui.TextBox(160);
    private readonly TextBox _name = Ui.TextBox(280);

    public FrmQuanLyKhoa()
    {
        FlowLayoutPanel editor = Ui.Row(Ui.Label("Mã khoa:"), _code, Ui.Label("Tên khoa:"), _name,
            Ui.Button("Thêm", (_, _) => Add()), Ui.Button("Sửa", (_, _) => Edit()), Ui.Button("Xóa", (_, _) => Delete()), Ui.Button("Làm mới", (_, _) => RefreshTable("Khoa")));
        InitializeDatabaseForm("ADO.NET - QUẢN LÝ KHOA", "Khoa", editor);
        Grid.SelectionChanged += (_, _) => LoadSelected();
    }

    private void Add()
    {
        if (!Require((_code.Text, "Hãy nhập mã khoa."), (_name.Text, "Hãy nhập tên khoa."))) return;
        Execute("Khoa", "INSERT dbo.Khoa(MaKhoa,TenKhoa) VALUES(@code,@name)", new SqlParameter("@code", _code.Text.Trim()), new SqlParameter("@name", _name.Text.Trim()));
    }

    private void Edit()
    {
        if (!Require((_code.Text, "Hãy nhập mã khoa."), (_name.Text, "Hãy nhập tên khoa."))) return;
        Execute("Khoa", "UPDATE dbo.Khoa SET TenKhoa=@name WHERE MaKhoa=@code", new SqlParameter("@name", _name.Text.Trim()), new SqlParameter("@code", _code.Text.Trim()));
    }

    private void Delete()
    {
        if (!Require((_code.Text, "Hãy nhập mã khoa cần xóa."))) return;
        Execute("Khoa", "DELETE dbo.Khoa WHERE MaKhoa=@code", new SqlParameter("@code", _code.Text.Trim()));
    }

    private void LoadSelected()
    {
        if (Grid.CurrentRow == null) return;
        _code.Text = Convert.ToString(Grid.CurrentRow.Cells["MaKhoa"].Value);
        _name.Text = Convert.ToString(Grid.CurrentRow.Cells["TenKhoa"].Value);
    }
}
