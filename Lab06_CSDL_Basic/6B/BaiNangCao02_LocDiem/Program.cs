using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using Lab06B.Core;
using Lab06B.WinForms;

namespace BaiNangCao02_LocDiem;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmLocDiem());
    }
}

public sealed class FrmLocDiem : DatabaseFormBase
{
    private readonly ComboBox _monHoc = Ui.ComboBox(190);
    private readonly TextBox _tongDiem = Ui.TextBox(130);
    private readonly ListView _list = Ui.List("STT", "Tên sinh viên", "Ngày sinh", "Tên môn học", "Điểm");

    public FrmLocDiem()
    {
        Ui.Configure(this, "BÀI NÂNG CAO 2 - LỌC ĐIỂM", 1120, 680);
        _tongDiem.ReadOnly = true;
        _list.Columns[0].Width = 65;
        _list.Columns[1].Width = 250;
        _list.Columns[2].Width = 140;
        _list.Columns[3].Width = 260;
        _list.Columns[4].Width = 110;
        FlowLayoutPanel filter = Ui.Row(Ui.Label("Môn học"), _monHoc, Ui.Button("Lọc dữ liệu", (_, _) => NapDiem()), Ui.Label("Tổng điểm"), _tongDiem);
        Controls.Add(_list);
        Controls.Add(Ui.Hint());
        Controls.Add(filter);
        Controls.Add(Ui.Title("BÀI NÂNG CAO 2 - DANH SÁCH ĐIỂM THEO MÔN"));
        Shown += (_, _) => SafeRun(() => { DatabaseInitializer.EnsureUniversity(); NapMonHoc(); NapDiem(); });
    }

    private void NapMonHoc()
    {
        _monHoc.Items.Clear();
        _monHoc.Items.Add(string.Empty); // Item rỗng đại diện cho tất cả môn học.
        using SqlConnection connection = DataAccess.OpenUniversity();
        using SqlCommand command = new("SELECT MaMonHoc FROM dbo.MonHoc ORDER BY MaMonHoc", connection);
        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read()) _monHoc.Items.Add(reader.GetString(0));
        _monHoc.SelectedIndex = 0;
    }

    private void NapDiem() => SafeRun(() =>
    {
        string maMon = Convert.ToString(_monHoc.SelectedItem) ?? string.Empty;
        string sql = @"SELECT sv.HoTen,sv.NgaySinh,mh.TenMonHoc,d.Diem
                       FROM dbo.Diem d
                       JOIN dbo.SinhVien sv ON sv.MaSinhVien=d.MaSinhVien
                       JOIN dbo.MonHoc mh ON mh.MaMonHoc=d.MaMonHoc
                       WHERE (@ma=N'' OR d.MaMonHoc=@ma)
                       ORDER BY sv.HoTen,mh.TenMonHoc";
        DataTable table = DataAccess.QueryUniversity(sql, new SqlParameter("@ma", maMon));
        _list.Items.Clear();
        decimal total = 0;
        int index = 1;
        foreach (DataRow row in table.Rows)
        {
            decimal score = Convert.ToDecimal(row["Diem"]);
            total += score;
            ListViewItem item = new((index++).ToString());
            item.SubItems.Add(Convert.ToString(row["HoTen"]));
            item.SubItems.Add(Convert.ToDateTime(row["NgaySinh"]).ToString("dd/MM/yyyy"));
            item.SubItems.Add(Convert.ToString(row["TenMonHoc"]));
            item.SubItems.Add(score.ToString("0.##", CultureInfo.InvariantCulture));
            _list.Items.Add(item);
        }
        _tongDiem.Text = total.ToString("0.##", CultureInfo.InvariantCulture);
    });
}
