using System;
using System.Windows.Forms;
using Lab05D.Core;
using Lab05D.WinForms;

namespace BaiMau02_ListView;

internal static class Program
{
    [STAThread] private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmDanhSachSinhVien());
    }
}

public sealed class FrmDanhSachSinhVien : Form
{
    private readonly TextBox _id = Ui.TextBox(180);
    private readonly TextBox _name = Ui.TextBox(280);
    private readonly ListView _list = Ui.ListView(("MSSV", 180), ("Họ và tên", 430));
    private readonly StudentListService _service = new();

    public FrmDanhSachSinhVien()
    {
        Ui.Configure(this, "Bài mẫu 2 - ListView", 780, 540);
        Controls.Add(_list);
        Controls.Add(Ui.Row(Ui.Label("Mã sinh viên:"), _id, Ui.Label("Họ và tên:"), _name,
            Ui.Button("Thêm", (_, _) => Add()), Ui.Button("Sửa", (_, _) => Edit()), Ui.Button("Xóa", (_, _) => Delete())));
        Controls.Add(Ui.Title("QUẢN LÝ SINH VIÊN BẰNG LISTVIEW"));
        _list.SelectedIndexChanged += (_, _) => LoadSelected();
    }

    private void Add()
    {
        if (!_service.TryAdd(new StudentListItem(_name.Text, _id.Text, string.Empty, string.Empty, string.Empty), out string error)) { Warn(error); return; }
        RefreshList(); ClearInputs();
    }

    private void Edit()
    {
        if (_list.SelectedItems.Count == 0) { Warn("Hãy chọn sinh viên cần sửa."); return; }
        string originalId = _list.SelectedItems[0].Text;
        if (!_service.TryUpdate(originalId, new StudentListItem(_name.Text, _id.Text, string.Empty, string.Empty, string.Empty), out string error)) { Warn(error); return; }
        RefreshList(); ClearInputs();
    }

    private void Delete()
    {
        if (_list.SelectedItems.Count == 0) { Warn("Hãy chọn sinh viên cần xóa."); return; }
        _service.Remove(_list.SelectedItems[0].Text); RefreshList(); ClearInputs();
    }

    private void LoadSelected()
    {
        if (_list.SelectedItems.Count == 0) return;
        _id.Text = _list.SelectedItems[0].Text;
        _name.Text = _list.SelectedItems[0].SubItems[1].Text;
    }

    private void RefreshList()
    {
        _list.Items.Clear();
        foreach (StudentListItem student in _service.Items)
            _list.Items.Add(new ListViewItem(new[] { student.Id, student.FullName }));
    }

    private void ClearInputs() { _id.Clear(); _name.Clear(); _id.Focus(); }
    private static void Warn(string text) => MessageBox.Show(text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
}
