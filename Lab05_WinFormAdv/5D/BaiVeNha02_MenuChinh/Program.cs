using System;
using System.Drawing;
using System.Windows.Forms;
using Lab05D.WinForms;

namespace BaiVeNha02_MenuChinh;

internal static class Program
{
    [STAThread] private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmMenuChinh());
    }
}

public sealed class FrmMenuChinh : Form
{
    public FrmMenuChinh()
    {
        Ui.Configure(this, "Bài về nhà 2 - Menu chính Lab05D", 980, 570);
        MenuStrip menu = new();
        ToolStripMenuItem winForms = new("Windows Forms");
        Add(winForms, "1. Đồng hồ hệ thống", () => new BaiMau01_Timer.FrmDongHo());
        Add(winForms, "2. ListView sinh viên", () => new BaiMau02_ListView.FrmDanhSachSinhVien());
        Add(winForms, "3. File Dialog", () => new BaiMau03_FileDialog.FrmSoanThaoVanBan());
        Add(winForms, "4. Quản lý sinh viên", () => new Bai01_QuanLySinhVienListView.FrmQuanLySinhVien());
        Add(winForms, "5. Quản lý tài khoản", () => new Bai02_QuanLyTaiKhoan.FrmQuanLyTaiKhoan());
        Add(winForms, "6. Quản lý học viên", () => new BaiNangCao_QuanLyHocVien.FrmQuanLyHocVien());
        Add(winForms, "7. Đồng hồ đếm ngược", () => new BaiVeNha01_DemNguoc.FrmDemNguoc());

        ToolStripMenuItem database = new("ADO.NET");
        Add(database, "1. Quản lý khoa", () => new ADO_BaiMau_QuanLyKhoa.FrmQuanLyKhoa());
        Add(database, "2. Quản lý môn học", () => new ADO_Bai01_QuanLyMonHoc.FrmQuanLyMonHoc());
        Add(database, "3. Quản lý sinh viên", () => new ADO_BaiNangCao01_QuanLySinhVien.FrmQuanLySinhVien());
        Add(database, "4. Quản lý điểm", () => new ADO_BaiNangCao02_QuanLyDiem.FrmQuanLyDiem());
        Add(database, "5. Quản lý lớp", () => new ADO_BaiVeNha_QuanLyLop.FrmQuanLyLop());
        menu.Items.Add(winForms); menu.Items.Add(database); menu.Items.Add("Thoát", null, (_, _) => Close());
        Controls.Add(menu); MainMenuStrip = menu;

        Label welcome = new()
        {
            Text = "LAB 05D\nWINDOWS FORMS NÂNG CAO & ADO.NET\n\nChọn một bài tập trên thanh menu để bắt đầu.",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 18F, FontStyle.Bold),
            ForeColor = Ui.Primary,
            BackColor = Color.White
        };
        Controls.Add(welcome);
    }

    private void Add(ToolStripMenuItem parent, string caption, Func<Form> factory)
    {
        parent.DropDownItems.Add(caption, null, (_, _) =>
        {
            Form child = factory();
            // Hiển thị độc lập để mọi bài vẫn dùng được ngay cả khi mở từ menu tổng.
            child.Show(this);
        });
    }
}
