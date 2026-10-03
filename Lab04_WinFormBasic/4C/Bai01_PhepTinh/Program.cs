using System;
using System.Windows.Forms;

namespace Bai01_PhepTinh;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmPhepTinh());
    }
}
