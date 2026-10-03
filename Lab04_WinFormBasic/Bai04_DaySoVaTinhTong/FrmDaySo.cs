using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lab04.Core;

namespace Bai04_DaySoVaTinhTong;

/// <summary>Nhập dãy số nguyên, tính tổng toàn bộ, tổng chẵn và tổng lẻ.</summary>
public sealed class FrmDaySo : Form
{
    private readonly IntegerSequence _sequence = new();
    private readonly TextBox _txtNumber = new();
    private readonly TextBox _txtSequence = new();
    private readonly TextBox _txtSum = new();
    private readonly TextBox _txtEven = new();
    private readonly TextBox _txtOdd = new();
    private readonly ErrorProvider _errors = new();

    public FrmDaySo()
    {
        Text = "Dãy số và tính tổng";
        ClientSize = new Size(700, 410);
        Font = new Font("Segoe UI", 11F);
        BackColor = Color.White;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;

        Label title = new() { AutoSize = true, Text = "NHẬP DÃY SỐ VÀ TÍNH TỔNG", Font = new Font("Segoe UI", 18F, FontStyle.Bold), ForeColor = Color.Firebrick, Location = new Point(103, 28) };
        Controls.Add(title);
        AddLabel("Nhập số:", 72, 101); ConfigureInput(_txtNumber, 255, 96, 210);
        Button btnAdd = CreateButton("&Nhập", 495, 94, 105); btnAdd.Click += AddNumber;
        AddLabel("Dãy vừa nhập:", 72, 151); ConfigureOutput(_txtSequence, 255, 146, 345);
        AddLabel("Tổng các phần tử:", 72, 201); ConfigureOutput(_txtSum, 255, 196, 160);
        AddLabel("Tổng chẵn:", 72, 251); ConfigureOutput(_txtEven, 255, 246, 120);
        AddLabel("Tổng lẻ:", 406, 251); ConfigureOutput(_txtOdd, 480, 246, 120);

        Button btnContinue = CreateButton("&Tiếp tục", 190, 321, 135); btnContinue.Click += Continue;
        Button btnExit = CreateButton("T&hoát", 370, 321, 135); btnExit.Click += (_, _) => Close();
        AcceptButton = btnAdd;
        CancelButton = btnExit;
        _errors.ContainerControl = this;
        _txtNumber.KeyPress += IntegerKeyPress;
        FormClosing += ConfirmClosing;
    }

    private void AddNumber(object? sender, EventArgs e)
    {
        _errors.Clear();
        if (!_sequence.TryAdd(_txtNumber.Text, out string error))
        {
            _errors.SetError(_txtNumber, error);
            MessageBox.Show(error, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _txtNumber.SelectAll();
            return;
        }

        _txtSequence.Text = string.Join("  ", _sequence.Numbers);
        _txtSum.Text = _sequence.Sum.ToString();
        _txtEven.Text = _sequence.EvenSum.ToString();
        _txtOdd.Text = _sequence.OddSum.ToString();
        _txtNumber.Clear();
        _txtNumber.Focus();
    }

    private void Continue(object? sender, EventArgs e)
    {
        _sequence.Clear();
        foreach (TextBox box in new[] { _txtNumber, _txtSequence, _txtSum, _txtEven, _txtOdd }) box.Clear();
        _errors.Clear();
        _txtNumber.Focus();
    }

    private static void IntegerKeyPress(object? sender, KeyPressEventArgs e)
    {
        TextBox box = (TextBox)sender!;
        bool signAtStart = e.KeyChar == '-' && box.SelectionStart == 0 && !box.Text.Contains('-');
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !signAtStart) e.Handled = true;
    }

    private void ConfirmClosing(object? sender, FormClosingEventArgs e)
    {
        DialogResult result = MessageBox.Show("Bạn có muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        e.Cancel = result == DialogResult.No;
    }

    private void AddLabel(string text, int x, int y) => Controls.Add(new Label { AutoSize = true, Text = text, Location = new Point(x, y) });
    private void ConfigureInput(TextBox box, int x, int y, int width) { box.Location = new Point(x, y); box.Size = new Size(width, 29); Controls.Add(box); }
    private void ConfigureOutput(TextBox box, int x, int y, int width) { ConfigureInput(box, x, y, width); box.ReadOnly = true; box.BackColor = Color.FromArgb(245, 248, 252); }
    private Button CreateButton(string text, int x, int y, int width) { Button button = new() { Text = text, Location = new Point(x, y), Size = new Size(width, 42) }; Controls.Add(button); return button; }
}
