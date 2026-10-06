using System;

namespace Lab04D.Core;

public enum PhepToan
{
    Cong,
    Tru,
    Nhan,
    Chia
}

/// <summary>Lưu hai toán hạng và thực hiện bốn phép tính của bài mẫu 1.</summary>
public sealed class TinhToan
{
    public double A { get; set; }
    public double B { get; set; }

    public TinhToan() { }
    public TinhToan(double a, double b) { A = a; B = b; }

    public double Cong() => A + B;
    public double Tru() => A - B;
    public double Nhan() => A * B;
    public double Chia() => A / B;

    public bool TryCalculate(PhepToan operation, out double result, out string error)
    {
        if (operation == PhepToan.Chia && Math.Abs(B) < double.Epsilon)
        {
            result = 0;
            error = "Không thể chia cho 0.";
            return false;
        }

        result = operation switch
        {
            PhepToan.Cong => Cong(),
            PhepToan.Tru => Tru(),
            PhepToan.Nhan => Nhan(),
            PhepToan.Chia => Chia(),
            _ => throw new ArgumentOutOfRangeException(nameof(operation))
        };
        error = string.Empty;
        return true;
    }
}
