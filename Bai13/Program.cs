namespace Bai13;

public sealed class Student
{
    public string StudentId { get; }
    public string FullName { get; }
    public string Address { get; }
    public int Year { get; }

    public Student(string studentId, string fullName, string address, int year)
    {
        if (string.IsNullOrWhiteSpace(studentId)) throw new ArgumentException("Mã sinh viên không được trống.", nameof(studentId));
        if (string.IsNullOrWhiteSpace(fullName)) throw new ArgumentException("Họ tên không được trống.", nameof(fullName));
        if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Địa chỉ không được trống.", nameof(address));
        if (year < 1) throw new ArgumentOutOfRangeException(nameof(year), "Năm học phải từ 1 trở lên.");
        StudentId = studentId.Trim();
        FullName = fullName.Trim();
        Address = address.Trim();
        Year = year;
    }

    /// <summary>Tạo chuỗi gồm toàn bộ thông tin của sinh viên.</summary>
    public string Display() => $"Mã sinh viên: {StudentId}\nHọ tên: {FullName}\nĐịa chỉ: {Address}\nSinh viên năm thứ: {Year}";
}

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        string studentId = ReadRequired("Nhập mã sinh viên: ");
        string fullName = ReadRequired("Nhập họ tên: ");
        string address = ReadRequired("Nhập địa chỉ: ");
        int year = ReadPositiveInteger("Nhập sinh viên năm thứ mấy: ");
        var student = new Student(studentId, fullName, address, year);
        Console.WriteLine("\nTHÔNG TIN SINH VIÊN");
        Console.WriteLine(student.Display());
    }

    private static string ReadRequired(string message)
    {
        while (true)
        {
            Console.Write(message);
            string value = Console.ReadLine()?.Trim() ?? string.Empty;
            if (value.Length > 0) return value;
            Console.WriteLine("Dữ liệu không được để trống.");
        }
    }

    private static int ReadPositiveInteger(string message)
    {
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out int value) && value > 0) return value;
            Console.WriteLine("Vui lòng nhập số nguyên dương.");
        }
    }
}

