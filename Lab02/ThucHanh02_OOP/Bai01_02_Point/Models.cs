using System;

namespace Bai01_02_Point;

/// <summary>Điểm trong mặt phẳng, có các phép toán theo từng tọa độ.</summary>
public class Point
{
    private double x, y;
    public double X { get => x; set { if (!double.IsFinite(value)) throw new ArgumentException("X phải hữu hạn."); x = value; } }
    public double Y { get => y; set { if (!double.IsFinite(value)) throw new ArgumentException("Y phải hữu hạn."); y = value; } }
    public Point() : this(0, 0) { }
    public Point(double x, double y) { X = x; Y = y; }
    public Point(Point p) : this(p.X, p.Y) { }
    public void Input() { X = Nhap.SoThuc("x = "); Y = Nhap.SoThuc("y = "); }
    public void Output() => Console.WriteLine(this);
    public override string ToString() => $"({X:G8}, {Y:G8})";
    // Cùng một công thức, cung cấp cả lời gọi thành viên lẫn lời gọi tĩnh theo đề.
    public double Distance(Point other) => Distance(this, other);
    public static double Distance(Point a, Point b) => Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
    public Point Midpoint(Point other) => Midpoint(this, other);
    public static Point Midpoint(Point a, Point b) => new(a.X / 2 + b.X / 2, a.Y / 2 + b.Y / 2);
    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
    public static Point operator -(Point a) => new(-a.X, -a.Y);
}
