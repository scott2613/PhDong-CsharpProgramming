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
