<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Linq</Namespace>
</Query>

void Main()
{
    var dsHe = new[]
    {
        new He("KTV", "Kỹ thuật viên"),
        new He("CD", "Chuyên đề"),
        new He("QT", "Chứng chỉ quốc tế")
    };

    dsHe.Dump("Bài 6.1 - Danh sách hệ đào tạo");
    dsHe.Count().Dump("Tổng số hệ");
}

record He(string MaHe, string TenHe);
