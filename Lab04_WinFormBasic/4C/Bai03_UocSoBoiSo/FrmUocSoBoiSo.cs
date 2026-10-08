// Form của Bài 3 ước số và bội số: nhận thao tác người dùng, kiểm tra dữ liệu và hiển thị kết quả.
using System;
using System.Windows.Forms;
using Lab04.Core;

namespace Bai03_UocSoBoiSo;

public partial class FrmUocSoBoiSo : Form
{
    // Khởi tạo bố cục, thiết lập phím tắt và đăng ký các sự kiện của form.
    public FrmUocSoBoiSo()
    {
        InitializeComponent();
    }

    private void BtnCalculate_Click(object? sender, EventArgs e)
    {
        errorProvider.Clear();
        bool firstValid = Validation.TryParsePositiveInteger(txtA.Text, out int a);
        bool secondValid = Validation.TryParsePositiveInteger(txtB.Text, out int b);

        if (!firstValid) errorProvider.SetError(txtA, "a phải là số nguyên dương.");
        if (!secondValid) errorProvider.SetError(txtB, "b phải là số nguyên dương.");
        if (!firstValid || !secondValid)
        {
            MessageBox.Show("Vui lòng nhập hai số nguyên dương.", "Lỗi dữ liệu",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        txtGcd.Text = NumberTheory.GreatestCommonDivisor(a, b).ToString();
        txtLcm.Text = NumberTheory.LeastCommonMultiple(a, b).ToString();
    }

    private void BtnContinue_Click(object? sender, EventArgs e)
    {
        txtA.Clear(); txtB.Clear(); txtGcd.Clear(); txtLcm.Clear();
        errorProvider.Clear(); txtA.Focus();
    }

    private void BtnExit_Click(object? sender, EventArgs e) => Close();

    private void PositiveIntegerKeyPress(object? sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
    }

    private void NumberTextChanged(object? sender, EventArgs e)
    {
        if (sender is not TextBox textBox) return;
        bool valid = textBox.Text.Length == 0 || Validation.TryParsePositiveInteger(textBox.Text, out _);
        errorProvider.SetError(textBox, valid ? string.Empty : "Chỉ nhập số nguyên dương.");
        txtGcd.Clear(); txtLcm.Clear();
    }

    private void FrmUocSoBoiSo_FormClosing(object? sender, FormClosingEventArgs e)
    {
        DialogResult choice = MessageBox.Show("Bạn có muốn thoát?", "Xác nhận",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        e.Cancel = choice == DialogResult.No;
    }
}
