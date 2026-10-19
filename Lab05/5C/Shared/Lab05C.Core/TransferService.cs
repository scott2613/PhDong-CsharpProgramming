// Lớp nghiệp vụ cho bài mẫu ListBox, tách thao tác chuyển phần tử khỏi giao diện.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab05C.Core;

/// <summary>Chuyển một hoặc nhiều phần tử giữa hai danh sách và giữ nguyên thứ tự.</summary>
public static class TransferService
{
    public static bool MoveOne<T>(IList<T> source, IList<T> destination, int index)
    {
        if (index < 0 || index >= source.Count) return false;
        T item = source[index];
        destination.Add(item);
        source.RemoveAt(index);
        return true;
    }

    public static int MoveSelected<T>(IList<T> source, IList<T> destination, IEnumerable<int> indices)
    {
        int[] validIndices = indices.Where(index => index >= 0 && index < source.Count)
            .Distinct().OrderBy(index => index).ToArray();
        foreach (int index in validIndices) destination.Add(source[index]);
        // Xóa từ cuối về đầu để các chỉ số còn lại không bị thay đổi.
        foreach (int index in validIndices.OrderByDescending(index => index)) source.RemoveAt(index);
        return validIndices.Length;
    }

    public static int MoveAll<T>(IList<T> source, IList<T> destination)
    {
        int count = source.Count;
        foreach (T item in source.ToArray()) destination.Add(item);
        source.Clear();
        return count;
    }
}
