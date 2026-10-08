// Tệp Designer khai báo, khởi tạo và bố trí các điều khiển trực quan của form.
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Bai02_DangKyTaiKhoan;

public partial class FrmDangKyTaiKhoan
{
    private IContainer components = null!;
    private Label lblTitle = null!;
    private Label lblUserName = null!;
    private Label lblEmail = null!;
    private Label lblPassword = null!;
    private Label lblConfirm = null!;
    private Label lblRequired = null!;
    private TextBox txtUserName = null!;
    private TextBox txtEmail = null!;
    private TextBox txtPassword = null!;
    private TextBox txtConfirm = null!;
    private Button btnRegister = null!;
    private ErrorProvider errorProvider = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new Container();
        lblTitle = new Label(); lblUserName = new Label(); lblEmail = new Label();
        lblPassword = new Label(); lblConfirm = new Label(); lblRequired = new Label();
        txtUserName = new TextBox(); txtEmail = new TextBox(); txtPassword = new TextBox(); txtConfirm = new TextBox();
        btnRegister = new Button(); errorProvider = new ErrorProvider(components);
        ((ISupportInitialize)errorProvider).BeginInit(); SuspendLayout();

        lblTitle.AutoSize = true; lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(0, 120, 215); lblTitle.Location = new Point(150, 24); lblTitle.Text = "ĐĂNG KÝ TÀI KHOẢN";
        lblRequired.AutoSize = true; lblRequired.ForeColor = Color.Firebrick; lblRequired.Location = new Point(360, 74); lblRequired.Text = "(*) bắt buộc";

        ConfigureLabel(lblUserName, "Tên đăng nhập (*):", 55, 111);
        ConfigureLabel(lblEmail, "Địa chỉ email (*):", 55, 160);
        ConfigureLabel(lblPassword, "Mật khẩu (*):", 55, 209);
        ConfigureLabel(lblConfirm, "Xác nhận mật khẩu (*):", 55, 258);

        ConfigureTextBox(txtUserName, 240, 107, 0); txtUserName.Leave += RequiredTextBox_Leave;
        ConfigureTextBox(txtEmail, 240, 156, 1); txtEmail.Leave += TxtEmail_Leave;
        ConfigureTextBox(txtPassword, 240, 205, 2); txtPassword.UseSystemPasswordChar = true; txtPassword.Leave += RequiredTextBox_Leave;
        ConfigureTextBox(txtConfirm, 240, 254, 3); txtConfirm.UseSystemPasswordChar = true; txtConfirm.Leave += RequiredTextBox_Leave;

        btnRegister.Location = new Point(240, 321); btnRegister.Size = new Size(190, 44);
        btnRegister.Text = "&Đăng ký"; btnRegister.TabIndex = 4; btnRegister.Click += BtnRegister_Click;

        errorProvider.ContainerControl = this; AcceptButton = btnRegister;
        AutoScaleDimensions = new SizeF(9F, 21F); AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White; ClientSize = new Size(620, 410);
        Controls.AddRange(new Control[] { lblTitle, lblRequired, lblUserName, txtUserName, lblEmail, txtEmail,
            lblPassword, txtPassword, lblConfirm, txtConfirm, btnRegister });
        Font = new Font("Segoe UI", 11F); FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false; StartPosition = FormStartPosition.CenterScreen; Text = "Bài 2 - Đăng ký tài khoản";
        FormClosing += FrmDangKyTaiKhoan_FormClosing;
        ((ISupportInitialize)errorProvider).EndInit(); ResumeLayout(false); PerformLayout();
    }

    private static void ConfigureLabel(Label label, string text, int x, int y)
    {
        label.AutoSize = true; label.Location = new Point(x, y); label.Text = text;
    }

    private static void ConfigureTextBox(TextBox textBox, int x, int y, int tabIndex)
    {
        textBox.Location = new Point(x, y); textBox.Size = new Size(300, 29); textBox.TabIndex = tabIndex;
    }
}
