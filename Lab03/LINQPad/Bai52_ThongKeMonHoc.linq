<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Linq</Namespace>
</Query>

void Main()
{
    var monHocs = DuLieuMonHoc.Tao();

    // a, b, c) Các thống kê đơn.
    monHocs.Count().Dump("a - Tổng số môn");
    monHocs.Count(mon => mon.TenMon.StartsWith("Lập trình", StringComparison.OrdinalIgnoreCase))
        .Dump("b - Số môn bắt đầu bằng Lập trình");
    monHocs.Where(mon => mon.He == "KTV").Sum(mon => mon.SoTiet)
        .Dump("c - Tổng số tiết hệ KTV");

    // d, e) Nhóm theo hệ và theo số tiết.
    monHocs.GroupBy(mon => mon.He)
        .Select(nhom => new { He = NhanHe(nhom.Key), TongMon = nhom.Count() })
        .OrderBy(dong => dong.He)
        .Dump("d - Tổng số môn của mỗi hệ");
    monHocs.GroupBy(mon => mon.SoTiet)
        .OrderByDescending(nhom => nhom.Key)
        .Select(nhom => new { SoTiet = nhom.Key, TongMon = nhom.Count(), CacMon = nhom })
        .Dump("e - Nhóm theo số tiết giảm dần");

    // f) Có thể có nhiều môn đồng hạng cao nhất nên lọc theo giá trị Max.
    byte soTietCaoNhat = monHocs.Max(mon => mon.SoTiet);
    monHocs.Where(mon => mon.SoTiet == soTietCaoNhat)
        .Dump("f - Môn có số tiết cao nhất");

    // g) Thống kê đầy đủ cho từng hệ.
    monHocs.GroupBy(mon => mon.He).Select(nhom => new
    {
        He = NhanHe(nhom.Key),
        TongMon = nhom.Count(),
        TongTiet = nhom.Sum(mon => mon.SoTiet),
        CaoNhat = nhom.Max(mon => mon.SoTiet),
        ThapNhat = nhom.Min(mon => mon.SoTiet)
    }).Dump("g - Thống kê đầy đủ theo hệ");

    // h, i) Dump trực tiếp IGrouping để xem cả khóa lẫn các phần tử.
    monHocs.GroupBy(mon => NhanHe(mon.He)).OrderBy(nhom => nhom.Key)
        .Dump("h - Danh sách môn phân nhóm theo hệ");
    monHocs.GroupBy(mon => mon.SoTiet).OrderBy(nhom => nhom.Key)
        .Dump("i - Danh sách môn phân nhóm theo số tiết tăng dần");

    // j) HP2_1 và HP2_2 cùng có khóa HP2; tương tự HP3, HP4, HP5.
    monHocs.Where(mon => mon.He == "KTV")
        .GroupBy(mon => mon.MaMon.Split('_')[0])
        .OrderBy(nhom => nhom.Key)
        .Dump("j - Hệ KTV phân nhóm theo học phần");

    // k) Lọc số tiết trước, sau đó mới nhóm theo hệ.
    monHocs.Where(mon => mon.SoTiet > 40)
        .GroupBy(mon => NhanHe(mon.He))
        .OrderBy(nhom => nhom.Key)
        .Dump("k - Nhóm theo hệ, chỉ lấy môn có số tiết trên 40");
}

string NhanHe(string he) => string.IsNullOrWhiteSpace(he) ? "Chưa khai báo" : he;

record MonHoc(string MaMon, string TenMon, string He, byte SoTiet);

static class DuLieuMonHoc
{
    public static MonHoc[] Tao() => new[]
    {
        new MonHoc("HP2_1", "Nền tảng C#", "KTV", 64),
        new MonHoc("HP2_2", "Công nghệ ADO.NET", "KTV", 64),
        new MonHoc("HP3_1", "Lập trình Windows Forms", "KTV", 64),
        new MonHoc("HP3_2", "Xây dựng ứng dụng Windows Forms", "KTV", 64),
        new MonHoc("HP4_1", "Lập trình Web với HTML, CSS và JavaScript", "KTV", 64),
        new MonHoc("HP4_2", "Xây dựng ứng dụng Web với ASP.NET", "KTV", 64),
        new MonHoc("HP5_1", "Lập trình CSDL SQL Server căn bản", "KTV", 64),
        new MonHoc("HP5_2", "Lập trình CSDL SQL Server nâng cao", "KTV", 64),
        new MonHoc("JLCB", "Joomla cơ bản", "CD", 72),
        new MonHoc("LINQ", "Language-Integrated Query", "CD", 64),
        new MonHoc("DAWEB", "Đồ án thực tế Web với ASP.NET", "CD", 40),
        new MonHoc("DAWIN", "Đồ án thực tế Windows Forms", "CD", 40),
        new MonHoc("CC++", "Lập trình hướng đối tượng với C/C++", "CD", 128),
        new MonHoc("JQUE", "JQuery", "CD", 22),
        new MonHoc("XML", "Công nghệ XML", "CD", 32),
        new MonHoc("CRYS", "Crystal Report trong Visual Studio", "CD", 32),
        new MonHoc("BWEB", "HTML, CSS và JavaScript", "CD", 32),
        new MonHoc("XYZ", "Chưa đặt tên môn", "", 0)
    };
}
