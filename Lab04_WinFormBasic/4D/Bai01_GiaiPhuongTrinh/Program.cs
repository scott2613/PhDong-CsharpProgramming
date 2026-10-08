// Điểm khởi động của Bài 1 giải phương trình: cấu hình WinForms và mở form chính.
using System;
using System.Windows.Forms;

namespace Bai01_GiaiPhuongTrinh;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmGiaiPhuongTrinh());
    }
}
