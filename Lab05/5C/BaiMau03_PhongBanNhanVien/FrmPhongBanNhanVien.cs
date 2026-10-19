// Form bài mẫu TreeView: quản lý phòng ban và thêm nhân viên vào đúng phòng ban.
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lab05C.Core;

namespace BaiMau03_PhongBanNhanVien;

public sealed class FrmPhongBanNhanVien : Form
{
    private readonly DepartmentDirectory _directory = new();
    private readonly TreeView _tree = new();
    private readonly ComboBox _department = new();
    private readonly TextBox _newDepartment = new();
    private readonly TextBox _id = new();
    private readonly TextBox _name = new();
    private readonly TextBox _address = new();

    public FrmPhongBanNhanVien()
    {
        Text = "Phòng ban - Nhân viên";
        ClientSize = new Size(920, 560);
        Font = new Font("Segoe UI", 10.5F);
        BackColor = Color.White;
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        Controls.Add(new Label { Text = "HỒ SƠ NHÂN VIÊN", AutoSize = true, Font = new Font("Segoe UI", 20F, FontStyle.Bold), ForeColor = Color.RoyalBlue, Location = new Point(510, 25) });
        _tree.Location = new Point(35, 75); _tree.Size = new Size(350, 370); Controls.Add(_tree);
        AddLabel("Phòng ban mới:", 35, 470); ConfigureText(_newDepartment, 170, 465, 215);
        Controls.Add(CreateButton("Thêm phòng ban", 35, 510, 165, AddDepartment));
        Controls.Add(CreateButton("Xóa phòng ban", 220, 510, 165, RemoveDepartment));

        AddLabel("Mã số:", 465, 105); ConfigureText(_id, 585, 100, 275);
        AddLabel("Họ tên:", 465, 155); ConfigureText(_name, 585, 150, 275);
        AddLabel("Địa chỉ:", 465, 205); ConfigureText(_address, 585, 200, 275);
        AddLabel("Phòng ban:", 465, 255);
        _department.Location = new Point(585, 250); _department.Size = new Size(275, 33); _department.DropDownStyle = ComboBoxStyle.DropDownList; Controls.Add(_department);
        Controls.Add(CreateButton("&Thêm nhân viên", 520, 320, 170, AddEmployee));
        Controls.Add(CreateButton("T&hoát", 705, 320, 125, (_, _) => Close()));

        foreach (string item in new[] { "Giám đốc", "Tổ chức hành chính", "Kế hoạch", "Kế toán" }) _directory.AddDepartment(item, out _);
        RefreshDirectory();
        FormClosing += ConfirmClosing;
    }

    private void AddDepartment(object? sender, EventArgs e)
    {
        if (!_directory.AddDepartment(_newDepartment.Text, out string error)) { ShowError(error); return; }
        _newDepartment.Clear(); _newDepartment.Focus(); RefreshDirectory();
    }

    private void RemoveDepartment(object? sender, EventArgs e)
    {
        TreeNode? node = _tree.SelectedNode;
        if (node is null || node.Parent is not null) { ShowError("Vui lòng chọn một node phòng ban."); return; }
        if (MessageBox.Show($"Xóa phòng ban {node.Text} và toàn bộ nhân viên?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        _directory.RemoveDepartment(node.Text);
        RefreshDirectory();
    }

    private void AddEmployee(object? sender, EventArgs e)
    {
        if (!_directory.AddEmployee(_department.Text, _id.Text, _name.Text, _address.Text, out string error)) { ShowError(error); return; }
        _id.Clear(); _name.Clear(); _address.Clear(); _id.Focus(); RefreshDirectory();
    }

    private void RefreshDirectory()
    {
        string selected = _department.Text;
        _tree.BeginUpdate(); _tree.Nodes.Clear();
        foreach (string department in _directory.Departments)
        {
            TreeNode departmentNode = _tree.Nodes.Add(department);
            foreach (Employee employee in _directory.GetEmployees(department))
                departmentNode.Nodes.Add($"{employee.FullName} ({employee.Id})").Tag = employee;
        }
        _tree.ExpandAll(); _tree.EndUpdate();
        _department.DataSource = _directory.Departments.ToList();
        if (_department.Items.Contains(selected)) _department.SelectedItem = selected;
    }

    private void AddLabel(string text, int x, int y) => Controls.Add(new Label { Text = text, AutoSize = true, Location = new Point(x, y) });
    private void ConfigureText(TextBox box, int x, int y, int width) { box.Location = new Point(x, y); box.Size = new Size(width, 32); Controls.Add(box); }
    private static Button CreateButton(string text, int x, int y, int width, EventHandler click) { Button button = new() { Text = text, Location = new Point(x, y), Size = new Size(width, 42) }; button.Click += click; return button; }
    private static void ShowError(string error) => MessageBox.Show(error, "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    private static void ConfirmClosing(object? sender, FormClosingEventArgs e) { if (MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) e.Cancel = true; }
}
