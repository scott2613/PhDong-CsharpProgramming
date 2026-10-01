using System;
using System.Collections.Generic;
using System.Linq;

namespace BaiThucHanhLINQ;

internal static class Bai62
{
    public static void Run()
    {
        TrinhBay.TieuDe("BÀI 6.2 - JOIN VÀ CÁC TOÁN TỬ TẬP HỢP");
        var heDaoTao = DuLieu.DS_He();
        var monHocs = DuLieu.DS_Mon();

        var innerJoin = from he in heDaoTao
                        join mon in monHocs on he.MaHe equals mon.He
                        orderby he.MaHe, mon.MaMon
                        select new { he.TenHe, mon.MaMon, mon.TenMon };
        Console.WriteLine("a - Inner join hệ và môn học:");
        foreach (var dong in innerJoin)
            Console.WriteLine($"  {dong.TenHe} | {dong.MaMon} | {dong.TenMon}");

        var leftJoin = from he in heDaoTao
                       join mon in monHocs on he.MaHe equals mon.He into nhomMon
                       from mon in nhomMon.DefaultIfEmpty()
                       orderby he.MaHe, mon == null ? "" : mon.MaMon
                       select new
                       {
                           he.MaHe,
                           he.TenHe,
                           MaMon = mon?.MaMon ?? "(không có môn)",
                           TenMon = mon?.TenMon ?? "(không có môn)"
                       };
        Console.WriteLine("b - Left outer join, gồm cả hệ chưa có môn:");
        foreach (var dong in leftJoin)
            Console.WriteLine($"  {dong.MaHe} | {dong.TenHe} | {dong.MaMon} | {dong.TenMon}");

        // Full outer join = left join cộng các môn không khớp với bất kỳ hệ nào.
        var monKhongCoHe = monHocs.Where(mon => !heDaoTao.Any(he => he.MaHe == mon.He))
            .Select(mon => new { MaHe = "(chưa khai báo)", TenHe = "(chưa khai báo)", mon.MaMon, mon.TenMon });
        var fullOuterJoin = leftJoin.Select(dong => new { dong.MaHe, dong.TenHe, dong.MaMon, dong.TenMon })
            .Concat(monKhongCoHe);
        Console.WriteLine("c - Full outer join:");
        foreach (var dong in fullOuterJoin)
            Console.WriteLine($"  {dong.MaHe} | {dong.TenHe} | {dong.MaMon} | {dong.TenMon}");

        var chiKhongKhop = leftJoin.Where(dong => dong.MaMon == "(không có môn)")
            .Select(dong => new { dong.MaHe, dong.TenHe, dong.MaMon, dong.TenMon })
            .Concat(monKhongCoHe);
        Console.WriteLine("d - Chỉ các bản ghi không khớp:");
        foreach (var dong in chiKhongKhop)
            Console.WriteLine($"  {dong.MaHe} | {dong.TenHe} | {dong.MaMon} | {dong.TenMon}");

        var namMonDau = monHocs.OrderByDescending(mon => mon.SoTiet).ThenBy(mon => mon.MaMon).Take(5)
            .GroupJoin(heDaoTao, mon => mon.He, he => he.MaHe, (mon, he) => new { mon, he })
            .SelectMany(x => x.he.DefaultIfEmpty(), (x, he) => new
            {
                TenHe = he?.TenHe ?? "Chưa khai báo",
                x.mon.MaMon,
                x.mon.TenMon,
                x.mon.SoTiet
            });
        Console.WriteLine("e - Năm môn đầu có số tiết giảm dần:");
        foreach (var dong in namMonDau)
            Console.WriteLine($"  {dong.TenHe} | {dong.MaMon} | {dong.TenMon} | {dong.SoTiet} tiết");

        var demTheoHe = heDaoTao.GroupJoin(monHocs, he => he.MaHe, mon => mon.He,
            (he, dsMon) => new { he.MaHe, he.TenHe, TongMon = dsMon.Count() });
        Console.WriteLine("f - Tổng số môn của mỗi hệ:");
        foreach (var dong in demTheoHe)
            Console.WriteLine($"  {dong.MaHe} | {dong.TenHe} | {dong.TongMon} môn");

        TrinhBay.Dong("g - Số loại số tiết khác nhau", monHocs.Select(mon => mon.SoTiet).Distinct().Count());
        TrinhBay.Dong("h - Môn đầu tiên bắt đầu bằng Lập trình",
            monHocs.First(mon => mon.TenMon.StartsWith("Lập trình", StringComparison.OrdinalIgnoreCase)));

        var monTheoHe = heDaoTao.GroupJoin(monHocs, he => he.MaHe, mon => mon.He,
            (he, dsMon) => new { he, dsMon = dsMon.OrderBy(mon => mon.MaMon) });
        Console.WriteLine("i - Môn theo từng hệ, có số thứ tự trong nhóm:");
        foreach (var nhom in monTheoHe)
        {
            Console.WriteLine($"  [{nhom.he.MaHe} - {nhom.he.TenHe}]");
            var danhSo = nhom.dsMon.Select((mon, index) => new { SoThuTu = index + 1, Mon = mon });
            foreach (var dong in danhSo)
                Console.WriteLine($"    {dong.SoThuTu}. {dong.Mon.MaMon} | {dong.Mon.TenMon}");
            if (!danhSo.Any()) Console.WriteLine("    (không có môn)");
        }
    }
}
