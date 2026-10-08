// Lớp nghiệp vụ dùng chung của Lab04D, được tách khỏi giao diện để dễ tái sử dụng và kiểm tra.
using System;

namespace Lab04D.Core;

public enum HotelRoomType
{
    Single,
    Double,
    Triple
}

/// <summary>Tính tiền trả phòng tại Khách sạn Thanh Thanh.</summary>
public sealed class HotelBill
{
    public required string CustomerName { get; init; }
    public required string Address { get; init; }
    public int Days { get; init; }
    public HotelRoomType RoomType { get; init; }
    public bool Television { get; init; }
    public bool Internet { get; init; }
    public bool HotWater { get; init; }
    public bool Karaoke { get; init; }
    public bool Breakfast { get; init; }

    public decimal RoomRate => RoomType switch
    {
        HotelRoomType.Single => 300_000,
        HotelRoomType.Double => 350_000,
        HotelRoomType.Triple => 400_000,
        _ => throw new ArgumentOutOfRangeException(nameof(RoomType))
    };

    public int AmenityCount => (Television ? 1 : 0) + (Internet ? 1 : 0) + (HotWater ? 1 : 0);
    public decimal RoomCharge => RoomRate * Days;
    public decimal AmenityCharge => AmenityCount * 10_000m;
    public decimal ServiceCharge => (Karaoke ? 50_000m : 0m) + (Breakfast ? 15_000m * Days : 0m);
    public decimal Total => RoomCharge + AmenityCharge + ServiceCharge;

    public bool TryValidate(out string error)
    {
        if (string.IsNullOrWhiteSpace(CustomerName))
        {
            error = "Họ và tên khách hàng không được để trống.";
            return false;
        }
        if (string.IsNullOrWhiteSpace(Address))
        {
            error = "Địa chỉ không được để trống.";
            return false;
        }
        if (Days <= 0)
        {
            error = "Số ngày ở phải là số nguyên dương.";
            return false;
        }
        error = string.Empty;
        return true;
    }
}
