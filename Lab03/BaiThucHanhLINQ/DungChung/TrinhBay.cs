using System;
using System.Collections.Generic;

namespace BaiThucHanhLINQ;

/// <summary>Chuẩn hóa nhãn và cách in kết quả để từng câu dễ đối chiếu.</summary>
internal static class TrinhBay
{
    public static void TieuDe(string tenBai) => Console.WriteLine($"\n===== {tenBai} =====");

    public static void DanhSach<T>(string nhan, IEnumerable<T> values)
        => Console.WriteLine($"{nhan}: {string.Join(", ", values)}");

    public static void Dong(string nhan, object? value) => Console.WriteLine($"{nhan}: {value}");
}
