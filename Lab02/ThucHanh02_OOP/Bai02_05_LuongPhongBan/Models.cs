using System;

namespace Bai02_05_LuongPhongBan;

public class NhanVien
{
    private string hoTen;
    private decimal mucLuong;
    private int ngayVang;
    public string HoTen => hoTen;
    public decimal MucLuong => mucLuong;
    public int NgayVang => ngayVang;
    public NhanVien() : this("Chưa nhập", 0, 0) { }
    public NhanVien(string ten, decimal luong, int vang)
    {
        if (string.IsNullOrWhiteSpace(ten) || luong < 0 || vang < 0) throw new ArgumentException("Thông tin nhân viên không hợp lệ.");
        hoTen = ten; mucLuong = luong; ngayVang = vang;
    }
    public NhanVien(NhanVien n) : this(n.HoTen, n.MucLuong, n.NgayVang) { }
    // Áp dụng đúng công thức đề: không tự chặn kết quả âm.
    public decimal TinhLuong() => MucLuong - 100000m * NgayVang;
}
public class PhongBan
{
    private readonly System.Collections.Generic.List<NhanVien> staff = new();
    public int Count => staff.Count;
    public void Add(NhanVien n) => staff.Add(new NhanVien(n));
    public decimal TongLuong() { decimal s = 0; foreach (var n in staff) s += n.TinhLuong(); return s; }
    public void Input()
    {
        staff.Clear(); int n = Nhap.SoNguyen("Số nhân viên: ", 0, 10000);
        for (int i = 0; i < n; i++) Add(new NhanVien(Nhap.Chuoi("Họ tên: "), Nhap.Tien("Mức lương: "), Nhap.SoNguyen("Ngày vắng: ", 0, 31)));
    }
    public void Output() { foreach (var n in staff) Console.WriteLine($"{n.HoTen}: {n.TinhLuong():N0} VNĐ"); }
}
