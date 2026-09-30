using System;

namespace Bai05;

public static class MenuCalculator
{
    /// <summary>Tính lũy thừa x mũ y.</summary>
    public static double Power(double x, double y) => Math.Pow(x, y);

    /// <summary>Tính căn bậc hai của x và y; số âm không có căn thực.</summary>
    public static (double X, double Y) SquareRoots(double x, double y)
    {
        if (x < 0 || y < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(x), "Không thể tính căn bậc hai thực của số âm.");
        }

        return (Math.Sqrt(x), Math.Sqrt(y));
    }
}

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        double x = 0;
        double y = 0;
        bool hasValues = false;

        while (true)
        {
            PrintMenu();
            Console.Write("Chọn chức năng: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    x = ReadDouble("Nhập x: ");
                    y = ReadDouble("Nhập y: ");
                    hasValues = true;
                    break;
                case "2" when hasValues:
                    Console.WriteLine($"{x} mũ {y} = {MenuCalculator.Power(x, y)}");
                    break;
                case "3" when hasValues && x >= 0 && y >= 0:
                    (double rootX, double rootY) = MenuCalculator.SquareRoots(x, y);
                    Console.WriteLine($"Căn bậc hai của x = {rootX}");
                    Console.WriteLine($"Căn bậc hai của y = {rootY}");
                    break;
                case "3" when hasValues:
                    Console.WriteLine("Không thể tính căn bậc hai thực của số âm.");
                    break;
                case "2" or "3":
                    Console.WriteLine("Bạn cần chọn chức năng 1 để nhập x và y trước.");
                    break;
                case "4":
                    Console.WriteLine("Đã thoát chương trình.");
                    return;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn từ 1 đến 4.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine("MENU");
        Console.WriteLine("1. Nhập hai giá trị số thực cho x, y");
        Console.WriteLine("2. Tính x^y");
        Console.WriteLine("3. Tính căn bậc 2 của x và y");
        Console.WriteLine("4. Thoát");
    }

    /// <summary>Đọc số thực và lặp lại nếu người dùng nhập sai định dạng.</summary>
    private static double ReadDouble(string message)
    {
        while (true)
        {
            Console.Write(message);
            if (double.TryParse(Console.ReadLine(), out double value))
            {
                return value;
            }

            Console.WriteLine("Dữ liệu không hợp lệ. Vui lòng nhập một số thực.");
        }
    }
}
