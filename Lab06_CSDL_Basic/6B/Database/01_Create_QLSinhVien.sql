IF DB_ID(N'QLSinhVien') IS NULL CREATE DATABASE QLSinhVien;
GO
USE QLSinhVien;
GO
IF OBJECT_ID(N'dbo.Khoa',N'U') IS NULL CREATE TABLE dbo.Khoa(MaKhoa nvarchar(20) PRIMARY KEY,TenKhoa nvarchar(100) NOT NULL);
IF OBJECT_ID(N'dbo.Lop',N'U') IS NULL CREATE TABLE dbo.Lop(MaLop nvarchar(20) PRIMARY KEY,TenLop nvarchar(100) NOT NULL,MaKhoa nvarchar(20) NOT NULL REFERENCES dbo.Khoa(MaKhoa));
IF OBJECT_ID(N'dbo.SinhVien',N'U') IS NULL CREATE TABLE dbo.SinhVien(MaSinhVien nvarchar(20) PRIMARY KEY,HoTen nvarchar(120) NOT NULL,NgaySinh date NOT NULL,MaLop nvarchar(20) NOT NULL REFERENCES dbo.Lop(MaLop));
IF OBJECT_ID(N'dbo.MonHoc',N'U') IS NULL CREATE TABLE dbo.MonHoc(MaMonHoc nvarchar(20) PRIMARY KEY,TenMonHoc nvarchar(120) NOT NULL);
IF OBJECT_ID(N'dbo.Diem',N'U') IS NULL CREATE TABLE dbo.Diem(MaSinhVien nvarchar(20) NOT NULL REFERENCES dbo.SinhVien(MaSinhVien),MaMonHoc nvarchar(20) NOT NULL REFERENCES dbo.MonHoc(MaMonHoc),Diem decimal(4,2) CHECK(Diem BETWEEN 0 AND 10),CONSTRAINT PK_Diem PRIMARY KEY(MaSinhVien,MaMonHoc));
GO
