// Lớp nghiệp vụ của bài từ điển, hỗ trợ tìm gần đúng theo tiền tố và dịch hai chiều.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab05C.Core;

public enum DictionaryDirection { EnglishToVietnamese, VietnameseToEnglish }

/// <summary>Lưu từ vựng Anh Việt và cung cấp tra cứu không phân biệt chữ hoa thường.</summary>
public sealed class BilingualDictionary
{
    private readonly Dictionary<string, string> _english = new(StringComparer.CurrentCultureIgnoreCase)
    {
        ["student"] = "sinh viên, học sinh", ["teacher"] = "giáo viên", ["worker"] = "công nhân",
        ["hat"] = "cái mũ", ["head"] = "cái đầu", ["mouse"] = "con chuột",
        ["cat"] = "con mèo", ["dog"] = "con chó", ["snake"] = "con rắn",
        ["lion"] = "sư tử", ["frog"] = "con ếch", ["sheep"] = "con cừu"
    };

    public IReadOnlyList<string> Words(DictionaryDirection direction) => Source(direction).Keys.OrderBy(word => word).ToArray();

    public IReadOnlyList<string> Suggest(DictionaryDirection direction, string? prefix) =>
        Source(direction).Keys.Where(word => word.StartsWith(prefix?.Trim() ?? string.Empty, StringComparison.CurrentCultureIgnoreCase))
            .OrderBy(word => word).ToArray();

    public bool TryTranslate(DictionaryDirection direction, string? word, out string meaning)
    {
        if (!string.IsNullOrWhiteSpace(word) && Source(direction).TryGetValue(word.Trim(), out string? result))
        { meaning = result; return true; }
        meaning = "Không tìm thấy từ cần tra.";
        return false;
    }

    private Dictionary<string, string> Source(DictionaryDirection direction)
    {
        if (direction == DictionaryDirection.EnglishToVietnamese) return _english;
        // Một nghĩa tiếng Việt có thể chứa chú thích; lấy cụm đầu tiên làm khóa tra ngược dễ dùng.
        return _english.ToDictionary(pair => pair.Value.Split(',')[0].Trim(), pair => pair.Key, StringComparer.CurrentCultureIgnoreCase);
    }
}
