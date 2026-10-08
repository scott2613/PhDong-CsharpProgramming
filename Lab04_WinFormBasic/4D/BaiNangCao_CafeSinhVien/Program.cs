// Điểm khởi động của bài nâng cao Cafe Sinh Viên: cấu hình WinForms và mở form chính.
using System;
using System.Windows.Forms;

namespace BaiNangCao_CafeSinhVien;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmCafeSinhVien());
    }
}
