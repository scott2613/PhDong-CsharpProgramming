// Điểm khởi động của bài mẫu chuyển phần tử giữa hai ListBox.
using System;
using System.Windows.Forms;

namespace BaiMau01_ListBox;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmListBox());
    }    
}
