using System;

namespace Bai01_03_Person;

/// <summary>Yod bằng 0 biểu diễn người còn sống; năm mất không được trước năm sinh.</summary>
public class Person
{
    private string id = "", name = "";
    private int yob = 2000, yod;
    public string Id { get => id; set => id = !string.IsNullOrWhiteSpace(value) ? value.Trim() : throw new ArgumentException("Mã trống."); }
    public string Name { get => name; set => name = !string.IsNullOrWhiteSpace(value) ? value.Trim() : throw new ArgumentException("Tên trống."); }
    public int Yob { get => yob; set { if (value < 1 || value > DateTime.Today.Year || (yod != 0 && value > yod)) throw new ArgumentOutOfRangeException(nameof(value)); yob = value; } }
    public int Yod { get => yod; set { if (value != 0 && (value < yob || value > DateTime.Today.Year)) throw new ArgumentOutOfRangeException(nameof(value)); yod = value; } }
    public Person() : this("P0", "Chưa nhập", 2000, 0) { }
    public Person(string id, string name, int yob, int yod) { Id = id; Name = name; Yob = yob; Yod = yod; }
    public Person(Person p) : this(p.Id, p.Name, p.Yob, p.Yod) { }
    public bool IsLiving() => Yod == 0;
    public void Input()
    {
        string newId = Nhap.Chuoi("Mã: "), newName = Nhap.Chuoi("Họ tên: ");
        int birth = Nhap.SoNguyen("Năm sinh: ", 1, DateTime.Today.Year), death;
        do { death = Nhap.SoNguyen("Năm mất (0 nếu còn sống): ", 0, DateTime.Today.Year); }
        while (death != 0 && death < birth);
        // Đặt về trạng thái sống trước khi cập nhật, tránh ràng buộc chéo với năm mất cũ.
        Yod = 0; Id = newId; Name = newName; Yob = birth; Yod = death;
    }
    public void Output() => Console.WriteLine(this);
    public override string ToString() => $"{Id} | {Name} | sinh {Yob} | " + (IsLiving() ? "Còn sống" : $"mất {Yod}");
}
