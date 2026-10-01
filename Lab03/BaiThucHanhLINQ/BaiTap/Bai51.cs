using System;
using System.Linq;

namespace BaiThucHanhLINQ;

internal static class Bai51
{
    public static void Run()
    {
        TrinhBay.TieuDe("BÀI 5.1 - TRUY VẤN CƠ BẢN LIST<MONHOC>");
        var monHocs = DuLieu.DS_Mon();

        var lapTrinh = from mon in monHocs
                       where mon.TenMon.StartsWith("Lập trình", StringComparison.OrdinalIgnoreCase)
                       select mon.TenMon;
        TrinhBay.DanhSach("a - Tên môn bắt đầu bằng Lập trình", lapTrinh);

        var heCd = monHocs.Where(mon => mon.He == "CD")
            .OrderByDescending(mon => mon.SoTiet)
            .ThenBy(mon => mon.MaMon);
        Console.WriteLine("b - Môn hệ CD theo số tiết giảm dần, mã môn tăng dần:");
        foreach (var mon in heCd)
            Console.WriteLine($"  {mon}");

        var chuaWeb = monHocs
            .Where(mon => mon.TenMon.Contains("web", StringComparison.OrdinalIgnoreCase))
            .Select(mon => new { mon.TenMon, mon.He });
        Console.WriteLine("c - Môn có tên chứa web:");
        foreach (var mon in chuaWeb)
            Console.WriteLine($"  {mon.TenMon} | {mon.He}");

        var heKtv = from mon in monHocs
                    where mon.He == "KTV"
                    orderby mon.MaMon
                    select mon;
        Console.WriteLine("d - Môn hệ KTV theo mã môn tăng dần:");
        foreach (var mon in heKtv)
            Console.WriteLine($"  {mon}");
    }
}
