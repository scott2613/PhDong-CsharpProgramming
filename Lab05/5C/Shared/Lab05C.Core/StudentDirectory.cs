// Lớp nghiệp vụ của bài quản lý sinh viên, bảo đảm lớp và mã sinh viên không bị trùng.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab05C.Core;

public sealed record Student(string Id, string FullName, string Address);

/// <summary>Quản lý nhiều lớp học và danh sách sinh viên của từng lớp.</summary>
public sealed class StudentDirectory
{
    private readonly Dictionary<string, List<Student>> _classes = new(StringComparer.CurrentCultureIgnoreCase);
    public IReadOnlyCollection<string> Classes => _classes.Keys.OrderBy(name => name).ToArray();

    public bool AddClass(string? className, out string error)
    {
        className = className?.Trim();
        if (string.IsNullOrWhiteSpace(className)) { error = "Tên lớp không được để trống."; return false; }
        if (!_classes.TryAdd(className, new List<Student>())) { error = "Tên lớp đã tồn tại."; return false; }
        error = string.Empty;
        return true;
    }

    public bool AddStudent(string? className, string? id, string? fullName, string? address, out string error)
    {
        if (string.IsNullOrWhiteSpace(className) || !_classes.TryGetValue(className.Trim(), out List<Student>? students))
        { error = "Vui lòng chọn lớp hợp lệ."; return false; }
        id = id?.Trim(); fullName = fullName?.Trim(); address = address?.Trim();
        if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(address))
        { error = "Mã sinh viên, họ tên và địa chỉ không được để trống."; return false; }
        if (_classes.Values.SelectMany(list => list).Any(student => student.Id.Equals(id, StringComparison.CurrentCultureIgnoreCase)))
        { error = "Mã sinh viên đã tồn tại."; return false; }
        students.Add(new Student(id, fullName, address));
        error = string.Empty;
        return true;
    }

    public Student? FindStudent(string id) => _classes.Values.SelectMany(list => list)
        .FirstOrDefault(student => student.Id.Equals(id, StringComparison.CurrentCultureIgnoreCase));

    public bool RemoveStudent(string id)
    {
        foreach (List<Student> students in _classes.Values)
        {
            Student? student = students.FirstOrDefault(item => item.Id.Equals(id, StringComparison.CurrentCultureIgnoreCase));
            if (student is not null) return students.Remove(student);
        }
        return false;
    }

    public IReadOnlyList<Student> GetStudents(string className) =>
        _classes.TryGetValue(className, out List<Student>? students) ? students.AsReadOnly() : Array.Empty<Student>();
}
