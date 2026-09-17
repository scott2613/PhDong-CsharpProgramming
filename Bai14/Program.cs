namespace Bai14;

public sealed class Employee
{
    private const decimal DeductionPerDay = 100_000m;

    public string FullName { get; }
    public decimal BaseSalary { get; }
    public int AbsentDays { get; }

    public Employee(string fullName, decimal baseSalary, int absentDays)
    {
        if (string.IsNullOrWhiteSpace(fullName)) throw new ArgumentException("Họ tên không được trống.", nameof(fullName));
        if (baseSalary < 0) throw new ArgumentOutOfRangeException(nameof(baseSalary), "Mức lương không được âm.");
        if (absentDays < 0) throw new ArgumentOutOfRangeException(nameof(absentDays), "Số ngày vắng không được âm.");
        FullName = fullName.Trim();
        BaseSalary = baseSalary;
        AbsentDays = absentDays;
    }

    /// <summary>Mỗi ngày vắng trừ 100.000 đồng và lương thực nhận không âm.</summary>
    public decimal CalculateSalary() => Math.Max(0m, BaseSalary - AbsentDays * DeductionPerDay);
}

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Write("Nhập họ tên nhân viên: ");
        string fullName = Console.ReadLine()?.Trim() ?? string.Empty;
        decimal baseSalary = ReadNonNegativeDecimal("Nhập mức lương: ");
        int absentDays = ReadNonNegativeInteger("Nhập số ngày vắng: ");
        var employee = new Employee(fullName, baseSalary, absentDays);
        Console.WriteLine($"Lương thực nhận của {employee.FullName}: {employee.CalculateSalary():N0} VNĐ");
    }

    private static decimal ReadNonNegativeDecimal(string message)
    {
        while (true)
        {
            Console.Write(message);
            if (decimal.TryParse(Console.ReadLine(), out decimal value) && value >= 0) return value;
            Console.WriteLine("Vui lòng nhập số không âm.");
        }
    }

    private static int ReadNonNegativeInteger(string message)
    {
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out int value) && value >= 0) return value;
            Console.WriteLine("Vui lòng nhập số nguyên không âm.");
        }
    }
}

