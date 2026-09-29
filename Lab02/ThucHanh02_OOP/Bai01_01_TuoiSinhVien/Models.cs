using System;

namespace Bai01_01_TuoiSinhVien;

public class SinhVien
{
    private string hoTen = "";
    private int namSinh;
    public string HoTen { get => hoTen; set => hoTen = !string.IsNullOrWhiteSpace(value) ? value.Trim() : throw new ArgumentException("Họ tên trống."); }
    public int NamSinh { get => namSinh; set { if (value < 1 || value > DateTime.Today.Year) throw new ArgumentOutOfRangeException(nameof(value)); namSinh = value; } }
    public SinhVien() : this("Chưa nhập", 2000) { }
    public SinhVien(string ten, int nam) { HoTen = ten; NamSinh = nam; }
    public SinhVien(SinhVien s) : this(s.HoTen, s.NamSinh) { }
    // Đề chỉ có năm sinh nên tính tuổi theo năm, chưa xét ngày sinh.
    public int TinhTuoi(int namHienTai)
    {
        if (namHienTai < NamSinh) throw new ArgumentOutOfRangeException(nameof(namHienTai));
        return namHienTai - NamSinh;
    }
    public void Input() { HoTen = Nhap.Chuoi("Họ tên: "); NamSinh = Nhap.SoNguyen("Năm sinh: ", 1, DateTime.Today.Year); }
    public void Output() => Console.WriteLine($"{HoTen}: {TinhTuoi(DateTime.Today.Year)} tuổi (năm {DateTime.Today.Year}).");
}
