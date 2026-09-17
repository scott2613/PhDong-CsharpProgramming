namespace Bai01;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Write("Nhập họ tên của bạn: ");
        string hoTen = Console.ReadLine()?.Trim() ?? string.Empty;

        // Yêu cầu người dùng nhập lại để kết quả không bị bỏ trống.
        while (hoTen.Length == 0)
        {
            Console.Write("Họ tên không được để trống. Nhập lại: ");
            hoTen = Console.ReadLine()?.Trim() ?? string.Empty;
        }

        Console.WriteLine($"Họ tên bạn đã nhập: {hoTen}");
    }
}

