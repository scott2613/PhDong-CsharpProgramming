// Điểm khởi động của Bài 2 mảng số nguyên: cấu hình WinForms và mở form chính.
using System;
using System.Windows.Forms;

namespace Bai02_MangSoNguyen;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmMangSoNguyen());
    }
}
