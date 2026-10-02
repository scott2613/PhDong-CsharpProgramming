using System;

namespace Lab04.Core;

public static class NumberTheory
{
    public static int GreatestCommonDivisor(int first, int second)
    {
        first = Math.Abs(first);
        second = Math.Abs(second);
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
        return gcd == 0 ? 0 : Math.Abs((long)first / gcd * second);
    }
}
