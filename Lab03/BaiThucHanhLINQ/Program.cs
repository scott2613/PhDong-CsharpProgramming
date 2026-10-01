using System;

namespace BaiThucHanhLINQ;

internal static class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Chế độ kiểm tra nhanh dùng để đối chiếu kết quả mà không cần thư viện ngoài.
        if (Array.Exists(args, value => value == "--verify"))
        {
            KiemTra.Run();
            return;
        }

        Bai21.Run();
        Bai22.Run();
        Bai31.Run();
        Bai32.Run();
        Bai41.Run();
        Bai51.Run();
        Bai52.Run();
        Bai61.Run();
        Bai62.Run();
    }
}
