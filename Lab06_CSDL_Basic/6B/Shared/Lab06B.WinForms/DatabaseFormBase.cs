using System;
using System.Windows.Forms;
using Lab06B.Core;

namespace Lab06B.WinForms;

/// <summary>Lớp nền xử lý thông báo lỗi và các trường bắt buộc.</summary>
public abstract class DatabaseFormBase : Form
{
    protected static bool Require(params (string Value, string Message)[] values)
    {
        foreach ((string value, string message) in values)
        {
            if (!InputValidator.HasText(value))
            {
                MessageBox.Show(message, "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        return true;
    }

    protected static void SafeRun(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    protected static void ShowResult(int affected)
    {
        MessageBox.Show(affected > 0 ? "Thao tác thành công." : "Không tìm thấy dữ liệu phù hợp.",
            "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
