# Lab04D WinForm Basic - Giai đoạn 1

- Sinh viên: Nguyễn Huỳnh Phương Đông
- MSSV: 3124411071
- Tài liệu: Thực hành 04d - Windows Forms cơ bản 2

## Phạm vi đã hoàn thành

Tài liệu có 6 đặc tả ứng dụng nếu tính 2 bài mẫu, 2 bài tại lớp, 1 bài nâng cao và 1 bài về nhà. Giai đoạn này hoàn thành đúng 3 ứng dụng đầu:

1. `BaiMau01_PhepTinhRadio`: nhập hai số, chọn cộng, trừ, nhân hoặc chia bằng RadioButton; kiểm tra dữ liệu hai mức và xác nhận thoát.
2. `BaiMau02_DinhDangLabel`: dùng CheckBox thay đổi kiểu chữ và RadioButton thay đổi màu của Label.
3. `Bai01_GiaiPhuongTrinh`: giải phương trình bậc nhất hoặc bậc hai; điều khiển trạng thái ô nhập và nút Giải theo chế độ.

Các phần chưa làm theo yêu cầu dừng ở nửa đầu: Bài 2 mảng một chiều, bài nâng cao quản lý quán cà phê và bài về nhà quản lý tiền phòng khách sạn.

## Mở và chạy

Mở `Lab04D_WinFormBasic.sln` bằng Visual Studio 2022, chọn project Form cần chạy làm **Startup Project**, sau đó nhấn `F5`.

```powershell
dotnet build Lab04D_WinFormBasic.sln -c Release
dotnet run --project KiemTra/Lab04D.SelfTest/Lab04D.SelfTest.csproj -c Release
```

Các thư mục `bin` và `obj` không thuộc bài nộp.
