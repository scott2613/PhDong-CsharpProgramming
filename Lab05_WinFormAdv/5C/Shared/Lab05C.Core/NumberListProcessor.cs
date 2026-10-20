// Lớp nghiệp vụ cho bài ListBox số tự nhiên, chứa toàn bộ thao tác biến đổi danh sách.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab05C.Core;

/// <summary>Quản lý danh sách số tự nhiên và các thao tác được yêu cầu trên ListBox.</summary>
public sealed class NumberListProcessor
{
    private readonly List<int> _numbers = new();
    public IReadOnlyList<int> Numbers => _numbers;

    public bool TryAdd(string? text, out string error)
    {
        if (!int.TryParse(text?.Trim(), out int value) || value < 0)
        { error = "Vui lòng nhập một số tự nhiên."; return false; }
        _numbers.Add(value);
        error = string.Empty;
        return true;
    }

    public long Sum() => _numbers.Sum(number => (long)number);

    public void RemoveFirstAndLast()
    {
        if (_numbers.Count == 0) return;
        if (_numbers.Count == 1) { _numbers.Clear(); return; }
        _numbers.RemoveAt(_numbers.Count - 1);
        _numbers.RemoveAt(0);
    }

    public int RemoveSelected(IEnumerable<int> indices)
    {
        int[] valid = indices.Where(index => index >= 0 && index < _numbers.Count).Distinct().OrderByDescending(index => index).ToArray();
        foreach (int index in valid) _numbers.RemoveAt(index);
        return valid.Length;
    }

    public void AddTwo() { for (int index = 0; index < _numbers.Count; index++) _numbers[index] += 2; }
    public void Square() { for (int index = 0; index < _numbers.Count; index++) checked { _numbers[index] *= _numbers[index]; } }
    public IReadOnlyList<int> EvenIndices() => _numbers.Select((number, index) => (number, index)).Where(pair => pair.number % 2 == 0).Select(pair => pair.index).ToArray();
    public IReadOnlyList<int> OddIndices() => _numbers.Select((number, index) => (number, index)).Where(pair => pair.number % 2 != 0).Select(pair => pair.index).ToArray();
}
