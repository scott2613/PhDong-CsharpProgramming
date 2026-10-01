using System;
using System.Linq;

namespace BaiThucHanhLINQ;

internal static class Bai21
{
    internal static readonly int[] MangSo = [50, 42, 16, 3, 9, 8, 12, 7, 24, 0];

    public static void Run()
    {
        TrinhBay.TieuDe("BÀI 2.1 - TRUY VẤN MẢNG SỐ NGUYÊN");

        // Câu a được viết bằng cả hai cú pháp để đối chiếu kết quả.
        var chiaHetQuery = from so in MangSo where so % 4 == 0 && so % 3 == 0 select so;
        var chiaHetMethod = MangSo.Where(so => so % 4 == 0 && so % 3 == 0);
        TrinhBay.DanhSach("a - Query Syntax", chiaHetQuery);
        TrinhBay.DanhSach("a - Method Syntax", chiaHetMethod);

        var nhoHonBangBa = MangSo.Where(so => so <= 3);
        TrinhBay.DanhSach("b - Các số <= 3", nhoHonBangBa);

        // Select tạo dãy mới; mảng ban đầu không bị thay đổi.
        var bienDoi = MangSo.Select(so => so % 2 == 0 ? so / 2 : so);
        TrinhBay.DanhSach("c - Chẵn chia đôi, lẻ giữ nguyên", bienDoi);
    }
}
