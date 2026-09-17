using System.Globalization;

namespace Bai12;

public static class StringUtilities
{
    /// <summary>Trả về dạng chữ thường, chữ hoa và số từ trong chuỗi.</summary>
    public static (string Lower, string Upper, int WordCount) Analyze(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        CultureInfo vietnamese = CultureInfo.GetCultureInfo("vi-VN");
        string[] words = value.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
        return (value.ToLower(vietnamese), value.ToUpper(vietnamese), words.Length);
    }
}

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Write("Nhập một chuỗi gồm nhiều từ: ");
        string value = Console.ReadLine() ?? string.Empty;
        (string lower, string upper, int wordCount) = StringUtilities.Analyze(value);
        Console.WriteLine($"Chuỗi chữ thường: {lower}");
        Console.WriteLine($"Chuỗi chữ hoa: {upper}");
        Console.WriteLine($"Số từ trong chuỗi: {wordCount}");
    }
}
