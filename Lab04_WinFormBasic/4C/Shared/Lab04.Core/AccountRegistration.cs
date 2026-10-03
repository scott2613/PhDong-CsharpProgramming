using System;

namespace Lab04.Core;

public static class AccountRegistration
{
    public static bool Validate(
        string? userName,
        string? email,
        string? password,
        string? confirmation,
        out string error)
    {
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
