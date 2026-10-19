// Điểm khởi động của bài mẫu nạp và lựa chọn dữ liệu ComboBox.
using System;
using System.Windows.Forms;

namespace BaiMau02_ComboBox;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmDanToc());
    }    
}
