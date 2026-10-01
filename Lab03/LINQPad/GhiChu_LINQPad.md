# Ghi chú thực hành LINQPad

## Cách chạy

1. Mở LINQPad và chọn ngôn ngữ **C# Program**.
2. Mở lần lượt ba tệp `.linq` trong thư mục này.
3. Nhấn **F5** để chạy. LINQPad dùng `Dump()` để hiện dữ liệu dạng bảng/cây nên dễ quan sát cấu trúc nhóm hơn `Console.WriteLine`.
4. Sau khi đối chiếu kết quả, các truy vấn tương ứng đã được đưa vào dự án Console `BaiThucHanhLINQ` để chạy bằng VS Code hoặc lệnh `dotnet run`.

## Nội dung đã thử

- `Bai21_MangSo.linq`: so sánh **Query Syntax** với **Method Syntax**, đồng thời kiểm tra `Where` và `Select`.
- `Bai31_ThongKe.linq`: thử `Count`, `Sum`, `Max`, `Min`, `Distinct` và xem cấu trúc từng nhóm do `GroupBy` tạo ra.
- `Bai62_Join.linq`: phân biệt `join` thông thường với `group join` kết hợp `DefaultIfEmpty()` để tạo left outer join.

## Nhận xét

- Query Syntax và Method Syntax tạo kết quả tương đương; Method Syntax thuận tiện khi nối nhiều toán tử liên tiếp.
- LINQ sử dụng cơ chế thực thi trì hoãn đối với truy vấn trả về `IEnumerable<T>`. Truy vấn chỉ thật sự được duyệt khi gọi `Dump()`, `foreach`, `ToList()` hoặc một toán tử tổng hợp.
- `GroupBy` trả về tập hợp các nhóm; mỗi nhóm có `Key` và các phần tử thuộc khóa đó.
- Inner join loại bỏ dữ liệu không có khóa tương ứng. Left outer join vẫn giữ mọi hệ đào tạo và biểu diễn hệ chưa có môn bằng dòng “không có môn”.
- Trong dự án Console, kết quả được gắn nhãn rõ ràng và in bằng `foreach`; các vòng lặp chỉ dùng để trình bày, không thay thế thao tác truy vấn LINQ.
