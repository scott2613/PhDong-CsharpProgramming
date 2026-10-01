<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.Linq</Namespace>
</Query>

void Main()
{
    var dsMon = TaoDanhSachMonHoc();
    dsMon.Dump("Bài 4.1 - Danh sách môn học");
    dsMon.Count.Dump("Tổng số môn");
}

List<MonHoc> TaoDanhSachMonHoc() => new List<MonHoc>
{
    new MonHoc("HP2_1", "Nền tảng C#", "KTV", 64),
    new MonHoc("HP2_2", "Công nghệ ADO.NET", "KTV", 64),
    new MonHoc("HP3_1", "Lập trình Windows Forms", "KTV", 64),
    new MonHoc("HP3_2", "Xây dựng ứng dụng Windows Forms", "KTV", 64),
    new MonHoc("HP4_1", "Lập trình Web với HTML, CSS và JavaScript", "KTV", 64),
    new MonHoc("HP4_2", "Xây dựng ứng dụng Web với ASP.NET", "KTV", 64),
    new MonHoc("HP5_1", "Lập trình CSDL SQL Server căn bản", "KTV", 64),
    new MonHoc("HP5_2", "Lập trình CSDL SQL Server nâng cao", "KTV", 64),
    new MonHoc("JLCB", "Joomla cơ bản", "CD", 72),
    new MonHoc("LINQ", "Language-Integrated Query", "CD", 64),
    new MonHoc("DAWEB", "Đồ án thực tế Web với ASP.NET", "CD", 40),
    new MonHoc("DAWIN", "Đồ án thực tế Windows Forms", "CD", 40),
    new MonHoc("CC++", "Lập trình hướng đối tượng với C/C++", "CD", 128),
    new MonHoc("JQUE", "JQuery", "CD", 22),
    new MonHoc("XML", "Công nghệ XML", "CD", 32),
    new MonHoc("CRYS", "Crystal Report trong Visual Studio", "CD", 32),
    new MonHoc("BWEB", "HTML, CSS và JavaScript", "CD", 32),
    new MonHoc("XYZ", "Chưa đặt tên môn", "", 0)
};

record MonHoc(string MaMon, string TenMon, string He, byte SoTiet);
