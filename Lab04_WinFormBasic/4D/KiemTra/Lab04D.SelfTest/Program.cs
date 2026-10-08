// Chạy các tình huống kiểm tra tự động để xác nhận phần nghiệp vụ hoạt động đúng yêu cầu.
using System;
using Lab04D.Core;

namespace Lab04D.SelfTest;

internal static class Program
{
    private static int _passed;

    private static void Main()
    {
        // Kiểm tra nhập số theo cả dấu chấm và dấu phẩy thường dùng trên Windows.
        Check(NumericValidation.TryParseDouble("12.5", out double dot) && dot == 12.5, "Nhập số dùng dấu chấm");
        Check(NumericValidation.TryParseDouble("12,5", out double comma) && comma == 12.5, "Nhập số dùng dấu phẩy");
        Check(!NumericValidation.TryParseDouble("abc", out _), "Từ chối chuỗi không phải số");

        // Kiểm tra bốn phép toán cơ bản và nhánh lỗi chia cho 0.
        var calculation = new TinhToan(8, 2);
        Check(calculation.Cong() == 10, "Phép cộng");
        Check(calculation.Tru() == 6, "Phép trừ");
        Check(calculation.Nhan() == 16, "Phép nhân");
        Check(calculation.TryCalculate(PhepToan.Chia, out double quotient, out _) && quotient == 4, "Phép chia");
        calculation.B = 0;
        Check(!calculation.TryCalculate(PhepToan.Chia, out _, out _), "Chặn chia cho 0");

        // Mỗi tổ hợp CheckBox phải được quy đổi về đúng kiểu chữ hiển thị.
        Check(LabelStyleResolver.Resolve(true, false, false, false) == LabelTextStyle.Regular, "Kiểu chữ thường");
        Check(LabelStyleResolver.Resolve(false, true, false, false) == LabelTextStyle.Bold, "Kiểu chữ đậm");
        Check(LabelStyleResolver.Resolve(false, false, true, false) == LabelTextStyle.Italic, "Kiểu chữ nghiêng");
        Check(LabelStyleResolver.Resolve(false, true, true, false) == LabelTextStyle.BoldItalic, "Kiểu chữ đậm nghiêng");

        // Bao quát các trường hợp nghiệm của phương trình bậc nhất và bậc hai.
        Check(PhuongTrinh.SolveLinear(2, -4).Contains("x = 2", StringComparison.Ordinal), "Phương trình bậc nhất một nghiệm");
        Check(PhuongTrinh.SolveLinear(0, 0).Contains("vô số", StringComparison.Ordinal), "Phương trình bậc nhất vô số nghiệm");
        Check(PhuongTrinh.SolveLinear(0, 2).Contains("vô nghiệm", StringComparison.Ordinal), "Phương trình bậc nhất vô nghiệm");
        Check(PhuongTrinh.SolveQuadratic(1, -3, 2).Contains("x1 = 2; x2 = 1", StringComparison.Ordinal), "Phương trình bậc hai hai nghiệm");
        Check(PhuongTrinh.SolveQuadratic(1, -2, 1).Contains("nghiệm kép x = 1", StringComparison.Ordinal), "Phương trình bậc hai nghiệm kép");
        Check(PhuongTrinh.SolveQuadratic(1, 0, 1).Contains("vô nghiệm", StringComparison.Ordinal), "Phương trình bậc hai vô nghiệm");
        Check(PhuongTrinh.SolveQuadratic(0, 2, -4).Contains("x = 2", StringComparison.Ordinal), "Bậc hai suy biến về bậc nhất");

        // Thực hiện liên tiếp các thao tác để bảo đảm trạng thái mảng được cập nhật đồng bộ.
        Check(MangSoNguyen.TryParse("5 1 4 2 3", out MangSoNguyen? parsedArray, out _), "Nhập mảng số nguyên hợp lệ");
        Check(!MangSoNguyen.TryParse("1 hai 3", out _, out _), "Từ chối phần tử không phải số nguyên");
        parsedArray!.SortAscending();
        Check(parsedArray.ToString() == "1 2 3 4 5", "Sắp xếp mảng tăng dần");
        parsedArray.SortDescending();
        Check(parsedArray.ToString() == "5 4 3 2 1", "Sắp xếp mảng giảm dần");
        Check(parsedArray.FindValue(3) == 2, "Tìm vị trí theo giá trị");
        Check(parsedArray.ValueAt(4) == 2, "Tìm giá trị theo vị trí");
        parsedArray.InsertAt(2, 9);
        Check(parsedArray.ToString() == "5 9 4 3 2 1", "Thêm phần tử tại vị trí chỉ định");
        Check(parsedArray.RemoveValue(9), "Xóa phần tử theo giá trị");
        Check(parsedArray.RemoveAt(5) == 1, "Xóa phần tử theo vị trí");
        Check(parsedArray.ReplaceValue(3, 8) == 1, "Thay thế theo giá trị");
        Check(parsedArray.ReplaceAt(2, 7) == 4, "Thay thế theo vị trí");
        Check(parsedArray.Sum == 22, "Tính tổng mảng");
        Check(parsedArray.EvenSum == 10 && parsedArray.OddSum == 12, "Tính tổng chẵn và tổng lẻ");
        Check(parsedArray.Maximum == 8 && parsedArray.Minimum == 2, "Tìm giá trị lớn nhất và nhỏ nhất");

        // Đối chiếu đơn Cafe thường, đơn sinh viên và các dữ liệu bắt buộc.
        var regularCafeOrder = new CafeOrder
        {
            CustomerName = "Nguyễn An",
            GuestCount = 2,
            Drink = CafeDrink.CafeDa,
            Foods = new[] { CafeFood.BanhMyTrung, CafeFood.MyXaoBo }
        };
        Check(regularCafeOrder.TryValidate(out _), "Đơn Cafe đầy đủ thông tin");
        Check(regularCafeOrder.UnitPrice == 70_000, "Tính đơn giá nước uống và thức ăn");
        Check(regularCafeOrder.Subtotal == 140_000, "Tính tiền theo số khách");
        var studentCafeOrder = new CafeOrder
        {
            CustomerName = "Sinh viên Đông",
            GuestCount = 2,
            IsStudent = true,
            Drink = CafeDrink.CafeDa,
            Foods = new[] { CafeFood.BanhMyTrung, CafeFood.MyXaoBo }
        };
        Check(studentCafeOrder.Discount == 28_000 && studentCafeOrder.Total == 112_000, "Giảm 20 phần trăm cho sinh viên");
        Check(!new CafeOrder { CustomerName = "", GuestCount = 1, Drink = CafeDrink.CafeDen, Foods = new[] { CafeFood.BanhMyCa } }.TryValidate(out _), "Từ chối tên khách Cafe trống");
        Check(!new CafeOrder { CustomerName = "An", GuestCount = 0, Drink = CafeDrink.CafeDen, Foods = new[] { CafeFood.BanhMyCa } }.TryValidate(out _), "Từ chối số khách không hợp lệ");
        Check(!new CafeOrder { CustomerName = "An", GuestCount = 1, Drink = CafeDrink.CafeDen }.TryValidate(out _), "Yêu cầu chọn thức ăn");

        // Kiểm tra tiền phòng riêng và hóa đơn có đầy đủ tiện nghi, dịch vụ.
        var singleRoom = new HotelBill { CustomerName = "An", Address = "TP HCM", Days = 2, RoomType = HotelRoomType.Single };
        Check(singleRoom.Total == 600_000, "Tính tiền phòng đơn");
        var fullServiceRoom = new HotelBill
        {
            CustomerName = "Bình", Address = "Đà Nẵng", Days = 3, RoomType = HotelRoomType.Double,
            Television = true, Internet = true, Karaoke = true, Breakfast = true
        };
        Check(fullServiceRoom.Total == 1_165_000, "Tính tiền phòng, tiện nghi và dịch vụ");
        var breakfastRoom = new HotelBill { CustomerName = "Chi", Address = "Huế", Days = 2, RoomType = HotelRoomType.Triple, Breakfast = true };
        Check(breakfastRoom.Total == 830_000, "Tính tiền ăn sáng theo số ngày");
        Check(!new HotelBill { CustomerName = "", Address = "TP HCM", Days = 1 }.TryValidate(out _), "Từ chối tên khách sạn trống");
        Check(!new HotelBill { CustomerName = "An", Address = "TP HCM", Days = 0 }.TryValidate(out _), "Từ chối số ngày ở không hợp lệ");

        Console.WriteLine($"KIỂM TRA THÀNH CÔNG: {_passed}/45 điều kiện đạt.");
    }

    private static void Check(bool condition, string name)
    {
        if (!condition) throw new InvalidOperationException($"Kiểm tra thất bại: {name}");
        _passed++;
    }
}
