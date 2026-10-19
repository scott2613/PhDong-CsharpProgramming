// Điểm khởi động của bài nâng cao xử lý danh sách họ tên.
using System;
using System.Windows.Forms;

namespace BaiNangCao_XuLyChuoi;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmXuLyChuoi());
    }    
}
