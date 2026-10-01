using System;
using System.Linq;

namespace BaiThucHanhLINQ;

internal static class Bai52
{
    public static void Run()
    {
        TrinhBay.TieuDe("BÀI 5.2 - THỐNG KÊ LIST<MONHOC>");
        var monHocs = DuLieu.DS_Mon();

        TrinhBay.Dong("a - Tổng số môn", monHocs.Count());
        TrinhBay.Dong("b - Số môn bắt đầu bằng Lập trình",
            monHocs.Count(mon => mon.TenMon.StartsWith("Lập trình", StringComparison.OrdinalIgnoreCase)));
        TrinhBay.Dong("c - Tổng số tiết hệ KTV", monHocs.Where(mon => mon.He == "KTV").Sum(mon => mon.SoTiet));

        var tongTheoHe = monHocs.GroupBy(mon => mon.He).OrderBy(nhom => nhom.Key);
        Console.WriteLine("d - Tổng số môn của mỗi hệ:");
        foreach (var nhom in tongTheoHe)
            Console.WriteLine($"  {NhanHe(nhom.Key)}: {nhom.Count()} môn");

        var theoSoTiet = monHocs.GroupBy(mon => mon.SoTiet).OrderByDescending(nhom => nhom.Key);
        Console.WriteLine("e - Nhóm theo số tiết giảm dần:");
        foreach (var nhom in theoSoTiet)
            Console.WriteLine($"  {nhom.Key} tiết: {nhom.Count()} môn");

        byte soTietCaoNhat = monHocs.Max(mon => mon.SoTiet);
        Console.WriteLine("f - Môn có số tiết cao nhất:");
        foreach (var mon in monHocs.Where(mon => mon.SoTiet == soTietCaoNhat))
            Console.WriteLine($"  {mon}");

        var thongKeHe = monHocs.GroupBy(mon => mon.He).Select(nhom => new
        {
            He = nhom.Key,
            TongMon = nhom.Count(),
            TongTiet = nhom.Sum(mon => mon.SoTiet),
            CaoNhat = nhom.Max(mon => mon.SoTiet),
            ThapNhat = nhom.Min(mon => mon.SoTiet)
        });
        Console.WriteLine("g - Thống kê đầy đủ theo hệ:");
        foreach (var dong in thongKeHe)
            Console.WriteLine($"  {NhanHe(dong.He)} | {dong.TongMon} môn | Tổng {dong.TongTiet} | Max {dong.CaoNhat} | Min {dong.ThapNhat}");

        Console.WriteLine("h - Danh sách môn phân nhóm theo hệ:");
        foreach (var nhom in tongTheoHe)
        {
            Console.WriteLine($"  [{NhanHe(nhom.Key)}]");
            foreach (var mon in nhom.OrderBy(mon => mon.MaMon)) Console.WriteLine($"    {mon}");
        }

        Console.WriteLine("i - Danh sách môn phân nhóm theo số tiết tăng dần:");
        foreach (var nhom in monHocs.GroupBy(mon => mon.SoTiet).OrderBy(nhom => nhom.Key))
        {
            Console.WriteLine($"  [{nhom.Key} tiết]");
            foreach (var mon in nhom.OrderBy(mon => mon.MaMon)) Console.WriteLine($"    {mon.MaMon} | {mon.TenMon}");
        }

        var hocPhanKtv = monHocs.Where(mon => mon.He == "KTV")
            .GroupBy(mon => mon.MaMon.Split('_')[0])
            .OrderBy(nhom => nhom.Key);
        Console.WriteLine("j - Hệ KTV phân nhóm theo học phần:");
        foreach (var nhom in hocPhanKtv)
            TrinhBay.DanhSach($"  {nhom.Key}", nhom.OrderBy(mon => mon.MaMon).Select(mon => mon.MaMon));

        var trenBonMuoi = monHocs.Where(mon => mon.SoTiet > 40)
            .GroupBy(mon => mon.He)
            .OrderBy(nhom => nhom.Key);
        Console.WriteLine("k - Nhóm theo hệ, chỉ lấy môn có số tiết > 40:");
        foreach (var nhom in trenBonMuoi)
        {
            Console.WriteLine($"  [{NhanHe(nhom.Key)}]");
            foreach (var mon in nhom.OrderBy(mon => mon.MaMon)) Console.WriteLine($"    {mon}");
        }
    }

    private static string NhanHe(string he) => string.IsNullOrWhiteSpace(he) ? "Chưa khai báo" : he;
}
