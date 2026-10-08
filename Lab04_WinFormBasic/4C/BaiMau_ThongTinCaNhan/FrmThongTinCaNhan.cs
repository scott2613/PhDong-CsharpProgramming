// Form của bài mẫu thông tin cá nhân: nhận thao tác người dùng, kiểm tra dữ liệu và hiển thị kết quả.
using System;
using System.Windows.Forms;
using Lab04.Core;

namespace BaiMau_ThongTinCaNhan;

public partial class FrmThongTinCaNhan : Form
{
    // Khởi tạo bố cục, thiết lập phím tắt và đăng ký các sự kiện của form.
    public FrmThongTinCaNhan()
    {
        InitializeComponent();
    }

    private void BtnShow_Click(object? sender, EventArgs e)
    {
        errorProvider.Clear();
        bool valid = true;

        if (string.IsNullOrWhiteSpace(txtYourName.Text))
        {
            errorProvider.SetError(txtYourName, "Bạn phải nhập họ tên.");
            valid = false;
        }

        if (!PersonInfo.TryCalculateAge(txtYear.Text, DateTime.Now.Year, out int age))
        {
            errorProvider.SetError(txtYear, $"Năm sinh phải từ 1900 đến {DateTime.Now.Year}.");
            valid = false;
        }

        if (!valid)
        {
            MessageBox.Show("Vui lòng kiểm tra dữ liệu đã nhập.", "Dữ liệu không hợp lệ",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        MessageBox.Show($"Họ tên: {txtYourName.Text.Trim()}\nTuổi: {age}", "Thông tin",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void BtnClear_Click(object? sender, EventArgs e)
    {
        txtYourName.Clear();
        txtYear.Clear();
        errorProvider.Clear();
        txtYourName.Focus();
    }

    private void BtnExit_Click(object? sender, EventArgs e) => Close();

    private void TxtYourName_Leave(object? sender, EventArgs e)
    {
        errorProvider.SetError(txtYourName,
            string.IsNullOrWhiteSpace(txtYourName.Text) ? "Bạn phải nhập họ tên." : string.Empty);
    }

    private void TxtYear_TextChanged(object? sender, EventArgs e)
    {
        bool valid = txtYear.Text.Length == 0 || int.TryParse(txtYear.Text, out _);
        errorProvider.SetError(txtYear, valid ? string.Empty : "Năm sinh phải là số nguyên.");
    }

    private void NumericTextBox_KeyPress(object? sender, KeyPressEventArgs e)
    {
        // Cho phép phím điều khiển như Backspace; các ký tự còn lại bắt buộc là chữ số.
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
    }

    private void FrmThongTinCaNhan_FormClosing(object? sender, FormClosingEventArgs e)
    {
        DialogResult choice = MessageBox.Show("Bạn có muốn thoát?", "Xác nhận thoát",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        e.Cancel = choice == DialogResult.No;
    }
}
