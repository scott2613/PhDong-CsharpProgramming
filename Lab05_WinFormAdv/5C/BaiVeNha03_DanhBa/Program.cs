// Điểm khởi động của bài về nhà quản lý danh bạ theo chữ cái.
using System;
using System.Windows.Forms;

namespace BaiVeNha03_DanhBa;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmDanhBa());
    }    
}
