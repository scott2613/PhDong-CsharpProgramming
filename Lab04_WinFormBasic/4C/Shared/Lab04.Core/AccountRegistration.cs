// Lớp nghiệp vụ dùng chung của Lab04C, được tách khỏi giao diện để dễ tái sử dụng và kiểm tra.
using System;

namespace Lab04.Core;

/// <summary>Kiểm tra lần lượt thông tin bắt buộc của một tài khoản mới.</summary>
public static class AccountRegistration
{
    public static bool Validate(
        string? userName,
        string? email,
        string? password,
        string? confirmation,
        out string error)
    {
        // Dừng tại lỗi đầu tiên để Form có thể hướng người dùng sửa đúng trường dữ liệu.
        if (string.IsNullOrWhiteSpace(userName))
        {
            error = "Tên đăng nhập không được để trống.";
            return false;
        }

        if (!Validation.IsValidEmail(email))
        {
            error = "Địa chỉ email không đúng định dạng.";
            return false;
        }

        if (string.IsNullOrEmpty(password) || password.Length < 6)
        {
            error = "Mật khẩu phải có ít nhất 6 ký tự.";
            return false;
        }

        if (!string.Equals(password, confirmation, StringComparison.Ordinal))
        {
            error = "Xác nhận mật khẩu không khớp.";
            return false;
        }

        error = string.Empty;
        return true;
    }
}
