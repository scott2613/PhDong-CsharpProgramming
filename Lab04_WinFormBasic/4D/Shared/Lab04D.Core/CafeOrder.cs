// Lớp nghiệp vụ dùng chung của Lab04D, được tách khỏi giao diện để dễ tái sử dụng và kiểm tra.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab04D.Core;

public enum CafeDrink
{
    CafeDen,
    CafeDa,
    CafeSua,
    CafeSuaDa,
    CafeKem
}

public enum CafeFood
{
    BanhMyTrung,
    BanhMyCa,
    MyTomTrung,
    MyXaoBo,
    MyCay
}

/// <summary>Tính tiền một nhóm khách của quán Cafe Sinh Viên.</summary>
public sealed class CafeOrder
{
    private static readonly IReadOnlyDictionary<CafeDrink, decimal> DrinkPrices = new Dictionary<CafeDrink, decimal>
    {
        [CafeDrink.CafeDen] = 20_000,
        [CafeDrink.CafeDa] = 25_000,
        [CafeDrink.CafeSua] = 25_000,
        [CafeDrink.CafeSuaDa] = 30_000,
        [CafeDrink.CafeKem] = 35_000
    };

    private static readonly IReadOnlyDictionary<CafeFood, decimal> FoodPrices = new Dictionary<CafeFood, decimal>
    {
        [CafeFood.BanhMyTrung] = 15_000,
        [CafeFood.BanhMyCa] = 15_000,
        [CafeFood.MyTomTrung] = 20_000,
        [CafeFood.MyXaoBo] = 30_000,
        [CafeFood.MyCay] = 50_000
    };

    public required string CustomerName { get; init; }
    public int GuestCount { get; init; }
    public bool IsStudent { get; init; }
    public CafeDrink Drink { get; init; }
    public IReadOnlyCollection<CafeFood> Foods { get; init; } = Array.Empty<CafeFood>();

    public decimal UnitPrice => DrinkPrices[Drink] + Foods.Distinct().Sum(food => FoodPrices[food]);
    public decimal Subtotal => UnitPrice * GuestCount;
    public decimal Discount => IsStudent ? Subtotal * 0.20m : 0m;
    public decimal Total => Subtotal - Discount;

    public bool TryValidate(out string error)
    {
        if (string.IsNullOrWhiteSpace(CustomerName))
        {
            error = "Tên khách hàng không được để trống.";
            return false;
        }
        if (GuestCount <= 0)
        {
            error = "Số khách hàng phải là số nguyên dương.";
            return false;
        }
        if (Foods.Count == 0)
        {
            error = "Vui lòng chọn ít nhất một món ăn.";
            return false;
        }
        error = string.Empty;
        return true;
    }
}
