using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Lab04D.Core;

namespace BaiVeNha_KhachSanThanhThanh;

/// <summary>Tính tiền trả phòng và tổng kết doanh thu trong ngày.</summary>
public sealed class FrmKhachSanThanhThanh : Form
{
    private readonly TextBox _txtCustomer = new();
    private readonly TextBox _txtAddress = new();
    private readonly TextBox _txtDays = new();
    private readonly RadioButton _rdoSingle = new();
    private readonly RadioButton _rdoDouble = new();
    private readonly RadioButton _rdoTriple = new();
    private readonly CheckBox _chkTelevision = new();
    private readonly CheckBox _chkInternet = new();
    private readonly CheckBox _chkHotWater = new();
    private readonly CheckBox _chkKaraoke = new();
    private readonly CheckBox _chkBreakfast = new();
    private readonly Button _btnPayment = new();
    private readonly Button _btnNew = new();
    private readonly Button _btnSummary = new();
    private readonly Label _lblAmount = new();
    private readonly Label _lblGuestSummary = new();
    private readonly Label _lblMoneySummary = new();
    private readonly ErrorProvider _errors = new();
    private int _dailyGuests;
    private decimal _dailyRevenue;
    private bool _paidCurrentGuest;

    public FrmKhachSanThanhThanh()
    {
        Text = "Khách sạn Thanh Thanh - Trả phòng";
        ClientSize = new Size(900, 560);
        Font = new Font("Segoe UI", 10.5F);
        BackColor = Color.White;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;

        Controls.Add(new Label { AutoSize = true, Text = "KHÁCH SẠN THANH THANH - TRẢ PHÒNG", Font = new Font("Segoe UI", 17F, FontStyle.Bold), ForeColor = Color.DarkOrange, Location = new Point(155, 24) });
        Panel left = new() { Location = new Point(25, 78), Size = new Size(535, 435), BorderStyle = BorderStyle.FixedSingle };
        Panel right = new() { Location = new Point(575, 78), Size = new Size(300, 435), BorderStyle = BorderStyle.FixedSingle };
        Controls.AddRange(new Control[] { left, right });

        AddLabel("Họ và tên:", 25, 28, left);
        ConfigureTextBox(_txtCustomer, 130, 23, 350, left);
        AddLabel("Địa chỉ:", 25, 73, left);
        ConfigureTextBox(_txtAddress, 130, 68, 350, left);
        AddLabel("Số ngày ở:", 25, 118, left);
        ConfigureTextBox(_txtDays, 130, 113, 125, left);
        _txtDays.KeyPress += WholeNumberKeyPress;

        GroupBox roomGroup = new() { Text = "Loại phòng", Location = new Point(25, 165), Size = new Size(145, 215) };
        ConfigureRadio(_rdoSingle, "Phòng đơn", 16, 35, true);
        ConfigureRadio(_rdoDouble, "Phòng đôi", 16, 83);
        ConfigureRadio(_rdoTriple, "Phòng ba", 16, 131);
        roomGroup.Controls.AddRange(new Control[] { _rdoSingle, _rdoDouble, _rdoTriple });

        GroupBox amenityGroup = new() { Text = "Tiện nghi", Location = new Point(180, 165), Size = new Size(200, 215) };
        ConfigureCheck(_chkTelevision, "Ti vi", 16, 35);
        ConfigureCheck(_chkInternet, "Internet", 16, 83);
        ConfigureCheck(_chkHotWater, "Máy nước nóng", 16, 131);
        amenityGroup.Controls.AddRange(new Control[] { _chkTelevision, _chkInternet, _chkHotWater });

        GroupBox serviceGroup = new() { Text = "Dịch vụ", Location = new Point(390, 165), Size = new Size(120, 215) };
        ConfigureCheck(_chkKaraoke, "Karaoke", 16, 50);
        ConfigureCheck(_chkBreakfast, "Ăn sáng", 16, 108);
        serviceGroup.Controls.AddRange(new Control[] { _chkKaraoke, _chkBreakfast });
        left.Controls.AddRange(new Control[] { roomGroup, amenityGroup, serviceGroup });

        _btnPayment.Text = "&Thanh toán"; ConfigureButton(_btnPayment, 12, 20, 130, Payment, right);
        _btnNew.Text = "&Nhập mới"; ConfigureButton(_btnNew, 154, 20, 130, (_, _) => ResetCurrentGuest(), right);
        AddLabel("Thành tiền:", 18, 86, right);
        ConfigureResultLabel(_lblAmount, 18, 115, 260, right);
        _btnSummary.Text = "Tổng &kết"; ConfigureButton(_btnSummary, 18, 170, 120, ShowSummary, right);
        AddLabel("Thông tin tổng kết", 18, 228, right, true);
        AddLabel("Số lượt người:", 18, 267, right);
        ConfigureResultLabel(_lblGuestSummary, 160, 260, 118, right);
        AddLabel("Tổng số tiền:", 18, 311, right);
        ConfigureResultLabel(_lblMoneySummary, 160, 304, 118, right);
        Button btnExit = new() { Text = "T&hoát" }; ConfigureButton(btnExit, 18, 370, 120, (_, _) => Close(), right);

        foreach (Control control in new Control[] { _txtCustomer, _txtAddress, _txtDays, _rdoSingle, _rdoDouble, _rdoTriple })
        {
            if (control is TextBox box) box.TextChanged += InputChanged;
            if (control is RadioButton radio) radio.CheckedChanged += InputChanged;
        }

        _errors.ContainerControl = this;
        AcceptButton = _btnPayment;
        CancelButton = btnExit;
        FormClosing += ConfirmClosing;
        ResetCurrentGuest();
    }

    private void InputChanged(object? sender, EventArgs e)
    {
        _btnPayment.Enabled = !_paidCurrentGuest
            && !string.IsNullOrWhiteSpace(_txtCustomer.Text)
            && !string.IsNullOrWhiteSpace(_txtAddress.Text)
            && int.TryParse(_txtDays.Text, out int days) && days > 0;
    }

    private void Payment(object? sender, EventArgs e)
    {
        _errors.Clear();
        if (!int.TryParse(_txtDays.Text, out int days) || days <= 0)
        {
            ShowError(_txtDays, "Số ngày ở phải là số nguyên dương.");
            return;
        }
        var bill = new HotelBill
        {
            CustomerName = _txtCustomer.Text.Trim(),
            Address = _txtAddress.Text.Trim(),
            Days = days,
            RoomType = SelectedRoom(),
            Television = _chkTelevision.Checked,
            Internet = _chkInternet.Checked,
            HotWater = _chkHotWater.Checked,
            Karaoke = _chkKaraoke.Checked,
            Breakfast = _chkBreakfast.Checked
        };
        if (!bill.TryValidate(out string error)) { ShowError(_txtCustomer, error); return; }

        _lblAmount.Text = Money(bill.Total);
        _dailyGuests++;
        _dailyRevenue += bill.Total;
        _paidCurrentGuest = true;
        _btnPayment.Enabled = false;
        _btnNew.Enabled = true;
        _btnSummary.Enabled = true;
    }

    private void ShowSummary(object? sender, EventArgs e)
    {
        _lblGuestSummary.Text = _dailyGuests.ToString();
        _lblMoneySummary.Text = Money(_dailyRevenue);
        _dailyGuests = 0;
        _dailyRevenue = 0;
        _btnSummary.Enabled = false;
    }

    private void ResetCurrentGuest()
    {
        _paidCurrentGuest = false;
        _txtCustomer.Clear();
        _txtAddress.Clear();
        _txtDays.Clear();
        _rdoSingle.Checked = true;
        _chkTelevision.Checked = _chkInternet.Checked = _chkHotWater.Checked = false;
        _chkKaraoke.Checked = _chkBreakfast.Checked = false;
        _lblAmount.Text = "0 đ";
        _btnPayment.Enabled = false;
        _btnNew.Enabled = false;
        _errors.Clear();
        _txtCustomer.Focus();
    }

    private HotelRoomType SelectedRoom() => _rdoDouble.Checked ? HotelRoomType.Double : _rdoTriple.Checked ? HotelRoomType.Triple : HotelRoomType.Single;
    private static string Money(decimal amount) => amount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " đ";
    private static void ConfigureRadio(RadioButton radio, string text, int x, int y, bool selected = false) { radio.Text = text; radio.AutoSize = true; radio.Location = new Point(x, y); radio.Checked = selected; }
    private static void ConfigureCheck(CheckBox check, string text, int x, int y) { check.Text = text; check.AutoSize = true; check.Location = new Point(x, y); }
    private static void ConfigureButton(Button button, int x, int y, int width, EventHandler click, Control parent) { button.Location = new Point(x, y); button.Size = new Size(width, 40); button.Click += click; parent.Controls.Add(button); }
    private static void AddLabel(string text, int x, int y, Control parent, bool bold = false) => parent.Controls.Add(new Label { AutoSize = true, Text = text, Font = new Font("Segoe UI", 10.5F, bold ? FontStyle.Bold : FontStyle.Regular), Location = new Point(x, y) });
    private static void ConfigureTextBox(TextBox box, int x, int y, int width, Control parent) { box.Location = new Point(x, y); box.Size = new Size(width, 29); parent.Controls.Add(box); }
    private static void ConfigureResultLabel(Label label, int x, int y, int width, Control parent) { label.Text = "0"; label.TextAlign = ContentAlignment.MiddleLeft; label.BorderStyle = BorderStyle.FixedSingle; label.BackColor = Color.FromArgb(245, 248, 252); label.Location = new Point(x, y); label.Size = new Size(width, 32); parent.Controls.Add(label); }
    private void ShowError(Control control, string message) { _errors.SetError(control, message); MessageBox.Show(message, "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    private static void WholeNumberKeyPress(object? sender, KeyPressEventArgs e) { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; }
    private void ConfirmClosing(object? sender, FormClosingEventArgs e) { if (MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) e.Cancel = true; }
}
