using System;

namespace Bai08;

public static class NumberUtilities
{
    /// <summary>Dùng tham chiếu ref để đổi trực tiếp giá trị của hai biến.</summary>
    public static void Swap(ref double a, ref double b)
    {
        double temporary = a;
        a = b;
        b = temporary;
    }
}

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        double a = ReadDouble("Nhập số thực a: ");
        double b = ReadDouble("Nhập số thực b: ");
        Console.WriteLine($"Trước hoán vị: a = {a}, b = {b}");
        NumberUtilities.Swap(ref a, ref b);
        Console.WriteLine($"Sau hoán vị: a = {a}, b = {b}");
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
