using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab04.Core;

/// <summary>Quản lý dãy số nguyên và cung cấp các phép tổng hợp theo yêu cầu Bài 4.</summary>
public sealed class IntegerSequence
{
    private readonly List<int> _numbers = new();

    public IReadOnlyList<int> Numbers => _numbers;
    public int Sum => _numbers.Sum();
    public int EvenSum => _numbers.Where(number => number % 2 == 0).Sum();
    public int OddSum => _numbers.Where(number => number % 2 != 0).Sum();

    public bool TryAdd(string text, out string error)
    {
        if (!int.TryParse(text.Trim(), out int value))
        {
            error = "Vui lòng nhập một số nguyên hợp lệ.";
            return false;
        }

        _numbers.Add(value);
        error = string.Empty;
        return true;
    }

    public void Clear() => _numbers.Clear();
}
