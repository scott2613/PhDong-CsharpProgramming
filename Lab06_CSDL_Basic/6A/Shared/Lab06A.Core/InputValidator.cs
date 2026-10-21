using System;
using System.Globalization;

namespace Lab06A.Core;

/// <summary>Các phép kiểm tra dữ liệu tách khỏi giao diện để dễ dùng lại và kiểm thử.</summary>
public static class InputValidator
{
    public static bool HasText(string? value) => !string.IsNullOrWhiteSpace(value);

    public static bool TryReadScore(string? text, out decimal score)
    {
        string normalized = (text ?? string.Empty).Trim().Replace(',', '.');
        return decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.InvariantCulture, out score)
            && score >= 0
            && score <= 10;
    }
}
