// Điểm khởi động của Bài 3 ước số và bội số: cấu hình WinForms và mở form chính.
using System;
using System.Windows.Forms;

namespace Bai03_UocSoBoiSo;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmUocSoBoiSo());
    }
}
