using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Lab04D.Core;

/// <summary>Lưu trữ và xử lý mảng một chiều số nguyên cho bài tập tại lớp 2.</summary>
public sealed class MangSoNguyen
{
    private readonly List<int> _values;

    public MangSoNguyen(IEnumerable<int> values)
    {
        _values = values?.ToList() ?? throw new ArgumentNullException(nameof(values));
        if (_values.Count == 0) throw new ArgumentException("Mảng phải có ít nhất một phần tử.", nameof(values));
    }

    public IReadOnlyList<int> Values => _values;
    public int Count => _values.Count;
    public long Sum => _values.Sum(value => (long)value);
    public long EvenSum => _values.Where(value => value % 2 == 0).Sum(value => (long)value);
    public long OddSum => _values.Where(value => value % 2 != 0).Sum(value => (long)value);
    public int Maximum => _values.Max();
    public int Minimum => _values.Min();

    public static bool TryParse(string? text, out MangSoNguyen? array, out string error)
    {
        string[] parts = (text ?? string.Empty).Split(
            new[] { ' ', ',', ';', '\t', '\r', '\n' },
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (parts.Length == 0)
        {
            array = null;
            error = "Vui lòng nhập ít nhất một số nguyên.";
            return false;
        }

        var values = new List<int>(parts.Length);
        foreach (string part in parts)
        {
            if (!int.TryParse(part, NumberStyles.Integer, CultureInfo.InvariantCulture, out int value))
            {
                array = null;
                error = $"'{part}' không phải là số nguyên hợp lệ.";
                return false;
            }
            values.Add(value);
        }

        array = new MangSoNguyen(values);
        error = string.Empty;
        return true;
    }

    public void SortAscending() => _values.Sort();
    public void SortDescending() => _values.Sort((left, right) => right.CompareTo(left));
    public int FindValue(int value) => _values.IndexOf(value);

    public int ValueAt(int oneBasedPosition)
    {
        int index = ToIndex(oneBasedPosition, false);
        return _values[index];
    }

    public void InsertAt(int oneBasedPosition, int value)
    {
        int index = ToIndex(oneBasedPosition, true);
        _values.Insert(index, value);
    }

    public bool RemoveValue(int value) => _values.Remove(value);

    public int RemoveAt(int oneBasedPosition)
    {
        int index = ToIndex(oneBasedPosition, false);
        int removed = _values[index];
        _values.RemoveAt(index);
        return removed;
    }

    public int ReplaceValue(int oldValue, int newValue)
    {
        int count = 0;
        for (int index = 0; index < _values.Count; index++)
        {
            if (_values[index] != oldValue) continue;
            _values[index] = newValue;
            count++;
        }
        return count;
    }

    public int ReplaceAt(int oneBasedPosition, int newValue)
    {
        int index = ToIndex(oneBasedPosition, false);
        int oldValue = _values[index];
        _values[index] = newValue;
        return oldValue;
    }

    public override string ToString() => string.Join(" ", _values);

    private int ToIndex(int oneBasedPosition, bool allowEnd)
    {
        int maximum = allowEnd ? _values.Count + 1 : _values.Count;
        if (oneBasedPosition < 1 || oneBasedPosition > maximum)
            throw new ArgumentOutOfRangeException(nameof(oneBasedPosition), $"Vị trí phải từ 1 đến {maximum}.");
        return oneBasedPosition - 1;
    }
}
