using System;
using System.Windows.Forms;

namespace BaiNangCao_BanVeRapPhim;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmBanVeRapPhim());
    }
}
