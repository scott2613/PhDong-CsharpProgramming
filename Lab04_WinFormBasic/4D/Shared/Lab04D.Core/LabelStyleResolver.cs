using System;
namespace Lab04D.Core;

public enum LabelTextStyle { Regular, Bold, Italic, BoldItalic }

/// <summary>Ghép các lựa chọn CheckBox thành FontStyle cho nhãn xem trước.</summary>
public static class LabelStyleResolver
{
    public static LabelTextStyle Resolve(bool regular, bool bold, bool italic, bool boldItalic)
    {
        if (boldItalic || (bold && italic)) return LabelTextStyle.BoldItalic;
        if (bold) return LabelTextStyle.Bold;
        if (italic) return LabelTextStyle.Italic;
        return regular ? LabelTextStyle.Regular : LabelTextStyle.Regular;
    }
}
