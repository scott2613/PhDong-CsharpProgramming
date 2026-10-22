using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Lab06B.Core;

/// <summary>Tạo hai cơ sở dữ liệu và dữ liệu mẫu để bài có thể chạy ngay trên máy khác.</summary>
public static class DatabaseInitializer
{
    public static void EnsureUniversity()
    {
        EnsureDatabase("QLSinhVien");
        ExecuteBatch(DatabaseConfiguration.UniversityConnection, UniversityCommands());
    }

    public static void EnsureStore()
    {
        EnsureDatabase("QLCuaHang");
        ExecuteBatch(DatabaseConfiguration.StoreConnection, StoreCommands());
    }

    private static void EnsureDatabase(string name)
    {
        using SqlConnection connection = new(DatabaseConfiguration.ServerConnection);
        connection.Open();
        using SqlCommand command = new($"IF DB_ID(N'{name}') IS NULL CREATE DATABASE [{name}];", connection);
        command.ExecuteNonQuery();
    }

    private static void ExecuteBatch(string connectionString, IEnumerable<string> commands)
    {
        using SqlConnection connection = new(connectionString);
        connection.Open();
        foreach (string text in commands)
        {
            using SqlCommand command = new(text, connection);
            command.ExecuteNonQuery();
        }
    }

    private static IEnumerable<string> UniversityCommands()
    {
        yield return "IF OBJECT_ID(N'dbo.Khoa',N'U') IS NULL CREATE TABLE dbo.Khoa(MaKhoa nvarchar(20) NOT NULL PRIMARY KEY,TenKhoa nvarchar(100) NOT NULL);";
        yield return "IF OBJECT_ID(N'dbo.Lop',N'U') IS NULL CREATE TABLE dbo.Lop(MaLop nvarchar(20) NOT NULL PRIMARY KEY,TenLop nvarchar(100) NOT NULL,MaKhoa nvarchar(20) NOT NULL REFERENCES dbo.Khoa(MaKhoa));";
        yield return "IF OBJECT_ID(N'dbo.SinhVien',N'U') IS NULL CREATE TABLE dbo.SinhVien(MaSinhVien nvarchar(20) NOT NULL PRIMARY KEY,HoTen nvarchar(120) NOT NULL,NgaySinh date NOT NULL,MaLop nvarchar(20) NOT NULL REFERENCES dbo.Lop(MaLop));";
        yield return "IF OBJECT_ID(N'dbo.MonHoc',N'U') IS NULL CREATE TABLE dbo.MonHoc(MaMonHoc nvarchar(20) NOT NULL PRIMARY KEY,TenMonHoc nvarchar(120) NOT NULL);";
        yield return "IF OBJECT_ID(N'dbo.Diem',N'U') IS NULL CREATE TABLE dbo.Diem(MaSinhVien nvarchar(20) NOT NULL REFERENCES dbo.SinhVien(MaSinhVien),MaMonHoc nvarchar(20) NOT NULL REFERENCES dbo.MonHoc(MaMonHoc),Diem decimal(4,2) NOT NULL CHECK(Diem BETWEEN 0 AND 10),CONSTRAINT PK_Diem PRIMARY KEY(MaSinhVien,MaMonHoc));";
        // MERGE vừa thêm dữ liệu khi database mới, vừa sửa lại chuỗi Unicode nếu database cũ từng bị sai mã hóa.
        yield return @"MERGE dbo.Khoa AS t USING (VALUES
            (N'CNTT',N'Công nghệ thông tin'),(N'QTKD',N'Quản trị kinh doanh'),(N'NN',N'Ngoại ngữ'),
            (N'TCKT',N'Tài chính kế toán'),(N'DT',N'Điện tử viễn thông')) AS s(MaKhoa,TenKhoa)
            ON t.MaKhoa=s.MaKhoa WHEN MATCHED THEN UPDATE SET TenKhoa=s.TenKhoa
            WHEN NOT MATCHED THEN INSERT(MaKhoa,TenKhoa) VALUES(s.MaKhoa,s.TenKhoa);";
        yield return @"MERGE dbo.Lop AS t USING (VALUES
            (N'DCT124C1',N'Công nghệ thông tin 1',N'CNTT'),(N'DCT124C2',N'Công nghệ thông tin 2',N'CNTT'),
            (N'DQK124A',N'Quản trị kinh doanh',N'QTKD'),(N'DNN124A',N'Ngôn ngữ Anh',N'NN'),
            (N'DTC124A',N'Tài chính kế toán',N'TCKT')) AS s(MaLop,TenLop,MaKhoa)
            ON t.MaLop=s.MaLop WHEN MATCHED THEN UPDATE SET TenLop=s.TenLop,MaKhoa=s.MaKhoa
            WHEN NOT MATCHED THEN INSERT(MaLop,TenLop,MaKhoa) VALUES(s.MaLop,s.TenLop,s.MaKhoa);";
        yield return @"MERGE dbo.SinhVien AS t USING (VALUES
            (N'3124411071',N'Nguyễn Huỳnh Phương Đông',CONVERT(date,'2006-01-15'),N'DCT124C1'),
            (N'3124411002',N'Trần Minh Anh',CONVERT(date,'2006-04-20'),N'DCT124C1'),
            (N'3124411003',N'Lê Thanh Bình',CONVERT(date,'2006-07-08'),N'DCT124C2'),
            (N'3124411004',N'Phạm Hoài Nam',CONVERT(date,'2006-10-11'),N'DQK124A'),
            (N'3124411005',N'Võ Ngọc Lan',CONVERT(date,'2006-12-03'),N'DNN124A')) AS s(MaSinhVien,HoTen,NgaySinh,MaLop)
            ON t.MaSinhVien=s.MaSinhVien WHEN MATCHED THEN UPDATE SET HoTen=s.HoTen,NgaySinh=s.NgaySinh,MaLop=s.MaLop
            WHEN NOT MATCHED THEN INSERT(MaSinhVien,HoTen,NgaySinh,MaLop) VALUES(s.MaSinhVien,s.HoTen,s.NgaySinh,s.MaLop);";
        yield return @"MERGE dbo.MonHoc AS t USING (VALUES
            (N'CS001',N'Lập trình C#'),(N'CS002',N'Cơ sở dữ liệu'),(N'CS003',N'Cấu trúc dữ liệu'),
            (N'CS004',N'Mạng máy tính'),(N'CS005',N'Công nghệ phần mềm')) AS s(MaMonHoc,TenMonHoc)
            ON t.MaMonHoc=s.MaMonHoc WHEN MATCHED THEN UPDATE SET TenMonHoc=s.TenMonHoc
            WHEN NOT MATCHED THEN INSERT(MaMonHoc,TenMonHoc) VALUES(s.MaMonHoc,s.TenMonHoc);";
        yield return @"MERGE dbo.Diem AS t USING (VALUES
            (N'3124411071',N'CS001',CONVERT(decimal(4,2),9)),(N'3124411002',N'CS001',CONVERT(decimal(4,2),8.5)),
            (N'3124411003',N'CS002',CONVERT(decimal(4,2),7.5)),(N'3124411004',N'CS003',CONVERT(decimal(4,2),8)),
            (N'3124411005',N'CS004',CONVERT(decimal(4,2),9.5))) AS s(MaSinhVien,MaMonHoc,Diem)
            ON t.MaSinhVien=s.MaSinhVien AND t.MaMonHoc=s.MaMonHoc WHEN MATCHED THEN UPDATE SET Diem=s.Diem
            WHEN NOT MATCHED THEN INSERT(MaSinhVien,MaMonHoc,Diem) VALUES(s.MaSinhVien,s.MaMonHoc,s.Diem);";
    }

    private static IEnumerable<string> StoreCommands()
    {
        yield return "IF OBJECT_ID(N'dbo.SANPHAM',N'U') IS NULL CREATE TABLE dbo.SANPHAM(MaSP int IDENTITY(1,1) NOT NULL PRIMARY KEY,TenSP nvarchar(120) NOT NULL);";
        yield return "IF OBJECT_ID(N'dbo.HOADON',N'U') IS NULL CREATE TABLE dbo.HOADON(MaHD int IDENTITY(1,1) NOT NULL PRIMARY KEY,NgayTao date NOT NULL,TinhTrangThanhToan bit NOT NULL);";
        yield return "IF OBJECT_ID(N'dbo.CHITIET_HOADON',N'U') IS NULL CREATE TABLE dbo.CHITIET_HOADON(MaHD int NOT NULL REFERENCES dbo.HOADON(MaHD),MaSP int NOT NULL REFERENCES dbo.SANPHAM(MaSP),SoLuong int NOT NULL CHECK(SoLuong>0),CONSTRAINT PK_CHITIET_HOADON PRIMARY KEY(MaHD,MaSP));";
        yield return "IF OBJECT_ID(N'dbo.TONKHO',N'U') IS NULL CREATE TABLE dbo.TONKHO(MaSP int NOT NULL REFERENCES dbo.SANPHAM(MaSP),NgayTon date NOT NULL,SLDau int NOT NULL,SLNhap int NOT NULL,SLBan int NOT NULL,CONSTRAINT PK_TONKHO PRIMARY KEY(MaSP,NgayTon));";
        yield return "IF NOT EXISTS(SELECT 1 FROM dbo.SANPHAM) INSERT dbo.SANPHAM(TenSP) VALUES(N'Trà xanh'),(N'Mức dâu'),(N'Cafe');";
        yield return "INSERT dbo.TONKHO(MaSP,NgayTon,SLDau,SLNhap,SLBan) SELECT MaSP,CONVERT(date,GETDATE()),50,0,0 FROM dbo.SANPHAM s WHERE NOT EXISTS(SELECT 1 FROM dbo.TONKHO t WHERE t.MaSP=s.MaSP AND t.NgayTon=CONVERT(date,GETDATE()));";
    }
}
