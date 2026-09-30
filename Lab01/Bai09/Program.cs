using System;

namespace Bai09;

public static class NumberUtilities
{
    /// <summary>Trả đồng thời giá trị nhỏ nhất và lớn nhất qua hai tham số out.</summary>
    public static void FindMinMax(double a, double b, double c, out double min, out double max)
    {
        min = Math.Min(a, Math.Min(b, c));
        max = Math.Max(a, Math.Max(b, c));
    }
}

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        double a = ReadDouble("Nhập a: ");
        double b = ReadDouble("Nhập b: ");
        double c = ReadDouble("Nhập c: ");
        NumberUtilities.FindMinMax(a, b, c, out double min, out double max);
        Console.WriteLine($"Giá trị nhỏ nhất: {min}");
        Console.WriteLine($"Giá trị lớn nhất: {max}");
    }

    private static double ReadDouble(string message)
    {
        while (true)
        {
            Console.Write(message);
            if (double.TryParse(Console.ReadLine(), out double value)) return value;
            Console.WriteLine("Dữ liệu không hợp lệ. Vui lòng nhập số thực.");
        }
    }
}
