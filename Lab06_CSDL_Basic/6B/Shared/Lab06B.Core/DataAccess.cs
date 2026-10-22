using System;
using System.Data;
using System.Data.SqlClient;

namespace Lab06B.Core;

/// <summary>Các thao tác ADO.NET có tham số, dùng chung để tránh nối chuỗi SQL từ dữ liệu nhập.</summary>
public static class DataAccess
{
    public static SqlConnection OpenUniversity() => Open(DatabaseConfiguration.UniversityConnection);
    public static SqlConnection OpenStore() => Open(DatabaseConfiguration.StoreConnection);

    public static int ExecuteUniversity(string sql, params SqlParameter[] parameters) =>
        Execute(DatabaseConfiguration.UniversityConnection, sql, parameters);

    public static int ExecuteStore(string sql, params SqlParameter[] parameters) =>
        Execute(DatabaseConfiguration.StoreConnection, sql, parameters);

    public static bool ExistsUniversity(string sql, params SqlParameter[] parameters) =>
        Scalar(DatabaseConfiguration.UniversityConnection, sql, parameters) != null;

    public static bool ExistsStore(string sql, params SqlParameter[] parameters) =>
        Scalar(DatabaseConfiguration.StoreConnection, sql, parameters) != null;

    public static DataTable QueryUniversity(string sql, params SqlParameter[] parameters) =>
        Query(DatabaseConfiguration.UniversityConnection, sql, parameters);

    public static DataTable QueryStore(string sql, params SqlParameter[] parameters) =>
        Query(DatabaseConfiguration.StoreConnection, sql, parameters);

    public static object? ScalarStore(string sql, params SqlParameter[] parameters) =>
        Scalar(DatabaseConfiguration.StoreConnection, sql, parameters);

    private static SqlConnection Open(string connectionString)
    {
        SqlConnection connection = new(connectionString);
        connection.Open();
        return connection;
    }

    private static int Execute(string connectionString, string sql, SqlParameter[] parameters)
    {
        using SqlConnection connection = Open(connectionString);
        using SqlCommand command = new(sql, connection);
        command.Parameters.AddRange(parameters);
        return command.ExecuteNonQuery();
    }

    private static object? Scalar(string connectionString, string sql, SqlParameter[] parameters)
    {
        using SqlConnection connection = Open(connectionString);
        using SqlCommand command = new(sql, connection);
        command.Parameters.AddRange(parameters);
        object value = command.ExecuteScalar();
        return value == DBNull.Value ? null : value;
    }

    private static DataTable Query(string connectionString, string sql, SqlParameter[] parameters)
    {
        using SqlConnection connection = Open(connectionString);
        using SqlCommand command = new(sql, connection);
        command.Parameters.AddRange(parameters);
        using SqlDataAdapter adapter = new(command);
        DataTable table = new();
        adapter.Fill(table);
        return table;
    }
}
