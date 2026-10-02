# Lab04 WinForm Basic - Giai đoạn 1

- Sinh viên: Nguyễn Huỳnh Phương Đông
- MSSV: 3124411071
- Tài liệu: Thực hành 04c - Window Form cơ bản 1

## Phạm vi đã hoàn thành

Tài liệu có 8 đặc tả ứng dụng nếu tính bài mẫu, 5 bài tại lớp, 1 bài nâng cao và 1 bài về nhà. Giai đoạn này hoàn thành đúng 4 ứng dụng đầu:

1. `BaiMau_ThongTinCaNhan`: nhập họ tên, năm sinh; kiểm tra bằng `ErrorProvider`; hiển thị tuổi; xóa dữ liệu và xác nhận thoát.
2. `Bai01_PhepTinh`: cộng, trừ, nhân, chia hai số; kiểm tra hai mức, chặn ký tự sai và xử lý chia cho 0.
3. `Bai02_DangKyTaiKhoan`: kiểm tra trường bắt buộc, định dạng email, mật khẩu xác nhận; hỗ trợ phím Enter và xác nhận đóng Form.
4. `Bai03_UocSoBoiSo`: nhập hai số nguyên dương, tính UCLN và BCNN; tiếp tục và xác nhận thoát.

Các phần chưa làm theo yêu cầu dừng ở nửa đầu: Bài 4 dãy số, Bài 5 đọc số thành chữ, bài nâng cao bán vé rạp phim và bài về nhà máy tính bỏ túi.

## Mở và chạy

Mở `Lab04_WinFormBasic.sln` bằng Visual Studio 2022, chọn project cần chạy làm **Startup Project**, sau đó nhấn `F5`.

Có thể build và tự kiểm tra phần xử lý nghiệp vụ bằng terminal:

```powershell
dotnet build Lab04_WinFormBasic.sln -c Release
dotnet run --project KiemTra/Lab04.SelfTest/Lab04.SelfTest.csproj -c Release
```

Các thư mục `bin` và `obj` không thuộc bài nộp.
