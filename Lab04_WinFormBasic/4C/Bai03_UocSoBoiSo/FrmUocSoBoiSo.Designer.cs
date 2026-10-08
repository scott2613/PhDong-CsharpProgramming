// Tệp Designer khai báo, khởi tạo và bố trí các điều khiển trực quan của form.
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Bai03_UocSoBoiSo;

public partial class FrmUocSoBoiSo
{
    private IContainer components = null!;
    private Label lblTitle = null!;
    private Label lblA = null!;
    private Label lblB = null!;
    private Label lblGcd = null!;
    private Label lblLcm = null!;
    private TextBox txtA = null!;
    private TextBox txtB = null!;
    private TextBox txtGcd = null!;
    private TextBox txtLcm = null!;
    private Button btnCalculate = null!;
    private Button btnContinue = null!;
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
        lblTitle = new Label(); lblA = new Label(); lblB = new Label(); lblGcd = new Label(); lblLcm = new Label();
        txtA = new TextBox(); txtB = new TextBox(); txtGcd = new TextBox(); txtLcm = new TextBox();
        btnCalculate = new Button(); btnContinue = new Button(); btnExit = new Button(); errorProvider = new ErrorProvider(components);
        ((ISupportInitialize)errorProvider).BeginInit(); SuspendLayout();

        lblTitle.AutoSize = true; lblTitle.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(192, 0, 0); lblTitle.Location = new Point(96, 23); lblTitle.Text = "ƯỚC SỐ CHUNG - BỘI SỐ CHUNG";
        ConfigureLabel(lblA, "Nhập số a:", 90, 91); ConfigureTextBox(txtA, 285, 87, false, 0);
        ConfigureLabel(lblB, "Nhập số b:", 90, 137); ConfigureTextBox(txtB, 285, 133, false, 1);
        ConfigureLabel(lblGcd, "Ước số chung lớn nhất:", 90, 183); ConfigureTextBox(txtGcd, 285, 179, true, 2);
        ConfigureLabel(lblLcm, "Bội số chung nhỏ nhất:", 90, 229); ConfigureTextBox(txtLcm, 285, 225, true, 3);
        txtA.KeyPress += PositiveIntegerKeyPress; txtB.KeyPress += PositiveIntegerKeyPress;
        txtA.TextChanged += NumberTextChanged; txtB.TextChanged += NumberTextChanged;

        btnCalculate.Location = new Point(88, 296); btnCalculate.Size = new Size(125, 42); btnCalculate.Text = "&Thực hiện"; btnCalculate.TabIndex = 4; btnCalculate.Click += BtnCalculate_Click;
        btnContinue.Location = new Point(238, 296); btnContinue.Size = new Size(125, 42); btnContinue.Text = "&Tiếp tục"; btnContinue.TabIndex = 5; btnContinue.Click += BtnContinue_Click;
        btnExit.Location = new Point(388, 296); btnExit.Size = new Size(125, 42); btnExit.Text = "T&hoát"; btnExit.TabIndex = 6; btnExit.Click += BtnExit_Click;

        errorProvider.ContainerControl = this; AcceptButton = btnCalculate; CancelButton = btnExit;
        AutoScaleDimensions = new SizeF(9F, 21F); AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White; ClientSize = new Size(600, 385);
        Controls.AddRange(new Control[] { lblTitle, lblA, txtA, lblB, txtB, lblGcd, txtGcd, lblLcm, txtLcm, btnCalculate, btnContinue, btnExit });
        Font = new Font("Segoe UI", 11F); FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen; Text = "Bài 3 - UCLN và BCNN"; FormClosing += FrmUocSoBoiSo_FormClosing;
        ((ISupportInitialize)errorProvider).EndInit(); ResumeLayout(false); PerformLayout();
    }

    private static void ConfigureLabel(Label label, string text, int x, int y)
    {
        label.AutoSize = true; label.Location = new Point(x, y); label.Text = text;
    }

    private static void ConfigureTextBox(TextBox textBox, int x, int y, bool readOnly, int tabIndex)
    {
        textBox.Location = new Point(x, y); textBox.Size = new Size(225, 29);
        textBox.ReadOnly = readOnly; textBox.TabIndex = tabIndex; textBox.TabStop = !readOnly;
    }
}
