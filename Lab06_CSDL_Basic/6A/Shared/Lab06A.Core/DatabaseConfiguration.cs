using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Lab06A.Core;

/// <summary>Cấu hình và tự dò SQL Server dùng chung cho toàn bộ bài thực hành 6A.</summary>
public static class DatabaseConfiguration
{
    private const string DatabaseName = "QLSinhVien";
    private static string? _serverConnection;

    /// <summary>
    /// Ưu tiên chuỗi kết nối trong biến môi trường LAB06_CONNECTION_STRING.
    /// Nếu không có, chương trình lần lượt thử SQL Server Express và LocalDB.
    /// </summary>
    public static string ServerConnection => _serverConnection ??= FindAvailableServer();

    public static string ApplicationConnection
    {
        get
        {
            SqlConnectionStringBuilder builder = new(ServerConnection)
            {
                InitialCatalog = DatabaseName
            };
            return builder.ConnectionString;
        }
    }

    private static string FindAvailableServer()
    {
        foreach (string candidate in Candidates())
        {
            try
            {
                using SqlConnection connection = new(candidate);
                connection.Open();
                return candidate;
            }
            catch (SqlException)
            {
                // Thử cấu hình tiếp theo khi máy không cài đúng phiên bản SQL Server này.
            }
        }

        throw new InvalidOperationException(
            "Không kết nối được SQL Server. Hãy bật SQL Server Express/LocalDB hoặc đặt biến " +
            "LAB06_CONNECTION_STRING với chuỗi kết nối đến CSDL master.");
    }

    private static IEnumerable<string> Candidates()
    {
        string? custom = Environment.GetEnvironmentVariable("LAB06_CONNECTION_STRING");
        if (!string.IsNullOrWhiteSpace(custom))
        {
            SqlConnectionStringBuilder customBuilder = new(custom)
            {
                InitialCatalog = "master",
                ConnectTimeout = 3,
                Encrypt = false,
                TrustServerCertificate = true
            };
            yield return customBuilder.ConnectionString;
        }

        yield return @"Data Source=.\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;Connect Timeout=3;Encrypt=False;TrustServerCertificate=True";
        yield return @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=3;Encrypt=False;TrustServerCertificate=True";
    }
}
