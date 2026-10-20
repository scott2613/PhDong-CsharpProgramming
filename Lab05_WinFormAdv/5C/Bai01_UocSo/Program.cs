// Điểm khởi động của bài tại lớp phân tích các ước số.
using System;
using System.Windows.Forms;

namespace Bai01_UocSo;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmUocSo());
    }    
}
