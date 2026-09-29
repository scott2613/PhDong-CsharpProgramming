using System;
using MyLib;

namespace Buoi01Prj;

public class GiaiPTBac2
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        try
        {
            double a = Nhap.SoThuc("a = "), b = Nhap.SoThuc("b = "), c = Nhap.SoThuc("c = ");
            double x1 = 0, x2 = 0;
            int sn = LibBaiTap.GiaiPTBac2(a, b, c, ref x1, ref x2);
            Console.WriteLine($"Số nghiệm: {sn}");
            if (sn == -1) Console.WriteLine("Vô số nghiệm");
            else if (sn == 0) Console.WriteLine("Vô nghiệm");
            else
            {
                Console.WriteLine($"x1 = {x1:G12}");
                if (sn == 2) Console.WriteLine($"x2 = {x2:G12}");
            }
        }
        catch (System.IO.EndOfStreamException e) { Console.WriteLine(e.Message); }
    }
}

/// <summary>Main thứ hai minh họa lựa chọn điểm vào bằng StartupObject.</summary>
public class Program2
{
    public static void Main() => Console.WriteLine("Main 2");
}
