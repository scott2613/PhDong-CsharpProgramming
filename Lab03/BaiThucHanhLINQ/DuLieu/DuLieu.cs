using System;
using System.Collections.Generic;

namespace BaiThucHanhLINQ;

/// <summary>Tạo mới nguồn dữ liệu mỗi lần gọi để các bài không làm thay đổi lẫn nhau.</summary>
public static class DuLieu
{
    public static List<MonHoc> DS_Mon() =>
    [
        new() { MaMon = "HP2_1", TenMon = "Nền tảng C#", He = "KTV", SoTiet = 64 },
        new() { MaMon = "HP2_2", TenMon = "Công nghệ ADO.NET", He = "KTV", SoTiet = 64 },
        new() { MaMon = "HP3_1", TenMon = "Lập trình Windows Forms", He = "KTV", SoTiet = 64 },
        new() { MaMon = "HP3_2", TenMon = "Xây dựng ứng dụng Windows Forms", He = "KTV", SoTiet = 64 },
        new() { MaMon = "HP4_1", TenMon = "Lập trình Web với HTML, CSS và JavaScript", He = "KTV", SoTiet = 64 },
        new() { MaMon = "HP4_2", TenMon = "Xây dựng ứng dụng Web với ASP.NET", He = "KTV", SoTiet = 64 },
        new() { MaMon = "HP5_1", TenMon = "Lập trình CSDL SQL Server căn bản", He = "KTV", SoTiet = 64 },
        new() { MaMon = "HP5_2", TenMon = "Lập trình CSDL SQL Server nâng cao", He = "KTV", SoTiet = 64 },
        new() { MaMon = "JLCB", TenMon = "Joomla cơ bản", He = "CD", SoTiet = 72 },
        new() { MaMon = "LINQ", TenMon = "Language-Integrated Query", He = "CD", SoTiet = 64 },
        new() { MaMon = "DAWEB", TenMon = "Đồ án thực tế Web với ASP.NET", He = "CD", SoTiet = 40 },
        new() { MaMon = "DAWIN", TenMon = "Đồ án thực tế Windows Forms", He = "CD", SoTiet = 40 },
        new() { MaMon = "CC++", TenMon = "Lập trình hướng đối tượng với C/C++", He = "CD", SoTiet = 128 },
        new() { MaMon = "JQUE", TenMon = "JQuery", He = "CD", SoTiet = 22 },
        new() { MaMon = "XML", TenMon = "Công nghệ XML", He = "CD", SoTiet = 32 },
        new() { MaMon = "CRYS", TenMon = "Crystal Report trong Visual Studio", He = "CD", SoTiet = 32 },
        new() { MaMon = "BWEB", TenMon = "HTML, CSS và JavaScript", He = "CD", SoTiet = 32 },
        new() { MaMon = "XYZ", TenMon = "Chưa đặt tên môn", He = "", SoTiet = 0 }
    ];

    public static List<He> DS_He() =>
    [
        new() { MaHe = "KTV", TenHe = "Kỹ thuật viên" },
        new() { MaHe = "CD", TenHe = "Chuyên đề" },
        new() { MaHe = "QT", TenHe = "Chứng chỉ quốc tế" }
    ];
}
