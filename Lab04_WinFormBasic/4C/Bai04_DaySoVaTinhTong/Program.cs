// Điểm khởi động của Bài 4 dãy số và tính tổng: cấu hình WinForms và mở form chính.
using System;
using System.Windows.Forms;

namespace Bai04_DaySoVaTinhTong;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmDaySo());
    }
}
