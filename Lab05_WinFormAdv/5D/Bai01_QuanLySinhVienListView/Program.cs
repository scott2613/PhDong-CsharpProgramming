using System;
using System.Linq;
using System.Windows.Forms;
using Lab05D.Core;
using Lab05D.WinForms;

namespace Bai01_QuanLySinhVienListView;

internal static class Program
{
    [STAThread] private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmQuanLySinhVien());
    }
}

public sealed class FrmQuanLySinhVien : Form
{
    private readonly TextBox _id = Ui.TextBox(150);
    private readonly TextBox _name = Ui.TextBox(230);
    private readonly RadioButton _male = new() { Text = "Nam", Checked = true, AutoSize = true, Margin = new Padding(8, 10, 2, 2) };
    private readonly RadioButton _female = new() { Text = "Nữ", AutoSize = true, Margin = new Padding(8, 10, 2, 2) };
    private readonly CheckBox _english = new() { Text = "Anh", AutoSize = true, Margin = new Padding(8, 10, 2, 2) };
    private readonly CheckBox _french = new() { Text = "Pháp", AutoSize = true, Margin = new Padding(8, 10, 2, 2) };
    private readonly CheckBox _japanese = new() { Text = "Nhật", AutoSize = true, Margin = new Padding(8, 10, 2, 2) };
    private readonly ComboBox _ethnicity = new() { Width = 125, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(6) };
    private readonly StudentListService _service = new();
    private readonly ListView _list = Ui.ListView(("MSSV", 120), ("Họ tên", 240), ("Giới tính", 100), ("Ngoại ngữ", 190), ("Dân tộc", 110));

    public FrmQuanLySinhVien()
    {
        Ui.Configure(this, "Bài 1 - Quản lý sinh viên", 960, 610);
        _ethnicity.Items.AddRange(new object[] { "Kinh", "Hoa", "Khmer", "Chăm", "Khác" });
        _ethnicity.SelectedIndex = 0;
        ContextMenuStrip menu = new();
        menu.Items.Add("Xóa sinh viên", null, (_, _) => Delete());
        _list.ContextMenuStrip = menu;
        Controls.Add(_list);
        Controls.Add(Ui.Row(Ui.Label("Ngoại ngữ:"), _english, _french, _japanese, Ui.Label("Dân tộc:"), _ethnicity,
            Ui.Button("Thêm", (_, _) => Add()), Ui.Button("Cập nhật", (_, _) => Edit()), Ui.Button("Xóa", (_, _) => Delete()), Ui.Button("Đóng", (_, _) => Close())));
        Controls.Add(Ui.Row(Ui.Label("MSSV:"), _id, Ui.Label("Họ tên:"), _name, Ui.Label("Giới tính:"), _male, _female));
        Controls.Add(Ui.Title("QUẢN LÝ SINH VIÊN TRONG LỚP"));
        _list.SelectedIndexChanged += (_, _) => LoadSelected();
        FormClosing += ConfirmClosing;
    }

    private StudentListItem ReadStudent()
    {
        string languages = string.Join(", ", new[] { _english.Checked ? "Anh" : "", _french.Checked ? "Pháp" : "", _japanese.Checked ? "Nhật" : "" }.Where(x => x.Length > 0));
        return new StudentListItem(_name.Text, _id.Text, _male.Checked ? "Nam" : "Nữ", languages, _ethnicity.Text);
    }

    private void Add()
    {
        if (!_service.TryAdd(ReadStudent(), out string error)) { Warn(error); return; }
        RefreshList(); ClearInputs();
    }

    private void Edit()
    {
        if (_list.SelectedItems.Count == 0) { Warn("Hãy chọn sinh viên cần cập nhật."); return; }
        string originalId = _list.SelectedItems[0].Text;
        if (!_service.TryUpdate(originalId, ReadStudent(), out string error)) { Warn(error); return; }
        RefreshList(); ClearInputs();
    }

    private void Delete()
    {
        if (_list.SelectedItems.Count == 0) { Warn("Hãy chọn sinh viên cần xóa."); return; }
        if (MessageBox.Show("Xóa sinh viên đang chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        _service.Remove(_list.SelectedItems[0].Text); RefreshList(); ClearInputs();
    }

    private void LoadSelected()
    {
        if (_list.SelectedItems.Count == 0) return;
        ListViewItem item = _list.SelectedItems[0];
        _id.Text = item.Text; _id.Enabled = false; // Không cho đổi mã sinh viên khi sửa.
        _name.Text = item.SubItems[1].Text;
        _male.Checked = item.SubItems[2].Text == "Nam";
        _female.Checked = !_male.Checked;
        _english.Checked = item.SubItems[3].Text.Contains("Anh");
        _french.Checked = item.SubItems[3].Text.Contains("Pháp");
        _japanese.Checked = item.SubItems[3].Text.Contains("Nhật");
        _ethnicity.Text = item.SubItems[4].Text;
    }

    private void RefreshList()
    {
        _list.Items.Clear();
        foreach (StudentListItem s in _service.Items)
            _list.Items.Add(new ListViewItem(new[] { s.Id, s.FullName, s.Gender, s.Languages, s.Ethnicity }));
    }

    private void ClearInputs()
    {
        _id.Enabled = true; _id.Clear(); _name.Clear(); _male.Checked = true;
        _english.Checked = _french.Checked = _japanese.Checked = false; _ethnicity.SelectedIndex = 0; _id.Focus();
    }

    private static void Warn(string message) => MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    private void ConfirmClosing(object? sender, FormClosingEventArgs e)
    {
        if (MessageBox.Show("Bạn có muốn đóng chương trình?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) e.Cancel = true;
    }
}
