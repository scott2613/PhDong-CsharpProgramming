# Lab04 WinForm Basic - Bản hoàn chỉnh

- Sinh viên: Nguyễn Huỳnh Phương Đông
- MSSV: 3124411071
- Tài liệu: Thực hành 04c - Window Form cơ bản 1

## Phạm vi đã hoàn thành

Tài liệu có 8 đặc tả ứng dụng nếu tính bài mẫu, 5 bài tại lớp, 1 bài nâng cao và 1 bài về nhà. Bài nộp đã hoàn thành đủ 8 ứng dụng:

1. `BaiMau_ThongTinCaNhan`: nhập họ tên, năm sinh; kiểm tra bằng `ErrorProvider`; hiển thị tuổi; xóa dữ liệu và xác nhận thoát.
2. `Bai01_PhepTinh`: cộng, trừ, nhân, chia hai số; kiểm tra hai mức, chặn ký tự sai và xử lý chia cho 0.
3. `Bai02_DangKyTaiKhoan`: kiểm tra trường bắt buộc, định dạng email, mật khẩu xác nhận; hỗ trợ phím Enter và xác nhận đóng Form.
4. `Bai03_UocSoBoiSo`: nhập hai số nguyên dương, tính UCLN và BCNN; tiếp tục và xác nhận thoát.
5. `Bai04_DaySoVaTinhTong`: nhập nhiều số nguyên; hiển thị dãy, tổng toàn bộ, tổng chẵn và tổng lẻ.
6. `Bai05_DocSoThanhChu`: đọc số nguyên từ 1 đến 999 thành chữ tiếng Việt.
7. `BaiNangCao_BanVeRapPhim`: chọn 15 ghế, quản lý ba trạng thái bằng màu và tính tiền theo ba lô ghế.
8. `BaiTapVeNha_MayTinhBoTui`: máy tính cộng, trừ, nhân, chia, tính kết quả và xóa dữ liệu.

## Mở và chạy

Mở `Lab04_WinFormBasic.sln` bằng Visual Studio 2022, chọn project cần chạy làm **Startup Project**, sau đó nhấn `F5`.

Có thể build và tự kiểm tra phần xử lý nghiệp vụ bằng terminal:

```powershell
dotnet build Lab04_WinFormBasic.sln -c Release
dotnet run --project KiemTra/Lab04.SelfTest/Lab04.SelfTest.csproj -c Release
```

Bộ tự kiểm tra gồm 24 điều kiện cho toàn bộ tám ứng dụng.

Các thư mục `bin` và `obj` không thuộc bài nộp.
