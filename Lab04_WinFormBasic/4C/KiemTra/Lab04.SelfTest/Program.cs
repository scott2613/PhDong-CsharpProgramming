using System;
using Lab04.Core;

namespace Lab04.SelfTest;

internal static class Program
{
    private static int _passed;

    private static void Main()
    {
        Check(PersonInfo.TryCalculateAge("2006", 2026, out int age) && age == 20, "Tính tuổi");
        Check(!PersonInfo.TryCalculateAge("abc", 2026, out _), "Từ chối năm sinh sai");
        Check(Validation.TryParseNumber("12.5", out _), "Nhập số thực");
        Check(Arithmetic.TryCalculate(8, 2, ArithmeticOperation.Divide, out double quotient, out _) && quotient == 4, "Phép chia");
        Check(!Arithmetic.TryCalculate(8, 0, ArithmeticOperation.Divide, out _, out _), "Chặn chia cho 0");
        Check(Validation.IsValidEmail("dong@example.com"), "Email hợp lệ");
        Check(AccountRegistration.Validate("phuongdong", "dong@example.com", "123456", "123456", out _), "Đăng ký hợp lệ");
        Check(!AccountRegistration.Validate("phuongdong", "sai-email", "123456", "123456", out _), "Từ chối email sai");
        Check(NumberTheory.GreatestCommonDivisor(18, 24) == 6, "UCLN");
        Check(NumberTheory.LeastCommonMultiple(18, 24) == 72, "BCNN");

        var sequence = new IntegerSequence();
        Check(sequence.TryAdd("2", out _) && sequence.TryAdd("3", out _) && sequence.TryAdd("-4", out _), "Nhập dãy số nguyên");
        Check(sequence.Sum == 1 && sequence.EvenSum == -2 && sequence.OddSum == 3, "Tổng dãy, tổng chẵn và tổng lẻ");
        Check(!sequence.TryAdd("2.5", out _), "Từ chối phần tử không phải số nguyên");

        Check(VietnameseNumberReader.Read(1) == "một", "Đọc số có một chữ số");
        Check(VietnameseNumberReader.Read(105) == "một trăm lẻ năm", "Đọc số có hàng trăm và hàng lẻ");
        Check(VietnameseNumberReader.Read(231) == "hai trăm ba mươi mốt", "Đọc số có hàng chục và mốt");
        Check(!VietnameseNumberReader.TryRead("1000", out _, out _), "Chặn số ngoài phạm vi 1-999");

        var ticketOffice = new CinemaTicketOffice();
        Check(ticketOffice.ToggleSelection(1, out _) && ticketOffice.ToggleSelection(8, out _), "Chọn ghế trống");
        Check(ticketOffice.ConfirmSelection() == 2500, "Tính tiền theo lô ghế");
        Check(ticketOffice[1] == SeatState.Sold && !ticketOffice.ToggleSelection(1, out _), "Khóa ghế đã bán");
        Check(CinemaTicketOffice.GetPrice(15) == 2000, "Giá vé lô C");
        ticketOffice.ToggleSelection(15, out _);
        ticketOffice.CancelSelection();
        Check(ticketOffice[15] == SeatState.Available, "Hủy ghế đang chọn");

        var calculator = new PocketCalculator();
        calculator.EnterDigit('8'); calculator.SetOperation('/'); calculator.EnterDigit('2');
        Check(calculator.TryEvaluate(out _) && calculator.Display == "4", "Máy tính thực hiện phép chia");
        calculator.Clear(); calculator.EnterDigit('5'); calculator.SetOperation('/'); calculator.EnterDigit('0');
        Check(!calculator.TryEvaluate(out _), "Máy tính chặn chia cho 0");

        Console.WriteLine($"KIỂM TRA THÀNH CÔNG: {_passed}/24 điều kiện đạt.");
    }

    private static void Check(bool condition, string name)
    {
        if (!condition) throw new InvalidOperationException($"Kiểm tra thất bại: {name}");
        _passed++;
    }
}
