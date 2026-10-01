using System;
using System.Linq;

namespace BaiThucHanhLINQ;

internal static class KiemTra
{
    public static void Run()
    {
        // Các bất biến này đối chiếu những kết quả dễ sai nhất của đề.
        KhangDinh(Bai21.MangSo.Where(x => x % 4 == 0 && x % 3 == 0).SequenceEqual([12, 24, 0]), "Bài 2.1a");
        KhangDinh(Bai21.MangSo.Select(x => x % 2 == 0 ? x / 2 : x).SequenceEqual([25, 21, 8, 3, 9, 4, 6, 7, 12, 0]), "Bài 2.1c");
        KhangDinh(Bai31.MangSo.Count() == 11 && Bai31.MangSo.Distinct().Count() == 8, "Bài 3.1");
        KhangDinh(Bai32.MonAn.Count(x => x.StartsWith("Bánh", StringComparison.Ordinal)) == 3, "Bài 3.2c");
        KhangDinh(DuLieu.DS_Mon().Count == 18, "Bài 4.1");
        KhangDinh(DuLieu.DS_Mon().Where(x => x.He == "KTV").Sum(x => x.SoTiet) == 512, "Bài 5.2c");
        KhangDinh(DuLieu.DS_He().Single(x => x.MaHe == "QT").TenHe == "Chứng chỉ quốc tế", "Bài 6.1");
        KhangDinh(DuLieu.DS_Mon().Count(x => string.IsNullOrEmpty(x.He)) == 1, "Bài 6.2c");
        Console.WriteLine("KIỂM TRA THÀNH CÔNG: 8/8 điều kiện đạt.");
    }

    private static void KhangDinh(bool condition, string label)
    {
        if (!condition) throw new InvalidOperationException($"Kiểm tra không đạt: {label}");
    }
}
