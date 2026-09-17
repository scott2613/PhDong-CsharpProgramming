# Bài thực hành 01 Ngôn ngữ lập trình C Sharp

## Thông tin sinh viên

- Họ tên: Nguyễn Huỳnh Phương Đông
- Mã số sinh viên: 3124411071
- Nội dung: Lab 01 - Làm quen với ngôn ngữ C#

## Cấu trúc bài nộp

- `Bai01` đến `Bai17`: mỗi thư mục là một ứng dụng Console độc lập.
- `Bai01/B2/Bai01.il`: mã MSIL của phần B2.
- `Bai01/B3/Bai01.exe`: file PE được lắp ráp lại từ mã MSIL bằng `ilasm.exe`.
- `Lab01.Tests`: bộ kiểm thử tự động không phụ thuộc gói NuGet ngoài.
- `scripts/run_samples.ps1`: chạy dữ liệu mẫu và lưu kết quả của 17 bài.
- `BaoCao_Lab01_NguyenHuynhPhuongDong_3124411071.docx`: báo cáo thực hành.

## Yêu cầu môi trường

- .NET SDK 8.0 trở lên.
- Visual Studio 2022, Visual Studio Code hoặc cửa sổ lệnh đều có thể sử dụng.
- Developer Command Prompt for Visual Studio nếu cần thực hiện lại Bài 1 phần B2 và B3.

## Build toàn bộ bài

```powershell
dotnet build Lab01.sln
```

## Chạy một bài

Thay `Bai03` bằng thư mục bài muốn chạy:

```powershell
dotnet run --project Bai03/Bai03.csproj
```

## Chạy kiểm thử

```powershell
dotnet run --project Lab01.Tests/Lab01.Tests.csproj
```

Kết quả đạt yêu cầu khi dòng cuối hiển thị `0 loi`.

## Chạy toàn bộ dữ liệu mẫu

```powershell
pwsh -File scripts/run_samples.ps1
```

Script tạo các transcript UTF-8 trong `artifacts/sample-output`. Thư mục `artifacts` chỉ phục vụ kiểm tra và không nằm trong gói nộp bài.

## Ghi chú Bài 1

`ildasm.exe` và `ilasm.exe` thuộc bộ công cụ phát triển .NET Framework. Hướng dẫn lệnh cụ thể được lưu tại `Bai01/B3/README.md`. File `Bai01.exe` trong B3 đã được tạo thành công bằng Microsoft .NET Framework IL Assembler 4.8.
