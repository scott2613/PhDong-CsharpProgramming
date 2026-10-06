using System;
using Lab04D.Core;

namespace Lab04D.SelfTest;

internal static class Program
{
    private static int _passed;

    private static void Main()
    {
        Check(NumericValidation.TryParseDouble("12.5", out double dot) && dot == 12.5, "Nhập số dùng dấu chấm");
        Check(NumericValidation.TryParseDouble("12,5", out double comma) && comma == 12.5, "Nhập số dùng dấu phẩy");
        Check(!NumericValidation.TryParseDouble("abc", out _), "Từ chối chuỗi không phải số");

        var calculation = new TinhToan(8, 2);
        Check(calculation.Cong() == 10, "Phép cộng");
        Check(calculation.Tru() == 6, "Phép trừ");
        Check(calculation.Nhan() == 16, "Phép nhân");
        Check(calculation.TryCalculate(PhepToan.Chia, out double quotient, out _) && quotient == 4, "Phép chia");
        calculation.B = 0;
        Check(!calculation.TryCalculate(PhepToan.Chia, out _, out _), "Chặn chia cho 0");

        Check(LabelStyleResolver.Resolve(true, false, false, false) == LabelTextStyle.Regular, "Kiểu chữ thường");
        Check(LabelStyleResolver.Resolve(false, true, false, false) == LabelTextStyle.Bold, "Kiểu chữ đậm");
        Check(LabelStyleResolver.Resolve(false, false, true, false) == LabelTextStyle.Italic, "Kiểu chữ nghiêng");
        Check(LabelStyleResolver.Resolve(false, true, true, false) == LabelTextStyle.BoldItalic, "Kiểu chữ đậm nghiêng");

        Check(PhuongTrinh.SolveLinear(2, -4).Contains("x = 2", StringComparison.Ordinal), "Phương trình bậc nhất một nghiệm");
        Check(PhuongTrinh.SolveLinear(0, 0).Contains("vô số", StringComparison.Ordinal), "Phương trình bậc nhất vô số nghiệm");
        Check(PhuongTrinh.SolveLinear(0, 2).Contains("vô nghiệm", StringComparison.Ordinal), "Phương trình bậc nhất vô nghiệm");
        Check(PhuongTrinh.SolveQuadratic(1, -3, 2).Contains("x1 = 2; x2 = 1", StringComparison.Ordinal), "Phương trình bậc hai hai nghiệm");
        Check(PhuongTrinh.SolveQuadratic(1, -2, 1).Contains("nghiệm kép x = 1", StringComparison.Ordinal), "Phương trình bậc hai nghiệm kép");
        Check(PhuongTrinh.SolveQuadratic(1, 0, 1).Contains("vô nghiệm", StringComparison.Ordinal), "Phương trình bậc hai vô nghiệm");
        Check(PhuongTrinh.SolveQuadratic(0, 2, -4).Contains("x = 2", StringComparison.Ordinal), "Bậc hai suy biến về bậc nhất");

        Console.WriteLine($"KIỂM TRA THÀNH CÔNG: {_passed}/19 điều kiện đạt.");
    }

    private static void Check(bool condition, string name)
    {
        if (!condition) throw new InvalidOperationException($"Kiểm tra thất bại: {name}");
        _passed++;
    }
}
