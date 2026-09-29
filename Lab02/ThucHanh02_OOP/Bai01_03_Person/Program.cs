using System;
using System.Globalization;

namespace Bai01_03_Person;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("vi-VN");
        try
        {
            var p = new Person(); p.Input(); p.Output();
            Console.WriteLine($"IsLiving = {p.IsLiving()}");
            var copy = new Person(p); Console.WriteLine($"Bản sao: {copy}");
        }
        catch (System.IO.EndOfStreamException e) { Console.WriteLine(e.Message); }
        catch (ArgumentException e) { Console.WriteLine("Lỗi dữ liệu: " + e.Message); }
        catch (ArithmeticException e) { Console.WriteLine("Lỗi tính toán: " + e.Message); }
    }
}
