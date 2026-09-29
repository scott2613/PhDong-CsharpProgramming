using System;
using System.Globalization;
using System.IO;

namespace Bai03_01_ArraySort;

/// <summary>Đọc dữ liệu có kiểm tra; báo hết dữ liệu thay vì lặp vô hạn khi đóng luồng nhập.</summary>
internal static class Nhap
{
    public static string Chuoi(string nhan)
    {
        while (true)
        {
            Console.Write(nhan);
            string s = Console.ReadLine() ?? throw new EndOfStreamException("Đã kết thúc dữ liệu nhập.");
            if (!string.IsNullOrWhiteSpace(s)) return s.Trim();
            Console.WriteLine("Không được để trống.");
        }
    }
    public static int SoNguyen(string nhan, int min = int.MinValue, int max = int.MaxValue)
    {
        while (true)
        {
            string s = Chuoi(nhan);
            if (int.TryParse(s, out int n) && n >= min && n <= max) return n;
            Console.WriteLine($"Nhập số nguyên trong [{min}, {max}].");
        }
    }
    public static double SoThuc(string nhan, double min = double.MinValue, double max = double.MaxValue)
    {
        while (true)
        {
            string s = Chuoi(nhan).Replace(',', '.');
            if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out double n)
                && double.IsFinite(n) && n >= min && n <= max) return n;
            Console.WriteLine("Số thực không hợp lệ hoặc ngoài khoảng cho phép.");
        }
    }
    public static decimal Tien(string nhan)
    {
        while (true)
        {
            string s = Chuoi(nhan).Replace(',', '.');
            if (decimal.TryParse(s, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal n) && n >= 0)
                return n;
            Console.WriteLine("Nhập số tiền không âm, không có dấu phân cách hàng nghìn.");
        }
    }
}
