<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Linq</Namespace>
</Query>

void Main()
{
    var heDaoTao = DuLieu.TaoHe();
    var monHocs = DuLieu.TaoMon();

    // a) Inner join chỉ giữ các môn có mã hệ tương ứng.
    var innerJoin = from he in heDaoTao
                    join mon in monHocs on he.MaHe equals mon.He
                    select new { he.TenHe, mon.MaMon, mon.TenMon };
    innerJoin.Dump("a - Inner join hệ và môn học");

    // b) Group join + DefaultIfEmpty tạo left outer join.
    var leftJoin = from he in heDaoTao
                   join mon in monHocs on he.MaHe equals mon.He into nhomMon
                   from mon in nhomMon.DefaultIfEmpty()
                   select new
                   {
                       he.MaHe,
                       he.TenHe,
                       MaMon = mon?.MaMon ?? "(không có môn)",
                       TenMon = mon?.TenMon ?? "(không có môn)"
                   };
    leftJoin.Dump("b - Left outer join");

    // c) Full outer join = left join + các môn không tìm được hệ.
    var monKhongCoHe = monHocs
        .Where(mon => !heDaoTao.Any(he => he.MaHe == mon.He))
        .Select(mon => new
        {
            MaHe = "(chưa khai báo)", TenHe = "(chưa khai báo)", mon.MaMon, mon.TenMon
        });
    var fullOuterJoin = leftJoin.Concat(monKhongCoHe);
    fullOuterJoin.Dump("c - Full outer join");

    // d) Chỉ lấy bản ghi không khớp ở hai phía.
    leftJoin.Where(dong => dong.MaMon == "(không có môn)")
        .Concat(monKhongCoHe)
        .Dump("d - Các bản ghi không khớp");

    // e) Năm môn đầu theo số tiết giảm dần, mã môn tăng dần khi đồng hạng.
    monHocs.OrderByDescending(mon => mon.SoTiet)
        .ThenBy(mon => mon.MaMon)
        .Take(5)
        .GroupJoin(heDaoTao, mon => mon.He, he => he.MaHe,
            (mon, he) => new { Mon = mon, He = he.FirstOrDefault() })
        .Select(dong => new
        {
            TenHe = dong.He?.TenHe ?? "Chưa khai báo",
            dong.Mon.MaMon, dong.Mon.TenMon, dong.Mon.SoTiet
        }).Dump("e - Năm môn đầu có số tiết giảm dần");

    // f) Đếm môn theo từng hệ, kể cả hệ chưa có môn.
    heDaoTao.GroupJoin(monHocs, he => he.MaHe, mon => mon.He,
        (he, dsMon) => new { he.MaHe, he.TenHe, TongMon = dsMon.Count() })
        .Dump("f - Tổng số môn của mỗi hệ");

    // g, h) Truy vấn giá trị đơn.
    monHocs.Select(mon => mon.SoTiet).Distinct().Count()
        .Dump("g - Số loại số tiết khác nhau");
    monHocs.FirstOrDefault(mon =>
        mon.TenMon.StartsWith("Lập trình", StringComparison.OrdinalIgnoreCase))
        .Dump("h - Môn đầu tiên bắt đầu bằng Lập trình");

    // i) Select có chỉ số đánh số thứ tự lại từ 1 trong từng hệ.
    heDaoTao.GroupJoin(monHocs, he => he.MaHe, mon => mon.He,
        (he, dsMon) => new
        {
            he.MaHe,
            he.TenHe,
            MonHoc = dsMon.OrderBy(mon => mon.MaMon)
                .Select((mon, index) => new { SoThuTu = index + 1, mon.MaMon, mon.TenMon })
        }).Dump("i - Môn theo từng hệ, có số thứ tự trong nhóm");
}

record He(string MaHe, string TenHe);
record MonHoc(string MaMon, string TenMon, string He, byte SoTiet);

static class DuLieu
{
    public static He[] TaoHe() => new[]
    {
        new He("KTV", "Kỹ thuật viên"),
        new He("CD", "Chuyên đề"),
        new He("QT", "Chứng chỉ quốc tế")
    };

    public static MonHoc[] TaoMon() => new[]
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
