// Form bài mẫu ComboBox: nạp danh sách dân tộc và hiển thị lựa chọn theo hai cách.
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BaiMau02_ComboBox;

public sealed class FrmDanToc : Form
{
    private readonly ComboBox _ethnicity = new();
    private readonly Label _result = new();
    private bool _loaded;

    public FrmDanToc()
    {
        Text = "Dữ liệu dân tộc";
        ClientSize = new Size(620, 360);
        Font = new Font("Segoe UI", 11F);
        BackColor = Color.White;
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        Controls.Add(new Label { Text = "COMBOBOX DÂN TỘC", AutoSize = true, Font = new Font("Segoe UI", 20F, FontStyle.Bold), ForeColor = Color.Firebrick, Location = new Point(145, 30) });
        Button load = CreateButton("&Load dữ liệu ComboBox", 185, 95, 250, LoadData);
        Controls.Add(load);
        Controls.Add(new Label { Text = "Dân tộc:", AutoSize = true, Location = new Point(95, 165) });
        _ethnicity.Location = new Point(200, 160); _ethnicity.Size = new Size(315, 33); _ethnicity.DropDownStyle = ComboBoxStyle.DropDownList;
        _ethnicity.SelectedIndexChanged += ShowSelectionMessage;
        Controls.Add(_ethnicity);
        Controls.Add(CreateButton("&Hiển thị", 225, 220, 170, ShowSelection));
        _result.AutoSize = true; _result.ForeColor = Color.Firebrick; _result.Font = new Font("Segoe UI", 11F, FontStyle.Bold); _result.Location = new Point(95, 290);
        Controls.Add(_result);
    }

    private void LoadData(object? sender, EventArgs e)
    {
        if (_loaded) { MessageBox.Show("Dữ liệu đã được nạp.", "Thông báo"); return; }
        _ethnicity.Items.AddRange(new object[] { "Kinh", "Hoa", "K'Me", "H'Mông", "Khác" });
        _loaded = true;
        _ethnicity.SelectedIndex = 0;
    }

    private void ShowSelection(object? sender, EventArgs e) =>
        _result.Text = _ethnicity.SelectedIndex >= 0 ? $"Dân tộc được chọn: {_ethnicity.SelectedItem}" : "Bạn chưa chọn dân tộc.";

    private void ShowSelectionMessage(object? sender, EventArgs e)
    {
        if (_ethnicity.SelectedIndex >= 0) MessageBox.Show($"Dân tộc được chọn: {_ethnicity.SelectedItem}", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private static Button CreateButton(string text, int x, int y, int width, EventHandler click)
    {
        Button button = new() { Text = text, Location = new Point(x, y), Size = new Size(width, 42) };
        button.Click += click;
        return button;
    }
}
