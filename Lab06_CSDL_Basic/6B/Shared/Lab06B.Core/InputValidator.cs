using System;
using System.Globalization;

namespace Lab06B.Core;

/// <summary>Các quy tắc kiểm tra dữ liệu được tách riêng để dễ tự kiểm thử.</summary>
public static class InputValidator
{
    public static bool HasText(string? value) => !string.IsNullOrWhiteSpace(value);

    public static bool IsAtLeast17(DateTime birthDate, DateTime today) =>
        today.Year - birthDate.Year >= 17;

    public static bool TryReadScore(string? text, out decimal score)
    {
        string normalized = (text ?? string.Empty).Trim().Replace(',', '.');
        return decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.InvariantCulture, out score)
               && score > 0 && score <= 10;
    }

    public static bool TryReadPositiveInteger(string? text, out int value) =>
        int.TryParse(text, out value) && value > 0;
}
