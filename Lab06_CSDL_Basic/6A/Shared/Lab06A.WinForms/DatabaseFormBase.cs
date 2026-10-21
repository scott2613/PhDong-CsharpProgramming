using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Lab06A.Core;

namespace Lab06A.WinForms;

/// <summary>Lớp nền gom phần khởi tạo CSDL, tải bảng và xử lý lỗi cho các form CRUD.</summary>
public abstract class DatabaseFormBase : Form
{
    protected readonly UniversityRepository Repository = new();
    protected readonly DataGridView Grid = Ui.Grid();
    private string _table = string.Empty;

    protected void InitializeDatabaseForm(string title, string table, Control editor, int width = 1080)
    {
        _table = table;
        Ui.Configure(this, title, width);
        Controls.Add(Grid);
        Controls.Add(Ui.Hint());
        Controls.Add(editor);
        Controls.Add(Ui.Title(title));

        // Chỉ tạo và nạp dữ liệu khi form đã hiển thị để giao diện không bị treo lúc khởi tạo.
        Shown += (_, _) => SafeRun(() =>
        {
            DatabaseInitializer.EnsureCreated();
            RefreshTable();
        });
    }

    protected void RefreshTable() => Grid.DataSource = Repository.Load(_table);

    protected void Execute(string sql, params SqlParameter[] parameters)
    {
        SafeRun(() =>
        {
            int affected = Repository.Execute(sql, parameters);
            MessageBox.Show(
                affected > 0 ? "Thao tác thành công." : "Không tìm thấy dữ liệu phù hợp.",
                "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshTable();
        });
    }

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
}
