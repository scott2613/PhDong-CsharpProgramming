using System;
using Lab06B.Core;

namespace Lab06B.SelfTest;

internal static class Program
{
    private static int _passed;
    private static int _failed;

    private static void Main()
    {
        Check("Chuỗi hợp lệ", InputValidator.HasText("CNTT"));
        Check("Chuỗi rỗng", !InputValidator.HasText("  "));
        Check("Đủ 17 tuổi", InputValidator.IsAtLeast17(new DateTime(2009, 12, 31), new DateTime(2026, 1, 1)));
        Check("Chưa đủ 17 tuổi", !InputValidator.IsAtLeast17(new DateTime(2010, 1, 1), new DateTime(2026, 12, 31)));
        Check("Điểm 0 không hợp lệ", !InputValidator.TryReadScore("0", out _));
        Check("Điểm 10 hợp lệ", InputValidator.TryReadScore("10", out _));
        Check("Điểm dấu chấm", InputValidator.TryReadScore("8.5", out _));
        Check("Điểm dấu phẩy", InputValidator.TryReadScore("8,75", out _));
        Check("Điểm lớn hơn 10", !InputValidator.TryReadScore("10.1", out _));
        Check("Điểm không phải số", !InputValidator.TryReadScore("abc", out _));
        Check("Số lượng dương", InputValidator.TryReadPositiveInteger("3", out int quantity) && quantity == 3);
        Check("Số lượng bằng 0", !InputValidator.TryReadPositiveInteger("0", out _));
        Check("Số lượng thập phân", !InputValidator.TryReadPositiveInteger("2.5", out _));
        Console.WriteLine($"Kết quả: {_passed} đạt, {_failed} lỗi.");
        Environment.ExitCode = _failed == 0 ? 0 : 1;
    }

    private static void Check(string name, bool condition)
    {
        if (condition) { _passed++; Console.WriteLine($"[ĐẠT] {name}"); }
        else { _failed++; Console.WriteLine($"[LỖI] {name}"); }
    }
}
