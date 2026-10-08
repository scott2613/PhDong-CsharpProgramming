// Lớp nghiệp vụ dùng chung của Lab04C, được tách khỏi giao diện để dễ tái sử dụng và kiểm tra.
using System;
using System.Globalization;

namespace Lab04.Core;

/// <summary>Bộ máy tính bỏ túi đơn giản, độc lập với giao diện WinForms.</summary>
public sealed class PocketCalculator
{
    private decimal? _leftOperand;
    private char? _operation;
    private bool _startNewNumber = true;

    public string Display { get; private set; } = "0";

    public void EnterDigit(char digit)
    {
        if (!char.IsDigit(digit)) throw new ArgumentException("Ký tự phải là chữ số.", nameof(digit));
        Display = _startNewNumber || Display == "0" ? digit.ToString() : Display + digit;
        _startNewNumber = false;
    }

    public void SetOperation(char operation)
    {
        if (operation is not ('+' or '-' or '*' or '/'))
            throw new ArgumentException("Phép toán không hợp lệ.", nameof(operation));

        _leftOperand = decimal.Parse(Display, CultureInfo.InvariantCulture);
        _operation = operation;
        _startNewNumber = true;
    }

    public bool TryEvaluate(out string error)
    {
        if (_leftOperand is null || _operation is null)
        {
            error = "Hãy chọn phép toán trước khi tính kết quả.";
            return false;
        }

        decimal right = decimal.Parse(Display, CultureInfo.InvariantCulture);
        if (_operation == '/' && right == 0)
        {
            error = "Không thể chia cho 0.";
            return false;
        }

        decimal result = _operation switch
        {
            '+' => _leftOperand.Value + right,
            '-' => _leftOperand.Value - right,
            '*' => _leftOperand.Value * right,
            '/' => _leftOperand.Value / right,
            _ => throw new InvalidOperationException()
        };

        Display = result.ToString("0.##########", CultureInfo.InvariantCulture);
        _leftOperand = null;
        _operation = null;
        _startNewNumber = true;
        error = string.Empty;
        return true;
    }

    public void Clear()
    {
        Display = "0";
        _leftOperand = null;
        _operation = null;
        _startNewNumber = true;
    }
}
