// Lớp nghiệp vụ dùng chung của Lab04C, được tách khỏi giao diện để dễ tái sử dụng và kiểm tra.
using System;
using System.Globalization;
using System.Net.Mail;

namespace Lab04.Core;

/// <summary>Các hàm kiểm tra dữ liệu dùng chung cho bốn Form của nửa đầu Lab04.</summary>
public static class Validation
{
    public static bool TryParseNumber(string? text, out double value) =>
        double.TryParse(text, NumberStyles.Float, CultureInfo.CurrentCulture, out value) ||
        double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value);

    public static bool TryParsePositiveInteger(string? text, out int value) =>
        int.TryParse(text, out value) && value > 0;

    public static bool IsValidEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        try
        {
            MailAddress address = new(email.Trim());
            return string.Equals(address.Address, email.Trim(), StringComparison.OrdinalIgnoreCase);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
