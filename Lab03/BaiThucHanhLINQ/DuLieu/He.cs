using System;

namespace BaiThucHanhLINQ;

/// <summary>Thông tin một hệ đào tạo để thực hiện join với danh sách môn học.</summary>
public sealed class He
{
    public string MaHe { get; set; } = string.Empty;
    public string TenHe { get; set; } = string.Empty;

    public override string ToString() => $"{MaHe} | {TenHe}";
}
