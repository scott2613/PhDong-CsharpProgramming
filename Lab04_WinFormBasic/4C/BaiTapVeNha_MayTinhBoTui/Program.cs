// Điểm khởi động của bài tập về nhà máy tính bỏ túi: cấu hình WinForms và mở form chính.
using System;
using System.Windows.Forms;

namespace BaiTapVeNha_MayTinhBoTui;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmMayTinhBoTui());
    }
}
