using System;
using Lab04.Core;

namespace Lab04.SelfTest;

internal static class Program
{
    private static int _passed;

    private static void Main()
    {
        Check(PersonInfo.TryCalculateAge("2006", 2026, out int age) && age == 20, "Tính tuổi");
        Check(!PersonInfo.TryCalculateAge("abc", 2026, out _), "Từ chối năm sinh sai");
        Check(Validation.TryParseNumber("12.5", out _), "Nhập số thực");
        Check(Arithmetic.TryCalculate(8, 2, ArithmeticOperation.Divide, out double quotient, out _) && quotient == 4, "Phép chia");
        Check(!Arithmetic.TryCalculate(8, 0, ArithmeticOperation.Divide, out _, out _), "Chặn chia cho 0");
        Check(Validation.IsValidEmail("dong@example.com"), "Email hợp lệ");
        Check(AccountRegistration.Validate("phuongdong", "dong@example.com", "123456", "123456", out _), "Đăng ký hợp lệ");
        Check(!AccountRegistration.Validate("phuongdong", "sai-email", "123456", "123456", out _), "Từ chối email sai");
        Check(NumberTheory.GreatestCommonDivisor(18, 24) == 6, "UCLN");
        Check(NumberTheory.LeastCommonMultiple(18, 24) == 72, "BCNN");

        Console.WriteLine($"KIỂM TRA THÀNH CÔNG: {_passed}/10 điều kiện đạt.");
    }

    private static void Check(bool condition, string name)
    {
        if (!condition) throw new InvalidOperationException($"Kiểm tra thất bại: {name}");
        _passed++;
    }
}
