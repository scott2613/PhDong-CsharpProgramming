# Thiết kế bài thực hành 01 ngôn ngữ lập trình C Sharp

## Mục tiêu

Hoàn thành đầy đủ 17 bài tập trong tài liệu thực hành 01. Mỗi bài có mã nguồn C Sharp độc lập, chú thích tiếng Việt rõ ràng, chạy được bằng .NET SDK và có báo cáo Word trình bày kết quả cùng phần giải thích.

Thông tin sinh viên dùng thống nhất trong báo cáo và gói nộp bài:

- Họ tên: Nguyễn Huỳnh Phương Đông
- Mã số sinh viên: 3124411071

## Cấu trúc mã nguồn

Thư mục gốc chứa solution chung và các thư mục `Bai01` đến `Bai17`. Mỗi bài là một ứng dụng Console riêng, có thể build và chạy độc lập.

Riêng `Bai01` được chia thành ba phần:

- `B1`: chương trình nhập và xuất họ tên.
- `B2`: mã MSIL được xuất từ chương trình B1.
- `B3`: kết quả lắp ráp lại mã MSIL thành file PE khi bộ công cụ .NET Framework trên máy hỗ trợ `ildasm.exe` và `ilasm.exe`.

Các thư mục sinh tự động như `bin` và `obj` không được đưa vào Git hoặc gói nộp bài.

## Nguyên tắc cài đặt

- Code ở mức nhập môn, ưu tiên câu lệnh và cấu trúc đã xuất hiện trong nội dung môn học.
- Tên lớp, phương thức và biến rõ nghĩa; chú thích tiếng Việt giải thích mục đích thay vì lặp lại câu lệnh.
- Dữ liệu nhập từ bàn phím được kiểm tra để tránh chương trình dừng đột ngột.
- Các bài yêu cầu `return`, `bool`, `ref`, `out` và phương thức thành viên phải thể hiện đúng kỹ thuật tương ứng.
- Kết quả hiển thị bằng tiếng Việt không dấu để tương thích ổn định với nhiều cửa sổ Console; chú thích mã nguồn vẫn dùng tiếng Việt đầy đủ dấu.

## Kiểm tra

- Build toàn bộ solution bằng .NET SDK.
- Chạy thử từng chương trình bằng dữ liệu mẫu có kiểm soát.
- Kiểm tra thêm trường hợp sai định dạng cho các bài có nhập số.
- Đối chiếu đầu ra với yêu cầu trong đề, đặc biệt là lũy thừa, số nguyên tố, chuỗi, mảng và ma trận.
- Lưu kết quả chạy tiêu biểu để đưa vào báo cáo.

## Báo cáo Word

Báo cáo được tạo dưới dạng DOCX và gồm:

1. Trang bìa theo phong cách mẫu của Trường Đại học Sài Gòn, có tên trường, khoa, logo, tên bài thực hành, họ tên và mã số sinh viên.
2. Mục lục nội dung.
3. Phần trình bày 17 bài, mỗi bài có yêu cầu, mã nguồn chính, kết quả chạy và giải thích cách làm.
4. Kết luận ngắn về các kiến thức đã thực hành.

Các trang nội dung có watermark mờ ghi `NGUYỄN HUỲNH PHƯƠNG ĐÔNG - 3124411071`. Tài liệu sử dụng kiểu chữ thống nhất, tiêu đề phân cấp rõ, hình kết quả dễ đọc và số trang ở chân trang. Báo cáo phải được render thành ảnh để kiểm tra toàn bộ các trang trước khi giao.

## Đóng gói và Git

Gói nộp bài có tên `Lab01_NguyenHuynhPhuongDong_3124411071.zip`, chứa solution, 17 thư mục bài tập, báo cáo DOCX và README hướng dẫn build/chạy. Gói không chứa thư mục build tạm hoặc dữ liệu kiểm thử nội bộ.

Lịch sử Git được chia theo các mốc công việc tự nhiên: thiết kế và cấu trúc ban đầu, nhóm bài nhập xuất, nhóm bài phương thức, nhóm bài chuỗi, nhóm bài lớp và mảng, báo cáo, rồi bước hoàn thiện đóng gói. Sau khi kiểm tra xong, repository được đẩy lên `https://github.com/scott2613/PhDong-CsharpProgramming` bằng tài khoản người dùng.

## Tiêu chí hoàn thành

- Có đủ 17 bài và cấu trúc thư mục đúng quy ước.
- Toàn bộ project build thành công.
- Các ca chạy thử chính cho kết quả đúng.
- Báo cáo DOCX có trang bìa, watermark, nội dung, kết quả và giải thích cho từng bài.
- File ZIP đúng tên, mở được và chỉ chứa các tập tin cần nộp.
- Repository GitHub có lịch sử commit rõ ràng và đầy đủ.
