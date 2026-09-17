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

    public static void Throws<TException>(Action action, string name) where TException : Exception
    {
        try
        {
            action();
            Fail(name, $"không phát sinh {typeof(TException).Name}");
        }
        catch (TException)
        {
            Pass(name);
        }
        catch (Exception exception)
        {
            Fail(name, $"phát sinh {exception.GetType().Name} thay vì {typeof(TException).Name}");
        }
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
        Test.Equal(-2, Bai06.NumberUtilities.Max(-9, -2, -2), "Bai06 tim max voi so am va trung nhau");
        Test.True(Bai07.NumberUtilities.IsPrime(2), "Bai07 nhan dien so nguyen to 2");
        Test.True(Bai07.NumberUtilities.IsPrime(17), "Bai07 nhan dien so nguyen to 17");
        foreach (int value in new[] { -1, 0, 1, 9 })
        {
            Test.True(!Bai07.NumberUtilities.IsPrime(value), $"Bai07 loai {value} khoi tap so nguyen to");
        }

        double first = 1.5;
        double second = -2;
        Bai08.NumberUtilities.Swap(ref first, ref second);
        Test.Equal(-2d, first, "Bai08 gia tri thu nhat sau hoan vi");
        Test.Equal(1.5d, second, "Bai08 gia tri thu hai sau hoan vi");
        Bai09.NumberUtilities.FindMinMax(4.5, -3, 4.5, out double min, out double max);
        Test.Equal(-3d, min, "Bai09 tim min");
        Test.Equal(4.5d, max, "Bai09 tim max");
        Test.True(Bai10.StringUtilities.IsPalindrome(string.Empty), "Bai10 chuoi rong la doi xung");
        Test.True(Bai10.StringUtilities.IsPalindrome("level"), "Bai10 nhan dien chuoi doi xung");
        Test.True(!Bai10.StringUtilities.IsPalindrome("hello"), "Bai10 nhan dien chuoi khong doi xung");
        Test.Equal("gnôĐ", Bai11.StringUtilities.Reverse("Đông"), "Bai11 dao chuoi co ky tu Viet");
        (string lower, string upper, int wordCount) = Bai12.StringUtilities.Analyze("  Nguyễn\tHuỳnh\nPhương Đông  ");
        Test.Equal("  nguyễn\thuỳnh\nphương đông  ", lower, "Bai12 chuyen chu thuong");
        Test.Equal("  NGUYỄN\tHUỲNH\nPHƯƠNG ĐÔNG  ", upper, "Bai12 chuyen chu hoa");
        Test.Equal(4, wordCount, "Bai12 dem tu voi khoang trang hon hop");
        var student = new Bai13.Student("3124411071", "Nguyễn Huỳnh Phương Đông", "TP. Hồ Chí Minh", 2);
        string studentDisplay = student.Display();
        Test.True(studentDisplay.Contains("3124411071") && studentDisplay.Contains("Nguyễn Huỳnh Phương Đông") && studentDisplay.Contains("TP. Hồ Chí Minh") && studentDisplay.Contains("2"), "Bai13 xuat du thong tin sinh vien");

        var employee = new Bai14.Employee("Nguyễn Văn A", 1_000_000m, 3);
        Test.Equal(700_000m, employee.CalculateSalary(), "Bai14 tru luong theo ngay vang");
        var heavilyAbsent = new Bai14.Employee("Trần Văn B", 200_000m, 5);
        Test.Equal(0m, heavilyAbsent.CalculateSalary(), "Bai14 khong de luong am");

        (int arrayMin, int arrayMax) = Bai15.ArrayUtilities.MinMax(new[] { 4, -8, 11, 11, 2 });
        Test.Equal(-8, arrayMin, "Bai15 tim min trong mang");
        Test.Equal(11, arrayMax, "Bai15 tim max trong mang");
        Test.SequenceEqual(new[] { 2, 11, 11 }, Bai15.ArrayUtilities.GetPrimes(new[] { 4, 2, 11, -3, 11 }), "Bai15 loc so nguyen to va giu thu tu");
        Test.Throws<ArgumentException>(() => Bai15.ArrayUtilities.MinMax(Array.Empty<int>()), "Bai15 tu choi mang rong");

        Test.SequenceEqual(
            new[] { "Lê Đông", "Nguyễn An", "Trần Bình" },
            Bai16.NameUtilities.SortAscending(new[] { "Trần Bình", "Nguyễn An", "Lê Đông" }),
            "Bai16 sap xep ten theo tieng Viet");

        int[,] matrix = Bai17.MatrixUtilities.Generate(3, 4, 2026);
        Test.True(matrix.Cast<int>().All(value => value is >= 10 and <= 100), "Bai17 sinh gia tri trong doan 10 den 100");
        Bai17.MatrixUtilities.SplitEvenOdd(matrix, out int[] even, out int[] odd);
        Test.Equal(matrix.Length, even.Length + odd.Length, "Bai17 chia du phan tu chan le");
        Test.True(even.All(value => value % 2 == 0), "Bai17 mang chan chi chua so chan");
        Test.True(odd.All(value => value % 2 != 0), "Bai17 mang le chi chua so le");
        Test.Throws<ArgumentOutOfRangeException>(() => Bai17.MatrixUtilities.Generate(0, 2, 1), "Bai17 tu choi kich thuoc khong hop le");
        return Test.Finish();
    }
}
