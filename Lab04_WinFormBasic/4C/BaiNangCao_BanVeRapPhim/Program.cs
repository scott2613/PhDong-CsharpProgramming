// Điểm khởi động của bài nâng cao bán vé rạp phim: cấu hình WinForms và mở form chính.
using System;
using System.Windows.Forms;

namespace BaiNangCao_BanVeRapPhim;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmBanVeRapPhim());
    }
}
