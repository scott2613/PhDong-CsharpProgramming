using System;
using System.Windows.Forms;

namespace Bai01_GiaiPhuongTrinh;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmGiaiPhuongTrinh());
    }
}
