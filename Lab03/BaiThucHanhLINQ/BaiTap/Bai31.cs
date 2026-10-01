using System;
using System.Linq;

namespace BaiThucHanhLINQ;

internal static class Bai31
{
    internal static readonly int[] MangSo = [50, 42, 12, 3, 9, 8, 1, 50, 3, 42, 85];

    public static void Run()
    {
        TrinhBay.TieuDe("BÀI 3.1 - THỐNG KÊ MẢNG SỐ");
        TrinhBay.Dong("a - Tổng số phần tử", MangSo.Count());
        TrinhBay.Dong("a - Số phần tử chẵn", MangSo.Count(so => so % 2 == 0));
        TrinhBay.Dong("a - Số phần tử lẻ", MangSo.Count(so => so % 2 != 0));
        TrinhBay.Dong("b - Tổng các giá trị", MangSo.Sum());
        TrinhBay.Dong("b - Giá trị lớn nhất", MangSo.Max());
        TrinhBay.Dong("b - Giá trị nhỏ nhất", MangSo.Min());
        TrinhBay.Dong("c - Số giá trị khác nhau", MangSo.Distinct().Count());

        var nhomSoDu = MangSo.GroupBy(so => so % 5).OrderBy(nhom => nhom.Key);
        Console.WriteLine("d - Nhóm theo số dư khi chia cho 5:");
        foreach (var nhom in nhomSoDu)
            TrinhBay.DanhSach($"  Số dư {nhom.Key}", nhom);
    }
}
