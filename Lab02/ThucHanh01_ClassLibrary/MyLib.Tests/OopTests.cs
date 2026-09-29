using System;
using Xunit;

namespace MyLib.Tests;

public class OopTests
{
    [Fact] public void TuoiTheoNam() => Assert.Equal(20, new Bai01_01_TuoiSinhVien.SinhVien("Đông", 2006).TinhTuoi(2026));
    [Fact]
    public void PointDistanceMidpointOperators()
    {
        var a = new Bai01_02_Point.Point(0, 0); var b = new Bai01_02_Point.Point(3, 4);
        Assert.Equal(5, a.Distance(b)); Assert.Equal(5, Bai01_02_Point.Point.Distance(a, b));
        Assert.Equal(1.5, a.Midpoint(b).X); Assert.Equal(2, Bai01_02_Point.Point.Midpoint(a, b).Y);
        Assert.Equal(3, (a + b).X); Assert.Equal(-4, (a - b).Y); Assert.Equal(-3, (-b).X);
    }
    [Fact]
    public void PersonSaoChepVaTrangThai()
    {
        var p = new Bai01_03_Person.Person("P", "An", 2000, 0);
        var q = new Bai01_03_Person.Person(p); q.Name = "Bình"; q.Yod = 2020;
        Assert.True(p.IsLiving()); Assert.False(q.IsLiving()); Assert.Equal("An", p.Name);
        Assert.Throws<ArgumentOutOfRangeException>(() => q.Yod = 1990);
    }
    [Fact]
    public void PhanSoToanTuVaChuanHoa()
    {
        var a = new Bai01_04_PhanSo.PhanSo(1, 2); var b = new Bai01_04_PhanSo.PhanSo(3, 4);
        Assert.Equal("5/4", (a + b).ToString()); Assert.Equal("-1/4", (a - b).ToString());
        Assert.Equal("3/8", (a * b).ToString()); Assert.Equal("2/3", (a / b).ToString());
        Assert.Equal("-1/2", (-a).ToString()); Assert.Equal(a, +a);
        Assert.True(a < b); Assert.True(b > a); Assert.True(a <= b); Assert.True(b >= a); Assert.True(a != b);
        var c = new Bai01_04_PhanSo.PhanSo(-2, -4);
        Assert.True(a == c); Assert.Equal(a.GetHashCode(), c.GetHashCode());
        Assert.Throws<DivideByZeroException>(() => a / new Bai01_04_PhanSo.PhanSo());
        Assert.Throws<DivideByZeroException>(() => new Bai01_04_PhanSo.PhanSo(1, 0));
    }
    [Fact]
    public void DaoHamHangSoVaBacHai()
    {
        var p = new Bai01_05_DonThuc.DonThuc(3, 2);
        Assert.Equal(12, p.Tinh(2)); Assert.Equal(6, p.DaoHam().HeSo); Assert.Equal(1, p.DaoHam().SoMu);
        Assert.Equal(0, new Bai01_05_DonThuc.DonThuc(5, 0).DaoHam().Tinh(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Bai01_05_DonThuc.DonThuc(1, -1));
    }
    [Fact]
    public void ArrayPointIndexerVaSaoChep()
    {
        var a = new Bai02_01_ArrayPoint.ArrayPoint(); a.Add(new Bai02_01_ArrayPoint.Point(1, 2));
        var b = new Bai02_01_ArrayPoint.ArrayPoint(a); b[0] = new Bai02_01_ArrayPoint.Point(3, 4);
        Assert.Equal(1, a[0].X); Assert.Equal(3, b[0].X);
    }
    [Fact]
    public void LocNguoiConSong()
    {
        var a = new Bai02_02_PersonList.PersonList();
        a.Add(new Bai02_02_PersonList.Person("P1", "An", 2000, 0));
        a.Add(new Bai02_02_PersonList.Person("P2", "Bình", 1950, 2020));
        Assert.Equal(1, a.LivingPeople().Count); Assert.Equal(2, new Bai02_02_PersonList.PersonList(a).Count);
        Assert.Equal(0, new Bai02_02_PersonList.PersonList().LivingPeople().Count);
    }
    [Fact]
    public void DaySoLocChanVaClone()
    {
        var a = new Bai02_03_DaySo.DaySo(new[] { -2, 0, 3, 4 }); var b = new Bai02_03_DaySo.DaySo(a); b[0] = 9;
        Assert.Equal(-2, a[0]); Assert.Equal(3, a.SoChan().Count); Assert.Equal(0, new Bai02_03_DaySo.DaySo().SoChan().Count);
    }
    [Fact]
    public void MaTranNguyenTo()
    {
        var a = new Bai02_04_MaTran.MaTran(new[,] { { -1, 0, 1 }, { 2, 3, 4 } });
        Assert.Equal(new[] { 2, 3 }, a.SoNguyenTo());
        Assert.True(Bai02_04_MaTran.MaTran.LaNguyenTo(2147483647));
        var b = new Bai02_04_MaTran.MaTran(a); b[0, 0] = 7; Assert.Equal(-1, a[0, 0]);
    }
    [Fact]
    public void DaThucHornerIndexer()
    {
        var p = new Bai02_03b_DaThuc.DaThuc(new double[] { 1, -2, 3 });
        Assert.Equal(9, p.Tinh(2)); Assert.Equal(1, p.Tinh(0));
        var q = new Bai02_03b_DaThuc.DaThuc(p); q[0] = new Bai02_03b_DaThuc.DonThuc(7, 0);
        Assert.Equal(1, p[0].HeSo); Assert.Equal(7, q[0].HeSo);
        Assert.Throws<ArgumentException>(() => p[0] = new Bai02_03b_DaThuc.DonThuc(1, 1));
    }
    [Fact]
    public void TongPhanSo()
    {
        var a = new Bai02_04b_DayPhanSo.DayPhanSo(new[] { new Bai02_04b_DayPhanSo.PhanSo(1, 2), new Bai02_04b_DayPhanSo.PhanSo(1, 3), new Bai02_04b_DayPhanSo.PhanSo(1, 6) });
        Assert.Equal("1", a.Tong().ToString()); Assert.Equal("0", new Bai02_04b_DayPhanSo.DayPhanSo().Tong().ToString());
    }
    [Fact]
    public void TongLuongPhongBan()
    {
        var p = new Bai02_05_LuongPhongBan.PhongBan();
        p.Add(new Bai02_05_LuongPhongBan.NhanVien("An", 10000000, 2));
        p.Add(new Bai02_05_LuongPhongBan.NhanVien("Bình", 8000000, 1));
        Assert.Equal(17700000m, p.TongLuong());
    }
    [Fact]
    public void ArraySortDoiTuong()
    {
        var a = new[] { new Bai03_01_ArraySort.SinhVien("A", 8), new Bai03_01_ArraySort.SinhVien("B", 6) };
        Array.Sort(a); Assert.Equal(6, a[0].Diem);
    }
    [Fact]
    public void SortInterfaceSoVaDoiTuong()
    {
        int[] a = { 3, 1, 1, -2 }; Bai03_02_InterfaceSort.SapXep.Sort(a); Assert.Equal(new[] { -2, 1, 1, 3 }, a);
        var b = new[] { new Bai03_02_InterfaceSort.SinhVien("A", 8), new Bai03_02_InterfaceSort.SinhVien("B", 6) };
        Bai03_02_InterfaceSort.SapXep.Sort(b); Assert.Equal(6, b[0].Diem);
        Bai03_02_InterfaceSort.SapXep.Sort(Array.Empty<int>());
    }
    [Fact]
    public void SortDelegateHaiChieu()
    {
        int[] a = { 3, 1, 2 }; Bai03_03_DelegateSort.SapXep.Sort(a, (x, y) => y.CompareTo(x)); Assert.Equal(new[] { 3, 2, 1 }, a);
        Bai03_03_DelegateSort.SapXep.Sort(a, (x, y) => x.CompareTo(y)); Assert.Equal(new[] { 1, 2, 3 }, a);
    }
    [Fact]
    public void MenuChiPhatSuKienKhiHopLe()
    {
        var menu = new Bai03_04_ConsoleMenu.ConsoleMenu(); menu.Add(1, "Một");
        int count = 0; menu.Choose += (_, e) => { Assert.Equal(1, e.Choice); count++; };
        Assert.False(menu.Select(9)); Assert.Equal(0, count);
        Assert.True(menu.Select(1)); Assert.Equal(1, count);
    }
    [Theory]
    [InlineData(3000, 3000000)]
    [InlineData(3001, 3151050)]
    [InlineData(0, 0)]
    public void LuongSanXuatBienThuong(int n, int expected) => Assert.Equal((decimal)expected, new Bai03_05_LuongKeThua.NhanVienSanXuat("A", "An", n).TinhLuong());
    [Fact] public void LuongKinhDoanh() => Assert.Equal(10000000m, new Bai03_05_LuongKeThua.NhanVienKinhDoanh("A", "An", 8000000, 4).TinhLuong());
    [Theory]
    [InlineData(6.9, 24)]
    [InlineData(7, 25)]
    [InlineData(8, 25)]
    [InlineData(8.5, 24)]
    [InlineData(9, 26)]
    [InlineData(10, 26)]
    public void ThuongTiengAnh(double anh, double expected) => Assert.Equal(expected, new Bai03_06_ThiSinh.Chuyen("A", "An", 7, 8, 9, anh).TongDiem);
    [Fact] public void DiemSieuCup() => Assert.Equal(32, new Bai03_06_ThiSinh.SieuCup("S", "Bình", 8, 8, 9, 7).TongDiem);
}
