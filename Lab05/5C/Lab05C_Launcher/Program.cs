using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab05C_Launcher;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmMenuLab05C());
    }
}

/// <summary>Menu khởi động giúp chạy và kiểm tra tất cả ứng dụng Lab05C từ một project thực thi.</summary>
public sealed class FrmMenuLab05C : Form
{
    private readonly TableLayoutPanel _buttons = new();

    public FrmMenuLab05C()
    {
        Text = "Lab05C - Windows Forms nâng cao";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(800, 600);
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.White;

        Label title = new()
        {
            Text = "LAB 05C - WINDOWS FORMS NÂNG CAO",
            Dock = DockStyle.Top,
            Height = 76,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 18F, FontStyle.Bold),
            ForeColor = Color.FromArgb(31, 78, 121),
            BackColor = Color.FromArgb(234, 242, 248)
        };

        _buttons.Dock = DockStyle.Fill;
        _buttons.Padding = new Padding(32, 24, 32, 24);
        _buttons.ColumnCount = 2;
        _buttons.RowCount = 5;
        _buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        _buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        for (int i = 0; i < 5; i++) _buttons.RowStyles.Add(new RowStyle(SizeType.Percent, 20));

        Add("Bài mẫu 1 - ListBox", () => new BaiMau01_ListBox.FrmListBox());
        Add("Bài mẫu 2 - ComboBox", () => new BaiMau02_ComboBox.FrmDanToc());
        Add("Bài mẫu 3 - Phòng ban", () => new BaiMau03_PhongBanNhanVien.FrmPhongBanNhanVien());
        Add("Bài 1 - Ước số", () => new Bai01_UocSo.FrmUocSo());
        Add("Bài 2 - Quản lý sinh viên", () => new Bai02_QuanLySinhVien.FrmQuanLySinhVien());
        Add("Bài nâng cao - Xử lý chuỗi", () => new BaiNangCao_XuLyChuoi.FrmXuLyChuoi());
        Add("Bài về nhà 1 - Từ điển", () => new BaiVeNha01_TuDien.FrmTuDien());
        Add("Bài về nhà 2 - Xử lý ListBox", () => new BaiVeNha02_XuLyListBox.FrmXuLyListBox());
        Add("Bài về nhà 3 - Danh bạ", () => new BaiVeNha03_DanhBa.FrmDanhBa());
        Add("Đóng chương trình", () => { Close(); return null; });

        Controls.Add(_buttons);
        Controls.Add(title);
    }

    private void Add(string caption, Func<Form?> createForm)
    {
        Button button = new()
        {
            Text = caption,
            Dock = DockStyle.Fill,
            Margin = new Padding(10),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(246, 249, 252),
            ForeColor = Color.FromArgb(31, 78, 121)
        };
        button.Click += (_, _) => createForm()?.Show(this);
        _buttons.Controls.Add(button);
    }
}
