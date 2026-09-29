using System;

namespace Bai02_03b_DaThuc;

public class DonThuc
{
    private double heSo;
    private int soMu;
    public double HeSo { get => heSo; set { if (!double.IsFinite(value)) throw new ArgumentException("Hệ số phải hữu hạn."); heSo = value; } }
    public int SoMu { get => soMu; set { if (value < 0) throw new ArgumentOutOfRangeException(nameof(value)); soMu = value; } }
    public DonThuc() : this(0, 0) { }
    public DonThuc(double a, int n) { HeSo = a; SoMu = n; }
    public DonThuc(DonThuc p) : this(p.HeSo, p.SoMu) { }
    public double Tinh(double x) => HeSo * Math.Pow(x, SoMu);
    // Đạo hàm hằng số bằng 0, không tạo số mũ -1.
    public DonThuc DaoHam() => SoMu == 0 ? new DonThuc() : new DonThuc(HeSo * SoMu, SoMu - 1);
    public override string ToString() => SoMu == 0 ? $"{HeSo:G8}" : $"{HeSo:G8}*x^{SoMu}";
}

public class DaThuc
{
    private DonThuc[] terms;
    public int Count => terms.Length;
    public DaThuc() : this(0) { }
    public DaThuc(int bac)
    {
        if (bac < 0) throw new ArgumentOutOfRangeException(nameof(bac));
        terms = new DonThuc[bac + 1];
        for (int i = 0; i < Count; i++) terms[i] = new DonThuc(0, i);
    }
    public DaThuc(double[] heSo) : this(Math.Max(0, heSo.Length - 1))
    { for (int i = 0; i < heSo.Length; i++) terms[i].HeSo = heSo[i]; }
    public DaThuc(DaThuc p) : this(p.Count - 1)
    { for (int i = 0; i < Count; i++) terms[i] = new DonThuc(p.terms[i]); }
    public DonThuc this[int i]
    {
        get => new(terms[i]);
        set { if (value.SoMu != i) throw new ArgumentException("Đơn thức thứ i phải có số mũ i."); terms[i] = new DonThuc(value); }
    }
    public double Tinh(double x)
    {
        // Horner: giảm số phép nhân, không tính lặp lại từng lũy thừa.
        double s = 0; for (int i = Count - 1; i >= 0; i--) s = s * x + terms[i].HeSo;
        return s;
    }
    public void Input()
    {
        int n = Nhap.SoNguyen("Bậc đa thức: ", 0, 1000); terms = new DonThuc[n + 1];
        for (int i = 0; i <= n; i++) terms[i] = new DonThuc(Nhap.SoThuc($"a[{i}] = "), i);
    }
    public void Output() => Console.WriteLine(ToString());
    public override string ToString() => string.Join(" + ", (object[])terms);
}
