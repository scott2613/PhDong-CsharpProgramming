using System;

namespace Bai03_05_LuongKeThua;

public abstract class NhanVien
{
    public string Ma { get; }
    public string HoTen { get; }
    protected NhanVien(string ma, string ten)
    {
        if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten)) throw new ArgumentException("Mã và tên không được trống.");
        Ma = ma; HoTen = ten;
    }
    public abstract decimal TinhLuong();
    public override string ToString() => $"{Ma} | {HoTen} | {TinhLuong():N0} VNĐ";
}
public class NhanVienKinhDoanh : NhanVien
{
    public decimal LuongCoBan { get; }
    public int SoHopDong { get; }
    public NhanVienKinhDoanh(string ma, string ten, decimal luong, int hopDong) : base(ma, ten)
    {
        if (luong < 0 || hopDong < 0) throw new ArgumentOutOfRangeException(nameof(luong));
        LuongCoBan = luong; SoHopDong = hopDong;
    }
    public override decimal TinhLuong() => LuongCoBan + 500000m * SoHopDong;
}
public class NhanVienSanXuat : NhanVien
{
    public int SoSanPham { get; }
    public NhanVienSanXuat(string ma, string ten, int soSanPham) : base(ma, ten)
    {
        if (soSanPham < 0) throw new ArgumentOutOfRangeException(nameof(soSanPham)); SoSanPham = soSanPham;
    }
    // Chỉ thưởng khi TRÊN 3000; đúng 3000 không có thưởng.
    public override decimal TinhLuong() => SoSanPham * 1000m * (SoSanPham > 3000 ? 1.05m : 1m);
}
