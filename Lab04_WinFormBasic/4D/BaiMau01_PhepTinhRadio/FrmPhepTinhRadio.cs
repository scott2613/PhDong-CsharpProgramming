// Form của bài mẫu phép tính với RadioButton: nhận thao tác người dùng, kiểm tra dữ liệu và hiển thị kết quả.
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Lab04D.Core;

namespace BaiMau01_PhepTinhRadio;

/// <summary>Thực hiện bốn phép toán theo RadioButton được chọn.</summary>
public sealed class FrmPhepTinhRadio : Form
{
    private readonly TextBox _txtA = new();
    private readonly TextBox _txtB = new();
    private readonly TextBox _txtResult = new();
    private readonly RadioButton _rdoAdd = new();
    private readonly RadioButton _rdoSubtract = new();
    private readonly RadioButton _rdoMultiply = new();
    private readonly RadioButton _rdoDivide = new();
    private readonly ErrorProvider _errors = new();

    // Khởi tạo bố cục, thiết lập phím tắt và đăng ký các sự kiện của form.
    public FrmPhepTinhRadio()
    {
        Text = "Cộng trừ nhân chia bằng RadioButton";
        ClientSize = new Size(650, 370);
        Font = new Font("Segoe UI", 11F);
        BackColor = Color.White;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;

        Controls.Add(new Label { AutoSize = true, Text = "PHÉP TÍNH VỚI RADIOBUTTON", Font = new Font("Segoe UI", 17F, FontStyle.Bold), ForeColor = Color.Firebrick, Location = new Point(106, 25) });
        AddLabel("Số a:", 72, 101); ConfigureTextBox(_txtA, 140, 96, 160);
        AddLabel("Số b:", 342, 101); ConfigureTextBox(_txtB, 410, 96, 160);
        AddLabel("Kết quả:", 72, 153); ConfigureTextBox(_txtResult, 166, 148, 404); _txtResult.ReadOnly = true; _txtResult.BackColor = Color.FromArgb(245, 248, 252);

        GroupBox operations = new() { Text = "Chọn phép toán", Location = new Point(72, 205), Size = new Size(498, 72) };
        ConfigureRadio(_rdoAdd, "+", 40, PhepToan.Cong); ConfigureRadio(_rdoSubtract, "-", 150, PhepToan.Tru);
        ConfigureRadio(_rdoMultiply, "×", 260, PhepToan.Nhan); ConfigureRadio(_rdoDivide, "/", 370, PhepToan.Chia);
        operations.Controls.AddRange(new Control[] { _rdoAdd, _rdoSubtract, _rdoMultiply, _rdoDivide });
        Controls.Add(operations);

        Button btnCalculate = new() { Text = "&Tính", Location = new Point(205, 302), Size = new Size(110, 42) };
        Button btnExit = new() { Text = "T&hoát", Location = new Point(335, 302), Size = new Size(110, 42) };
        btnCalculate.Click += (_, _) => Calculate(true); btnExit.Click += (_, _) => Close();
        Controls.AddRange(new Control[] { btnCalculate, btnExit });
        AcceptButton = btnCalculate; CancelButton = btnExit; _errors.ContainerControl = this;

        _txtA.KeyPress += NumericKeyPress; _txtB.KeyPress += NumericKeyPress;
        _txtA.TextChanged += (_, _) => InputChanged(); _txtB.TextChanged += (_, _) => InputChanged();
        _rdoAdd.Checked = true;
        FormClosing += ConfirmClosing;
    }

    private void InputChanged()
    {
        _txtResult.Clear();
        ValidateTextBox(_txtA, "Số a không hợp lệ.");
        ValidateTextBox(_txtB, "Số b không hợp lệ.");
    }

    private void Calculate(bool showMessage)
    {
        _errors.Clear();
        bool aValid = NumericValidation.TryParseDouble(_txtA.Text, out double a);
        bool bValid = NumericValidation.TryParseDouble(_txtB.Text, out double b);
        if (!aValid) _errors.SetError(_txtA, "Số a không hợp lệ.");
        if (!bValid) _errors.SetError(_txtB, "Số b không hợp lệ.");
        if (!aValid || !bValid)
        {
            if (showMessage) MessageBox.Show("Vui lòng nhập đầy đủ hai số hợp lệ.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        PhepToan operation = GetSelectedOperation();
        var calculator = new TinhToan(a, b);
        if (!calculator.TryCalculate(operation, out double result, out string error))
        {
            _errors.SetError(_txtB, error);
            if (showMessage) MessageBox.Show(error, "Không thể tính", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        _txtResult.Text = result.ToString("0.##########", CultureInfo.InvariantCulture);
    }

    private PhepToan GetSelectedOperation() => _rdoSubtract.Checked ? PhepToan.Tru
        : _rdoMultiply.Checked ? PhepToan.Nhan : _rdoDivide.Checked ? PhepToan.Chia : PhepToan.Cong;

    private void ConfigureRadio(RadioButton radio, string text, int x, PhepToan operation)
    {
        radio.Text = text; radio.Tag = operation; radio.AutoSize = true; radio.Location = new Point(x, 30);
        radio.CheckedChanged += (_, _) => { if (radio.Checked && _txtA.Text.Length > 0 && _txtB.Text.Length > 0) Calculate(false); };
    }

    private void ValidateTextBox(TextBox box, string message)
    {
        bool valid = box.Text.Length == 0 || NumericValidation.TryParseDouble(box.Text, out _);
        _errors.SetError(box, valid ? string.Empty : message);
    }

    // Lọc ký tự ngay khi nhập để giảm lỗi trước bước kiểm tra chính.
    private static void NumericKeyPress(object? sender, KeyPressEventArgs e)
    {
        if (sender is not TextBox box) return;
        bool sign = e.KeyChar == '-' && box.SelectionStart == 0 && !box.Text.Contains('-');
        bool separator = (e.KeyChar == '.' || e.KeyChar == ',') && !box.Text.Contains('.') && !box.Text.Contains(',');
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !sign && !separator) e.Handled = true;
    }

    // Hỏi lại người dùng trước khi đóng để tránh mất dữ liệu đang nhập.
    private void ConfirmClosing(object? sender, FormClosingEventArgs e)
    {
        DialogResult result = MessageBox.Show("Bạn có muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        e.Cancel = result == DialogResult.No;
    }

    private void AddLabel(string text, int x, int y) => Controls.Add(new Label { AutoSize = true, Text = text, Location = new Point(x, y) });
    private void ConfigureTextBox(TextBox box, int x, int y, int width) { box.Location = new Point(x, y); box.Size = new Size(width, 29); Controls.Add(box); }
}
