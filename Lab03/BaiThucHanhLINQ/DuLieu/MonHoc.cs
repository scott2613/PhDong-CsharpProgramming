using System;

namespace BaiThucHanhLINQ;

/// <summary>Thông tin một môn học dùng làm nguồn dữ liệu đối tượng cho LINQ.</summary>
public sealed class MonHoc
{
    public string MaMon { get; set; } = string.Empty;
    public string TenMon { get; set; } = string.Empty;
    public string He { get; set; } = string.Empty;
    public byte SoTiet { get; set; }

    public override string ToString() => $"{MaMon} | {TenMon} | {HeLabel} | {SoTiet} tiết";

    public string HeLabel => string.IsNullOrWhiteSpace(He) ? "Chưa khai báo" : He;
}
