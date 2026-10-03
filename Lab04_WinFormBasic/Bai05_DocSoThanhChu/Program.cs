using System;
using System.Windows.Forms;

namespace Bai05_DocSoThanhChu;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmDocSo());
    }
}
