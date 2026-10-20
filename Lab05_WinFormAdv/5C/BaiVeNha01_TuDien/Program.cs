// Điểm khởi động của bài về nhà từ điển Anh Việt và Việt Anh.
using System;
using System.Windows.Forms;

namespace BaiVeNha01_TuDien;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmTuDien());
    }    
}
