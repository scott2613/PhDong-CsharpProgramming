using System;
using System.Globalization;

namespace Bai02_05_LuongPhongBan;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("vi-VN");
        try
        {
            var p = new PhongBan(); p.Input(); p.Output();
            Console.WriteLine($"Tổng lương phòng ban: {p.TongLuong():N0} VNĐ");
        }
        catch (System.IO.EndOfStreamException e) { Console.WriteLine(e.Message); }
        catch (ArgumentException e) { Console.WriteLine("Lỗi dữ liệu: " + e.Message); }
        catch (ArithmeticException e) { Console.WriteLine("Lỗi tính toán: " + e.Message); }
    }
}
