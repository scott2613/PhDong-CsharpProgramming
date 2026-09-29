using System;
using System.Globalization;

namespace Bai01_02_Point;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("vi-VN");
        try
        {
            var a = new Point(); var b = new Point();
            Console.WriteLine("Điểm A"); a.Input(); Console.WriteLine("Điểm B"); b.Input();
            Console.WriteLine($"A = {a}; B = {b}");
            Console.WriteLine($"Khoảng cách thành viên = {a.Distance(b):G8}");
            Console.WriteLine($"Khoảng cách tĩnh = {Point.Distance(a, b):G8}");
            Console.WriteLine($"Trung điểm thành viên = {a.Midpoint(b)}");
            Console.WriteLine($"Trung điểm tĩnh = {Point.Midpoint(a, b)}");
            Console.WriteLine($"A+B = {a + b}; A-B = {a - b}; -A = {-a}");
        }
        catch (System.IO.EndOfStreamException e) { Console.WriteLine(e.Message); }
        catch (ArgumentException e) { Console.WriteLine("Lỗi dữ liệu: " + e.Message); }
        catch (ArithmeticException e) { Console.WriteLine("Lỗi tính toán: " + e.Message); }
    }
}
