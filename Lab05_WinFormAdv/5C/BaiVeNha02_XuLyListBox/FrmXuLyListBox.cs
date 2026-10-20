// Form xử lý ListBox số tự nhiên: thêm, xóa, biến đổi và chọn phần tử theo điều kiện.
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lab05C.Core;

namespace BaiVeNha02_XuLyListBox;

public sealed class FrmXuLyListBox : Form
{
    private readonly NumberListProcessor _processor = new();
    private readonly TextBox _input = new();
    private readonly ListBox _numbers = new();
    private readonly ErrorProvider _errors = new();

    public FrmXuLyListBox()
    {
        Text = "Xử lý ListBox";
        ClientSize = new Size(760, 590);
        Font = new Font("Segoe UI", 10.5F);
        BackColor = Color.White;
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        Controls.Add(new Label { Text = "LISTBOX SỐ TỰ NHIÊN", AutoSize = true, Font = new Font("Segoe UI", 21F, FontStyle.Bold), ForeColor = Color.DarkOrange, Location = new Point(220, 20) });
        Controls.Add(new Label { Text = "Nhập số:", AutoSize = true, Location = new Point(55, 90) });
        _input.Location = new Point(150, 85); _input.Size = new Size(220, 32); _input.KeyPress += NaturalNumberKeyPress; Controls.Add(_input);
        Button add = CreateButton("&Nhập", 390, 83, 120, AddNumber); Controls.Add(add);
        _numbers.Location = new Point(55, 140); _numbers.Size = new Size(280, 350); _numbers.SelectionMode = SelectionMode.MultiExtended; Controls.Add(_numbers);

        // Bắt đầu nhóm thao tác thấp hơn nút Nhập để hai hàng điều khiển không chồng lên nhau.
        int y = 140;
        Controls.Add(CreateButton("Tổng các phần tử trong List", 390, y, 300, (_, _) => MessageBox.Show($"Tổng các phần tử: {_processor.Sum():N0}", "Kết quả"))); y += 52;
        Controls.Add(CreateButton("Xóa phần tử đầu và cuối", 390, y, 300, (_, _) => { _processor.RemoveFirstAndLast(); RefreshList(); })); y += 52;
        Controls.Add(CreateButton("Xóa phần tử đang chọn", 390, y, 300, (_, _) => { _processor.RemoveSelected(_numbers.SelectedIndices.Cast<int>()); RefreshList(); })); y += 52;
        Controls.Add(CreateButton("Tăng mỗi phần tử lên 2", 390, y, 300, (_, _) => { _processor.AddTwo(); RefreshList(); })); y += 52;
        Controls.Add(CreateButton("Thay bằng bình phương", 390, y, 300, SquareAll)); y += 52;
        Controls.Add(CreateButton("Chọn số chẵn", 390, y, 300, (_, _) => SelectIndices(_processor.EvenIndices().ToArray()))); y += 52;
        Controls.Add(CreateButton("Chọn số lẻ", 390, y, 300, (_, _) => SelectIndices(_processor.OddIndices().ToArray())));
        Controls.Add(CreateButton("KẾT THÚC", 210, 520, 340, (_, _) => Close()));

        _errors.ContainerControl = this; AcceptButton = add; FormClosing += ConfirmClosing;
    }

    private void AddNumber(object? sender, EventArgs e)
    {
        _errors.Clear();
        if (!_processor.TryAdd(_input.Text, out string error)) { _errors.SetError(_input, error); MessageBox.Show(error, "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
        _input.Clear(); _input.Focus(); RefreshList();
    }

    private void SquareAll(object? sender, EventArgs e)
    {
        try { _processor.Square(); RefreshList(); }
        catch (OverflowException) { MessageBox.Show("Giá trị quá lớn để bình phương bằng kiểu số nguyên.", "Tràn số", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }

    private void RefreshList() { _numbers.BeginUpdate(); _numbers.Items.Clear(); _numbers.Items.AddRange(_processor.Numbers.Cast<object>().ToArray()); _numbers.EndUpdate(); }
    private void SelectIndices(int[] indices) { _numbers.ClearSelected(); foreach (int index in indices) _numbers.SetSelected(index, true); }
    private static Button CreateButton(string text, int x, int y, int width, EventHandler click) { Button button = new() { Text = text, Location = new Point(x, y), Size = new Size(width, 40) }; button.Click += click; return button; }
    private static void NaturalNumberKeyPress(object? sender, KeyPressEventArgs e) { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; }
    private static void ConfirmClosing(object? sender, FormClosingEventArgs e) { if (MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) e.Cancel = true; }
}
