// Lớp nghiệp vụ quản lý phòng ban và nhân viên cho bài mẫu TreeView.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab05C.Core;

public sealed record Employee(string Id, string FullName, string Address);

/// <summary>Lưu các phòng ban theo tên không phân biệt chữ hoa thường.</summary>
public sealed class DepartmentDirectory
{
    private readonly Dictionary<string, List<Employee>> _departments = new(StringComparer.CurrentCultureIgnoreCase);
    public IReadOnlyCollection<string> Departments => _departments.Keys.OrderBy(name => name).ToArray();

    public bool AddDepartment(string? name, out string error)
    {
        name = name?.Trim();
        if (string.IsNullOrWhiteSpace(name)) { error = "Tên phòng ban không được để trống."; return false; }
        if (!_departments.TryAdd(name, new List<Employee>())) { error = "Phòng ban đã tồn tại."; return false; }
        error = string.Empty;
        return true;
    }

    public bool RemoveDepartment(string? name) => !string.IsNullOrWhiteSpace(name) && _departments.Remove(name.Trim());

    public bool AddEmployee(string? department, string? id, string? fullName, string? address, out string error)
    {
        if (string.IsNullOrWhiteSpace(department) || !_departments.TryGetValue(department.Trim(), out List<Employee>? employees))
        { error = "Vui lòng chọn phòng ban hợp lệ."; return false; }
        id = id?.Trim(); fullName = fullName?.Trim(); address = address?.Trim();
        if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(address))
        { error = "Mã số, họ tên và địa chỉ không được để trống."; return false; }
        if (_departments.Values.SelectMany(list => list).Any(employee => employee.Id.Equals(id, StringComparison.CurrentCultureIgnoreCase)))
        { error = "Mã nhân viên đã tồn tại."; return false; }
        employees.Add(new Employee(id, fullName, address));
        error = string.Empty;
        return true;
    }

    public IReadOnlyList<Employee> GetEmployees(string department) =>
        _departments.TryGetValue(department, out List<Employee>? employees) ? employees.AsReadOnly() : Array.Empty<Employee>();
}
