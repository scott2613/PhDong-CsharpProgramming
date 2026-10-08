// Form của bài tập về nhà máy tính bỏ túi: nhận thao tác người dùng, kiểm tra dữ liệu và hiển thị kết quả.
using System;
using System.Drawing;
using System.Windows.Forms;
using Lab04.Core;

namespace BaiTapVeNha_MayTinhBoTui;

/// <summary>Máy tính bỏ túi thực hiện cộng, trừ, nhân, chia và xóa.</summary>
public sealed class FrmMayTinhBoTui : Form
{
    private readonly PocketCalculator _calculator = new();
    private readonly TextBox _display = new();

    // Khởi tạo bố cục, thiết lập phím tắt và đăng ký các sự kiện của form.
    public FrmMayTinhBoTui()
    {
        Text = "Máy tính bỏ túi";
        ClientSize = new Size(380, 510);
        Font = new Font("Segoe UI", 12F);
        BackColor = Color.FromArgb(245, 247, 250);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;

        Controls.Add(new Label { AutoSize = true, Text = "MÁY TÍNH BỎ TÚI", Font = new Font("Segoe UI", 18F, FontStyle.Bold), ForeColor = Color.Firebrick, Location = new Point(59, 31) });
        _display.Text = "0"; _display.ReadOnly = true; _display.TextAlign = HorizontalAlignment.Right; _display.Font = new Font("Segoe UI", 20F, FontStyle.Bold); _display.Location = new Point(36, 88); _display.Size = new Size(308, 43); Controls.Add(_display);

        string[,] keys =
        {
            { "7", "8", "9", "/" },
            { "4", "5", "6", "*" },
            { "1", "2", "3", "-" },
            { "0", "C", "=", "+" }
        };
        for (int row = 0; row < 4; row++)
        for (int column = 0; column < 4; column++)
        {
            string key = keys[row, column];
            Button button = new() { Text = key == "*" ? "×" : key, Tag = key, Location = new Point(36 + column * 79, 158 + row * 75), Size = new Size(65, 58), FlatStyle = FlatStyle.Flat, BackColor = column == 3 ? Color.FromArgb(220, 235, 249) : Color.White };
            button.Click += KeyClick;
            Controls.Add(button);
        }
    }

    private void KeyClick(object? sender, EventArgs e)
    {
        if (sender is not Button button || button.Tag is not string key) return;
        if (key.Length == 1 && char.IsDigit(key[0])) _calculator.EnterDigit(key[0]);
        else if (key is "+" or "-" or "*" or "/") _calculator.SetOperation(key[0]);
        else if (key == "C") _calculator.Clear();
        else if (!_calculator.TryEvaluate(out string error))
            MessageBox.Show(error, "Không thể tính", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        _display.Text = _calculator.Display;
    }
}
