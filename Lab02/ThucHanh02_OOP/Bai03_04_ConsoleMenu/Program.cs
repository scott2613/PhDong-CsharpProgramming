using System;
using System.Globalization;

namespace Bai03_04_ConsoleMenu;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("vi-VN");
        try
        {
            var app = new PTBac2Console();
            app.Choose += (_, e) => Console.WriteLine($"[Sự kiện Choose] Đã thực hiện chức năng {e.Choice}.");
            app.Run();
        }
        catch (System.IO.EndOfStreamException e) { Console.WriteLine(e.Message); }
        catch (ArgumentException e) { Console.WriteLine("Lỗi dữ liệu: " + e.Message); }
        catch (ArithmeticException e) { Console.WriteLine("Lỗi tính toán: " + e.Message); }
    }
}
