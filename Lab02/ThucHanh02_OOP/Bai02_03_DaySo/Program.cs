using System;
using System.Globalization;

namespace Bai02_03_DaySo;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("vi-VN");
        try
        {
            var a = new DaySo(); a.Input(); Console.WriteLine("Dãy số"); a.Output();
            Console.WriteLine("Các số chẵn"); a.SoChan().Output();
        }
        catch (System.IO.EndOfStreamException e) { Console.WriteLine(e.Message); }
        catch (ArgumentException e) { Console.WriteLine("Lỗi dữ liệu: " + e.Message); }
        catch (ArithmeticException e) { Console.WriteLine("Lỗi tính toán: " + e.Message); }
    }
}
