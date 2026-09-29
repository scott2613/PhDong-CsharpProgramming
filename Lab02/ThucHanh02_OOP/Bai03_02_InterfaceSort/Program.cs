using System;
using System.Globalization;

namespace Bai03_02_InterfaceSort;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("vi-VN");
        try
        {
            int n = Nhap.SoNguyen("Số sinh viên: ", 0, 10000);
            var a = new SinhVien[n];
            for (int i = 0; i < n; i++) a[i] = new SinhVien(Nhap.Chuoi("Họ tên: "), Nhap.SoThuc("Điểm: ", 0, 10));
            SapXep.Sort(a); foreach (var s in a) Console.WriteLine(s);
            int[] numbers = { 4, 1, 3, 2 }; SapXep.Sort(numbers);
            Console.WriteLine($"Mảng int: {string.Join(", ", numbers)}");
        }
        catch (System.IO.EndOfStreamException e) { Console.WriteLine(e.Message); }
        catch (ArgumentException e) { Console.WriteLine("Lỗi dữ liệu: " + e.Message); }
        catch (ArithmeticException e) { Console.WriteLine("Lỗi tính toán: " + e.Message); }
    }
}
