// Form của Bài 1 giải phương trình: nhận thao tác người dùng, kiểm tra dữ liệu và hiển thị kết quả.
using System;
using System.Drawing;
using System.Windows.Forms;
using Lab04D.Core;

namespace Bai01_GiaiPhuongTrinh;

/// <summary>Giải phương trình bậc nhất hoặc bậc hai tùy RadioButton.</summary>
public sealed class FrmGiaiPhuongTrinh : Form
{
    private readonly RadioButton _rdoLinear = new();
    private readonly RadioButton _rdoQuadratic = new();
    private readonly Label _lblC = new();
    private readonly TextBox _txtA = new();
    private readonly TextBox _txtB = new();
    private readonly TextBox _txtC = new();
    private readonly TextBox _txtResult = new();
    private readonly Button _btnSolve = new();
    private readonly ErrorProvider _errors = new();
    private bool _hasSolved;

    // Khởi tạo bố cục, thiết lập phím tắt và đăng ký các sự kiện của form.
    public FrmGiaiPhuongTrinh()
    {
        Text = "Giải phương trình bậc 1 và bậc 2";
        ClientSize = new Size(650, 520);
        Font = new Font("Segoe UI", 11F);
        BackColor = Color.White;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;

        Controls.Add(new Label { AutoSize = true, Text = "GIẢI PHƯƠNG TRÌNH", Font = new Font("Segoe UI", 21F, FontStyle.Bold), ForeColor = Color.Firebrick, Location = new Point(155, 25) });
        GroupBox modeGroup = new() { Text = "Bạn vui lòng chọn", Location = new Point(70, 91), Size = new Size(510, 112) };
        _rdoLinear.Text = "Phương trình bậc nhất"; _rdoLinear.AutoSize = true; _rdoLinear.Location = new Point(35, 33);
        _rdoQuadratic.Text = "Phương trình bậc hai"; _rdoQuadratic.AutoSize = true; _rdoQuadratic.Location = new Point(35, 70);
        _rdoLinear.CheckedChanged += ModeChanged; _rdoQuadratic.CheckedChanged += ModeChanged;
        modeGroup.Controls.AddRange(new Control[] { _rdoLinear, _rdoQuadratic }); Controls.Add(modeGroup);

        AddLabel("Nhập a:", 90, 237); ConfigureInput(_txtA, 205, 232);
        AddLabel("Nhập b:", 90, 284); ConfigureInput(_txtB, 205, 279);
        _lblC.Text = "Nhập c:"; _lblC.AutoSize = true; _lblC.Location = new Point(90, 331); Controls.Add(_lblC); ConfigureInput(_txtC, 205, 326);
        AddLabel("Kết quả:", 90, 384); _txtResult.Location = new Point(205, 375); _txtResult.Size = new Size(265, 70); _txtResult.Multiline = true; _txtResult.ReadOnly = true; _txtResult.BackColor = Color.FromArgb(245, 248, 252); Controls.Add(_txtResult);

        _btnSolve.Text = "&Giải"; _btnSolve.Location = new Point(495, 232); _btnSolve.Size = new Size(95, 58); _btnSolve.Enabled = false; _btnSolve.Click += Solve;
        Button btnExit = new() { Text = "T&hoát", Location = new Point(495, 308), Size = new Size(95, 58) }; btnExit.Click += (_, _) => Close();
        Controls.AddRange(new Control[] { _btnSolve, btnExit }); AcceptButton = _btnSolve; CancelButton = btnExit; _errors.ContainerControl = this;

        foreach (TextBox box in new[] { _txtA, _txtB, _txtC })
        {
            box.KeyPress += NumericKeyPress;
            box.TextChanged += InputChanged;
        }
        _rdoLinear.Checked = true;
        FormClosing += ConfirmClosing;
    }

    private void ModeChanged(object? sender, EventArgs e)
    {
        if (!_rdoLinear.Checked && !_rdoQuadratic.Checked) return;
        bool quadratic = _rdoQuadratic.Checked;
        _lblC.Enabled = quadratic; _txtC.Enabled = quadratic;
        if (!quadratic) { _txtC.Clear(); _errors.SetError(_txtC, string.Empty); }
        _txtResult.Clear(); _hasSolved = false; UpdateSolveState();
    }

    private void InputChanged(object? sender, EventArgs e)
    {
        if (sender is TextBox box && box.Enabled)
        {
            bool valid = box.Text.Length == 0 || NumericValidation.TryParseDouble(box.Text, out _);
            _errors.SetError(box, valid ? string.Empty : "Giá trị không phải là số hợp lệ.");
        }
        _txtResult.Clear(); _hasSolved = false; UpdateSolveState();
    }

    private void UpdateSolveState()
    {
        bool aValid = NumericValidation.TryParseDouble(_txtA.Text, out _);
        bool bValid = NumericValidation.TryParseDouble(_txtB.Text, out _);
        bool cValid = !_rdoQuadratic.Checked || NumericValidation.TryParseDouble(_txtC.Text, out _);
        _btnSolve.Enabled = !_hasSolved && aValid && bValid && cValid;
    }

    private void Solve(object? sender, EventArgs e)
    {
        _errors.Clear();
        if (!NumericValidation.TryParseDouble(_txtA.Text, out double a) || !NumericValidation.TryParseDouble(_txtB.Text, out double b))
        {
            MessageBox.Show("Vui lòng nhập hệ số a và b hợp lệ.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (_rdoQuadratic.Checked)
        {
            if (!NumericValidation.TryParseDouble(_txtC.Text, out double c))
            {
                _errors.SetError(_txtC, "Hệ số c không hợp lệ.");
                MessageBox.Show("Vui lòng nhập hệ số c hợp lệ.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _txtResult.Text = PhuongTrinh.SolveQuadratic(a, b, c);
        }
        else _txtResult.Text = PhuongTrinh.SolveLinear(a, b);

        _hasSolved = true;
        _btnSolve.Enabled = false;
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
    private void ConfigureInput(TextBox box, int x, int y) { box.Location = new Point(x, y); box.Size = new Size(265, 29); Controls.Add(box); }
}
