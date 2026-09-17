namespace Bai07;

public static class NumberUtilities
{
    /// <summary>Kiểm tra n có đúng hai ước dương là 1 và chính nó hay không.</summary>
    public static bool IsPrime(int n)
    {
        if (n < 2) return false;

        // Chỉ cần thử các ước đến căn bậc hai của n.
        for (int divisor = 2; divisor <= n / divisor; divisor++)
        {
            if (n % divisor == 0) return false;
        }

        return true;
    }
}

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Write("Nhập số nguyên n: ");
        if (!int.TryParse(Console.ReadLine(), out int n))
        {
            Console.WriteLine("Lỗi: dữ liệu phải là số nguyên.");
            return;
        }

        Console.WriteLine(NumberUtilities.IsPrime(n)
            ? $"{n} là số nguyên tố."
            : $"{n} không phải là số nguyên tố.");
    }
}

