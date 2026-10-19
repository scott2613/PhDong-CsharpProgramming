// Form bài tại lớp 1: nhập số, liệt kê ước số và tính các thống kê theo yêu cầu.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lab05C.Core;

namespace Bai01_UocSo;

public sealed class FrmUocSo : Form
{
    private readonly TextBox _input = new();
    private readonly ComboBox _numbers = new();
    private readonly ListBox _divisors = new();
    private readonly ErrorProvider _errors = new();

    public FrmUocSo()
    {
        Text = "Danh sách các ước số";
        ClientSize = new Size(820, 500);
        Font = new Font("Segoe UI", 10.5F);
        BackColor = Color.White;
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        Controls.Add(new Label { Text = "PHÂN TÍCH ƯỚC SỐ", AutoSize = true, Font = new Font("Segoe UI", 21F, FontStyle.Bold), ForeColor = Color.Firebrick, Location = new Point(245, 25) });
        AddLabel("Nhập số:", 65, 100); _input.Location = new Point(165, 95); _input.Size = new Size(230, 32); Controls.Add(_input);
        Button update = CreateButton("&Cập nhật", 415, 93, 130, AddNumber); Controls.Add(update);
        _numbers.Location = new Point(165, 150); _numbers.Size = new Size(380, 33); _numbers.DropDownStyle = ComboBoxStyle.DropDownList; _numbers.SelectedIndexChanged += NumberChanged; Controls.Add(_numbers);
        AddLabel("Số đã nhập:", 65, 155);

        _divisors.Location = new Point(585, 95); _divisors.Size = new Size(170, 235); Controls.Add(_divisors);
        Controls.Add(new Label { Text = "Danh sách các ước số", AutoSize = true, Location = new Point(580, 65), Font = new Font("Segoe UI", 10.5F, FontStyle.Bold) });
        // Chừa khoảng đệm ngang để nội dung dài không bị sát hoặc cắt chữ.
        Controls.Add(CreateButton("Tổng các ước số", 65, 230, 300, (_, _) => ShowStatistic("Tổng các ước số", values => DivisorAnalyzer.Sum(values))));
        Controls.Add(CreateButton("Số lượng ước số chẵn", 65, 285, 300, (_, _) => ShowStatistic("Số lượng ước số chẵn", values => DivisorAnalyzer.CountEven(values))));
        Controls.Add(CreateButton("Số lượng ước số nguyên tố", 65, 340, 300, (_, _) => ShowStatistic("Số lượng ước số nguyên tố", values => DivisorAnalyzer.CountPrime(values))));
        Controls.Add(CreateButton("T&hoát", 345, 395, 180, (_, _) => Close()));

        _input.KeyPress += PositiveIntegerKeyPress;
        AcceptButton = update;
        _errors.ContainerControl = this;
        FormClosing += ConfirmClosing;
    }

    private void AddNumber(object? sender, EventArgs e)
    {
        _errors.Clear();
        if (!DivisorAnalyzer.TryParsePositive(_input.Text, out int value, out string error)) { _errors.SetError(_input, error); ShowError(error); return; }
        if (_numbers.Items.Cast<int>().Contains(value)) { ShowError("Số vừa nhập đã tồn tại trong ComboBox."); _input.SelectAll(); return; }
        _numbers.Items.Add(value); _numbers.SelectedItem = value; _input.Clear(); _input.Focus();
    }

    private void NumberChanged(object? sender, EventArgs e)
    {
        _divisors.Items.Clear();
        if (_numbers.SelectedItem is int value)
            foreach (int divisor in DivisorAnalyzer.GetDivisors(value)) _divisors.Items.Add(divisor);
    }

    private void ShowStatistic(string caption, Func<IEnumerable<int>, int> calculation)
    {
        int[] values = _divisors.Items.Cast<int>().ToArray();
        if (values.Length == 0) { ShowError("Vui lòng chọn một số trong ComboBox."); return; }
        MessageBox.Show($"{caption}: {calculation(values)}", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void AddLabel(string text, int x, int y) => Controls.Add(new Label { Text = text, AutoSize = true, Location = new Point(x, y) });
    private static Button CreateButton(string text, int x, int y, int width, EventHandler click) { Button button = new() { Text = text, Location = new Point(x, y), Size = new Size(width, 42) }; button.Click += click; return button; }
    private static void PositiveIntegerKeyPress(object? sender, KeyPressEventArgs e) { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; }
    private static void ShowError(string text) => MessageBox.Show(text, "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    private static void ConfirmClosing(object? sender, FormClosingEventArgs e) { if (MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) e.Cancel = true; }
}
