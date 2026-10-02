using System;
using System.Windows.Forms;

namespace Bai02_DangKyTaiKhoan;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmDangKyTaiKhoan());
    }
}
