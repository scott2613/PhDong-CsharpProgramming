using System;
using System.Globalization;

namespace Bai02_03b_DaThuc;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("vi-VN");
        try
        {
            var p = new DaThuc(); p.Input(); p.Output();
            double x = Nhap.SoThuc("x = "); Console.WriteLine($"P({x}) = {p.Tinh(x):G10}");
            Console.WriteLine($"Đơn thức thứ 0: {p[0]}");
        }
        catch (System.IO.EndOfStreamException e) { Console.WriteLine(e.Message); }
        catch (ArgumentException e) { Console.WriteLine("Lỗi dữ liệu: " + e.Message); }
        catch (ArithmeticException e) { Console.WriteLine("Lỗi tính toán: " + e.Message); }
    }
}
