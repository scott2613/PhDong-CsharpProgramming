/*
    Lab06A - Tạo cơ sở dữ liệu QLSinhVien
    Sinh viên: Nguyễn Huỳnh Phương Đông - 3124411071
    Script có thể chạy nhiều lần và chỉ thêm dữ liệu mẫu khi bảng đang rỗng.
*/

IF DB_ID(N'QLSinhVien') IS NULL
    CREATE DATABASE QLSinhVien;
GO

USE QLSinhVien;
GO

IF OBJECT_ID(N'dbo.Khoa', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Khoa
    (
        MaKhoa  nvarchar(20)  NOT NULL PRIMARY KEY,
        TenKhoa nvarchar(100) NOT NULL
    );
END;

IF OBJECT_ID(N'dbo.Lop', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Lop
    (
        MaLop  nvarchar(20)  NOT NULL PRIMARY KEY,
        TenLop nvarchar(100) NOT NULL,
        MaKhoa nvarchar(20)  NOT NULL REFERENCES dbo.Khoa(MaKhoa)
    );
END;

IF OBJECT_ID(N'dbo.SinhVien', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.SinhVien
    (
        MaSinhVien nvarchar(20)  NOT NULL PRIMARY KEY,
        HoTen       nvarchar(120) NOT NULL,
        NgaySinh    date          NOT NULL,
        MaLop       nvarchar(20)  NOT NULL REFERENCES dbo.Lop(MaLop)
    );
END;

IF OBJECT_ID(N'dbo.MonHoc', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.MonHoc
    (
        MaMonHoc  nvarchar(20)  NOT NULL PRIMARY KEY,
        TenMonHoc nvarchar(120) NOT NULL
    );
END;

IF OBJECT_ID(N'dbo.Diem', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Diem
    (
        MaSinhVien nvarchar(20) NOT NULL REFERENCES dbo.SinhVien(MaSinhVien),
        MaMonHoc   nvarchar(20) NOT NULL REFERENCES dbo.MonHoc(MaMonHoc),
        Diem        decimal(4,2) NOT NULL CHECK (Diem BETWEEN 0 AND 10),
        CONSTRAINT PK_Diem PRIMARY KEY (MaSinhVien, MaMonHoc)
    );
END;

-- Mỗi bảng có tối thiểu 5 mẫu tin theo yêu cầu của đề.
IF NOT EXISTS (SELECT 1 FROM dbo.Khoa)
    INSERT dbo.Khoa VALUES
        (N'CNTT', N'Công nghệ thông tin'),
        (N'QTKD', N'Quản trị kinh doanh'),
        (N'NN', N'Ngoại ngữ'),
        (N'TCKT', N'Tài chính kế toán'),
        (N'DT', N'Điện tử viễn thông');

IF NOT EXISTS (SELECT 1 FROM dbo.Lop)
    INSERT dbo.Lop VALUES
        (N'DCT124C1', N'Công nghệ thông tin 1', N'CNTT'),
        (N'DCT124C2', N'Công nghệ thông tin 2', N'CNTT'),
        (N'DQK124A', N'Quản trị kinh doanh', N'QTKD'),
        (N'DNN124A', N'Ngôn ngữ Anh', N'NN'),
        (N'DTC124A', N'Tài chính kế toán', N'TCKT');

IF NOT EXISTS (SELECT 1 FROM dbo.SinhVien)
    INSERT dbo.SinhVien VALUES
        (N'3124411071', N'Nguyễn Huỳnh Phương Đông', '2006-01-15', N'DCT124C1'),
        (N'3124411002', N'Trần Minh Anh', '2006-04-20', N'DCT124C1'),
        (N'3124411003', N'Lê Thanh Bình', '2006-07-08', N'DCT124C2'),
        (N'3124411004', N'Phạm Hoài Nam', '2006-10-11', N'DQK124A'),
        (N'3124411005', N'Võ Ngọc Lan', '2006-12-03', N'DNN124A');

IF NOT EXISTS (SELECT 1 FROM dbo.MonHoc)
    INSERT dbo.MonHoc VALUES
        (N'CS001', N'Lập trình C#'),
        (N'CS002', N'Cơ sở dữ liệu'),
        (N'CS003', N'Cấu trúc dữ liệu'),
        (N'CS004', N'Mạng máy tính'),
        (N'CS005', N'Công nghệ phần mềm');

IF NOT EXISTS (SELECT 1 FROM dbo.Diem)
    INSERT dbo.Diem VALUES
        (N'3124411071', N'CS001', 9.00),
        (N'3124411002', N'CS001', 8.50),
        (N'3124411003', N'CS002', 7.50),
        (N'3124411004', N'CS003', 8.00),
        (N'3124411005', N'CS004', 9.50);
GO
