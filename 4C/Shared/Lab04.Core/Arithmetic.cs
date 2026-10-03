using System;

namespace Lab04.Core;

public enum ArithmeticOperation
{
    Add,
    Subtract,
    Multiply,
    Divide
}

public static class Arithmetic
{
    public static bool TryCalculate(
        double first,
        double second,
        ArithmeticOperation operation,
        out double result,
        out string error)
    {
        result = 0;
        error = string.Empty;

        if (operation == ArithmeticOperation.Divide && Math.Abs(second) < 1e-12)
        {
            error = "Không thể chia cho 0.";
            return false;
        }

        result = operation switch
        {
            ArithmeticOperation.Add => first + second,
            ArithmeticOperation.Subtract => first - second,
            ArithmeticOperation.Multiply => first * second,
            ArithmeticOperation.Divide => first / second,
            _ => throw new ArgumentOutOfRangeException(nameof(operation))
        };
        return true;
    }
}
