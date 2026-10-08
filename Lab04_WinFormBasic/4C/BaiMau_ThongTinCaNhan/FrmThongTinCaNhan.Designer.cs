// Tệp Designer khai báo, khởi tạo và bố trí các điều khiển trực quan của form.
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BaiMau_ThongTinCaNhan;

public partial class FrmThongTinCaNhan
{
    private IContainer components = null!;
    private Label lblTitle = null!;
    private Label lblYourName = null!;
    private Label lblYear = null!;
    private TextBox txtYourName = null!;
    private TextBox txtYear = null!;
    private Button btnShow = null!;
    private Button btnClear = null!;
    private Button btnExit = null!;
    private ErrorProvider errorProvider = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new Container();
        lblTitle = new Label();
        lblYourName = new Label();
        lblYear = new Label();
        txtYourName = new TextBox();
        txtYear = new TextBox();
        btnShow = new Button();
        btnClear = new Button();
        btnExit = new Button();
        errorProvider = new ErrorProvider(components);
        ((ISupportInitialize)errorProvider).BeginInit();
        SuspendLayout();

        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(31, 78, 121);
        lblTitle.Location = new Point(142, 25);
        lblTitle.Text = "THÔNG TIN CÁ NHÂN";

        lblYourName.AutoSize = true;
        lblYourName.Location = new Point(55, 92);
        lblYourName.Text = "Họ và tên:";
        txtYourName.Location = new Point(170, 88);
        txtYourName.Size = new Size(280, 29);
        txtYourName.TabIndex = 0;
        txtYourName.Leave += TxtYourName_Leave;

        lblYear.AutoSize = true;
        lblYear.Location = new Point(55, 141);
        lblYear.Text = "Năm sinh:";
        txtYear.Location = new Point(170, 137);
        txtYear.Size = new Size(280, 29);
        txtYear.TabIndex = 1;
        txtYear.TextChanged += TxtYear_TextChanged;
        txtYear.KeyPress += NumericTextBox_KeyPress;

        btnShow.Location = new Point(78, 205);
        btnShow.Size = new Size(110, 38);
        btnShow.TabIndex = 2;
        btnShow.Text = "&Hiển thị";
        btnShow.Click += BtnShow_Click;
        btnClear.Location = new Point(207, 205);
        btnClear.Size = new Size(110, 38);
        btnClear.TabIndex = 3;
        btnClear.Text = "&Xóa";
        btnClear.Click += BtnClear_Click;
        btnExit.Location = new Point(336, 205);
        btnExit.Size = new Size(110, 38);
        btnExit.TabIndex = 4;
        btnExit.Text = "T&hoát";
        btnExit.Click += BtnExit_Click;

        errorProvider.ContainerControl = this;
        AcceptButton = btnShow;
        CancelButton = btnExit;
        AutoScaleDimensions = new SizeF(9F, 21F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(520, 280);
        Controls.AddRange(new Control[] { lblTitle, lblYourName, txtYourName, lblYear, txtYear, btnShow, btnClear, btnExit });
        Font = new Font("Segoe UI", 11F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Bài mẫu - Thông tin cá nhân";
        FormClosing += FrmThongTinCaNhan_FormClosing;
        ((ISupportInitialize)errorProvider).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
