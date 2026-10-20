// Kho dữ liệu ADO.NET dùng truy vấn có tham số cho năm form quản lý QLSinhVien.
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Lab05D.Core;

public static class DatabaseConfiguration
{
    public const string ServerConnection = @"Data Source=.\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";
    public const string ApplicationConnection = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLSinhVien;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";
}

public static class DatabaseInitializer
{
    public static void EnsureCreated()
    {
        using (SqlConnection master = new SqlConnection(DatabaseConfiguration.ServerConnection))
        {
            master.Open();
            using SqlCommand createDatabase = new SqlCommand("IF DB_ID(N'QLSinhVien') IS NULL CREATE DATABASE QLSinhVien;", master);
            createDatabase.ExecuteNonQuery();
        }

        using SqlConnection connection = new SqlConnection(DatabaseConfiguration.ApplicationConnection);
        connection.Open();
        foreach (string commandText in SchemaCommands())
        {
            using SqlCommand command = new SqlCommand(commandText, connection);
            command.ExecuteNonQuery();
        }
    }

    private static IEnumerable<string> SchemaCommands()
    {
        yield return "IF OBJECT_ID(N'dbo.Khoa', N'U') IS NULL CREATE TABLE dbo.Khoa(MaKhoa nvarchar(20) NOT NULL PRIMARY KEY, TenKhoa nvarchar(100) NOT NULL);";
        yield return "IF OBJECT_ID(N'dbo.Lop', N'U') IS NULL CREATE TABLE dbo.Lop(MaLop nvarchar(20) NOT NULL PRIMARY KEY, TenLop nvarchar(100) NOT NULL, MaKhoa nvarchar(20) NOT NULL REFERENCES dbo.Khoa(MaKhoa));";
        yield return "IF OBJECT_ID(N'dbo.SinhVien', N'U') IS NULL CREATE TABLE dbo.SinhVien(MaSinhVien nvarchar(20) NOT NULL PRIMARY KEY, HoTen nvarchar(120) NOT NULL, NgaySinh date NOT NULL, MaLop nvarchar(20) NOT NULL REFERENCES dbo.Lop(MaLop));";
        yield return "IF OBJECT_ID(N'dbo.MonHoc', N'U') IS NULL CREATE TABLE dbo.MonHoc(MaMonHoc nvarchar(20) NOT NULL PRIMARY KEY, TenMonHoc nvarchar(120) NOT NULL);";
        yield return "IF OBJECT_ID(N'dbo.Diem', N'U') IS NULL CREATE TABLE dbo.Diem(MaSinhVien nvarchar(20) NOT NULL REFERENCES dbo.SinhVien(MaSinhVien), MaMonHoc nvarchar(20) NOT NULL REFERENCES dbo.MonHoc(MaMonHoc), Diem decimal(4,2) NOT NULL CHECK(Diem BETWEEN 0 AND 10), CONSTRAINT PK_Diem PRIMARY KEY(MaSinhVien, MaMonHoc));";
        yield return "IF NOT EXISTS(SELECT 1 FROM dbo.Khoa) INSERT dbo.Khoa VALUES (N'CNTT',N'Công nghệ thông tin'),(N'QTKD',N'Quản trị kinh doanh'),(N'NN',N'Ngoại ngữ'),(N'TCKT',N'Tài chính kế toán'),(N'DT',N'Điện tử');";
        yield return "IF NOT EXISTS(SELECT 1 FROM dbo.Lop) INSERT dbo.Lop VALUES (N'DCT124C1',N'Công nghệ thông tin 1',N'CNTT'),(N'DCT124C2',N'Công nghệ thông tin 2',N'CNTT'),(N'DQK124A',N'Quản trị kinh doanh',N'QTKD'),(N'DNN124A',N'Ngôn ngữ Anh',N'NN'),(N'DTC124A',N'Tài chính kế toán',N'TCKT');";
        yield return "IF NOT EXISTS(SELECT 1 FROM dbo.SinhVien) INSERT dbo.SinhVien VALUES (N'3124411071',N'Nguyễn Huỳnh Phương Đông','2006-01-15',N'DCT124C1'),(N'3124411002',N'Trần Minh Anh','2006-04-20',N'DCT124C1'),(N'3124411003',N'Lê Thanh Bình','2006-07-08',N'DCT124C2'),(N'3124411004',N'Phạm Hoài Nam','2006-10-11',N'DQK124A'),(N'3124411005',N'Võ Ngọc Lan','2006-12-03',N'DNN124A');";
        yield return "IF NOT EXISTS(SELECT 1 FROM dbo.MonHoc) INSERT dbo.MonHoc VALUES (N'CS001',N'Lập trình C#'),(N'CS002',N'Cơ sở dữ liệu'),(N'CS003',N'Cấu trúc dữ liệu'),(N'CS004',N'Mạng máy tính'),(N'CS005',N'Công nghệ phần mềm');";
        yield return "IF NOT EXISTS(SELECT 1 FROM dbo.Diem) INSERT dbo.Diem VALUES (N'3124411071',N'CS001',9.0),(N'3124411002',N'CS001',8.5),(N'3124411003',N'CS002',7.5),(N'3124411004',N'CS003',8.0),(N'3124411005',N'CS004',9.5);";
    }
}

public sealed class UniversityRepository
{
    public DataTable Load(string tableName)
    {
        string table = ValidateTable(tableName);
        using SqlConnection connection = Open();
        using SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM dbo.{table}", connection);
        DataTable result = new DataTable(table);
        adapter.Fill(result);
        return result;
    }

    public int Execute(string commandText, params SqlParameter[] parameters)
    {
        using SqlConnection connection = Open();
        using SqlCommand command = new SqlCommand(commandText, connection);
        command.Parameters.AddRange(parameters);
        return command.ExecuteNonQuery();
    }

    public static SqlParameter Text(string name, string? value) =>
        new SqlParameter(name, SqlDbType.NVarChar) { Value = (value ?? string.Empty).Trim() };

    private static SqlConnection Open()
    {
        DatabaseInitializer.EnsureCreated();
        SqlConnection connection = new SqlConnection(DatabaseConfiguration.ApplicationConnection);
        connection.Open();
        return connection;
    }

    private static string ValidateTable(string tableName)
    {
        switch (tableName)
        {
            case "Khoa": case "Lop": case "SinhVien": case "MonHoc": case "Diem": return tableName;
            default: throw new ArgumentException("Tên bảng không hợp lệ.", nameof(tableName));
        }
    }
}
