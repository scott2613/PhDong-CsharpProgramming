// Form bài nâng cao: tạo 50 họ tên và thực hiện các thao tác xử lý chuỗi trên phần tử được chọn.
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lab05C.Core;

namespace BaiNangCao_XuLyChuoi;

public sealed class FrmXuLyChuoi : Form
{
    private readonly ListBox _names = new();

    public FrmXuLyChuoi()
    {
        Text = "Xử lý chuỗi họ tên";
        ClientSize = new Size(1000, 570);
        Font = new Font("Segoe UI", 10.5F);
        BackColor = Color.White;
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        Controls.Add(new Label { Text = "XỬ LÝ DANH SÁCH HỌ TÊN", AutoSize = true, Font = new Font("Segoe UI", 20F, FontStyle.Bold), ForeColor = Color.Firebrick, Location = new Point(250, 22) });
        Controls.Add(CreateButton("Nhập tên ngẫu nhiên", 55, 82, 280, (_, _) => GenerateNames()));
        _names.Location = new Point(55, 140); _names.Size = new Size(330, 360); _names.SelectionMode = SelectionMode.MultiExtended; _names.DoubleClick += EditSelected; Controls.Add(_names);

        // Các nút được làm rộng hơn để câu mô tả dài vẫn có khoảng đệm hai bên.
        int y = 105;
        Controls.Add(CreateButton("Xóa phần tử đang chọn", 420, y, 520, (_, _) => RemoveSelected())); y += 55;
        Controls.Add(CreateButton("Xóa phần tử có tên là Sơn", 420, y, 520, (_, _) => RemoveWhere(name => NameProcessor.HasGivenName(name, "Sơn")))); y += 55;
        Controls.Add(CreateButton("Xóa phần tử có họ là Lê", 420, y, 520, (_, _) => RemoveWhere(name => NameProcessor.HasSurname(name, "Lê")))); y += 55;
        Controls.Add(CreateButton("Chuyển phần tử chọn thành chữ HOA", 420, y, 520, (_, _) => TransformSelected(name => name.ToUpper(new System.Globalization.CultureInfo("vi-VN"))))); y += 55;
        Controls.Add(CreateButton("Chuyển phần tử chọn thành chữ thường", 420, y, 520, (_, _) => TransformSelected(name => name.ToLower(new System.Globalization.CultureInfo("vi-VN"))))); y += 55;
        Controls.Add(CreateButton("Chuyển phần tử chọn thành viết Hoa đầu mỗi từ", 420, y, 520, (_, _) => TransformSelected(NameProcessor.ToTitleCase))); y += 55;
        Controls.Add(CreateButton("Xóa tất cả các phần tử", 420, y, 520, (_, _) => _names.Items.Clear()));
        Controls.Add(new Label { Text = "Mẹo: nhấp đúp vào một tên để sửa trực tiếp.", AutoSize = true, ForeColor = Color.DimGray, Location = new Point(55, 520) });
    }

    private void GenerateNames()
    {
        _names.Items.Clear();
        foreach (string name in NameProcessor.Generate(50)) _names.Items.Add(name);
    }

    private void RemoveSelected()
    {
        int[] indices = _names.SelectedIndices.Cast<int>().OrderByDescending(index => index).ToArray();
        foreach (int index in indices) _names.Items.RemoveAt(index);
    }

    private void RemoveWhere(Func<string, bool> predicate)
    {
        string[] matches = _names.Items.Cast<string>().Where(predicate).ToArray();
        foreach (string item in matches) _names.Items.Remove(item);
    }

    private void TransformSelected(Func<string, string> transform)
    {
        int[] indices = _names.SelectedIndices.Cast<int>().ToArray();
        foreach (int index in indices) _names.Items[index] = transform((string)_names.Items[index]);
    }

    private void EditSelected(object? sender, EventArgs e)
    {
        if (_names.SelectedIndex < 0) return;
        string current = (string)_names.SelectedItem!;
        string? updated = PromptDialog.Show("Nhập họ tên mới:", current);
        if (!string.IsNullOrWhiteSpace(updated)) _names.Items[_names.SelectedIndex] = NameProcessor.ToTitleCase(updated);
    }

    private static Button CreateButton(string text, int x, int y, int width, EventHandler click) { Button button = new() { Text = text, Location = new Point(x, y), Size = new Size(width, 42) }; button.Click += click; return button; }
}

internal sealed class PromptDialog : Form
{
    private readonly TextBox _input = new();
    private PromptDialog(string prompt, string initial)
    {
        Text = "Cập nhật họ tên"; ClientSize = new Size(430, 150); Font = new Font("Segoe UI", 10.5F); StartPosition = FormStartPosition.CenterParent; FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false; MinimizeBox = false;
        Controls.Add(new Label { Text = prompt, AutoSize = true, Location = new Point(20, 20) });
        _input.Text = initial; _input.Location = new Point(20, 52); _input.Size = new Size(390, 32); Controls.Add(_input);
        Button ok = new() { Text = "Đồng ý", DialogResult = DialogResult.OK, Location = new Point(220, 100), Size = new Size(90, 36) };
        Button cancel = new() { Text = "Hủy", DialogResult = DialogResult.Cancel, Location = new Point(320, 100), Size = new Size(90, 36) };
        Controls.AddRange(new Control[] { ok, cancel }); AcceptButton = ok; CancelButton = cancel;
    }
    public static string? Show(string prompt, string initial) { using PromptDialog dialog = new(prompt, initial); return dialog.ShowDialog() == DialogResult.OK ? dialog._input.Text.Trim() : null; }
}
