using System;
using System.Globalization;

namespace Bai03_05_LuongKeThua;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("vi-VN");
        try
        {
            int n = Nhap.SoNguyen("Số nhân viên: ", 0, 10000);
            var staff = new NhanVien[n];
            for (int i = 0; i < n; i++)
            {
                int type = Nhap.SoNguyen("Loại (1 kinh doanh, 2 sản xuất): ", 1, 2);
                string ma = Nhap.Chuoi("Mã: "), ten = Nhap.Chuoi("Họ tên: ");
                staff[i] = type == 1 ? new NhanVienKinhDoanh(ma, ten, Nhap.Tien("Lương cơ bản: "), Nhap.SoNguyen("Hợp đồng: ", 0))
                    : new NhanVienSanXuat(ma, ten, Nhap.SoNguyen("Sản phẩm: ", 0));
            }
            decimal total = 0;
            foreach (var nv in staff) { Console.WriteLine(nv); total += nv.TinhLuong(); }
            Console.WriteLine($"Tổng lương: {total:N0} VNĐ");
        }
        catch (System.IO.EndOfStreamException e) { Console.WriteLine(e.Message); }
        catch (ArgumentException e) { Console.WriteLine("Lỗi dữ liệu: " + e.Message); }
        catch (ArithmeticException e) { Console.WriteLine("Lỗi tính toán: " + e.Message); }
    }
}
