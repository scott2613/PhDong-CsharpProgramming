// Lớp nghiệp vụ dùng chung của Lab04C, được tách khỏi giao diện để dễ tái sử dụng và kiểm tra.
using System;

namespace Lab04.Core;

public enum ArithmeticOperation
{
    Add,
    Subtract,
    Multiply,
    Divide
}

/// <summary>Thực hiện bốn phép tính cơ bản và trả thông báo khi phép toán không hợp lệ.</summary>
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

        // Dùng sai số nhỏ thay vì so sánh số thực trực tiếp với 0.
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
