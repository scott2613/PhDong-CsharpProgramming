// Lớp nghiệp vụ của bài xử lý chuỗi tên, gom các thao tác để Form chỉ cập nhật giao diện.
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Lab05C.Core;

/// <summary>Tạo ngẫu nhiên và biến đổi danh sách họ tên tiếng Việt.</summary>
public static class NameProcessor
{
    public static readonly string[] Surnames = { "Lê", "Nguyễn", "Lý", "Trần", "Lâm", "Hồ", "Lai", "Huỳnh", "La" };
    public static readonly string[] MiddleNames = { "Quang", "Thành", "Ngọc", "Anh", "Xuân", "Bảo", "Cẩm", "Thị", "Kim", "Thái", "Hồng" };
    public static readonly string[] GivenNames = { "Hà", "Danh", "Sơn", "Mai", "Thắng", "Kỳ", "Thành", "Lâm", "Tâm", "Phụng", "Thắm" };

    public static IReadOnlyList<string> Generate(int count, int? seed = null)
    {
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
        Random random = seed.HasValue ? new Random(seed.Value) : Random.Shared;
        return Enumerable.Range(0, count).Select(_ =>
            $"{Surnames[random.Next(Surnames.Length)]} {MiddleNames[random.Next(MiddleNames.Length)]} {GivenNames[random.Next(GivenNames.Length)]}").ToArray();
    }

    public static bool HasSurname(string fullName, string surname) =>
        fullName.Trim().StartsWith(surname.Trim() + " ", StringComparison.CurrentCultureIgnoreCase);

    public static bool HasGivenName(string fullName, string givenName) =>
        fullName.Trim().EndsWith(" " + givenName.Trim(), StringComparison.CurrentCultureIgnoreCase)
        || fullName.Trim().Equals(givenName.Trim(), StringComparison.CurrentCultureIgnoreCase);

    public static string ToTitleCase(string value)
    {
        string normalized = string.Join(' ', value.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries));
        return CultureInfo.GetCultureInfo("vi-VN").TextInfo.ToTitleCase(normalized.ToLower(CultureInfo.GetCultureInfo("vi-VN")));
    }
}
