<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Linq</Namespace>
</Query>

void Main()
{
    var monHocs = DuLieuMonHoc.Tao();

    // a) Dùng Query Syntax để lọc tên bắt đầu bằng "Lập trình".
    var lapTrinh = from mon in monHocs
                   where mon.TenMon.StartsWith("Lập trình", StringComparison.OrdinalIgnoreCase)
                   select mon.TenMon;

    // b) Hệ CD: số tiết giảm dần, nếu bằng nhau thì mã môn tăng dần.
    var heCd = monHocs.Where(mon => mon.He == "CD")
        .OrderByDescending(mon => mon.SoTiet)
        .ThenBy(mon => mon.MaMon);

    // c) Tìm từ web không phân biệt chữ hoa, chỉ chọn tên môn và hệ.
    var chuaWeb = monHocs
        .Where(mon => mon.TenMon.Contains("web", StringComparison.OrdinalIgnoreCase))
        .Select(mon => new { mon.TenMon, mon.He });

    // d) Hệ KTV theo mã môn tăng dần.
    var heKtv = from mon in monHocs
                where mon.He == "KTV"
                orderby mon.MaMon
                select mon;

    lapTrinh.Dump("a - Tên môn bắt đầu bằng Lập trình");
    heCd.Dump("b - Môn hệ CD");
    chuaWeb.Dump("c - Môn có tên chứa web");
    heKtv.Dump("d - Môn hệ KTV");
}

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
