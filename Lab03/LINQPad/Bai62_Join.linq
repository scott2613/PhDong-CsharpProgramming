<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.Linq</Namespace>
</Query>

void Main()
{
    var cacHe = new[]
    {
        new He("KTV", "Kỹ thuật viên"),
        new He("CD", "Chuyên đề"),
        new He("QT", "Chứng chỉ quốc tế")
    };

    var cacMon = new[]
    {
        new MonHoc("HP2_1", "Nền tảng C#", "KTV", 64),
        new MonHoc("LINQ", "Language-Integrated Query", "CD", 64),
        new MonHoc("XYZ", "Chưa đặt tên môn", "", 0)
    };

    // Inner join chỉ giữ những cặp có mã hệ trùng nhau.
    var innerJoin = from he in cacHe
                    join mon in cacMon on he.MaHe equals mon.MaHe
                    select new { he.TenHe, mon.MaMon, mon.TenMon };

    // Group join kết hợp DefaultIfEmpty tạo left outer join.
    var leftJoin = from he in cacHe
                   join mon in cacMon on he.MaHe equals mon.MaHe into nhomMon
                   from mon in nhomMon.DefaultIfEmpty()
                   select new
                   {
                       he.MaHe,
                       he.TenHe,
                       MaMon = mon?.MaMon ?? "(không có môn)",
                       TenMon = mon?.TenMon ?? "(không có môn)"
                   };

    innerJoin.Dump("Inner join");
    leftJoin.Dump("Left outer join");
}

record He(string MaHe, string TenHe);
record MonHoc(string MaMon, string TenMon, string MaHe, int SoTiet);
