// Điểm khởi động của Bài 5 đọc số thành chữ: cấu hình WinForms và mở form chính.
using System;
using System.Windows.Forms;

namespace Bai05_DocSoThanhChu;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmDocSo());
    }
}
