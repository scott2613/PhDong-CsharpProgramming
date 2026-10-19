// Bộ kiểm tra tự động các lớp nghiệp vụ của toàn bộ chín ứng dụng Lab05C.
using System;
using System.Collections.Generic;
using System.Linq;
using Lab05C.Core;

namespace Lab05C.SelfTest;

internal static class Program
{
    private static int _passed;

    private static void Main()
    {
        // Kiểm tra chuyển một, nhiều và toàn bộ phần tử giữa hai danh sách.
        var left = new List<string> { "A", "B", "C", "D" };
        var right = new List<string>();
        Check(TransferService.MoveOne(left, right, 1) && right.SequenceEqual(new[] { "B" }), "Chuyển một phần tử");
        Check(!TransferService.MoveOne(left, right, 10), "Từ chối chỉ số không hợp lệ");
        Check(TransferService.MoveSelected(left, right, new[] { 0, 2 }) == 2, "Chuyển nhiều phần tử tùy ý");
        Check(TransferService.MoveAll(left, right) == 1 && left.Count == 0, "Chuyển toàn bộ phần tử");

        // Kiểm tra nhập số và thống kê ước số.
        Check(DivisorAnalyzer.TryParsePositive("20", out int parsed, out _) && parsed == 20, "Nhập số nguyên dương");
        Check(!DivisorAnalyzer.TryParsePositive("-2", out _, out _), "Từ chối số âm");
        Check(!DivisorAnalyzer.TryParsePositive("abc", out _, out _), "Từ chối chuỗi không phải số");
        IReadOnlyList<int> divisors = DivisorAnalyzer.GetDivisors(20);
        Check(divisors.SequenceEqual(new[] { 1, 2, 4, 5, 10, 20 }), "Liệt kê đúng ước số");
        Check(DivisorAnalyzer.Sum(divisors) == 42, "Tính tổng ước số");
        Check(DivisorAnalyzer.CountEven(divisors) == 4, "Đếm ước số chẵn");
        Check(DivisorAnalyzer.CountPrime(divisors) == 2, "Đếm ước số nguyên tố");
        Check(DivisorAnalyzer.IsPrime(2) && DivisorAnalyzer.IsPrime(17), "Nhận diện số nguyên tố");
        Check(!DivisorAnalyzer.IsPrime(1) && !DivisorAnalyzer.IsPrime(21), "Nhận diện số không nguyên tố");

        // Kiểm tra phòng ban và mã nhân viên không trùng trên toàn hệ thống.
        var departments = new DepartmentDirectory();
        Check(departments.AddDepartment("Kế toán", out _), "Thêm phòng ban");
        Check(!departments.AddDepartment("kế TOÁN", out _), "Chặn phòng ban trùng không phân biệt hoa thường");
        Check(departments.AddEmployee("Kế toán", "NV01", "Nguyễn An", "TP HCM", out _), "Thêm nhân viên");
        Check(!departments.AddEmployee("Kế toán", "NV01", "Người khác", "Huế", out _), "Chặn mã nhân viên trùng");
        Check(departments.GetEmployees("Kế toán").Count == 1, "Đọc danh sách nhân viên");
        Check(departments.RemoveDepartment("Kế toán"), "Xóa phòng ban");

        // Kiểm tra lớp học, sinh viên, tìm kiếm và xóa.
        var students = new StudentDirectory();
        Check(students.AddClass("05DHTH1", out _), "Thêm lớp");
        Check(!students.AddClass("05dhth1", out _), "Chặn tên lớp trùng");
        Check(students.AddStudent("05DHTH1", "200115", "Nguyễn Văn Bình", "TP HCM", out _), "Thêm sinh viên");
        Check(!students.AddStudent("05DHTH1", "200115", "Nguyễn An", "Huế", out _), "Chặn mã sinh viên trùng");
        Check(students.FindStudent("200115")?.FullName == "Nguyễn Văn Bình", "Tìm sinh viên");
        Check(students.RemoveStudent("200115") && students.FindStudent("200115") is null, "Xóa sinh viên");

        // Kiểm tra sinh tên và các thao tác nhận diện họ, tên.
        IReadOnlyList<string> names = NameProcessor.Generate(50, 2026);
        Check(names.Count == 50, "Sinh đủ 50 họ tên");
        Check(names.All(name => name.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length == 3), "Tên ngẫu nhiên đủ ba thành phần");
        Check(NameProcessor.HasSurname("Lê Quang Sơn", "Lê"), "Nhận diện họ Lê");
        Check(NameProcessor.HasGivenName("Lê Quang Sơn", "Sơn"), "Nhận diện tên Sơn");
        Check(NameProcessor.ToTitleCase("nGUYỄN  vĂN  an") == "Nguyễn Văn An", "Chuẩn hóa viết hoa đầu từ");

        // Kiểm tra từ điển ở cả hai chiều và gợi ý theo tiền tố.
        var dictionary = new BilingualDictionary();
        Check(dictionary.TryTranslate(DictionaryDirection.EnglishToVietnamese, "student", out string studentMeaning) && studentMeaning.Contains("sinh viên"), "Dịch Anh Việt");
        Check(dictionary.TryTranslate(DictionaryDirection.VietnameseToEnglish, "giáo viên", out string teacher) && teacher == "teacher", "Dịch Việt Anh");
        Check(dictionary.Suggest(DictionaryDirection.EnglishToVietnamese, "st").Contains("student"), "Gợi ý tiền tố tiếng Anh");
        Check(!dictionary.TryTranslate(DictionaryDirection.EnglishToVietnamese, "unknown", out _), "Thông báo từ không tồn tại");

        // Kiểm tra toàn bộ phép biến đổi của ListBox số tự nhiên.
        var numbers = new NumberListProcessor();
        Check(numbers.TryAdd("1", out _) && numbers.TryAdd("2", out _) && numbers.TryAdd("3", out _) && numbers.TryAdd("4", out _), "Nhập danh sách số tự nhiên");
        Check(!numbers.TryAdd("-1", out _), "Từ chối số âm");
        Check(numbers.Sum() == 10, "Tính tổng danh sách");
        Check(numbers.EvenIndices().SequenceEqual(new[] { 1, 3 }), "Chọn vị trí số chẵn");
        Check(numbers.OddIndices().SequenceEqual(new[] { 0, 2 }), "Chọn vị trí số lẻ");
        numbers.AddTwo(); Check(numbers.Numbers.SequenceEqual(new[] { 3, 4, 5, 6 }), "Tăng mỗi phần tử lên 2");
        numbers.Square(); Check(numbers.Numbers.SequenceEqual(new[] { 9, 16, 25, 36 }), "Bình phương các phần tử");
        Check(numbers.RemoveSelected(new[] { 1, 2 }) == 2 && numbers.Numbers.SequenceEqual(new[] { 9, 36 }), "Xóa phần tử được chọn");
        numbers.RemoveFirstAndLast(); Check(numbers.Numbers.Count == 0, "Xóa phần tử đầu và cuối");

        // Kiểm tra phân nhóm danh bạ và loại bỏ dấu tiếng Việt khi xác định chữ cái.
        var contacts = new ContactBook();
        Check(contacts.Add("Bình", "Ngô Thanh", out Contact? binh, out _) && binh?.DisplayName == "Bình, Ngô Thanh", "Thêm liên hệ");
        Check(contacts.Groups['B'].Count == 1, "Phân nhóm chữ B");
        Check(contacts.Add("Đông", "Nguyễn Huỳnh Phương", out _, out _) && contacts.Groups['D'].Count == 1, "Chuẩn hóa chữ Đ về D");
        Check(!contacts.Add("Bình", "Ngô Thanh", out _, out _), "Chặn liên hệ trùng");
        Check(!contacts.Add("", "Nguyễn", out _, out _), "Chặn tên trống");

        Console.WriteLine($"KIỂM TRA THÀNH CÔNG: {_passed}/48 điều kiện đạt.");
    }

    private static void Check(bool condition, string name)
    {
        if (!condition) throw new InvalidOperationException($"Kiểm tra thất bại: {name}");
        _passed++;
    }
}
