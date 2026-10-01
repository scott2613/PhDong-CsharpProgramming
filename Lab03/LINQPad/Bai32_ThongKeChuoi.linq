<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Linq</Namespace>
</Query>

void Main()
{
    string[] monAn =
    {
        "Bún bò Huế", "Hủ tiếu heo", "Bánh canh", "Bánh mì", "Nước Cà phê",
        "Mì quảng", "Cơm tấm", "Nước Chanh dây", "Mì xào", "Bún riêu",
        "Bánh cuốn", "Mì gói", "Bún chả", "Hủ tiếu Nam vang"
    };

    int nganNhat = monAn.Min(mon => mon.Length);
    int daiNhat = monAn.Max(mon => mon.Length);

    var tenNganNhat = monAn.Where(mon => mon.Length == nganNhat);
    var tenDaiNhat = monAn.Where(mon => mon.Length == daiNhat);
    var theoTuDau = monAn.GroupBy(mon =>
        mon.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]);
    int soMonBanh = monAn.Count(mon =>
        mon.StartsWith("Bánh", StringComparison.OrdinalIgnoreCase));

    tenNganNhat.Dump("a - Tên ngắn nhất");
    tenDaiNhat.Dump("a - Tên dài nhất");
    theoTuDau.Dump("b - Nhóm theo từ đầu tiên");
    soMonBanh.Dump("c - Số món bắt đầu bằng Bánh");
}
