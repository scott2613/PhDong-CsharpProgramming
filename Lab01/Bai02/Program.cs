using System;

namespace Bai02;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Write("Nhập họ tên của bạn: ");
        string hoTen = Console.ReadLine()?.Trim() ?? string.Empty;

        // Lặp đến khi nhận được một họ tên có ít nhất một ký tự.
        while (hoTen.Length == 0)
        {
            Console.Write("Họ tên không được để trống. Nhập lại: ");
            hoTen = Console.ReadLine()?.Trim() ?? string.Empty;
        }

        Console.WriteLine($"Chào bạn {hoTen}!");
    }
}
