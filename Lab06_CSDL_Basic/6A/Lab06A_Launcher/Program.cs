using System;
using System.Drawing;
using System.Windows.Forms;
using Lab06A.WinForms;

namespace Lab06A_Launcher;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Chạy menu tổng để người dùng không chọn nhầm hai project thư viện dùng chung.
        Application.Run(new FrmMenuLab06A());
    }
}

public sealed class FrmMenuLab06A : Form
{
    public FrmMenuLab06A()
    {
        Ui.Configure(this, "LAB06A - TƯƠNG TÁC CƠ SỞ DỮ LIỆU", 720, 560);

        Label title = Ui.Title("LAB06A - TƯƠNG TÁC CƠ SỞ DỮ LIỆU");
        Label instruction = new()
        {
            Text = "Chọn bài cần chạy. CSDL QLSinhVien sẽ được tự tạo ở lần mở đầu tiên.",
            Dock = DockStyle.Top,
            Height = 52,
            TextAlign = ContentAlignment.MiddleCenter,
            ForeColor = Color.DimGray
        };

        FlowLayoutPanel menu = new()
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoScroll = true,
            Padding = new Padding(80, 16, 80, 16)
        };

        menu.Controls.Add(MenuButton("Bài mẫu - Quản lý Khoa", () => new BaiMau_QuanLyKhoa.FrmQuanLyKhoa()));
        menu.Controls.Add(MenuButton("Bài 1 - Quản lý Môn học", () => new Bai01_QuanLyMonHoc.FrmQuanLyMonHoc()));
        menu.Controls.Add(MenuButton("Nâng cao 1 - Quản lý Sinh viên", () => new BaiNangCao01_QuanLySinhVien.FrmQuanLySinhVien()));
        menu.Controls.Add(MenuButton("Nâng cao 2 - Quản lý Điểm", () => new BaiNangCao02_QuanLyDiem.FrmQuanLyDiem()));
        menu.Controls.Add(MenuButton("Bài về nhà - Quản lý Lớp", () => new BaiVeNha_QuanLyLop.FrmQuanLyLop()));

        Controls.Add(menu);
        Controls.Add(instruction);
        Controls.Add(title);
    }

    private Button MenuButton(string text, Func<Form> createForm)
    {
        Button button = new()
        {
            Text = text,
            Width = 500,
            Height = 58,
            Margin = new Padding(8),
            Font = new Font("Segoe UI", 11F, FontStyle.Bold),
            BackColor = Ui.Accent,
            ForeColor = Ui.Primary,
            FlatStyle = FlatStyle.Flat
        };
        button.Click += (_, _) =>
        {
            using Form form = createForm();
            form.ShowDialog(this);
        };
        return button;
    }
}
