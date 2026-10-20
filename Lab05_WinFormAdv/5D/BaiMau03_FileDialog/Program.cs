using System;
using System.IO;
using System.Windows.Forms;
using Lab05D.WinForms;

namespace BaiMau03_FileDialog;

internal static class Program
{
    [STAThread] private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmSoanThaoVanBan());
    }
}

public sealed class FrmSoanThaoVanBan : Form
{
    private readonly TextBox _path = Ui.TextBox(560);
    private readonly RichTextBox _content = new() { Dock = DockStyle.Fill, Font = new System.Drawing.Font("Consolas", 11F), AcceptsTab = true };

    public FrmSoanThaoVanBan()
    {
        Ui.Configure(this, "Bài mẫu 3 - File Dialog", 1000, 620);
        _path.ReadOnly = true;
        Controls.Add(_content);
        Controls.Add(Ui.Row(Ui.Label("Tập tin:"), _path, Ui.Button("Mở...", (_, _) => Open()), Ui.Button("Lưu...", (_, _) => Save())));
        Controls.Add(Ui.Title("SOẠN THẢO TẬP TIN VĂN BẢN"));
    }

    private void Open()
    {
        using OpenFileDialog dialog = new() { Filter = "Tập tin văn bản (*.txt)|*.txt|Tất cả tập tin (*.*)|*.*" };
        if (dialog.ShowDialog() != DialogResult.OK) return;
        // UTF-8 được dùng để giữ nguyên nội dung tiếng Việt.
        _content.Text = File.ReadAllText(dialog.FileName, System.Text.Encoding.UTF8);
        _path.Text = dialog.FileName;
    }

    private void Save()
    {
        using SaveFileDialog dialog = new() { Filter = "Tập tin văn bản (*.txt)|*.txt", DefaultExt = "txt" };
        if (dialog.ShowDialog() != DialogResult.OK) return;
        File.WriteAllText(dialog.FileName, _content.Text, System.Text.Encoding.UTF8);
        _path.Text = dialog.FileName;
        MessageBox.Show("Đã lưu tập tin.", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
