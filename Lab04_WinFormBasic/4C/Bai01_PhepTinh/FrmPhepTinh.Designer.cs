// Tệp Designer khai báo, khởi tạo và bố trí các điều khiển trực quan của form.
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Bai01_PhepTinh;

public partial class FrmPhepTinh
{
    private IContainer components = null!;
    private Label lblTitle = null!;
    private Label lblA = null!;
    private Label lblB = null!;
    private Label lblResult = null!;
    private TextBox txtA = null!;
    private TextBox txtB = null!;
    private TextBox txtResult = null!;
    private Button btnAdd = null!;
    private Button btnSubtract = null!;
    private Button btnMultiply = null!;
    private Button btnDivide = null!;
    private ErrorProvider errorProvider = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new Container();
        lblTitle = new Label(); lblA = new Label(); lblB = new Label(); lblResult = new Label();
        txtA = new TextBox(); txtB = new TextBox(); txtResult = new TextBox();
        btnAdd = new Button(); btnSubtract = new Button(); btnMultiply = new Button(); btnDivide = new Button();
        errorProvider = new ErrorProvider(components);
        ((ISupportInitialize)errorProvider).BeginInit();
        SuspendLayout();

        lblTitle.AutoSize = true; lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(192, 0, 0); lblTitle.Location = new Point(130, 22); lblTitle.Text = "CỘNG TRỪ NHÂN CHIA";
        lblA.AutoSize = true; lblA.Location = new Point(55, 88); lblA.Text = "Số a:";
        txtA.Location = new Point(115, 84); txtA.Size = new Size(135, 29); txtA.TabIndex = 0;
        txtA.TextChanged += NumberTextChanged; txtA.KeyPress += NumberKeyPress;
        lblB.AutoSize = true; lblB.Location = new Point(285, 88); lblB.Text = "Số b:";
        txtB.Location = new Point(345, 84); txtB.Size = new Size(135, 29); txtB.TabIndex = 1;
        txtB.TextChanged += NumberTextChanged; txtB.KeyPress += NumberKeyPress;
        lblResult.AutoSize = true; lblResult.Location = new Point(55, 139); lblResult.Text = "Kết quả:";
        txtResult.Location = new Point(145, 135); txtResult.ReadOnly = true; txtResult.Size = new Size(335, 29); txtResult.TabStop = false;

        btnAdd.Location = new Point(64, 201); btnAdd.Size = new Size(92, 42); btnAdd.Text = "+"; btnAdd.TabIndex = 2; btnAdd.Click += BtnAdd_Click;
        btnSubtract.Location = new Point(174, 201); btnSubtract.Size = new Size(92, 42); btnSubtract.Text = "−"; btnSubtract.TabIndex = 3; btnSubtract.Click += BtnSubtract_Click;
        btnMultiply.Location = new Point(284, 201); btnMultiply.Size = new Size(92, 42); btnMultiply.Text = "×"; btnMultiply.TabIndex = 4; btnMultiply.Click += BtnMultiply_Click;
        btnDivide.Location = new Point(394, 201); btnDivide.Size = new Size(92, 42); btnDivide.Text = "÷"; btnDivide.TabIndex = 5; btnDivide.Click += BtnDivide_Click;

        errorProvider.ContainerControl = this;
        AutoScaleDimensions = new SizeF(9F, 21F); AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White; ClientSize = new Size(550, 285);
        Controls.AddRange(new Control[] { lblTitle, lblA, txtA, lblB, txtB, lblResult, txtResult, btnAdd, btnSubtract, btnMultiply, btnDivide });
        Font = new Font("Segoe UI", 11F); FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen; Text = "Bài 1 - Các phép tính";
        FormClosing += FrmPhepTinh_FormClosing;
        ((ISupportInitialize)errorProvider).EndInit();
        ResumeLayout(false); PerformLayout();
    }
}
