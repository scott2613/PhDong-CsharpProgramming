using System;
using System.Globalization;

namespace Bai01_04_PhanSo;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("vi-VN");
        try
        {
            Console.WriteLine("Phân số A"); var a = PhanSo.Input();
            Console.WriteLine("Phân số B"); var b = PhanSo.Input();
            Console.WriteLine($"A = {a}; B = {b}; +A = {+a}; -A = {-a}");
            Console.WriteLine($"A+B = {a + b}; A-B = {a - b}; A*B = {a * b}");
            if (b.Tu != 0) Console.WriteLine($"A/B = {a / b}");
            else Console.WriteLine("Không thể chia cho phân số 0.");
            Console.WriteLine($"A>B: {a > b}; A<B: {a < b}; A>=B: {a >= b}; A<=B: {a <= b}");
            Console.WriteLine($"A==B: {a == b}; A!=B: {a != b}");
        }
        catch (System.IO.EndOfStreamException e) { Console.WriteLine(e.Message); }
        catch (ArgumentException e) { Console.WriteLine("Lỗi dữ liệu: " + e.Message); }
        catch (ArithmeticException e) { Console.WriteLine("Lỗi tính toán: " + e.Message); }
    }
}
