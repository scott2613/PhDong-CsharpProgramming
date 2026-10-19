// Điểm khởi động của bài tại lớp quản lý lớp và sinh viên bằng TreeView.
using System;
using System.Windows.Forms;

namespace Bai02_QuanLySinhVien;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmQuanLySinhVien());
    }    
}
