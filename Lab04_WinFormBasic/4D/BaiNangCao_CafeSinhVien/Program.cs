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
