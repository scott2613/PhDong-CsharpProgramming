using System;

namespace Bai03_04_ConsoleMenu;

public class MenuChoiceEventArgs : EventArgs
{
    public int Choice { get; }
    public MenuChoiceEventArgs(int choice) { Choice = choice; }
}
public class ConsoleMenu
{
    private readonly System.Collections.Generic.SortedDictionary<int, string> options = new();
    public event EventHandler<MenuChoiceEventArgs>? Choose;
    public void Add(int key, string title)
    {
        if (key <= 0 || string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Mục menu không hợp lệ.");
        options.Add(key, title);
    }
    public virtual void Execute(int choice) => Console.WriteLine($"Bạn thực hiện chức năng {choice}");
    // Phương thức riêng giúp kiểm thử lựa chọn mà không cần giả lập bàn phím.
    public bool Select(int choice)
    {
        if (!options.ContainsKey(choice)) return false;
        Execute(choice); Choose?.Invoke(this, new MenuChoiceEventArgs(choice)); return true;
    }
    public void Run()
    {
        while (true)
        {
            Console.WriteLine("MENU");
            foreach (var item in options) Console.WriteLine($"{item.Key}. {item.Value}");
            Console.WriteLine("0. Thoát chương trình");
            int choice = Nhap.SoNguyen("Thực hiện: ", 0);
            if (choice == 0) { Console.WriteLine("Đã thoát."); return; }
            if (!Select(choice)) Console.WriteLine("Chức năng không tồn tại.");
        }
    }
}
public class PTBac2Console : ConsoleMenu
{
    public PTBac2Console() { Add(1, "Giải phương trình bậc hai"); Add(2, "Hướng dẫn"); }
    public override void Execute(int choice)
    {
        if (choice == 2) { Console.WriteLine("Nhập a, b, c; chương trình xét nghiệm thực."); return; }
        double a = Nhap.SoThuc("a = "), b = Nhap.SoThuc("b = "), c = Nhap.SoThuc("c = "), x1 = 0, x2 = 0;
        int count = MyLib.LibBaiTap.GiaiPTBac2(a, b, c, ref x1, ref x2);
        Console.WriteLine($"Số nghiệm: {count}");
        if (count == -1) Console.WriteLine("Vô số nghiệm");
        if (count == 0) Console.WriteLine("Vô nghiệm");
        if (count >= 1) Console.WriteLine($"x1 = {x1:G12}");
        if (count == 2) Console.WriteLine($"x2 = {x2:G12}");
    }
}
