// Form của bài nâng cao bán vé rạp phim: nhận thao tác người dùng, kiểm tra dữ liệu và hiển thị kết quả.
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Lab04.Core;

namespace BaiNangCao_BanVeRapPhim;

/// <summary>Quản lý chọn và bán vé cho 15 ghế, chia thành ba lô giá.</summary>
public sealed class FrmBanVeRapPhim : Form
{
    private readonly CinemaTicketOffice _office = new();
    private readonly Button[] _seatButtons = new Button[15];
    private readonly Label _lblTotal = new();

    // Khởi tạo bố cục, thiết lập phím tắt và đăng ký các sự kiện của form.
    public FrmBanVeRapPhim()
    {
        Text = "Bán vé rạp chiếu phim";
        ClientSize = new Size(610, 590);
        Font = new Font("Segoe UI", 10.5F);
        BackColor = Color.FromArgb(246, 248, 251);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;

        Label screen = new() { Text = "MÀN ẢNH", TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 16F, FontStyle.Bold), ForeColor = Color.DarkOrange, BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Location = new Point(45, 28), Size = new Size(520, 54) };
        Controls.Add(screen);

        string[] blocks = { "LÔ A - 1.000 đồng", "LÔ B - 1.500 đồng", "LÔ C - 2.000 đồng" };
        for (int row = 0; row < 3; row++)
        {
            Controls.Add(new Label { Text = blocks[row], AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(31, 78, 121), Location = new Point(45, 105 + row * 112) });
            for (int column = 0; column < 5; column++)
            {
                int seat = row * 5 + column + 1;
                Button button = new() { Text = seat.ToString(), Tag = seat, Location = new Point(45 + column * 106, 132 + row * 112), Size = new Size(80, 54), BackColor = Color.White, FlatStyle = FlatStyle.Flat };
                button.Click += ToggleSeat;
                _seatButtons[seat - 1] = button;
                Controls.Add(button);
            }
        }

        Controls.Add(new Label { AutoSize = true, Text = "Thành tiền:", Font = new Font("Segoe UI", 11F, FontStyle.Bold), Location = new Point(84, 470) });
        _lblTotal.Text = "0 đồng"; _lblTotal.TextAlign = ContentAlignment.MiddleRight; _lblTotal.BorderStyle = BorderStyle.FixedSingle; _lblTotal.BackColor = Color.White; _lblTotal.Location = new Point(205, 460); _lblTotal.Size = new Size(320, 40); Controls.Add(_lblTotal);
        Button btnConfirm = CreateButton("&Chọn", 84); btnConfirm.Click += ConfirmSelection;
        Button btnCancel = CreateButton("&Hủy bỏ", 241); btnCancel.Click += CancelSelection;
        Button btnExit = CreateButton("&Kết thúc", 398); btnExit.Click += (_, _) => Close();
        CancelButton = btnExit;
        FormClosing += ConfirmClosing;
    }

    private void ToggleSeat(object? sender, EventArgs e)
    {
        if (sender is not Button button || button.Tag is not int seat) return;
        if (!_office.ToggleSelection(seat, out string message))
        {
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        RefreshSeat(seat);
    }

    private void ConfirmSelection(object? sender, EventArgs e)
    {
        int total = _office.ConfirmSelection();
        _lblTotal.Text = total.ToString("N0", new CultureInfo("vi-VN")) + " đồng";
        RefreshAllSeats();
    }

    private void CancelSelection(object? sender, EventArgs e)
    {
        _office.CancelSelection();
        _lblTotal.Text = "0 đồng";
        RefreshAllSeats();
    }

    private void RefreshAllSeats() { for (int seat = 1; seat <= 15; seat++) RefreshSeat(seat); }
    private void RefreshSeat(int seat)
    {
        _seatButtons[seat - 1].BackColor = _office[seat] switch
        {
            SeatState.Available => Color.White,
            SeatState.Selected => Color.FromArgb(74, 144, 226),
            SeatState.Sold => Color.Gold,
            _ => Color.White
        };
    }

    // Hỏi lại người dùng trước khi đóng để tránh mất dữ liệu đang nhập.
    private void ConfirmClosing(object? sender, FormClosingEventArgs e)
    {
        DialogResult result = MessageBox.Show("Bạn có muốn kết thúc chương trình?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        e.Cancel = result == DialogResult.No;
    }

    private Button CreateButton(string text, int x) { Button button = new() { Text = text, Location = new Point(x, 520), Size = new Size(128, 42) }; Controls.Add(button); return button; }
}
