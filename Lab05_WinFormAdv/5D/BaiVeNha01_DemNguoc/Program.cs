using System;
using System.Drawing;
using System.Windows.Forms;
using Lab05D.Core;
using Lab05D.WinForms;

namespace BaiVeNha01_DemNguoc;

internal static class Program
{
    [STAThread] private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmDemNguoc());
    }
}

public sealed class FrmDemNguoc : Form
{
    private readonly NumericUpDown _minutes = new() { Minimum = 0, Maximum = 999, Value = 30, Width = 90, Margin = new Padding(6) };
    private readonly NumericUpDown _seconds = new() { Minimum = 0, Maximum = 59, Width = 90, Margin = new Padding(6) };
    private readonly Label _display = new() { Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Consolas", 46F, FontStyle.Bold), ForeColor = Ui.Primary };
    private readonly CountdownModel _model = new();
    private readonly Timer _timer = new() { Interval = 1000 };

    public FrmDemNguoc()
    {
        Ui.Configure(this, "Bài về nhà 1 - Đếm ngược", 680, 430);
        Controls.Add(_display);
        Controls.Add(Ui.Row(Ui.Label("Phút:"), _minutes, Ui.Label("Giây:"), _seconds,
            Ui.Button("Bắt đầu", (_, _) => StartCountdown()), Ui.Button("Dừng", (_, _) => StopCountdown()), Ui.Button("Đặt lại", (_, _) => ResetCountdown())));
        Controls.Add(Ui.Title("ĐỒNG HỒ ĐẾM NGƯỢC"));
        _timer.Tick += (_, _) => Tick();
        Shown += (_, _) => ResetCountdown();
    }

    private void StartCountdown()
    {
        _model.Start((int)_minutes.Value, (int)_seconds.Value);
        _display.Text = _model.Display;
        _timer.Start();
    }

    private void StopCountdown() { _timer.Stop(); _model.Stop(); }
    private void ResetCountdown() { StopCountdown(); _model.Start((int)_minutes.Value, (int)_seconds.Value); _model.Stop(); _display.Text = _model.Display; }
    private void Tick()
    {
        bool remains = _model.Tick();
        _display.Text = _model.Display;
        if (remains) return;
        _timer.Stop();
        MessageBox.Show("Đã hết thời gian!", "Đếm ngược", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
