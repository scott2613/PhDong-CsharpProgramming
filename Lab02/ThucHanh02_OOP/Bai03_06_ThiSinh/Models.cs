using System;

namespace Bai03_06_ThiSinh;

public abstract class ThiSinh
{
    public string Sbd { get; }
    public string HoTen { get; }
    public double Bai1 { get; }
    public double Bai2 { get; }
    public double Bai3 { get; }
    protected static double DiemHopLe(double x)
    {
        if (!double.IsFinite(x) || x < 0 || x > 10) throw new ArgumentOutOfRangeException(nameof(x));
        return x;
    }
    protected ThiSinh(string sbd, string ten, double a, double b, double c)
    {
        if (string.IsNullOrWhiteSpace(sbd) || string.IsNullOrWhiteSpace(ten)) throw new ArgumentException("SBD và tên không được trống.");
        Sbd = sbd; HoTen = ten; Bai1 = DiemHopLe(a); Bai2 = DiemHopLe(b); Bai3 = DiemHopLe(c);
    }
    public abstract double TongDiem { get; }
    public override string ToString() => $"{Sbd} | {HoTen} | {GetType().Name} | Tổng: {TongDiem:G6}";
}
public class Chuyen : ThiSinh
{
    public double TiengAnh { get; }
    public Chuyen(string sbd, string ten, double a, double b, double c, double anh) : base(sbd, ten, a, b, c) { TiengAnh = DiemHopLe(anh); }
    // Giữ nguyên hai khoảng trong đề: (8,9) không thuộc khoảng được cộng điểm.
    public double DiemThuong => TiengAnh >= 9 && TiengAnh <= 10 ? 2 : TiengAnh >= 7 && TiengAnh <= 8 ? 1 : 0;
    public override double TongDiem => Bai1 + Bai2 + Bai3 + DiemThuong;
}
public class SieuCup : ThiSinh
{
    public double Csdl { get; }
    public SieuCup(string sbd, string ten, double a, double b, double c, double csdl) : base(sbd, ten, a, b, c) { Csdl = DiemHopLe(csdl); }
    public override double TongDiem => Bai1 + Bai2 + Bai3 + Csdl;
}
public class CuocThi
{
    private readonly System.Collections.Generic.List<ThiSinh> danhSach = new();
    public void Add(ThiSinh t) { ArgumentNullException.ThrowIfNull(t); danhSach.Add(t); }
    public void Input()
    {
        danhSach.Clear(); int n = Nhap.SoNguyen("Số thí sinh: ", 0, 10000);
        for (int i = 0; i < n; i++)
        {
            int type = Nhap.SoNguyen("Loại (1 Chuyên, 2 Siêu cúp): ", 1, 2);
            string sbd = Nhap.Chuoi("SBD: "), ten = Nhap.Chuoi("Họ tên: ");
            double a = Nhap.SoThuc("Bài 1: ", 0, 10), b = Nhap.SoThuc("Bài 2: ", 0, 10), c = Nhap.SoThuc("Bài 3: ", 0, 10);
            double extra = Nhap.SoThuc(type == 1 ? "Tiếng Anh: " : "CSDL: ", 0, 10);
            Add(type == 1 ? new Chuyen(sbd, ten, a, b, c, extra) : new SieuCup(sbd, ten, a, b, c, extra));
        }
    }
    public void Output() { foreach (var t in danhSach) Console.WriteLine(t); }
}
