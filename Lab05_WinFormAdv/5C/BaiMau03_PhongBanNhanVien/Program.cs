// Điểm khởi động của bài mẫu quản lý phòng ban và nhân viên bằng TreeView.
using System;
using System.Windows.Forms;

namespace BaiMau03_PhongBanNhanVien;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmPhongBanNhanVien());
    }    
}
