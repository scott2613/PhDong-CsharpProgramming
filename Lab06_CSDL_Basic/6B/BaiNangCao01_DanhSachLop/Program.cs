using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Lab06B.Core;
using Lab06B.WinForms;

namespace BaiNangCao01_DanhSachLop;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmDanhSachLop());
    }
}

public sealed class FrmDanhSachLop : DatabaseFormBase
{
    private readonly ListView _list = Ui.List("STT", "Mã lớp", "Tên lớp", "Mã khoa");

    public FrmDanhSachLop()
    {
        Ui.Configure(this, "BÀI NÂNG CAO 1 - DANH SÁCH LỚP", 980, 620);
        _list.Columns[0].Width = 70;
        _list.Columns[1].Width = 180;
        _list.Columns[2].Width = 380;
        _list.Columns[3].Width = 180;
        FlowLayoutPanel actions = Ui.Row(Ui.Button("Hiển thị dữ liệu", (_, _) => NapDuLieu()), Ui.Button("Thoát", (_, _) => Close()));
        Controls.Add(_list);
        Controls.Add(Ui.Hint());
        Controls.Add(actions);
        Controls.Add(Ui.Title("BÀI NÂNG CAO 1 - DANH SÁCH LỚP"));
        Shown += (_, _) => SafeRun(() => { DatabaseInitializer.EnsureUniversity(); NapDuLieu(); });
    }

    private void NapDuLieu() => SafeRun(() =>
    {
        _list.Items.Clear();
        using SqlConnection connection = DataAccess.OpenUniversity();
        using SqlCommand command = new("SELECT MaLop,TenLop,MaKhoa FROM dbo.Lop ORDER BY MaLop", connection);
        using SqlDataReader reader = command.ExecuteReader();
        int index = 1;
        while (reader.Read())
        {
            // ListViewItem đầu tiên là số thứ tự, các cột còn lại nằm trong SubItems.
            ListViewItem item = new((index++).ToString());
            item.SubItems.Add(reader.GetString(0));
            item.SubItems.Add(reader.GetString(1));
            item.SubItems.Add(reader.GetString(2));
            _list.Items.Add(item);
        }
    });
}
