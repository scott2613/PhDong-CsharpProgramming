using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using Lab05D.Core;

namespace Lab05D.WinForms;

/// <summary>Lớp nền gom phần kết nối, tải dữ liệu và thông báo lỗi cho các bài ADO.NET.</summary>
public abstract class DatabaseFormBase : Form
{
    protected readonly UniversityRepository Repository = new();
    protected readonly DataGridView Grid = Ui.Grid();

    protected void InitializeDatabaseForm(string title, string table, Control editor, int width = 1040)
    {
        Ui.Configure(this, title, width, 660);
        Controls.Add(Grid);
        Controls.Add(editor);
        Controls.Add(Ui.Title(title));
        Shown += (_, _) => SafeRun(() => { DatabaseInitializer.EnsureCreated(); RefreshTable(table); });
    }

    protected void RefreshTable(string table) => Grid.DataSource = Repository.Load(table);

    protected void Execute(string table, string sql, params SqlParameter[] parameters)
    {
        SafeRun(() =>
        {
            int affected = Repository.Execute(sql, parameters);
            MessageBox.Show(affected > 0 ? "Thao tác thành công." : "Không tìm thấy dữ liệu phù hợp.", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshTable(table);
        });
    }

    protected static bool Require(params (string Value, string Message)[] values)
    {
        foreach ((string value, string message) in values)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                MessageBox.Show(message, "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        return true;
    }

    protected static void SafeRun(Action action)
    {
        try { action(); }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }
}
