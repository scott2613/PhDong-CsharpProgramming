using System;
using System.Globalization;

namespace Bai02_02_PersonList;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("vi-VN");
        try
        {
            var list = new PersonList(); list.Input();
            Console.WriteLine("Toàn bộ nhân khẩu"); list.Output();
            var living = list.LivingPeople(); Console.WriteLine($"Còn sống: {living.Count}"); living.Output();
            Console.WriteLine($"Bản sao có {new PersonList(list).Count} người.");
        }
        catch (System.IO.EndOfStreamException e) { Console.WriteLine(e.Message); }
        catch (ArgumentException e) { Console.WriteLine("Lỗi dữ liệu: " + e.Message); }
        catch (ArithmeticException e) { Console.WriteLine("Lỗi tính toán: " + e.Message); }
    }
}
