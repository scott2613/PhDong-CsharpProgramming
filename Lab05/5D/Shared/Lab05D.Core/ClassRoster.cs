// Nghiệp vụ chuyển, xóa và thêm học viên giữa lớp A và lớp B.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab05D.Core;

public sealed class ClassRoster
{
    private readonly List<string> _classA = new List<string> { "Trần Hùng", "Nguyễn Hương", "Lâm Tú" };
    private readonly List<string> _classB = new List<string> { "Tuấn Hùng", "Hồng Ngọc", "Ánh Minh", "Bảo Lâm" };

    public IReadOnlyList<string> ClassA => _classA;
    public IReadOnlyList<string> ClassB => _classB;

    public bool Add(string? fullName, string className, out string error)
    {
        string name = (fullName ?? string.Empty).Trim();
        if (name.Length == 0) { error = "Vui lòng nhập họ và tên học viên."; return false; }
        if (_classA.Concat(_classB).Any(item => item.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
        {
            error = "Học viên đã có trong danh sách.";
            return false;
        }
        Target(className).Add(name);
        error = string.Empty;
        return true;
    }

    public int Move(IEnumerable<int> indices, string fromClass, string toClass)
    {
        List<string> source = Target(fromClass);
        List<string> destination = Target(toClass);
        int[] selected = indices.Distinct().Where(index => index >= 0 && index < source.Count).OrderByDescending(index => index).ToArray();
        foreach (int index in selected)
        {
            string name = source[index];
            source.RemoveAt(index);
            destination.Add(name);
        }
        return selected.Length;
    }

    public int Move(string fromClass, string toClass, IEnumerable<string> names)
    {
        HashSet<string> selected = new HashSet<string>(names, StringComparer.CurrentCultureIgnoreCase);
        List<string> source = Target(fromClass);
        List<string> destination = Target(toClass);
        List<string> moving = source.Where(selected.Contains).ToList();
        foreach (string name in moving) { source.Remove(name); destination.Add(name); }
        return moving.Count;
    }

    public int Remove(string className, IEnumerable<string> names)
    {
        HashSet<string> selected = new HashSet<string>(names, StringComparer.CurrentCultureIgnoreCase);
        return Target(className).RemoveAll(selected.Contains);
    }

    public int Remove(IEnumerable<int> classAIndices, IEnumerable<int> classBIndices) =>
        RemoveFrom(_classA, classAIndices) + RemoveFrom(_classB, classBIndices);

    private static int RemoveFrom(List<string> source, IEnumerable<int> indices)
    {
        int[] selected = indices.Distinct().Where(index => index >= 0 && index < source.Count).OrderByDescending(index => index).ToArray();
        foreach (int index in selected) source.RemoveAt(index);
        return selected.Length;
    }

    private List<string> Target(string className) =>
        className.Equals("Lớp B", StringComparison.OrdinalIgnoreCase) || className.Equals("B", StringComparison.OrdinalIgnoreCase)
            ? _classB : _classA;
}
