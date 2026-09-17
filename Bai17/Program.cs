using System;
using System.Collections.Generic;

namespace Bai17;

public static class MatrixUtilities
{
    /// <summary>Sinh ma trận số nguyên trong đoạn đóng từ 10 đến 100.</summary>
    public static int[,] Generate(int rows, int columns, int seed)
    {
        if (rows <= 0) throw new ArgumentOutOfRangeException(nameof(rows), "Số dòng phải dương.");
        if (columns <= 0) throw new ArgumentOutOfRangeException(nameof(columns), "Số cột phải dương.");

        var random = new Random(seed);
        var matrix = new int[rows, columns];
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                matrix[row, column] = random.Next(10, 101);
            }
        }

        return matrix;
    }

    /// <summary>Phân loại toàn bộ phần tử thành hai mảng chẵn và lẻ qua tham số out.</summary>
    public static void SplitEvenOdd(int[,] matrix, out int[] even, out int[] odd)
    {
        ArgumentNullException.ThrowIfNull(matrix);
        var evenValues = new List<int>();
        var oddValues = new List<int>();
        foreach (int value in matrix)
        {
            if (value % 2 == 0) evenValues.Add(value);
            else oddValues.Add(value);
        }

        even = evenValues.ToArray();
        odd = oddValues.ToArray();
    }
}

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        int rows = ReadPositiveInteger("Nhập số dòng n: ");
        int columns = ReadPositiveInteger("Nhập số cột m: ");
        int seed = int.TryParse(Environment.GetEnvironmentVariable("LAB01_SEED"), out int fixedSeed)
            ? fixedSeed
            : Environment.TickCount;
        int[,] matrix = MatrixUtilities.Generate(rows, columns, seed);

        Console.WriteLine("Ma trận đã sinh:");
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++) Console.Write($"{matrix[row, column],4}");
            Console.WriteLine();
        }

        MatrixUtilities.SplitEvenOdd(matrix, out int[] even, out int[] odd);
        Console.WriteLine($"Mảng số chẵn: {string.Join(", ", even)}");
        Console.WriteLine($"Mảng số lẻ: {string.Join(", ", odd)}");
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
