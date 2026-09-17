using System;

namespace Bai11;

public static class StringUtilities
{
    /// <summary>Chuyển chuỗi thành mảng ký tự rồi đảo thứ tự các phần tử.</summary>
    public static string Reverse(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        char[] characters = value.ToCharArray();
        Array.Reverse(characters);
        return new string(characters);
    }
}

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Write("Nhập chuỗi: ");
        string value = Console.ReadLine() ?? string.Empty;
        Console.WriteLine($"Chuỗi đảo ngược: {StringUtilities.Reverse(value)}");
    }
}
