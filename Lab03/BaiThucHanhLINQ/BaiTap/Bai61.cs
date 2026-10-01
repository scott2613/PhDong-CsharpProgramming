using System;

namespace BaiThucHanhLINQ;

internal static class Bai61
{
    public static void Run()
    {
        TrinhBay.TieuDe("BÀI 6.1 - NGUỒN DỮ LIỆU HỆ ĐÀO TẠO");
        foreach (var he in DuLieu.DS_He())
            Console.WriteLine($"  {he}");
    }
}
