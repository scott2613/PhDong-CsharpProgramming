using System;
using System.Linq;

namespace BaiThucHanhLINQ;

internal static class Bai32
{
    internal static readonly string[] MonAn =
    [
        "Bún bò Huế", "Hủ tiếu heo", "Bánh canh", "Bánh mì", "Nước Cà phê", "Mì quảng",
        "Cơm tấm", "Nước Chanh dây", "Mì xào", "Bún riêu", "Bánh cuốn", "Mì gói",
        "Bún chả", "Hủ tiếu Nam vang"
    ];

    public static void Run()
    {
        TrinhBay.TieuDe("BÀI 3.2 - THỐNG KÊ MẢNG CHUỖI");
        int nganNhat = MonAn.Min(mon => mon.Length);
        int daiNhat = MonAn.Max(mon => mon.Length);
        TrinhBay.DanhSach("a - Tên ngắn nhất", MonAn.Where(mon => mon.Length == nganNhat));
        TrinhBay.DanhSach("a - Tên dài nhất", MonAn.Where(mon => mon.Length == daiNhat));

        var theoTuDau = MonAn.GroupBy(mon => mon.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]);
        Console.WriteLine("b - Nhóm theo từ đầu tiên:");
        foreach (var nhom in theoTuDau)
            TrinhBay.DanhSach($"  {nhom.Key}", nhom);

        TrinhBay.Dong("c - Số món bắt đầu bằng Bánh", theoTuDau.First(nhom => nhom.Key == "Bánh").Count());
    }
}
