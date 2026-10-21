using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Lab06A.Core;

/// <summary>Kho dữ liệu ADO.NET dùng SqlConnection, SqlCommand và câu lệnh có tham số.</summary>
public sealed class UniversityRepository
{
    private static readonly HashSet<string> AllowedTables = new(StringComparer.OrdinalIgnoreCase)
    {
        "Khoa", "Lop", "SinhVien", "MonHoc", "Diem"
    };

    public DataTable Load(string tableName)
    {
        string table = ValidateTable(tableName);
        using SqlConnection connection = Open();
        using SqlDataAdapter adapter = new($"SELECT * FROM dbo.{table}", connection);
        DataTable result = new(table);
        adapter.Fill(result);
        return result;
    }

    public int Execute(string commandText, params SqlParameter[] parameters)
    {
        using SqlConnection connection = Open();
        using SqlCommand command = new(commandText, connection);
        command.Parameters.AddRange(parameters);
        return command.ExecuteNonQuery();
    }

    private static SqlConnection Open()
    {
        SqlConnection connection = new(DatabaseConfiguration.ApplicationConnection);
        connection.Open();
        return connection;
    }

    private static string ValidateTable(string tableName)
    {
        if (!AllowedTables.Contains(tableName))
            throw new ArgumentException("Tên bảng không hợp lệ.", nameof(tableName));
        return tableName;
    }
}
