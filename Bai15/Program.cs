namespace Bai15;

public static class ArrayUtilities
{
    /// <summary>Duyệt mảng một lần để tìm phần tử nhỏ nhất và lớn nhất.</summary>
    public static (int Min, int Max) MinMax(int[] values)
    {
        ArgumentNullException.ThrowIfNull(values);
        if (values.Length == 0) throw new ArgumentException("Mảng phải có ít nhất một phần tử.", nameof(values));

        int min = values[0];
        int max = values[0];
        foreach (int value in values)
        {
            if (value < min) min = value;
            if (value > max) max = value;
        }

        return (min, max);
    }

    /// <summary>Trả về mảng mới chứa các số nguyên tố theo đúng thứ tự ban đầu.</summary>
    public static int[] GetPrimes(int[] values)
    {
        ArgumentNullException.ThrowIfNull(values);
        var primes = new List<int>();
        foreach (int value in values)
        {
            if (IsPrime(value)) primes.Add(value);
        }

        return primes.ToArray();
    }

    private static bool IsPrime(int value)
    {
        if (value < 2) return false;
        for (int divisor = 2; divisor <= value / divisor; divisor++)
        {
            if (value % divisor == 0) return false;
        }

        return true;
    }
}

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        int count = ReadPositiveInteger("Nhập số phần tử n: ");
        int[] values = new int[count];
        for (int index = 0; index < values.Length; index++)
        {
            values[index] = ReadInteger($"A[{index}] = ");
        }

        Console.WriteLine($"Mảng đã nhập: {string.Join(", ", values)}");
        (int min, int max) = ArrayUtilities.MinMax(values);
        Console.WriteLine($"Phần tử nhỏ nhất: {min}");
        Console.WriteLine($"Phần tử lớn nhất: {max}");
        Console.WriteLine($"Các số nguyên tố: {string.Join(", ", ArrayUtilities.GetPrimes(values))}");
    }

    private static int ReadInteger(string message)
    {
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out int value)) return value;
            Console.WriteLine("Vui lòng nhập số nguyên.");
        }
    }

    private static int ReadPositiveInteger(string message)
    {
        int value;
        do value = ReadInteger(message); while (value <= 0);
        return value;
    }
}

