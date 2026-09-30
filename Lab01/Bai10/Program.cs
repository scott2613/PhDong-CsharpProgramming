using System;

namespace Bai10;

public static class StringUtilities
{
    /// <summary>So sánh từng cặp ký tự từ hai đầu để kiểm tra chuỗi đối xứng.</summary>
    public static bool IsPalindrome(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        int left = 0;
        int right = value.Length - 1;

        while (left < right)
        {
            if (value[left] != value[right]) return false;
            left++;
            right--;
        }

        return true;
    }
}

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Write("Nhập chuỗi cần kiểm tra: ");
        string value = Console.ReadLine() ?? string.Empty;
        Console.WriteLine(StringUtilities.IsPalindrome(value)
            ? "Chuỗi đã nhập là chuỗi đối xứng."
            : "Chuỗi đã nhập không phải là chuỗi đối xứng.");
    }
}
