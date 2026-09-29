using System;

namespace Bai02_03_DaySo;

public class DaySo
{
    private int[] values;
    public int Count => values.Length;
    public DaySo() : this(0) { }
    public DaySo(int n) { if (n < 0) throw new ArgumentOutOfRangeException(nameof(n)); values = new int[n]; }
    public DaySo(int[] a) { values = (int[])a.Clone(); }
    public DaySo(DaySo a) : this(a.values) { }
    public int this[int i] { get => values[i]; set => values[i] = value; }
    public DaySo SoChan()
    {
        var result = new System.Collections.Generic.List<int>();
        foreach (int n in values) if (n % 2 == 0) result.Add(n);
        return new DaySo(result.ToArray());
    }
    public void Input()
    {
        values = new int[Nhap.SoNguyen("Số phần tử: ", 0, 10000)];
        for (int i = 0; i < Count; i++) this[i] = Nhap.SoNguyen($"a[{i}] = ");
    }
    public void Output() => Console.WriteLine(Count == 0 ? "(rỗng)" : string.Join(", ", values));
}
