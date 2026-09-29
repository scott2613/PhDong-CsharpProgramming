using System;
using System.Globalization;

namespace Bai03_03_DelegateSort;

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
            SapXep.Sort(a, (x, y) => y.Diem.CompareTo(x.Diem));
            Console.WriteLine("Giảm dần theo điểm"); foreach (var s in a) Console.WriteLine(s);
            SapXep.Sort(a, (x, y) => StringComparer.Ordinal.Compare(x.HoTen, y.HoTen));
            Console.WriteLine("Tăng dần theo tên"); foreach (var s in a) Console.WriteLine(s);
        }
        catch (System.IO.EndOfStreamException e) { Console.WriteLine(e.Message); }
        catch (ArgumentException e) { Console.WriteLine("Lỗi dữ liệu: " + e.Message); }
        catch (ArithmeticException e) { Console.WriteLine("Lỗi tính toán: " + e.Message); }
    }
}
