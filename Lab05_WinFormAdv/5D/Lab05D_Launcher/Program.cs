using System;
using System.Windows.Forms;

namespace Lab05D_Launcher;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Khởi động menu tổng thay vì chạy nhầm các thư viện Lab05D.Core hoặc Lab05D.WinForms.
        Application.Run(new BaiVeNha02_MenuChinh.FrmMenuChinh());
    }
}
