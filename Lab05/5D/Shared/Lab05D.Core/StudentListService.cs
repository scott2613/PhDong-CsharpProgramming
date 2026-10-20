// Nghiệp vụ quản lý sinh viên hiển thị trên ListView của bài tại lớp 1.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab05D.Core;

public sealed class StudentListItem
{
    public StudentListItem(string fullName, string id, string gender, string languages, string ethnicity)
    {
        FullName = fullName;
        Id = id;
        Gender = gender;
        Languages = languages;
        Ethnicity = ethnicity;
    }

    public string FullName { get; set; }
    public string Id { get; }
    public string Gender { get; set; }
    public string Languages { get; set; }
    public string Ethnicity { get; set; }
}

public sealed class StudentListService
{
    private readonly List<StudentListItem> _students = new List<StudentListItem>();
    public IReadOnlyList<StudentListItem> Students => _students;
    public IReadOnlyList<StudentListItem> Items => _students;

    public bool TryAdd(StudentListItem student, out string error) =>
        Add(student.FullName, student.Id, student.Gender, student.Languages, student.Ethnicity, out error);

    public bool TryUpdate(string id, StudentListItem student, out string error)
    {
        if (!id.Equals(student.Id, StringComparison.OrdinalIgnoreCase))
        {
            error = "Không được thay đổi mã sinh viên khi cập nhật.";
            return false;
        }
        return Update(id, student.FullName, student.Gender, student.Languages, student.Ethnicity, out error);
    }

    public bool Remove(string id) => Remove(new[] { id }) > 0;

    public bool Add(string? fullName, string? id, string? gender, string? languages, string? ethnicity, out string error)
    {
        string normalizedId = (id ?? string.Empty).Trim();
        string normalizedName = (fullName ?? string.Empty).Trim();
        if (normalizedName.Length == 0 || normalizedId.Length == 0)
        {
            error = "Họ tên và mã sinh viên không được để trống.";
            return false;
        }
        if (_students.Any(item => item.Id.Equals(normalizedId, StringComparison.OrdinalIgnoreCase)))
        {
            error = "Mã sinh viên đã tồn tại.";
            return false;
        }

        _students.Add(new StudentListItem(normalizedName, normalizedId, gender ?? string.Empty,
            languages ?? string.Empty, ethnicity ?? string.Empty));
        error = string.Empty;
        return true;
    }

    public bool Update(string id, string? fullName, string? gender, string? languages, string? ethnicity, out string error)
    {
        StudentListItem? student = _students.FirstOrDefault(item => item.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        if (student is null) { error = "Không tìm thấy sinh viên cần sửa."; return false; }
        if (string.IsNullOrWhiteSpace(fullName)) { error = "Họ tên không được để trống."; return false; }
        student.FullName = fullName!.Trim();
        student.Gender = gender ?? string.Empty;
        student.Languages = languages ?? string.Empty;
        student.Ethnicity = ethnicity ?? string.Empty;
        error = string.Empty;
        return true;
    }

    public int Remove(IEnumerable<string> ids)
    {
        HashSet<string> selected = new HashSet<string>(ids, StringComparer.OrdinalIgnoreCase);
        return _students.RemoveAll(item => selected.Contains(item.Id));
    }
}
