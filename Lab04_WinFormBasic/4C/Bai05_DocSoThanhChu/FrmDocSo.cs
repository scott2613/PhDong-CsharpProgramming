using System;
using System.Drawing;
using System.Windows.Forms;
using Lab04.Core;

namespace Bai05_DocSoThanhChu;

/// <summary>Đọc số nguyên từ 1 đến 999 thành chữ tiếng Việt.</summary>
public sealed class FrmDocSo : Form
{
    private readonly TextBox _txtNumber = new();
    private readonly TextBox _txtWords = new();
    private readonly ErrorProvider _errors = new();

    public FrmDocSo()
    {
        Text = "Đọc số thành chữ";
        ClientSize = new Size(610, 340);
        Font = new Font("Segoe UI", 11F);
        BackColor = Color.White;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;

        Controls.Add(new Label { AutoSize = true, Text = "ĐỌC SỐ THÀNH CHỮ", Font = new Font("Segoe UI", 21F, FontStyle.Bold), ForeColor = Color.Firebrick, Location = new Point(148, 30) });
        Controls.Add(new Label { AutoSize = true, Text = "Nhập số (từ 1 đến 999):", Location = new Point(56, 109) });
        _txtNumber.Location = new Point(310, 104); _txtNumber.Size = new Size(237, 29); Controls.Add(_txtNumber);
        _txtWords.Location = new Point(56, 160); _txtWords.Size = new Size(491, 40); _txtWords.ReadOnly = true; _txtWords.BackColor = Color.FromArgb(255, 245, 222); _txtWords.Font = new Font("Segoe UI", 12F, FontStyle.Bold); Controls.Add(_txtWords);

        Button btnRead = CreateButton("&Thực hiện", 70); btnRead.Click += ReadNumber;
        Button btnClear = CreateButton("&Xóa", 244); btnClear.Click += ClearForm;
        Button btnExit = CreateButton("T&hoát", 418); btnExit.Click += (_, _) => Close();
        AcceptButton = btnRead;
        CancelButton = btnExit;
        _errors.ContainerControl = this;
        _txtNumber.KeyPress += (_, e) => { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; };
        FormClosing += ConfirmClosing;
    }

    private void ReadNumber(object? sender, EventArgs e)
    {
        _errors.Clear();
        if (!VietnameseNumberReader.TryRead(_txtNumber.Text, out string words, out string error))
        {
            _txtWords.Clear();
            _errors.SetError(_txtNumber, error);
            MessageBox.Show(error, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        _txtWords.Text = char.ToUpper(words[0]) + words[1..];
    }

    private void ClearForm(object? sender, EventArgs e) { _txtNumber.Clear(); _txtWords.Clear(); _errors.Clear(); _txtNumber.Focus(); }

    private void ConfirmClosing(object? sender, FormClosingEventArgs e)
    {
        DialogResult result = MessageBox.Show("Bạn có muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        e.Cancel = result == DialogResult.No;
    }

    private Button CreateButton(string text, int x) { Button button = new() { Text = text, Location = new Point(x, 235), Size = new Size(120, 42) }; Controls.Add(button); return button; }
}
