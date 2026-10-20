using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Lab05D.WinForms;

namespace ADO_BaiNangCao01_QuanLySinhVien;

internal static class Program
{
    [STAThread] private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmQuanLySinhVien());
    }
}

public sealed class FrmQuanLySinhVien : DatabaseFormBase
{
    private readonly TextBox _id = Ui.TextBox(130);
    private readonly TextBox _name = Ui.TextBox(210);
    private readonly TextBox _classCode = Ui.TextBox(130);
    private readonly DateTimePicker _birth = new() { Width = 150, Format = DateTimePickerFormat.Short, ShowCheckBox = true, Margin = new Padding(6) };

    public FrmQuanLySinhVien()
    {
        FlowLayoutPanel editor = Ui.Row(Ui.Label("MSSV:"), _id, Ui.Label("Họ tên:"), _name, Ui.Label("Ngày sinh:"), _birth, Ui.Label("Mã lớp:"), _classCode,
            Ui.Button("Thêm", (_, _) => Add()), Ui.Button("Sửa", (_, _) => Edit()), Ui.Button("Xóa", (_, _) => Delete()));
        InitializeDatabaseForm("BÀI NÂNG CAO 1 - QUẢN LÝ SINH VIÊN", "SinhVien", editor, 1190);
        Grid.SelectionChanged += (_, _) => LoadSelected();
    }

    private void Add()
    {
        if (!Require((_id.Text, "Hãy nhập mã sinh viên."), (_name.Text, "Hãy nhập họ tên."), (_classCode.Text, "Hãy nhập mã lớp.")) || !_birth.Checked) { if (!_birth.Checked) MessageBox.Show("Hãy chọn ngày sinh."); return; }
        Execute("SinhVien", "INSERT dbo.SinhVien(MaSinhVien,HoTen,NgaySinh,MaLop) VALUES(@id,@name,@birth,@class)",
            new SqlParameter("@id", _id.Text.Trim()), new SqlParameter("@name", _name.Text.Trim()), DateParameter(), new SqlParameter("@class", _classCode.Text.Trim()));
    }

    private void Delete()
    {
        // Theo đề, thao tác xóa dùng MSSV và họ tên; lớp và ngày sinh phải bỏ trống.
        if (!Require((_id.Text, "Hãy nhập MSSV."), (_name.Text, "Hãy nhập họ tên."))) return;
        if (_classCode.TextLength > 0 || _birth.Checked) { MessageBox.Show("Khi xóa, mã lớp và ngày sinh phải để trống."); return; }
        Execute("SinhVien", "DELETE dbo.SinhVien WHERE MaSinhVien=@id AND HoTen=@name", new SqlParameter("@id", _id.Text.Trim()), new SqlParameter("@name", _name.Text.Trim()));
    }

    private void Edit()
    {
        if (!Require((_id.Text, "Hãy nhập MSSV cần sửa."))) return;
        List<string> sets = new(); List<SqlParameter> parameters = new();
        if (_name.TextLength > 0) { sets.Add("HoTen=@name"); parameters.Add(new SqlParameter("@name", _name.Text.Trim())); }
        if (_classCode.TextLength > 0) { sets.Add("MaLop=@class"); parameters.Add(new SqlParameter("@class", _classCode.Text.Trim())); }
        if (_birth.Checked) { sets.Add("NgaySinh=@birth"); parameters.Add(DateParameter()); }
        if (sets.Count == 0) { MessageBox.Show("Hãy nhập ít nhất một thông tin cần cập nhật."); return; }
        parameters.Add(new SqlParameter("@id", _id.Text.Trim()));
        Execute("SinhVien", "UPDATE dbo.SinhVien SET " + string.Join(",", sets) + " WHERE MaSinhVien=@id", parameters.ToArray());
    }

    private SqlParameter DateParameter() => new("@birth", SqlDbType.Date) { Value = _birth.Value.Date };
    private void LoadSelected()
    {
        if (Grid.CurrentRow == null) return;
        _id.Text = Convert.ToString(Grid.CurrentRow.Cells["MaSinhVien"].Value);
        _name.Text = Convert.ToString(Grid.CurrentRow.Cells["HoTen"].Value);
        _birth.Value = Convert.ToDateTime(Grid.CurrentRow.Cells["NgaySinh"].Value); _birth.Checked = true;
        _classCode.Text = Convert.ToString(Grid.CurrentRow.Cells["MaLop"].Value);
    }
}
