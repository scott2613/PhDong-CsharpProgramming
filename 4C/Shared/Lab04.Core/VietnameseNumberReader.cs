using System;
using System.Collections.Generic;

namespace Lab04.Core;

/// <summary>Đọc số nguyên dương từ 1 đến 999 thành chữ tiếng Việt.</summary>
public static class VietnameseNumberReader
{
    private static readonly string[] Digits =
    {
        "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín"
    };

    public static bool TryRead(string text, out string words, out string error)
    {
        if (!int.TryParse(text.Trim(), out int number) || number is < 1 or > 999)
        {
            words = string.Empty;
            error = "Số cần đọc phải là số nguyên từ 1 đến 999.";
            return false;
        }

        words = Read(number);
        error = string.Empty;
        return true;
    }

    public static string Read(int number)
    {
        if (number is < 1 or > 999)
            throw new ArgumentOutOfRangeException(nameof(number), "Chỉ đọc số từ 1 đến 999.");

        var parts = new List<string>();
        int hundreds = number / 100;
        int remainder = number % 100;

        if (hundreds > 0)
        {
            parts.Add($"{Digits[hundreds]} trăm");
            if (remainder is > 0 and < 10) parts.Add("lẻ");
        }

        int tens = remainder / 10;
        int units = remainder % 10;
        if (tens > 1) parts.Add($"{Digits[tens]} mươi");
        else if (tens == 1) parts.Add("mười");

        if (units > 0)
        {
            string unitWord = units switch
            {
                1 when tens > 1 => "mốt",
                5 when tens > 0 => "lăm",
                _ => Digits[units]
            };
            parts.Add(unitWord);
        }

        return string.Join(" ", parts);
    }
}
