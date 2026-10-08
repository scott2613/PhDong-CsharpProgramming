// Lớp nghiệp vụ dùng chung của Lab04C, được tách khỏi giao diện để dễ tái sử dụng và kiểm tra.
using System;

namespace Lab04.Core;

/// <summary>Cung cấp phép tìm ước chung lớn nhất và bội chung nhỏ nhất của hai số nguyên.</summary>
public static class NumberTheory
{
    public static int GreatestCommonDivisor(int first, int second)
    {
        first = Math.Abs(first);
        second = Math.Abs(second);
        // Thuật toán Euclid liên tục thay cặp số bằng số chia và phần dư.
        while (second != 0)
        {
            int remainder = first % second;
            first = second;
            second = remainder;
        }
        return first;
    }

    public static long LeastCommonMultiple(int first, int second)
    {
        int gcd = GreatestCommonDivisor(first, second);
        // Chia trước khi nhân giúp hạn chế tràn số trung gian; nếu cả hai bằng 0 thì trả về 0.
        return gcd == 0 ? 0 : Math.Abs((long)first / gcd * second);
    }
}
