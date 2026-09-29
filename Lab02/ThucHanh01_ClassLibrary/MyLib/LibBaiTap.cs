using System;

namespace MyLib;

/// <summary>Thư viện dùng chung: giải phương trình ax² + bx + c = 0 trên tập số thực.</summary>
public class LibBaiTap
{
    public const double EPS = 1e-9;

    /// <returns>-1: vô số nghiệm; 0: vô nghiệm; 1: một nghiệm; 2: hai nghiệm tăng dần.</returns>
    public static int GiaiPTBac2(double a, double b, double c, ref double x1, ref double x2)
    {
        if (!double.IsFinite(a) || !double.IsFinite(b) || !double.IsFinite(c))
            throw new ArgumentException("Các hệ số phải là số hữu hạn.");
        // Xóa kết quả cũ để hai biến ref không mang nghiệm của lần giải trước.
        x1 = x2 = 0;
        if (Math.Abs(a) < EPS)
        {
            if (Math.Abs(b) < EPS) return Math.Abs(c) < EPS ? -1 : 0;
            x1 = x2 = -c / b;
            return 1;
        }
        // Chuẩn hóa hệ số để hạn chế tràn số khi tính delta.
        double scale = Math.Max(Math.Abs(a), Math.Max(Math.Abs(b), Math.Abs(c)));
        double aa = a / scale, bb = b / scale, cc = c / scale;
        double delta = bb * bb - 4 * aa * cc;
        double tolerance = EPS * Math.Max(bb * bb, Math.Abs(4 * aa * cc));
        if (delta < -tolerance) return 0;
        if (Math.Abs(delta) <= tolerance)
        {
            x1 = x2 = -bb / (2 * aa);
            return 1;
        }
        // Dạng q hạn chế mất chữ số có nghĩa khi b và căn delta gần bằng nhau.
        double q = -0.5 * (bb + Math.CopySign(Math.Sqrt(delta), bb));
        x1 = q / aa;
        x2 = cc / q;
        if (x1 > x2) (x1, x2) = (x2, x1);
        return 2;
    }
}
