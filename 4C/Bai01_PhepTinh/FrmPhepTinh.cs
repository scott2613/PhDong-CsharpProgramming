using System;
using System.Globalization;
using System.Windows.Forms;
using Lab04.Core;

namespace Bai01_PhepTinh;

public partial class FrmPhepTinh : Form
{
    public FrmPhepTinh()
    {
        InitializeComponent();
    }

    private void Calculate(ArithmeticOperation operation)
    {
        errorProvider.Clear();
        if (!Validation.TryParseNumber(txtA.Text, out double a))
        {
            errorProvider.SetError(txtA, "Số a không hợp lệ.");
            MessageBox.Show("Vui lòng nhập số a hợp lệ.", "Lỗi dữ liệu",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtA.Focus();
            return;
        }

        if (!Validation.TryParseNumber(txtB.Text, out double b))
        {
            errorProvider.SetError(txtB, "Số b không hợp lệ.");
            MessageBox.Show("Vui lòng nhập số b hợp lệ.", "Lỗi dữ liệu",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtB.Focus();
            return;
        }

        if (!Arithmetic.TryCalculate(a, b, operation, out double result, out string error))
        {
            errorProvider.SetError(txtB, error);
            MessageBox.Show(error, "Không thể thực hiện", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        txtResult.Text = result.ToString("0.##########", CultureInfo.CurrentCulture);
    }

    private void BtnAdd_Click(object? sender, EventArgs e) => Calculate(ArithmeticOperation.Add);
    private void BtnSubtract_Click(object? sender, EventArgs e) => Calculate(ArithmeticOperation.Subtract);
    private void BtnMultiply_Click(object? sender, EventArgs e) => Calculate(ArithmeticOperation.Multiply);
    private void BtnDivide_Click(object? sender, EventArgs e) => Calculate(ArithmeticOperation.Divide);

    private void NumberTextChanged(object? sender, EventArgs e)
    {
        if (sender is not TextBox textBox) return;
        bool valid = textBox.Text.Length == 0 || Validation.TryParseNumber(textBox.Text, out _);
        errorProvider.SetError(textBox, valid ? string.Empty : "Chỉ được nhập một giá trị số.");
        txtResult.Clear();
    }

    private void NumberKeyPress(object? sender, KeyPressEventArgs e)
    {
        if (sender is not TextBox textBox || char.IsControl(e.KeyChar)) return;
        string separators = ".,";
        bool isSign = e.KeyChar == '-' && textBox.SelectionStart == 0 && !textBox.Text.Contains('-');
        bool isSeparator = separators.Contains(e.KeyChar) &&
                           !textBox.Text.Contains('.') && !textBox.Text.Contains(',');
        if (!char.IsDigit(e.KeyChar) && !isSign && !isSeparator) e.Handled = true;
    }

    private void FrmPhepTinh_FormClosing(object? sender, FormClosingEventArgs e)
    {
        DialogResult choice = MessageBox.Show("Bạn có muốn đóng chương trình?", "Xác nhận",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        e.Cancel = choice == DialogResult.No;
    }
}
