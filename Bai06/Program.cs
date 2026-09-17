using System;

namespace Bai06;

public static class NumberUtilities
{
    /// <summary>Trả về giá trị lớn nhất trong ba số nguyên.</summary>
    public static int Max(int a, int b, int c)
    {
        int max = a;
        if (b > max) max = b;
        if (c > max) max = c;
        return max;
    }
}

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        int a = ReadInteger("Nhập a: ");
        int b = ReadInteger("Nhập b: ");
        int c = ReadInteger("Nhập c: ");
        Console.WriteLine($"Số lớn nhất là: {NumberUtilities.Max(a, b, c)}");
    }

    private static int ReadInteger(string message)
    {
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out int value)) return value;
            Console.WriteLine("Dữ liệu không hợp lệ. Vui lòng nhập số nguyên.");
        }
    }
}
