using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Lab06B.Core;

/// <summary>Cấu hình kết nối dùng chung cho QLSinhVien và QLCuaHang.</summary>
public static class DatabaseConfiguration
{
    private static string? _serverConnection;

    public static string ServerConnection => _serverConnection ??= FindAvailableServer();
    public static string UniversityConnection => ForDatabase("QLSinhVien");
    public static string StoreConnection => ForDatabase("QLCuaHang");

    private static string ForDatabase(string databaseName)
    {
        SqlConnectionStringBuilder builder = new(ServerConnection) { InitialCatalog = databaseName };
        return builder.ConnectionString;
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
                // Máy có thể chỉ cài một trong hai phiên bản SQL Server bên dưới.
            }
        }

        throw new InvalidOperationException(
            "Không kết nối được SQL Server. Hãy bật SQL Server Express/LocalDB hoặc đặt " +
            "LAB06_CONNECTION_STRING đến cơ sở dữ liệu master.");
    }

    private static IEnumerable<string> Candidates()
    {
        string? custom = Environment.GetEnvironmentVariable("LAB06_CONNECTION_STRING");
        if (!string.IsNullOrWhiteSpace(custom))
        {
            SqlConnectionStringBuilder builder = new(custom)
            {
                InitialCatalog = "master",
                ConnectTimeout = 3,
                Encrypt = false,
                TrustServerCertificate = true
            };
            yield return builder.ConnectionString;
        }

        yield return @"Data Source=.\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;Connect Timeout=3;Encrypt=False;TrustServerCertificate=True";
        yield return @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=3;Encrypt=False;TrustServerCertificate=True";
    }
}
