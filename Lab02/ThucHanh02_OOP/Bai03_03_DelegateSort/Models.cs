using System;

namespace Bai03_03_DelegateSort;

public class SinhVien : IComparable<SinhVien>
{
    public string HoTen { get; }
    public double Diem { get; }
    public SinhVien(string ten, double diem)
    {
        if (string.IsNullOrWhiteSpace(ten) || !double.IsFinite(diem) || diem < 0 || diem > 10) throw new ArgumentException("Sinh viên không hợp lệ.");
        HoTen = ten; Diem = diem;
    }
    // Sắp tăng dần theo điểm; điểm bằng nhau thì xét tên để kết quả xác định.
    public int CompareTo(SinhVien? other)
    {
        if (other is null) return 1;
        int c = Diem.CompareTo(other.Diem);
        return c != 0 ? c : StringComparer.Ordinal.Compare(HoTen, other.HoTen);
    }
    public override string ToString() => $"{HoTen}: {Diem:G5}";
}
public static class SapXep
{
    public static void Sort<T>(T[] a, Comparison<T> compare)
    {
        ArgumentNullException.ThrowIfNull(a); ArgumentNullException.ThrowIfNull(compare);
        for (int i = 1; i < a.Length; i++)
        {
            T key = a[i]; int j = i - 1;
            while (j >= 0 && compare(a[j], key) > 0) { a[j + 1] = a[j]; j--; }
            a[j + 1] = key;
        }
    }
}
