using System;
using System.Linq;

namespace BaiThucHanhLINQ;

internal static class Bai22
{
    internal static readonly string[] MangChuoi =
        ["đầu", "lòng", "hai", "ả", "tố", "nga", "Thúy", "Kiều", "là", "chị", "em", "là", "Thúy", "Vân"];

    public static void Run()
    {
        TrinhBay.TieuDe("BÀI 2.2 - TRUY VẤN MẢNG CHUỖI");

        var bonKyTu = from tu in MangChuoi
                     where tu.Length == 4
                     orderby tu[0]
                     select tu;
        TrinhBay.DanhSach("a - Từ có 4 ký tự", bonKyTu);

        var haiDangChu = MangChuoi.Select(tu => $"{tu.ToLowerInvariant()} - {tu.ToUpperInvariant()}");
        TrinhBay.DanhSach("b - Chữ thường và chữ hoa", haiDangChu);

        // Contains kiểm tra đúng ký tự 'u'; ký tự có dấu như 'ú' là ký tự khác.
        var chuaU = MangChuoi.Where(tu => tu.Contains('u'));
        TrinhBay.DanhSach("c - Từ chứa ký tự u", chuaU.DefaultIfEmpty("(không có)"));

        var batDauInHoa = MangChuoi.Where(tu => char.IsUpper(tu[0]));
        TrinhBay.DanhSach("d - Các từ bắt đầu bằng chữ in hoa", batDauInHoa);
    }
}
