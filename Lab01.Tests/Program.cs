namespace Lab01.Tests;

/// <summary>
/// Bộ kiểm thử nhỏ gọn, không cần cài thêm thư viện từ NuGet.
/// </summary>
internal static class Test
{
    private static int passed;
    private static int failed;

    public static void Equal<T>(T expected, T actual, string name)
    {
        if (EqualityComparer<T>.Default.Equals(expected, actual))
        {
            Pass(name);
            return;
        }

        Fail(name, $"mong đợi <{expected}> nhưng nhận <{actual}>");
    }

    public static void True(bool condition, string name)
    {
        if (condition)
        {
            Pass(name);
            return;
        }

        Fail(name, "điều kiện trả về false");
    }

    public static void SequenceEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual, string name)
    {
        Equal(string.Join("|", expected), string.Join("|", actual), name);
    }

    public static int Finish()
    {
        Console.WriteLine($"Tong: {passed} dat, {failed} loi");
        return failed == 0 ? 0 : 1;
    }

    private static void Pass(string name)
    {
        passed++;
        Console.WriteLine($"[DAT] {name}");
    }

    private static void Fail(string name, string reason)
    {
        failed++;
        Console.WriteLine($"[LOI] {name}: {reason}");
    }
}

internal static class Program
{
    private static int Main()
    {
        Test.Equal(343d, Bai03.Power.Calculate(7, 3), "Bai03 tinh 7 mu 3");
        Test.True(!Bai04.PowerInput.TryCalculate("abc", "3", out _), "Bai04 tu choi x khong phai so nguyen");
        Test.True(Bai04.PowerInput.TryCalculate("7", "3", out double result), "Bai04 chap nhan hai so nguyen");
        Test.Equal(343d, result, "Bai04 tinh luy thua khi du lieu hop le");
        Test.Equal(8d, Bai05.MenuCalculator.Power(2, 3), "Bai05 tinh luy thua");
        (double rootX, double rootY) = Bai05.MenuCalculator.SquareRoots(9, 16);
        Test.Equal(3d, rootX, "Bai05 can bac hai x");
        Test.Equal(4d, rootY, "Bai05 can bac hai y");
        return Test.Finish();
    }
}
