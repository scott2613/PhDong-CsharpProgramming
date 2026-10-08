// Lớp nghiệp vụ dùng chung của Lab04D, được tách khỏi giao diện để dễ tái sử dụng và kiểm tra.
using System;
using System.Globalization;

namespace Lab04D.Core;

/// <summary>Các hàm kiểm tra dữ liệu số dùng chung cho ba ứng dụng WinForms.</summary>
public static class NumericValidation
{
    public static bool TryParseDouble(string text, out double value)
    {
        string normalized = text.Trim().Replace(',', '.');
        return double.TryParse(normalized, NumberStyles.Float, CultureInfo.InvariantCulture, out value)
            && double.IsFinite(value);
    }
}
