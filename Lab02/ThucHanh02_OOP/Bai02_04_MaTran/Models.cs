using System;

namespace Bai02_04_MaTran;

public class MaTran
{
    private int[,] values;
    public int Rows => values.GetLength(0);
    public int Columns => values.GetLength(1);
    public MaTran() : this(0, 0) { }
    public MaTran(int n, int m) { if (n < 0 || m < 0) throw new ArgumentOutOfRangeException(nameof(n)); values = new int[n, m]; }
    public MaTran(int[,] a) { values = (int[,])a.Clone(); }
    public MaTran(MaTran a) : this(a.values) { }
    public int this[int i, int j] { get => values[i, j]; set => values[i, j] = value; }
    public static bool LaNguyenTo(int n)
    {
        if (n < 2) return false;
        // Dùng n/d thay cho d*d để không tràn int khi n lớn.
        for (int d = 2; d <= n / d; d++) if (n % d == 0) return false;
        return true;
    }
    public int[] SoNguyenTo()
    {
        var result = new System.Collections.Generic.List<int>();
        foreach (int n in values) if (LaNguyenTo(n)) result.Add(n);
        return result.ToArray();
    }
    public void Input()
    {
        int n = Nhap.SoNguyen("Số dòng: ", 1, 100), m = Nhap.SoNguyen("Số cột: ", 1, 100);
        values = new int[n, m];
        for (int i = 0; i < n; i++) for (int j = 0; j < m; j++) this[i, j] = Nhap.SoNguyen($"a[{i},{j}] = ");
    }
    public void Output()
    {
        for (int i = 0; i < Rows; i++) { for (int j = 0; j < Columns; j++) Console.Write($"{this[i, j],6}"); Console.WriteLine(); }
    }
}
