namespace Bai03;

public static class Power
{
    /// <summary>Tính x mũ y và trả về số thực để hỗ trợ cả số mũ âm.</summary>
    public static double Calculate(int x, int y) => Math.Pow(x, y);
}

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        int x = ReadInteger("Nhập số nguyên x: ");
        int y = ReadInteger("Nhập số nguyên y: ");
        Console.WriteLine($"Kết quả {x} mũ {y} là: {Power.Calculate(x, y)}");
    }

    /// <summary>Đọc một số nguyên và yêu cầu nhập lại khi dữ liệu chưa hợp lệ.</summary>
    private static int ReadInteger(string message)
    {
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                return value;
            }

            Console.WriteLine("Dữ liệu không hợp lệ. Vui lòng nhập một số nguyên.");
        }
    }
}

