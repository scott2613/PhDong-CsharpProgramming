using System.Globalization;

namespace Bai16;

public static class NameUtilities
{
    /// <summary>Sắp xếp tăng dần theo quy tắc so sánh chuỗi tiếng Việt.</summary>
    public static string[] SortAscending(string[] names)
    {
        ArgumentNullException.ThrowIfNull(names);
        string[] result = (string[])names.Clone();
        CompareInfo comparer = CultureInfo.GetCultureInfo("vi-VN").CompareInfo;

        // Bubble sort giúp thể hiện rõ thao tác so sánh và hoán vị ở mức nhập môn.
        for (int i = 0; i < result.Length - 1; i++)
        {
            for (int j = 0; j < result.Length - i - 1; j++)
            {
                if (comparer.Compare(result[j], result[j + 1], CompareOptions.StringSort) > 0)
                {
                    (result[j], result[j + 1]) = (result[j + 1], result[j]);
                }
            }
        }

        return result;
    }
}

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        int count = ReadPositiveInteger("Nhập số người n: ");
        string[] names = new string[count];
        for (int index = 0; index < names.Length; index++)
        {
            Console.Write($"Nhập họ tên người thứ {index + 1}: ");
            names[index] = Console.ReadLine()?.Trim() ?? string.Empty;
        }

        Console.WriteLine("Danh sách sau khi sắp xếp:");
        foreach (string name in NameUtilities.SortAscending(names)) Console.WriteLine(name);
    }

    private static int ReadPositiveInteger(string message)
    {
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out int value) && value > 0) return value;
            Console.WriteLine("Vui lòng nhập số nguyên dương.");
        }
    }
}

