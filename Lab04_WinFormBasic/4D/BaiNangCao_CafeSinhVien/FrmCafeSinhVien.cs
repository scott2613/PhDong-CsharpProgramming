// Form của bài nâng cao Cafe Sinh Viên: nhận thao tác người dùng, kiểm tra dữ liệu và hiển thị kết quả.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Lab04D.Core;

namespace BaiNangCao_CafeSinhVien;

/// <summary>Quản lý tính tiền và thống kê theo nhóm khách tại Cafe Sinh Viên.</summary>
public sealed class FrmCafeSinhVien : Form
{
    private readonly TextBox _txtCustomer = new();
    private readonly TextBox _txtGuests = new();
    private readonly CheckBox _chkStudent = new();
    private readonly RadioButton[] _drinkRadios;
    private readonly CheckBox[] _foodChecks;
    private readonly Button _btnCalculate = new();
    private readonly Button _btnReset = new();
    private readonly Button _btnPayment = new();
    private readonly Label _lblTotalGuests = new();
    private readonly Label _lblRevenue = new();
    private readonly ErrorProvider _errors = new();
    private CafeOrder? _pendingOrder;
    private int _totalGuests;
    private decimal _totalRevenue;

    // Khởi tạo bố cục, thiết lập phím tắt và đăng ký các sự kiện của form.
    public FrmCafeSinhVien()
    {
        Text = "Thanh toán tiền - Cafe Sinh Viên";
        ClientSize = new Size(760, 590);
        Font = new Font("Segoe UI", 10.5F);
        BackColor = Color.White;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;

        Controls.Add(new Label { AutoSize = true, Text = "CAFE SINH VIÊN", Font = new Font("Segoe UI", 22F, FontStyle.Bold), ForeColor = Color.DarkOrange, Location = new Point(240, 23) });
        AddLabel("Tên khách hàng:", 68, 92);
        ConfigureTextBox(_txtCustomer, 225, 87, 455);
        AddLabel("Số khách hàng:", 68, 137);
        ConfigureTextBox(_txtGuests, 225, 132, 455);
        _txtGuests.KeyPress += WholeNumberKeyPress;

        _chkStudent.Text = "Sinh viên - giảm 20%";
        _chkStudent.AutoSize = true;
        _chkStudent.Location = new Point(225, 177);
        Controls.Add(_chkStudent);

        GroupBox drinkGroup = new() { Text = "Nước uống", Location = new Point(68, 220), Size = new Size(300, 205) };
        _drinkRadios = new[]
        {
            CreateDrink("Cafe đen", CafeDrink.CafeDen, 22, 34),
            CreateDrink("Cafe đá", CafeDrink.CafeDa, 165, 34),
            CreateDrink("Cafe sữa", CafeDrink.CafeSua, 22, 78),
            CreateDrink("Cafe kem", CafeDrink.CafeKem, 165, 78),
            CreateDrink("Cafe sữa đá", CafeDrink.CafeSuaDa, 22, 122)
        };
        drinkGroup.Controls.AddRange(_drinkRadios);

        GroupBox foodGroup = new() { Text = "Thức ăn", Location = new Point(388, 220), Size = new Size(330, 205) };
        _foodChecks = new[]
        {
            CreateFood("Bánh mỳ trứng", CafeFood.BanhMyTrung, 20, 34),
            CreateFood("Mỳ xào bò", CafeFood.MyXaoBo, 190, 34),
            CreateFood("Bánh mỳ cá", CafeFood.BanhMyCa, 20, 78),
            CreateFood("Mỳ cay", CafeFood.MyCay, 190, 78),
            CreateFood("Mỳ tôm trứng", CafeFood.MyTomTrung, 20, 122)
        };
        foodGroup.Controls.AddRange(_foodChecks);
        Controls.AddRange(new Control[] { drinkGroup, foodGroup });

        _btnCalculate.Text = "&Tính tiền"; ConfigureButton(_btnCalculate, 68, 447, 135, Calculate);
        _btnReset.Text = "&Nhập lại"; ConfigureButton(_btnReset, 222, 447, 135, (_, _) => ResetCurrent());
        _btnPayment.Text = "Thanh t&oán"; ConfigureButton(_btnPayment, 376, 447, 135, CommitPayment);
        Button btnExit = new() { Text = "T&hoát" }; ConfigureButton(btnExit, 545, 447, 135, (_, _) => Close());
        Controls.AddRange(new Control[] { _btnCalculate, _btnReset, _btnPayment, btnExit });

        AddLabel("Tổng khách hàng:", 68, 510);
        ConfigureSummary(_lblTotalGuests, 260, 505, 420);
        AddLabel("Tổng tiền thanh toán:", 68, 551);
        ConfigureSummary(_lblRevenue, 300, 546, 380);

        _txtCustomer.TextChanged += InputChanged;
        _txtGuests.TextChanged += InputChanged;
        _chkStudent.CheckedChanged += InputChanged;
        foreach (RadioButton radio in _drinkRadios) radio.CheckedChanged += InputChanged;
        foreach (CheckBox check in _foodChecks) check.CheckedChanged += InputChanged;

        _errors.ContainerControl = this;
        AcceptButton = _btnCalculate;
        CancelButton = btnExit;
        FormClosing += ConfirmClosing;
        ResetCurrent();
    }

    private void InputChanged(object? sender, EventArgs e)
    {
        bool complete = !string.IsNullOrWhiteSpace(_txtCustomer.Text)
            && int.TryParse(_txtGuests.Text, out int guests) && guests > 0
            && SelectedDrink() is not null
            && SelectedFoods().Count > 0;
        _btnCalculate.Enabled = complete && _pendingOrder is null;
    }

    private void Calculate(object? sender, EventArgs e)
    {
        _errors.Clear();
        if (!int.TryParse(_txtGuests.Text, out int guests) || guests <= 0)
        {
            ShowError(_txtGuests, "Số khách hàng phải là số nguyên dương.");
            return;
        }
        CafeDrink? drink = SelectedDrink();
        if (drink is null) { MessageBox.Show("Vui lòng chọn một loại nước uống.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

        var order = new CafeOrder
        {
            CustomerName = _txtCustomer.Text.Trim(),
            GuestCount = guests,
            IsStudent = _chkStudent.Checked,
            Drink = drink.Value,
            Foods = SelectedFoods()
        };
        if (!order.TryValidate(out string error)) { ShowError(_txtCustomer, error); return; }

        _pendingOrder = order;
        _btnCalculate.Enabled = false;
        _btnReset.Enabled = true;
        _btnPayment.Enabled = true;
        MessageBox.Show(
            $"Khách hàng: {order.CustomerName}\nSố khách: {order.GuestCount}\nTạm tính: {Money(order.Subtotal)}\nGiảm giá: {Money(order.Discount)}\nThành tiền: {Money(order.Total)}",
            "Kết quả tính tiền", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void CommitPayment(object? sender, EventArgs e)
    {
        if (_pendingOrder is null) return;
        _totalGuests += _pendingOrder.GuestCount;
        _totalRevenue += _pendingOrder.Total;
        _lblTotalGuests.Text = _totalGuests.ToString();
        _lblRevenue.Text = Money(_totalRevenue);
        ResetCurrent();
    }

    private void ResetCurrent()
    {
        _pendingOrder = null;
        _txtCustomer.Clear();
        _txtGuests.Clear();
        _chkStudent.Checked = false;
        foreach (RadioButton radio in _drinkRadios) radio.Checked = false;
        foreach (CheckBox check in _foodChecks) check.Checked = false;
        _btnCalculate.Enabled = false;
        _btnReset.Enabled = false;
        _btnPayment.Enabled = false;
        _errors.Clear();
        _txtCustomer.Focus();
    }

    private CafeDrink? SelectedDrink()
    {
        foreach (RadioButton radio in _drinkRadios)
            if (radio.Checked && radio.Tag is CafeDrink drink) return drink;
        return null;
    }

    private List<CafeFood> SelectedFoods()
    {
        var foods = new List<CafeFood>();
        foreach (CheckBox check in _foodChecks)
            if (check.Checked && check.Tag is CafeFood food) foods.Add(food);
        return foods;
    }

    private static string Money(decimal amount) => amount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " đ";
    private RadioButton CreateDrink(string text, CafeDrink drink, int x, int y) => new() { AutoSize = true, Text = text, Tag = drink, Location = new Point(x, y) };
    private CheckBox CreateFood(string text, CafeFood food, int x, int y) => new() { AutoSize = true, Text = text, Tag = food, Location = new Point(x, y) };
    private void AddLabel(string text, int x, int y) => Controls.Add(new Label { AutoSize = true, Text = text, Location = new Point(x, y) });
    private void ConfigureTextBox(TextBox box, int x, int y, int width) { box.Location = new Point(x, y); box.Size = new Size(width, 29); Controls.Add(box); }
    private static void ConfigureButton(Button button, int x, int y, int width, EventHandler click) { button.Location = new Point(x, y); button.Size = new Size(width, 42); button.Click += click; }
    private void ConfigureSummary(Label label, int x, int y, int width) { label.Text = "0"; label.AutoSize = false; label.TextAlign = ContentAlignment.MiddleLeft; label.BorderStyle = BorderStyle.FixedSingle; label.BackColor = Color.FromArgb(245, 248, 252); label.Location = new Point(x, y); label.Size = new Size(width, 32); Controls.Add(label); }
    private void ShowError(Control control, string message) { _errors.SetError(control, message); MessageBox.Show(message, "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    // Lọc ký tự ngay khi nhập để giảm lỗi trước bước kiểm tra chính.
    private static void WholeNumberKeyPress(object? sender, KeyPressEventArgs e) { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; }
    // Hỏi lại người dùng trước khi đóng để tránh mất dữ liệu đang nhập.
    private void ConfirmClosing(object? sender, FormClosingEventArgs e) { if (MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) e.Cancel = true; }
}
