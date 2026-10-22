using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Bai01_QuanLySinhVien;
using Bai02_QuanLyDiem;
using BaiMau01_QuanLyKhoa;
using BaiMau02_QuanLyLop;
using BaiNangCao01_DanhSachLop;
using BaiNangCao02_LocDiem;
using BaiVeNha_QLCuaHang;
using Lab06B.Core;
using Lab06B.WinForms;

namespace Lab06B_Launcher;

internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        if (args.Length == 2 && args[0] == "--screenshots")
        {
            CaptureScreenshots(args[1]);
            return;
        }
        Application.Run(new FrmLauncher());
    }

    private static void CaptureScreenshots(string outputDirectory)
    {
        Directory.CreateDirectory(outputDirectory);
        DatabaseInitializer.EnsureUniversity();
        DatabaseInitializer.EnsureStore();
        (string Name, Func<Form> Create)[] forms =
        {
            ("01_launcher", () => new FrmLauncher()),
            ("02_khoa", () => new FrmQuanLyKhoa()),
            ("03_lop", () => new FrmQuanLyLop()),
            ("04_sinhvien", () => new FrmQuanLySinhVien()),
            ("05_diem", () => new FrmQuanLyDiem()),
            ("06_danhsachlop", () => new FrmDanhSachLop()),
            ("07_locdiem", () => new FrmLocDiem()),
            ("08_cuahang", () => new FrmCuaHang()),
            ("09_sanpham", () => new FrmSanPham()),
            ("10_banhang", () => new FrmBanHang())
        };
        foreach ((string name, Func<Form> create) in forms)
        {
            using Form form = create();
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(-3000, -3000);
            form.ShowInTaskbar = false;
            form.Show();
            Application.DoEvents();
            Thread.Sleep(120);
            using Bitmap bitmap = new(form.Width, form.Height);
            form.DrawToBitmap(bitmap, new Rectangle(Point.Empty, bitmap.Size));
            bitmap.Save(Path.Combine(outputDirectory, name + ".png"), ImageFormat.Png);
            form.Close();
        }
    }
}

public sealed class FrmLauncher : Form
{
    public FrmLauncher()
    {
        Ui.Configure(this, "LAB06B - TƯƠNG TÁC CƠ SỞ DỮ LIỆU", 860, 650);
        TableLayoutPanel buttons = new()
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 4,
            Padding = new Padding(35),
            BackColor = Color.White
        };
        buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        for (int i = 0; i < 4; i++) buttons.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
        Add(buttons, "Bài mẫu 1 - Quản lý Khoa", 0, 0, () => new FrmQuanLyKhoa());
        Add(buttons, "Bài mẫu 2 - Quản lý Lớp", 1, 0, () => new FrmQuanLyLop());
        Add(buttons, "Bài 1 - Quản lý Sinh viên", 0, 1, () => new FrmQuanLySinhVien());
        Add(buttons, "Bài 2 - Quản lý Điểm", 1, 1, () => new FrmQuanLyDiem());
        Add(buttons, "Nâng cao 1 - Danh sách Lớp", 0, 2, () => new FrmDanhSachLop());
        Add(buttons, "Nâng cao 2 - Lọc Điểm", 1, 2, () => new FrmLocDiem());
        Add(buttons, "Bài về nhà - Quản lý cửa hàng", 0, 3, () => new FrmCuaHang());
        Add(buttons, "Thoát", 1, 3, () => null);
        Controls.Add(buttons);
        Controls.Add(Ui.Title("LAB06B - TƯƠNG TÁC CƠ SỞ DỮ LIỆU"));
    }

    private void Add(TableLayoutPanel panel, string text, int column, int row, Func<Form?> create)
    {
        Button button = new()
        {
            Text = text,
            Dock = DockStyle.Fill,
            Margin = new Padding(12),
            Font = new Font("Segoe UI", 11F, FontStyle.Bold),
            ForeColor = Ui.Primary,
            BackColor = Ui.Accent
        };
        button.Click += (_, _) =>
        {
            Form? form = create();
            if (form == null) Close(); else form.ShowDialog(this);
        };
        panel.Controls.Add(button, column, row);
    }
}
