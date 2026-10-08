// Điểm khởi động của bài mẫu định dạng Label: cấu hình WinForms và mở form chính.
using System;
using System.Windows.Forms;

namespace BaiMau02_DinhDangLabel;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmDinhDangLabel());
    }
}
