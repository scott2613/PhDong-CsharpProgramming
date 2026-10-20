// Lớp nghiệp vụ của bài danh bạ, tự động phân nhóm người liên hệ theo chữ cái đầu của tên.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab05C.Core;

public sealed record Contact(string FirstName, string LastName)
{
    public string DisplayName => $"{FirstName}, {LastName}";
}

/// <summary>Lưu danh bạ theo 26 nhóm chữ cái A đến Z.</summary>
public sealed class ContactBook
{
    private readonly Dictionary<char, List<Contact>> _groups = Enumerable.Range('A', 26)
        .ToDictionary(value => (char)value, _ => new List<Contact>());
    public IReadOnlyDictionary<char, List<Contact>> Groups => _groups;

    public bool Add(string? firstName, string? lastName, out Contact? contact, out string error)
    {
        firstName = firstName?.Trim(); lastName = lastName?.Trim();
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
        { contact = null; error = "First Name và Last Name không được để trống."; return false; }
        char group = char.ToUpperInvariant(RemoveVietnameseMarks(firstName)[0]);
        if (group is < 'A' or > 'Z') { contact = null; error = "Tên phải bắt đầu bằng một chữ cái."; return false; }
        Contact newContact = new(firstName, lastName);
        if (_groups[group].Any(item => item.DisplayName.Equals(newContact.DisplayName, StringComparison.CurrentCultureIgnoreCase)))
        { contact = null; error = "Người liên hệ đã tồn tại."; return false; }
        contact = newContact;
        _groups[group].Add(newContact);
        _groups[group].Sort((left, right) => string.Compare(left.DisplayName, right.DisplayName, StringComparison.CurrentCultureIgnoreCase));
        error = string.Empty;
        return true;
    }

    private static string RemoveVietnameseMarks(string value)
    {
        string normalized = value.Normalize(System.Text.NormalizationForm.FormD);
        string result = new(normalized.Where(character => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(character)
            != System.Globalization.UnicodeCategory.NonSpacingMark).ToArray());
        return result.Replace('Đ', 'D').Replace('đ', 'd');
    }
}
