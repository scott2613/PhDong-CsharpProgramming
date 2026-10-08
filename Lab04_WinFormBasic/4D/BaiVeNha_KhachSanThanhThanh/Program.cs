// Điểm khởi động của bài về nhà Khách sạn Thanh Thanh: cấu hình WinForms và mở form chính.
using System;
using System.Windows.Forms;

namespace BaiVeNha_KhachSanThanhThanh;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmKhachSanThanhThanh());
    }
}
