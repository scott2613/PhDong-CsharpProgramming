# Hướng dẫn lắp ráp lại mã MSIL

Mở **Developer Command Prompt for Visual Studio**, chuyển đến thư mục `Bai01` và thực hiện:

```bat
ildasm bin\Debug\net8.0\Bai01.dll /out=B2\Bai01.il
ilasm B2\Bai01.il /output=B3\Bai01.exe /exe
```

`ildasm.exe` dùng để xem và xuất mã MSIL. `ilasm.exe` đọc file `.il` rồi tạo lại file PE. Hai công cụ này thuộc bộ Visual Studio/.NET Framework Developer Tools nên có thể không hiện diện trong bản .NET SDK tối giản.

