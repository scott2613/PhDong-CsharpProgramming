using System;
using System.Data;
using Lab05D.Core;

namespace Lab05D.SelfTest;

internal static class Program
{
    private static int _passed;
    private static int _total;

    private static int Main()
    {
        TestStudents();
        TestAccounts();
        TestRoster();
        TestCountdown();
        TestDatabase();
        Console.WriteLine($"Kết quả: {_passed}/{_total} phép kiểm tra đạt.");
        return _passed == _total ? 0 : 1;
    }

    private static void TestStudents()
    {
        StudentListService service = new();
        Check(!service.Add("", "", "", "", "", out _), "Từ chối sinh viên trống");
        Check(service.Add("Nguyễn An", "31240001", "Nam", "Anh", "Kinh", out _), "Thêm sinh viên hợp lệ");
        Check(service.Students.Count == 1, "Danh sách có một sinh viên");
        Check(!service.Add("Trần Bình", "31240001", "Nam", "", "Kinh", out _), "Chặn MSSV trùng");
        Check(service.Update("31240001", "Nguyễn An Mới", "Nam", "Anh, Nhật", "Kinh", out _), "Cập nhật sinh viên");
        Check(service.Students[0].FullName == "Nguyễn An Mới", "Họ tên đã cập nhật");
        Check(!service.Update("không có", "Tên", "", "", "", out _), "Không sửa mã không tồn tại");
        Check(service.Remove("31240001"), "Xóa sinh viên");
        Check(service.Students.Count == 0, "Danh sách sinh viên rỗng");
    }

    private static void TestAccounts()
    {
        BankAccountService service = new();
        Check(!service.Add("", "", "", "", out _), "Từ chối tài khoản trống");
        Check(!service.Add("001", "An", "TP.HCM", "-2", out _), "Từ chối số dư âm");
        Check(service.Add("001", "An", "TP.HCM", "1500000", out _), "Thêm tài khoản 001");
        Check(service.Add("002", "Bình", "Hà Nội", "500000", out _), "Thêm tài khoản 002");
        Check(!service.Add("001", "Khác", "Huế", "1", out _), "Chặn số tài khoản trùng");
        Check(service.TotalBalance == 2000000m, "Tính tổng số dư");
        Check(service.Remove("001"), "Xóa tài khoản theo số");
        Check(service.TotalBalance == 500000m, "Cập nhật tổng sau xóa");
    }

    private static void TestRoster()
    {
        ClassRoster roster = new();
        int a = roster.ClassA.Count; int b = roster.ClassB.Count;
        Check(roster.Add("Học viên mới", "A", out _), "Thêm học viên vào lớp A");
        Check(roster.ClassA.Count == a + 1, "Sĩ số lớp A tăng");
        Check(!roster.Add("Học viên mới", "B", out _), "Chặn học viên trùng giữa hai lớp");
        Check(roster.Move("A", "B", new[] { "Học viên mới" }) == 1, "Chuyển A sang B");
        Check(roster.ClassA.Count == a, "Sĩ số A giảm sau chuyển");
        Check(roster.ClassB.Count == b + 1, "Sĩ số B tăng sau chuyển");
        Check(roster.Remove("B", new[] { "Học viên mới" }) == 1, "Xóa học viên đã chọn");
        Check(roster.ClassB.Count == b, "Sĩ số B trở lại ban đầu");
    }

    private static void TestCountdown()
    {
        CountdownModel model = new();
        Check(!model.Start("0", "0", out _), "Từ chối thời gian bằng 0");
        Check(!model.Start("1", "60", out _), "Từ chối giây lớn hơn 59");
        Check(model.Start("1", "2", out _), "Khởi động 1 phút 2 giây");
        Check(model.RemainingSeconds == 62, "Quy đổi tổng số giây");
        Check(model.Display == "01:02", "Hiển thị mm:ss");
        Check(model.Tick(), "Đếm một giây");
        Check(model.RemainingSeconds == 61, "Thời gian còn lại giảm");
        model.Stop();
        Check(!model.IsRunning, "Dừng đồng hồ");
    }

    private static void TestDatabase()
    {
        DatabaseInitializer.EnsureCreated();
        UniversityRepository repository = new();
        foreach (string table in new[] { "Khoa", "Lop", "SinhVien", "MonHoc", "Diem" })
        {
            DataTable data = repository.Load(table);
            Check(data.Rows.Count >= 5, $"Bảng {table} có ít nhất 5 dòng mẫu");
            Check(data.Columns.Count >= 2, $"Bảng {table} có đúng cấu trúc dữ liệu");
        }
        bool rejected = false;
        try { repository.Load("TenBangKhongHopLe"); } catch (ArgumentException) { rejected = true; }
        Check(rejected, "Chặn tên bảng không hợp lệ");
    }

    private static void Check(bool condition, string name)
    {
        _total++;
        if (condition) { _passed++; Console.WriteLine("[ĐẠT] " + name); }
        else Console.WriteLine("[LỖI] " + name);
    }
}
