using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using Lab05D.WinForms;

namespace ADO_BaiVeNha_QuanLyLop;

internal static class Program
{
    [STAThread] private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmQuanLyLop());
    }
}

public sealed class FrmQuanLyLop : DatabaseFormBase
{
    private readonly TextBox _department = Ui.TextBox(140);
    private readonly TextBox _classCode = Ui.TextBox(140);
    private readonly TextBox _className = Ui.TextBox(230);

    public FrmQuanLyLop()
    {
        FlowLayoutPanel editor = Ui.Row(Ui.Label("Mã khoa:"), _department, Ui.Label("Mã lớp:"), _classCode, Ui.Label("Tên lớp:"), _className,
            Ui.Button("Thêm", (_, _) => Add()), Ui.Button("Sửa", (_, _) => Edit()), Ui.Button("Xóa", (_, _) => Delete()));
        InitializeDatabaseForm("BÀI VỀ NHÀ - QUẢN LÝ LỚP", "Lop", editor, 1120);
        Grid.SelectionChanged += (_, _) => LoadSelected();
    }

    private void Add()
    {
        if (!Require((_department.Text, "Hãy nhập mã khoa."), (_classCode.Text, "Hãy nhập mã lớp."), (_className.Text, "Hãy nhập tên lớp."))) return;
        Execute("Lop", "INSERT dbo.Lop(MaLop,TenLop,MaKhoa) VALUES(@class,@name,@department)",
            new SqlParameter("@class", _classCode.Text.Trim()), new SqlParameter("@name", _className.Text.Trim()), new SqlParameter("@department", _department.Text.Trim()));
    }

    private void Delete()
    {
        // Đề yêu cầu khi xóa chỉ được nhập mã lớp.
        if (!Require((_classCode.Text, "Hãy nhập mã lớp cần xóa."))) return;
        if (_department.TextLength > 0 || _className.TextLength > 0) { MessageBox.Show("Khi xóa, chỉ nhập mã lớp; mã khoa và tên lớp phải để trống."); return; }
        Execute("Lop", "DELETE dbo.Lop WHERE MaLop=@class", new SqlParameter("@class", _classCode.Text.Trim()));
    }

    private void Edit()
    {
        if (!Require((_classCode.Text, "Hãy nhập mã lớp cần sửa."))) return;
        List<string> sets = new(); List<SqlParameter> parameters = new();
        if (_department.TextLength > 0) { sets.Add("MaKhoa=@department"); parameters.Add(new SqlParameter("@department", _department.Text.Trim())); }
        if (_className.TextLength > 0) { sets.Add("TenLop=@name"); parameters.Add(new SqlParameter("@name", _className.Text.Trim())); }
        if (sets.Count == 0) { MessageBox.Show("Hãy nhập ít nhất mã khoa hoặc tên lớp mới."); return; }
        parameters.Add(new SqlParameter("@class", _classCode.Text.Trim()));
        Execute("Lop", "UPDATE dbo.Lop SET " + string.Join(",", sets) + " WHERE MaLop=@class", parameters.ToArray());
    }

    private void LoadSelected()
    {
        if (Grid.CurrentRow == null) return;
        _classCode.Text = Convert.ToString(Grid.CurrentRow.Cells["MaLop"].Value);
        _className.Text = Convert.ToString(Grid.CurrentRow.Cells["TenLop"].Value);
        _department.Text = Convert.ToString(Grid.CurrentRow.Cells["MaKhoa"].Value);
    }
}
