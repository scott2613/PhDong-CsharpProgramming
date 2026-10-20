// Lớp nghiệp vụ của bài ước số, chịu trách nhiệm tính toán và kiểm tra số nhập.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab05C.Core;

/// <summary>Cung cấp danh sách và các thống kê liên quan đến ước số của số nguyên dương.</summary>
public static class DivisorAnalyzer
{
    public static bool TryParsePositive(string? text, out int value, out string error)
    {
        if (!int.TryParse(text?.Trim(), out value) || value <= 0)
        {
            error = "Vui lòng nhập một số nguyên dương.";
            return false;
        }
        error = string.Empty;
        return true;
    }

    public static IReadOnlyList<int> GetDivisors(int value)
    {
        if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
        var small = new List<int>();
        var large = new List<int>();
        // Chỉ duyệt tới căn bậc hai rồi ghép cặp ước để giảm số vòng lặp.
        for (int divisor = 1; divisor <= Math.Sqrt(value); divisor++)
        {
            if (value % divisor != 0) continue;
            small.Add(divisor);
            if (divisor != value / divisor) large.Add(value / divisor);
        }
        large.Reverse();
        small.AddRange(large);
        return small;
    }

    public static int Sum(IEnumerable<int> divisors) => divisors.Sum();
    public static int CountEven(IEnumerable<int> divisors) => divisors.Count(number => number % 2 == 0);
    public static int CountPrime(IEnumerable<int> divisors) => divisors.Count(IsPrime);

    public static bool IsPrime(int value)
    {
        if (value < 2) return false;
        for (int factor = 2; factor <= Math.Sqrt(value); factor++)
            if (value % factor == 0) return false;
        return true;
    }
}
