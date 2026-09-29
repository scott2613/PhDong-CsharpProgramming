using System;
using MyLib;
using Xunit;

namespace MyLib.Tests;

public class LibBaiTapTests
{
    // Bảy Fact tách riêng để thấy rõ từng nhánh tối thiểu được yêu cầu trong đề.
    [Fact] public void VoSoNghiem() => Check(0, 0, 0, -1, 0, 0);
    [Fact] public void SuyBienVoNghiem() => Check(0, 0, 5, 0, 0, 0);
    [Fact] public void BacNhat() => Check(0, 2, -4, 1, 2, 2);
    [Fact] public void DeltaAm() => Check(1, 0, 1, 0, 0, 0);
    [Fact] public void NghiemKep() => Check(1, -2, 1, 1, 1, 1);
    [Fact] public void HaiNghiem() => Check(1, -3, 2, 2, 1, 2);
    [Fact] public void HoanDoiNghiem() => Check(-1, 3, -2, 2, 1, 2);
    [Theory]
    [InlineData(1e-12, 2, -4, 1, 2, 2)]
    [InlineData(0, 1e-12, 1e-12, -1, 0, 0)]
    [InlineData(1, -2, 1 + 1e-12, 1, 1, 1)]
    [InlineData(1, 0, 0, 1, 0, 0)]
    [InlineData(1, 0, -4, 2, -2, 2)]
    public void SaiSoVaNghiemKhong(double a, double b, double c, int n, double x, double y) => Check(a, b, c, n, x, y);
    [Fact]
    public void HeSoLonKhongTranDelta()
    {
        Check(1e200, -3e200, 2e200, 2, 1, 2);
    }
    [Fact]
    public void TuChoiNaN()
    {
        double x = 0, y = 0;
        Assert.Throws<ArgumentException>(() => LibBaiTap.GiaiPTBac2(double.NaN, 1, 1, ref x, ref y));
    }
    private static void Check(double a, double b, double c, int expected, double first, double second)
    {
        double x = 99, y = 88;
        int count = LibBaiTap.GiaiPTBac2(a, b, c, ref x, ref y);
        Assert.Equal(expected, count);
        Assert.Equal(first, x, 8); Assert.Equal(second, y, 8);
        if (count == 2) Assert.True(x <= y);
    }
}
