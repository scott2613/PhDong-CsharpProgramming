using System;

namespace Bai04;

public static class PowerInput
{
    /// <summary>Kiểm tra hai chuỗi là số nguyên trước khi tính lũy thừa.</summary>
    public static bool TryCalculate(string? xText, string? yText, out double result)
    {
        result = 0;
        if (!int.TryParse(xText, out int x) || !int.TryParse(yText, out int y))
        {
            return false;
        }

        result = Math.Pow(x, y);
        return true;
    }
}

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Write("Nhập số nguyên x: ");
        string? xText = Console.ReadLine();
        Console.Write("Nhập số nguyên y: ");
        string? yText = Console.ReadLine();

        if (!PowerInput.TryCalculate(xText, yText, out double result))
        {
            Console.WriteLine("Lỗi: x và y phải là số nguyên.");
            return;
        }

        Console.WriteLine($"Kết quả {xText} mũ {yText} là: {result}");
    }
}
