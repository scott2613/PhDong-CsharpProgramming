<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Linq</Namespace>
</Query>

void Main()
{
    int[] daySo = { 50, 42, 12, 3, 9, 8, 1, 50, 3, 42, 85 };

    // Các toán tử tổng hợp chỉ duyệt nguồn dữ liệu và trả về một giá trị.
    new
    {
        TongPhanTu = daySo.Count(),
        SoChan = daySo.Count(so => so % 2 == 0),
        SoLe = daySo.Count(so => so % 2 != 0),
        TongGiaTri = daySo.Sum(),
        LonNhat = daySo.Max(),
        NhoNhat = daySo.Min(),
        SoGiaTriKhacNhau = daySo.Distinct().Count()
    }.Dump("Kết quả thống kê");

    // GroupBy tạo mỗi nhóm theo số dư khi chia cho 5.
    daySo.GroupBy(so => so % 5)
         .OrderBy(nhom => nhom.Key)
         .Dump("Nhóm theo số dư");
}
