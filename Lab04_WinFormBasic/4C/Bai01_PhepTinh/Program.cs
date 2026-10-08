// Điểm khởi động của Bài 1 phép tính: cấu hình WinForms và mở form chính.
using System;
using System.Windows.Forms;

namespace Bai01_PhepTinh;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmPhepTinh());
    }
}
