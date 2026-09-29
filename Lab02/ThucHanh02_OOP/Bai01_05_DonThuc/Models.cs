using System;

namespace Bai01_05_DonThuc;

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
