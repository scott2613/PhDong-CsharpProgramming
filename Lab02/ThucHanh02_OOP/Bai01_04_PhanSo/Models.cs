using System;

namespace Bai01_04_PhanSo;

/// <summary>Phân số chuẩn hóa: mẫu dương, tử và mẫu nguyên tố cùng nhau.</summary>
public sealed class PhanSo : IEquatable<PhanSo>, IComparable<PhanSo>
{
    private readonly System.Numerics.BigInteger tu, mau;
    public System.Numerics.BigInteger Tu => tu;
    public System.Numerics.BigInteger Mau => mau;
    public PhanSo() : this(0, 1) { }
    public PhanSo(int n) : this(n, 1) { }
    public PhanSo(PhanSo p) : this(p.Tu, p.Mau) { }
    public PhanSo(System.Numerics.BigInteger tu, System.Numerics.BigInteger mau)
    {
        if (mau.IsZero) throw new DivideByZeroException("Mẫu số không được bằng 0.");
        if (mau.Sign < 0) { tu = -tu; mau = -mau; }
        var gcd = System.Numerics.BigInteger.GreatestCommonDivisor(System.Numerics.BigInteger.Abs(tu), mau);
        this.tu = tu / gcd; this.mau = mau / gcd;
    }
    public static PhanSo Input()
    {
        int t = Nhap.SoNguyen("Tử: "), m;
        do { m = Nhap.SoNguyen("Mẫu khác 0: "); } while (m == 0);
        return new PhanSo(t, m);
    }
    public override string ToString() => Mau == 1 ? $"{Tu}" : $"{Tu}/{Mau}";
    public static PhanSo operator +(PhanSo p) => new(p);
    public static PhanSo operator -(PhanSo p) => new(-p.Tu, p.Mau);
    public static PhanSo operator +(PhanSo a, PhanSo b) => new(a.Tu * b.Mau + b.Tu * a.Mau, a.Mau * b.Mau);
    public static PhanSo operator -(PhanSo a, PhanSo b) => a + (-b);
    public static PhanSo operator *(PhanSo a, PhanSo b) => new(a.Tu * b.Tu, a.Mau * b.Mau);
    public static PhanSo operator /(PhanSo a, PhanSo b) => new(a.Tu * b.Mau, a.Mau * b.Tu);
    // BigInteger giữ phép nhân chéo chính xác và không tràn số nguyên.
    public int CompareTo(PhanSo? p) => p is null ? 1 : (Tu * p.Mau).CompareTo(p.Tu * Mau);
    public bool Equals(PhanSo? p) => p is not null && Tu == p.Tu && Mau == p.Mau;
    public override bool Equals(object? obj) => obj is PhanSo p && Equals(p);
    public override int GetHashCode() => HashCode.Combine(Tu, Mau);
    public static bool operator ==(PhanSo? a, PhanSo? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(PhanSo? a, PhanSo? b) => !(a == b);
    public static bool operator <(PhanSo a, PhanSo b) => a.CompareTo(b) < 0;
    public static bool operator >(PhanSo a, PhanSo b) => a.CompareTo(b) > 0;
    public static bool operator <=(PhanSo a, PhanSo b) => a.CompareTo(b) <= 0;
    public static bool operator >=(PhanSo a, PhanSo b) => a.CompareTo(b) >= 0;
}
