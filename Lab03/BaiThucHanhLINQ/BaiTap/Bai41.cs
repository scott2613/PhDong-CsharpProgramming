using System;

namespace BaiThucHanhLINQ;

internal static class Bai41
{
    public static void Run()
    {
        TrinhBay.TieuDe("BÀI 4.1 - NGUỒN DỮ LIỆU MÔN HỌC");
        var monHocs = DuLieu.DS_Mon();
        TrinhBay.Dong("Tổng số môn được khởi tạo", monHocs.Count);
        foreach (var mon in monHocs)
            Console.WriteLine($"  {mon}");
    }
}
