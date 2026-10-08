// Lớp nghiệp vụ dùng chung của Lab04D, được tách khỏi giao diện để dễ tái sử dụng và kiểm tra.
using System;
using System.Globalization;

namespace Lab04D.Core;

/// <summary>Giải phương trình bậc nhất và bậc hai, tách độc lập khỏi giao diện.</summary>
public static class PhuongTrinh
{
    private const double Epsilon = 1e-10;

    public static string SolveLinear(double a, double b)
    {
        if (Math.Abs(a) < Epsilon)
            return Math.Abs(b) < Epsilon ? "Phương trình vô số nghiệm." : "Phương trình vô nghiệm.";

        double x = -b / a;
        return $"Phương trình có nghiệm x = {Format(x)}";
    }

    public static string SolveQuadratic(double a, double b, double c)
    {
        if (Math.Abs(a) < Epsilon) return SolveLinear(b, c);

        double delta = b * b - 4 * a * c;
        if (delta < -Epsilon) return "Phương trình vô nghiệm.";
        if (Math.Abs(delta) < Epsilon)
        {
            double x = -b / (2 * a);
            return $"Phương trình có nghiệm kép x = {Format(x)}";
        }

        double squareRoot = Math.Sqrt(delta);
        double first = (-b + squareRoot) / (2 * a);
        double second = (-b - squareRoot) / (2 * a);
        return $"Phương trình có hai nghiệm x1 = {Format(first)}; x2 = {Format(second)}";
    }

    private static string Format(double value) => value.ToString("0.##########", CultureInfo.InvariantCulture);
}
