using System;
using System.Globalization;

namespace Bai01_05_DonThuc;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("vi-VN");
        try
        {
            double a = Nhap.SoThuc("Hệ số a: "); int n = Nhap.SoNguyen("Số mũ n: ", 0, 1000);
            double x = Nhap.SoThuc("x = "); var p = new DonThuc(a, n);
            Console.WriteLine($"P(x) = {p}"); Console.WriteLine($"P({x}) = {p.Tinh(x):G10}");
            Console.WriteLine($"P'(x) = {p.DaoHam()}");
        }
        catch (System.IO.EndOfStreamException e) { Console.WriteLine(e.Message); }
        catch (ArgumentException e) { Console.WriteLine("Lỗi dữ liệu: " + e.Message); }
        catch (ArithmeticException e) { Console.WriteLine("Lỗi tính toán: " + e.Message); }
    }
}
