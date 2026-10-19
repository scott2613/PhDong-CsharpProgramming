// Điểm khởi động của bài về nhà xử lý ListBox số tự nhiên.
using System;
using System.Windows.Forms;

namespace BaiVeNha02_XuLyListBox;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmXuLyListBox());
    }    
}
