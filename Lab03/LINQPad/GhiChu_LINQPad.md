# Ghi chú thực hành LINQPad

## Cách chạy

1. Mở LINQPad và chọn ngôn ngữ **C# Program**.
2. Mở lần lượt chín tệp `.linq` trong thư mục này. Mỗi tệp tương ứng đúng một mục bài tập từ 2.1 đến 6.2.
3. Nhấn **F5** để chạy. LINQPad dùng `Dump()` để hiện dữ liệu dạng bảng/cây nên dễ quan sát cấu trúc nhóm hơn `Console.WriteLine`.
4. Sau khi đối chiếu kết quả, các truy vấn tương ứng đã được đưa vào dự án Console `BaiThucHanhLINQ` để chạy bằng VS Code hoặc lệnh `dotnet run`.

## Danh sách bài đầy đủ

- `Bai21_MangSo.linq`: so sánh **Query Syntax** với **Method Syntax**, đồng thời kiểm tra `Where` và `Select`.
- `Bai22_MangChuoi.linq`: lọc theo độ dài, đổi kiểu chữ, tìm ký tự và nhận diện chữ in hoa.
- `Bai31_ThongKe.linq`: thử `Count`, `Sum`, `Max`, `Min`, `Distinct` và xem cấu trúc từng nhóm do `GroupBy` tạo ra.
- `Bai32_ThongKeChuoi.linq`: tìm tên món ngắn/dài nhất, nhóm theo từ đầu và đếm món bắt đầu bằng “Bánh”.
- `Bai41_DuLieuMonHoc.linq`: khai báo lớp `MonHoc` và kiểm tra đủ 18 môn học trong đề.
- `Bai51_TruyVanMonHoc.linq`: thực hiện đầy đủ bốn truy vấn lọc và sắp xếp danh sách môn học.
- `Bai52_ThongKeMonHoc.linq`: thực hiện đầy đủ các câu a đến k, gồm thống kê, `GroupBy` và nhóm lồng nhau.
- `Bai61_DuLieuHe.linq`: khai báo lớp `He` và ba hệ KTV, CD, QT.
- `Bai62_Join.linq`: thực hiện đầy đủ các câu a đến i: inner join, left/full outer join, dữ liệu không khớp, top 5, đếm nhóm, `Distinct`, `FirstOrDefault` và đánh số trong nhóm.

## Nhận xét

- Query Syntax và Method Syntax tạo kết quả tương đương; Method Syntax thuận tiện khi nối nhiều toán tử liên tiếp.
- LINQ sử dụng cơ chế thực thi trì hoãn đối với truy vấn trả về `IEnumerable<T>`. Truy vấn chỉ thật sự được duyệt khi gọi `Dump()`, `foreach`, `ToList()` hoặc một toán tử tổng hợp.
- `GroupBy` trả về tập hợp các nhóm; mỗi nhóm có `Key` và các phần tử thuộc khóa đó.
- Inner join loại bỏ dữ liệu không có khóa tương ứng. Left outer join vẫn giữ mọi hệ đào tạo; full outer join còn giữ cả môn XYZ chưa khai báo hệ.
- Trong dự án Console, kết quả được gắn nhãn rõ ràng và in bằng `foreach`; các vòng lặp chỉ dùng để trình bày, không thay thế thao tác truy vấn LINQ.
