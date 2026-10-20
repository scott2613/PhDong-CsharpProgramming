// Form danh bạ: tạo sẵn 26 node A đến Z và tự động đưa người mới vào đúng nhóm chữ cái.
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lab05C.Core;

namespace BaiVeNha03_DanhBa;

public sealed class FrmDanhBa : Form
{
    private readonly ContactBook _book = new();
    private readonly TreeView _tree = new();
    private readonly TextBox _firstName = new();
    private readonly TextBox _lastName = new();

    public FrmDanhBa()
    {
        Text = "Danh bạ theo chữ cái";
        ClientSize = new Size(760, 590);
        Font = new Font("Segoe UI", 10.5F);
        BackColor = Color.White;
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        Controls.Add(new Label { Text = "DANH BẠ A ĐẾN Z", AutoSize = true, Font = new Font("Segoe UI", 21F, FontStyle.Bold), ForeColor = Color.Firebrick, Location = new Point(255, 20) });
        _tree.Location = new Point(45, 80); _tree.Size = new Size(340, 450); Controls.Add(_tree);
        GroupBox inputGroup = new() { Text = "Thông tin liên hệ", Location = new Point(425, 110), Size = new Size(290, 230) };
        inputGroup.Controls.Add(new Label { Text = "First Name", AutoSize = true, Location = new Point(20, 45) });
        _firstName.Location = new Point(120, 40); _firstName.Size = new Size(145, 32); inputGroup.Controls.Add(_firstName);
        inputGroup.Controls.Add(new Label { Text = "Last Name", AutoSize = true, Location = new Point(20, 95) });
        _lastName.Location = new Point(120, 90); _lastName.Size = new Size(145, 32); inputGroup.Controls.Add(_lastName);
        Button add = CreateButton("&Add", 120, 145, 145, AddContact); inputGroup.Controls.Add(add);
        Controls.Add(inputGroup);
        Controls.Add(CreateButton("E&xit", 500, 385, 145, (_, _) => Close()));
        AcceptButton = add; RefreshTree();
    }

    private void AddContact(object? sender, EventArgs e)
    {
        if (!_book.Add(_firstName.Text, _lastName.Text, out Contact? contact, out string error))
        { MessageBox.Show(error, "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
        _firstName.Clear(); _lastName.Clear(); _firstName.Focus(); RefreshTree(contact);
    }

    private void RefreshTree(Contact? selected = null)
    {
        _tree.BeginUpdate(); _tree.Nodes.Clear(); TreeNode? selectedNode = null;
        foreach ((char letter, System.Collections.Generic.List<Contact> contacts) in _book.Groups)
        {
            TreeNode group = _tree.Nodes.Add(letter.ToString());
            foreach (Contact contact in contacts)
            {
                TreeNode node = group.Nodes.Add(contact.DisplayName); node.Tag = contact;
                if (selected is not null && contact == selected) selectedNode = node;
            }
        }
        if (selectedNode is not null) { selectedNode.Parent?.Expand(); _tree.SelectedNode = selectedNode; }
        _tree.EndUpdate();
    }

    private static Button CreateButton(string text, int x, int y, int width, EventHandler click) { Button button = new() { Text = text, Location = new Point(x, y), Size = new Size(width, 42) }; button.Click += click; return button; }
}
