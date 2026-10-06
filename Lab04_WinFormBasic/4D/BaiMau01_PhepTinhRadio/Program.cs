using System;
using System.Windows.Forms;

namespace BaiMau01_PhepTinhRadio;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmPhepTinhRadio());
    }
}
