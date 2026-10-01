<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Linq</Namespace>
</Query>

void Main()
{
    string[] mangChuoi =
    {
        "đầu", "lòng", "hai", "ả", "tố", "nga", "Thúy",
        "Kiều", "là", "chị", "em", "là", "Thúy", "Vân"
    };

    // a) Lọc từ có đúng 4 ký tự rồi sắp theo ký tự đầu và toàn bộ từ.
    var bonKyTu = from tu in mangChuoi
                  where tu.Length == 4
                  orderby tu[0], tu
                  select tu;

    // b) Tạo hai dạng chữ thường và chữ hoa cho mỗi phần tử.
    var haiDangChu = mangChuoi.Select(tu => new
    {
        ChuThuong = tu.ToLowerInvariant(),
        ChuHoa = tu.ToUpperInvariant()
    });

    // c, d) Kiểm tra ký tự u không dấu và ký tự đầu viết hoa.
    var chuaU = mangChuoi.Where(tu => tu.Contains('u'));
    var batDauInHoa = mangChuoi.Where(tu => char.IsUpper(tu[0]));

    bonKyTu.Dump("a - Từ có 4 ký tự");
    haiDangChu.Dump("b - Chữ thường và chữ hoa");
    chuaU.Dump("c - Từ chứa ký tự u");
    batDauInHoa.Dump("d - Từ bắt đầu bằng chữ in hoa");
}
