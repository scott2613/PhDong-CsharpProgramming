using System;
using System.Linq;
using System.Windows.Forms;
using Lab05D.Core;
using Lab05D.WinForms;

namespace BaiNangCao_QuanLyHocVien;

internal static class Program
{
    [STAThread] private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmQuanLyHocVien());
    }
}

public sealed class FrmQuanLyHocVien : Form
{
    private readonly ClassRoster _roster = new();
    private readonly ListBox _classA = new() { Dock = DockStyle.Fill, SelectionMode = SelectionMode.MultiExtended };
    private readonly ListBox _classB = new() { Dock = DockStyle.Fill, SelectionMode = SelectionMode.MultiExtended };

    public FrmQuanLyHocVien()
    {
        Ui.Configure(this, "Bài nâng cao - Quản lý học viên", 900, 590);
        MenuStrip menu = new();
        ToolStripMenuItem update = new("Cập nhật");
        update.DropDownItems.Add("Thêm học viên...", null, (_, _) => AddStudent());
        update.DropDownItems.Add("Xóa các học viên đã chọn", null, (_, _) => RemoveSelected());
        menu.Items.Add(update);
        menu.Items.Add("Kết thúc", null, (_, _) => Close());

        TableLayoutPanel table = new() { Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 2, Padding = new Padding(22) };
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        table.Controls.Add(new Label { Text = "LỚP A", Dock = DockStyle.Fill, TextAlign = System.Drawing.ContentAlignment.MiddleCenter, Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold), ForeColor = Ui.Primary }, 0, 0);
        table.Controls.Add(new Label { Text = "LỚP B", Dock = DockStyle.Fill, TextAlign = System.Drawing.ContentAlignment.MiddleCenter, Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold), ForeColor = Ui.Primary }, 2, 0);
        table.Controls.Add(_classA, 0, 1);
        table.Controls.Add(_classB, 2, 1);
        FlowLayoutPanel commands = new() { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, Padding = new Padding(3, 80, 3, 3) };
        commands.Controls.Add(Ui.Button("A  →  B", (_, _) => MoveAToB()));
        commands.Controls.Add(Ui.Button("B  →  A", (_, _) => MoveBToA()));
        table.Controls.Add(commands, 1, 1);

        Controls.Add(table);
        Controls.Add(Ui.Title("QUẢN LÝ HỌC VIÊN THEO LỚP"));
        Controls.Add(menu);
        MainMenuStrip = menu;
        Shown += (_, _) => RefreshLists();
        FormClosing += (_, e) => { if (MessageBox.Show("Kết thúc chương trình?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) e.Cancel = true; };
    }

    private void AddStudent()
    {
        using FrmNhapHocVien dialog = new();
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        if (!_roster.Add(dialog.StudentName, dialog.ClassName, out string error)) MessageBox.Show(error, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        RefreshLists();
    }

    private void RemoveSelected()
    {
        _roster.Remove("A", Selected(_classA));
        _roster.Remove("B", Selected(_classB));
        RefreshLists();
    }

    private void MoveAToB() { _roster.Move("A", "B", Selected(_classA)); RefreshLists(); }
    private void MoveBToA() { _roster.Move("B", "A", Selected(_classB)); RefreshLists(); }

    private static string[] Selected(ListBox list)
    {
        string[] names = new string[list.SelectedItems.Count];
        list.SelectedItems.CopyTo(names, 0);
        return names;
    }

    private void RefreshLists()
    {
        _classA.Items.Clear(); _classB.Items.Clear();
        _classA.Items.AddRange(_roster.ClassA.ToArray());
        _classB.Items.AddRange(_roster.ClassB.ToArray());
    }
}

public sealed class FrmNhapHocVien : Form
{
    private readonly TextBox _name = Ui.TextBox(260);
    private readonly ComboBox _className = new() { Width = 120, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(6) };
    public string StudentName => _name.Text.Trim();
    public string ClassName => _className.Text;

    public FrmNhapHocVien()
    {
        Ui.Configure(this, "Thêm học viên", 520, 240);
        _className.Items.AddRange(new object[] { "A", "B" }); _className.SelectedIndex = 0;
        Controls.Add(Ui.Row(Ui.Button("Đồng ý", (_, _) => Accept()), Ui.Button("Hủy", (_, _) => { DialogResult = DialogResult.Cancel; Close(); })));
        Controls.Add(Ui.Row(Ui.Label("Họ và tên:"), _name, Ui.Label("Lớp:"), _className));
        Controls.Add(Ui.Title("NHẬP HỌC VIÊN MỚI"));
        AcceptButton = null;
    }

    private void Accept()
    {
        if (StudentName.Length == 0) { MessageBox.Show("Hãy nhập họ tên học viên."); _name.Focus(); return; }
        DialogResult = DialogResult.OK; Close();
    }
}
