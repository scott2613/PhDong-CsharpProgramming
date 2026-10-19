// Form bài tại lớp 2: quản lý lớp và sinh viên, đồng bộ dữ liệu giữa TreeView và ComboBox.
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lab05C.Core;

namespace Bai02_QuanLySinhVien;

public sealed class FrmQuanLySinhVien : Form
{
    private readonly StudentDirectory _directory = new();
    private readonly TreeView _tree = new();
    private readonly ComboBox _classCombo = new();
    private readonly CheckBox _showAddClass = new();
    private readonly GroupBox _classGroup = new();
    private readonly TextBox _className = new();
    private readonly TextBox _studentId = new();
    private readonly TextBox _studentName = new();
    private readonly TextBox _studentAddress = new();

    public FrmQuanLySinhVien()
    {
        Text = "Quản lý sinh viên";
        ClientSize = new Size(940, 590);
        Font = new Font("Segoe UI", 10.5F);
        BackColor = Color.White;
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        Controls.Add(new Label { Text = "QUẢN LÝ SINH VIÊN", AutoSize = true, Font = new Font("Segoe UI", 21F, FontStyle.Bold), ForeColor = Color.Firebrick, Location = new Point(360, 20) });
        _tree.Location = new Point(35, 75); _tree.Size = new Size(365, 465); _tree.AfterSelect += TreeSelected; Controls.Add(_tree);
        AddLabel("Chọn lớp:", 465, 90); _classCombo.Location = new Point(570, 85); _classCombo.Size = new Size(310, 33); _classCombo.DropDownStyle = ComboBoxStyle.DropDownList; Controls.Add(_classCombo);

        GroupBox studentGroup = new() { Text = "Thông tin sinh viên", Location = new Point(450, 135), Size = new Size(450, 260) };
        AddLabel("Mã SV:", 25, 48, studentGroup); ConfigureText(_studentId, 145, 43, 260, studentGroup);
        AddLabel("Họ tên:", 25, 98, studentGroup); ConfigureText(_studentName, 145, 93, 260, studentGroup);
        AddLabel("Địa chỉ:", 25, 148, studentGroup); ConfigureText(_studentAddress, 145, 143, 260, studentGroup);
        studentGroup.Controls.Add(CreateButton("&Cập nhật", 85, 200, 130, AddStudent));
        studentGroup.Controls.Add(CreateButton("&Xóa", 245, 200, 120, RemoveStudent));
        Controls.Add(studentGroup);

        _showAddClass.Text = "Thêm lớp"; _showAddClass.AutoSize = true; _showAddClass.Location = new Point(465, 415); _showAddClass.CheckedChanged += (_, _) => _classGroup.Visible = _showAddClass.Checked; Controls.Add(_showAddClass);
        _classGroup.Text = "Thông tin lớp"; _classGroup.Location = new Point(450, 450); _classGroup.Size = new Size(450, 90); _classGroup.Visible = false;
        AddLabel("Tên lớp:", 25, 37, _classGroup); ConfigureText(_className, 115, 32, 200, _classGroup); _classGroup.Controls.Add(CreateButton("Thêm lớp", 325, 28, 105, AddClass)); Controls.Add(_classGroup);

        foreach (string className in new[] { "05DHTH1", "05DHTH2", "05DHTH3", "05DHTH4" }) _directory.AddClass(className, out _);
        RefreshTree();
    }

    private void AddClass(object? sender, EventArgs e)
    {
        if (!_directory.AddClass(_className.Text, out string error)) { ShowError(error); return; }
        _className.Clear(); _showAddClass.Checked = false; RefreshTree();
    }

    private void AddStudent(object? sender, EventArgs e)
    {
        if (!_directory.AddStudent(_classCombo.Text, _studentId.Text, _studentName.Text, _studentAddress.Text, out string error)) { ShowError(error); return; }
        ClearStudentInputs(); RefreshTree();
    }

    private void RemoveStudent(object? sender, EventArgs e)
    {
        if (_tree.SelectedNode?.Tag is not Student student) { ShowError("Chỉ được xóa node sinh viên đang chọn."); return; }
        if (MessageBox.Show($"Bạn có chắc chắn muốn xóa {student.FullName}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        _directory.RemoveStudent(student.Id); ClearStudentInputs(); RefreshTree();
    }

    private void TreeSelected(object? sender, TreeViewEventArgs e)
    {
        if (e.Node?.Tag is not Student student) return;
        _studentId.Text = student.Id; _studentName.Text = student.FullName; _studentAddress.Text = student.Address;
        if (e.Node.Parent is not null) _classCombo.SelectedItem = e.Node.Parent.Text;
    }

    private void RefreshTree()
    {
        string selectedClass = _classCombo.Text;
        _tree.BeginUpdate(); _tree.Nodes.Clear();
        TreeNode root = _tree.Nodes.Add("Danh sách lớp");
        foreach (string className in _directory.Classes)
        {
            TreeNode classNode = root.Nodes.Add(className);
            foreach (Student student in _directory.GetStudents(className))
            {
                TreeNode studentNode = classNode.Nodes.Add($"{student.Id}, {student.FullName}"); studentNode.Tag = student;
                studentNode.Nodes.Add(student.Address);
            }
        }
        _tree.ExpandAll(); _tree.EndUpdate();
        _classCombo.DataSource = _directory.Classes.ToList();
        if (_classCombo.Items.Contains(selectedClass)) _classCombo.SelectedItem = selectedClass;
    }

    private void ClearStudentInputs() { _studentId.Clear(); _studentName.Clear(); _studentAddress.Clear(); _studentId.Focus(); }
    private void AddLabel(string text, int x, int y, Control? parent = null) { Label label = new() { Text = text, AutoSize = true, Location = new Point(x, y) }; (parent?.Controls ?? Controls).Add(label); }
    private static void ConfigureText(TextBox box, int x, int y, int width, Control parent) { box.Location = new Point(x, y); box.Size = new Size(width, 32); parent.Controls.Add(box); }
    private static Button CreateButton(string text, int x, int y, int width, EventHandler click) { Button button = new() { Text = text, Location = new Point(x, y), Size = new Size(width, 38) }; button.Click += click; return button; }
    private static void ShowError(string error) => MessageBox.Show(error, "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
}
