// Lớp nghiệp vụ dùng chung của Lab04C, được tách khỏi giao diện để dễ tái sử dụng và kiểm tra.
using System;

namespace Lab04.Core;

/// <summary>Kiểm tra năm sinh và tính tuổi của người dùng tại năm được chọn.</summary>
public static class PersonInfo
{
    public static bool TryCalculateAge(string? birthYearText, int currentYear, out int age)
    {
        age = 0;
        // Chỉ nhận năm sinh hợp lý để tránh cho ra tuổi âm hoặc dữ liệu quá xa thực tế.
        if (!int.TryParse(birthYearText, out int birthYear) || birthYear < 1900 || birthYear > currentYear)
            return false;

        age = currentYear - birthYear;
        return true;
    }
}
