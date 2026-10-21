using System;
using Lab06A.Core;

namespace Lab06A.SelfTest;

internal static class Program
{
    private static int _passed;
    private static int _failed;

    private static int Main()
    {
        // Kiểm thử phần xử lý dữ liệu không cần kết nối SQL Server.
        Check("Chuỗi thường hợp lệ", InputValidator.HasText("CS001"));
        Check("Chuỗi rỗng không hợp lệ", !InputValidator.HasText(string.Empty));
        Check("Chuỗi khoảng trắng không hợp lệ", !InputValidator.HasText("   "));

        CheckScore("0", true, 0m);
        CheckScore("10", true, 10m);
        CheckScore("8.5", true, 8.5m);
        CheckScore("8,75", true, 8.75m);
        CheckScore("-1", false, 0m);
        CheckScore("10.1", false, 0m);
        CheckScore("abc", false, 0m);

        Console.WriteLine($"Kết quả: {_passed} đạt, {_failed} lỗi.");
        return _failed == 0 ? 0 : 1;
    }

    private static void CheckScore(string input, bool expectedValid, decimal expectedValue)
    {
        bool valid = InputValidator.TryReadScore(input, out decimal value);
        Check($"Điểm '{input}'", valid == expectedValid && (!valid || value == expectedValue));
    }

    private static void Check(string name, bool condition)
    {
        if (condition)
        {
            _passed++;
            Console.WriteLine("[ĐẠT] " + name);
        }
        else
        {
            _failed++;
            Console.WriteLine("[LỖI] " + name);
        }
    }
}
