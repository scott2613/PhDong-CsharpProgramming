using System;
using System.Drawing;
using System.Windows.Forms;
using Lab05D.WinForms;

namespace BaiMau01_Timer;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmDongHo());
    }
}

public sealed class FrmDongHo : Form
{
    private readonly Label _clock = new();
    private readonly Timer _timer = new() { Interval = 1000 };

    public FrmDongHo()
    {
        Ui.Configure(this, "Bài mẫu 1 - Đồng hồ hệ thống", 720, 330);
        Controls.Add(_clock);
        Controls.Add(Ui.Title("ĐỒNG HỒ HỆ THỐNG"));
        _clock.Dock = DockStyle.Fill;
        _clock.TextAlign = ContentAlignment.MiddleCenter;
        _clock.Font = new Font("Consolas", 31F, FontStyle.Bold);
        _clock.ForeColor = Ui.Primary;

        // Cứ mỗi giây, Timer yêu cầu biểu mẫu đọc lại thời gian của hệ điều hành.
        _timer.Tick += (_, _) => DisplayCurrentTime();
        Shown += (_, _) => { DisplayCurrentTime(); _timer.Start(); };
        FormClosed += (_, _) => _timer.Dispose();
    }

    private void DisplayCurrentTime() => _clock.Text = DateTime.Now.ToString("dd/MM/yyyy\nHH:mm:ss");
}
