// Form của Bài 2 đăng ký tài khoản: nhận thao tác người dùng, kiểm tra dữ liệu và hiển thị kết quả.
using System;
using System.Windows.Forms;
using Lab04.Core;

namespace Bai02_DangKyTaiKhoan;

public partial class FrmDangKyTaiKhoan : Form
{
    // Khởi tạo bố cục, thiết lập phím tắt và đăng ký các sự kiện của form.
    public FrmDangKyTaiKhoan()
    {
        InitializeComponent();
    }

    private bool ValidateInputs()
    {
        errorProvider.Clear();
        bool valid = true;

        if (string.IsNullOrWhiteSpace(txtUserName.Text))
        {
            errorProvider.SetError(txtUserName, "Tên đăng nhập là bắt buộc.");
            valid = false;
        }

        if (!Validation.IsValidEmail(txtEmail.Text))
        {
            errorProvider.SetError(txtEmail, "Email không đúng định dạng.");
            valid = false;
        }

        if (txtPassword.Text.Length < 6)
        {
            errorProvider.SetError(txtPassword, "Mật khẩu phải có ít nhất 6 ký tự.");
            valid = false;
        }

        if (txtPassword.Text != txtConfirm.Text)
        {
            errorProvider.SetError(txtConfirm, "Xác nhận mật khẩu không khớp.");
            valid = false;
        }

        return valid;
    }

    private void BtnRegister_Click(object? sender, EventArgs e)
    {
        bool controlsValid = ValidateInputs();
        bool registrationValid = AccountRegistration.Validate(
            txtUserName.Text, txtEmail.Text, txtPassword.Text, txtConfirm.Text, out string error);
        if (!controlsValid || !registrationValid)
        {
            MessageBox.Show(string.IsNullOrEmpty(error) ? "Vui lòng kiểm tra các ô có dấu (*)." : error,
                "Đăng ký chưa thành công", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Đề yêu cầu hiển thị thông tin của tất cả TextBox sau khi đăng ký.
        MessageBox.Show(
            $"Tên đăng nhập: {txtUserName.Text.Trim()}\n" +
            $"Địa chỉ email: {txtEmail.Text.Trim()}\n" +
            $"Mật khẩu: {txtPassword.Text}\n" +
            $"Xác nhận mật khẩu: {txtConfirm.Text}",
            "Thông tin đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void TxtEmail_Leave(object? sender, EventArgs e)
    {
        errorProvider.SetError(txtEmail,
            Validation.IsValidEmail(txtEmail.Text) ? string.Empty : "Email không đúng định dạng.");
    }

    private void RequiredTextBox_Leave(object? sender, EventArgs e)
    {
        if (sender is TextBox textBox)
            errorProvider.SetError(textBox,
                string.IsNullOrWhiteSpace(textBox.Text) ? "Không được để trống." : string.Empty);
    }

    private void FrmDangKyTaiKhoan_FormClosing(object? sender, FormClosingEventArgs e)
    {
        DialogResult choice = MessageBox.Show("Bạn có muốn đóng Form đăng ký?", "Xác nhận",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        e.Cancel = choice == DialogResult.No;
    }
}
