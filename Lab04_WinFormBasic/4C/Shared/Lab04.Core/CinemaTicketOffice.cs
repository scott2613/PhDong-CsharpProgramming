// Lớp nghiệp vụ dùng chung của Lab04C, được tách khỏi giao diện để dễ tái sử dụng và kiểm tra.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab04.Core;

public enum SeatState { Available, Selected, Sold }

/// <summary>Lưu trạng thái 15 ghế và xử lý chọn, hủy, thanh toán vé.</summary>
public sealed class CinemaTicketOffice
{
    private readonly SeatState[] _seats = new SeatState[15];

    public SeatState this[int seatNumber] => _seats[ToIndex(seatNumber)];

    public bool ToggleSelection(int seatNumber, out string message)
    {
        int index = ToIndex(seatNumber);
        if (_seats[index] == SeatState.Sold)
        {
            message = $"Ghế {seatNumber} đã được bán.";
            return false;
        }

        _seats[index] = _seats[index] == SeatState.Available
            ? SeatState.Selected
            : SeatState.Available;
        message = string.Empty;
        return true;
    }

    public int ConfirmSelection()
    {
        int total = SelectedSeats().Sum(GetPrice);
        foreach (int seat in SelectedSeats().ToArray()) _seats[seat - 1] = SeatState.Sold;
        return total;
    }

    public void CancelSelection()
    {
        foreach (int seat in SelectedSeats().ToArray()) _seats[seat - 1] = SeatState.Available;
    }

    public static int GetPrice(int seatNumber)
    {
        _ = ToIndex(seatNumber);
        return seatNumber <= 5 ? 1000 : seatNumber <= 10 ? 1500 : 2000;
    }

    private IEnumerable<int> SelectedSeats() => Enumerable.Range(1, 15)
        .Where(seat => _seats[seat - 1] == SeatState.Selected);

    private static int ToIndex(int seatNumber)
    {
        if (seatNumber is < 1 or > 15) throw new ArgumentOutOfRangeException(nameof(seatNumber));
        return seatNumber - 1;
    }
}
