using System;
using System.Drawing;
using System.Windows.Forms;
using Lab04D.Core;

namespace Bai02_MangSoNguyen;

/// <summary>Giao diện thực hiện đầy đủ các thao tác trên mảng một chiều số nguyên.</summary>
public sealed class FrmMangSoNguyen : Form
{
    private readonly TextBox _txtInput = new();
    private readonly TextBox _txtResult = new();
    private readonly TextBox _txtSearch = new();
    private readonly TextBox _txtInsertValue = new();
    private readonly TextBox _txtInsertPosition = new();
    private readonly TextBox _txtDelete = new();
    private readonly TextBox _txtReplaceTarget = new();
    private readonly TextBox _txtReplacement = new();
    private readonly RadioButton _rdoAscending = new();
    private readonly RadioButton _rdoDescending = new();
    private readonly RadioButton _rdoFindValue = new();
    private readonly RadioButton _rdoFindPosition = new();
    private readonly RadioButton _rdoDeleteValue = new();
    private readonly RadioButton _rdoDeletePosition = new();
    private readonly RadioButton _rdoReplaceValue = new();
    private readonly RadioButton _rdoReplacePosition = new();
    private readonly Label _lblTotal = new();
    private readonly Label _lblEven = new();
    private readonly Label _lblOdd = new();
    private readonly Label _lblMax = new();
    private readonly Label _lblMin = new();
    private readonly ErrorProvider _errors = new();
    private MangSoNguyen? _array;

    public FrmMangSoNguyen()
    {
        Text = "Mảng số nguyên";
        ClientSize = new Size(980, 690);
        Font = new Font("Segoe UI", 10.5F);
        BackColor = Color.White;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;

        Controls.Add(new Label { AutoSize = true, Text = "MẢNG SỐ NGUYÊN", Font = new Font("Segoe UI", 23F, FontStyle.Bold), ForeColor = Color.Firebrick, Location = new Point(334, 20) });
        AddLabel("Nhập mảng:", 42, 93);
        ConfigureTextBox(_txtInput, 190, 88, 575);
        _txtInput.PlaceholderText = "Ví dụ: 5 6 4 7 8 9 10 5 6 3 2 1";
        Button btnLoad = CreateButton("&Nạp mảng", 785, 86, 135, 36, LoadArray);

        AddLabel("Kết quả mảng:", 42, 140);
        ConfigureTextBox(_txtResult, 190, 135, 575);
        _txtResult.ReadOnly = true;
        _txtResult.BackColor = Color.FromArgb(245, 248, 252);
        Button btnReset = CreateButton("&Reset", 785, 133, 135, 36, (_, _) => ResetForm());

        GroupBox sortGroup = CreateGroup("Sắp xếp", 42, 190, 410, 105);
        ConfigureRadio(_rdoAscending, "Sắp xếp tăng", 22, 33, true);
        ConfigureRadio(_rdoDescending, "Sắp xếp giảm", 215, 33);
        sortGroup.Controls.AddRange(new Control[] { _rdoAscending, _rdoDescending, CreateButton("Thực hiện", 135, 67, 130, 30, SortArray) });

        GroupBox searchGroup = CreateGroup("Tìm kiếm", 470, 190, 450, 105);
        ConfigureRadio(_rdoFindValue, "Tìm giá trị", 18, 32, true);
        ConfigureRadio(_rdoFindPosition, "Tìm vị trí", 18, 68);
        ConfigureTextBox(_txtSearch, 150, 43, 105, searchGroup);
        searchGroup.Controls.AddRange(new Control[] { _rdoFindValue, _rdoFindPosition, CreateButton("Tìm", 285, 42, 130, 34, FindItem) });

        GroupBox insertGroup = CreateGroup("Thêm phần tử", 42, 315, 410, 120);
        AddLabel("Giá trị cần thêm:", 18, 35, insertGroup);
        ConfigureTextBox(_txtInsertValue, 180, 30, 90, insertGroup);
        AddLabel("Vị trí cần thêm:", 18, 76, insertGroup);
        ConfigureTextBox(_txtInsertPosition, 180, 71, 90, insertGroup);
        insertGroup.Controls.Add(CreateButton("Thêm", 292, 48, 95, 38, InsertItem));

        GroupBox deleteGroup = CreateGroup("Xóa phần tử", 470, 315, 450, 120);
        ConfigureRadio(_rdoDeleteValue, "Xóa theo giá trị", 18, 34, true);
        ConfigureRadio(_rdoDeletePosition, "Xóa theo vị trí", 18, 75);
        ConfigureTextBox(_txtDelete, 205, 49, 75, deleteGroup);
        deleteGroup.Controls.AddRange(new Control[] { _rdoDeleteValue, _rdoDeletePosition, CreateButton("Xóa", 298, 48, 120, 38, DeleteItem) });

        GroupBox totalGroup = CreateGroup("Tổng", 42, 455, 275, 145);
        ConfigureValueLabel(_lblTotal, "Tổng mảng:", 18, 34, totalGroup);
        ConfigureValueLabel(_lblEven, "Tổng chẵn:", 18, 71, totalGroup);
        ConfigureValueLabel(_lblOdd, "Tổng lẻ:", 18, 108, totalGroup);

        GroupBox minMaxGroup = CreateGroup("Max - Min", 335, 455, 250, 145);
        ConfigureValueLabel(_lblMax, "Lớn nhất:", 18, 43, minMaxGroup);
        ConfigureValueLabel(_lblMin, "Nhỏ nhất:", 18, 91, minMaxGroup);

        GroupBox replaceGroup = CreateGroup("Thay thế", 603, 455, 317, 145);
        ConfigureRadio(_rdoReplaceValue, "Theo giá trị", 15, 29, true);
        ConfigureRadio(_rdoReplacePosition, "Theo vị trí", 15, 65);
        ConfigureTextBox(_txtReplaceTarget, 170, 39, 55, replaceGroup);
        AddLabel("Số mới:", 15, 104, replaceGroup);
        ConfigureTextBox(_txtReplacement, 92, 99, 85, replaceGroup);
        replaceGroup.Controls.AddRange(new Control[] { _rdoReplaceValue, _rdoReplacePosition, CreateButton("Thay", 230, 68, 68, 38, ReplaceItem) });

        Button btnStatistics = CreateButton("Tính tổng và Min/Max", 230, 620, 250, 42, (_, _) => ShowStatistics());
        Button btnExit = CreateButton("T&hoát", 510, 620, 190, 42, (_, _) => Close());
        Controls.AddRange(new Control[] { btnLoad, btnReset, sortGroup, searchGroup, insertGroup, deleteGroup, totalGroup, minMaxGroup, replaceGroup, btnStatistics, btnExit });
        AcceptButton = btnLoad;
        CancelButton = btnExit;
        _errors.ContainerControl = this;
        FormClosing += ConfirmClosing;
    }

    private void LoadArray(object? sender, EventArgs e)
    {
        _errors.Clear();
        if (!MangSoNguyen.TryParse(_txtInput.Text, out MangSoNguyen? array, out string error))
        {
            _errors.SetError(_txtInput, error);
            MessageBox.Show(error, "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        _array = array;
        RefreshArray();
        ShowStatistics();
    }

    private void SortArray(object? sender, EventArgs e)
    {
        if (!EnsureArray()) return;
        if (_rdoAscending.Checked) _array!.SortAscending(); else _array!.SortDescending();
        RefreshArray();
    }

    private void FindItem(object? sender, EventArgs e)
    {
        if (!EnsureArray() || !TryReadInt(_txtSearch, out int value)) return;
        if (_rdoFindValue.Checked)
        {
            int index = _array!.FindValue(value);
            MessageBox.Show(index >= 0 ? $"Giá trị {value} xuất hiện đầu tiên tại vị trí {index + 1}." : $"Không tìm thấy giá trị {value}.", "Kết quả tìm kiếm");
        }
        else
        {
            try { MessageBox.Show($"Phần tử tại vị trí {value} là {_array!.ValueAt(value)}.", "Kết quả tìm kiếm"); }
            catch (ArgumentOutOfRangeException ex) { ShowOperationError(_txtSearch, ex.Message); }
        }
    }

    private void InsertItem(object? sender, EventArgs e)
    {
        if (!EnsureArray() || !TryReadInt(_txtInsertValue, out int value) || !TryReadInt(_txtInsertPosition, out int position)) return;
        try { _array!.InsertAt(position, value); RefreshArray(); ShowStatistics(); }
        catch (ArgumentOutOfRangeException ex) { ShowOperationError(_txtInsertPosition, ex.Message); }
    }

    private void DeleteItem(object? sender, EventArgs e)
    {
        if (!EnsureArray() || !TryReadInt(_txtDelete, out int value)) return;
        if (_array!.Count == 1) { ShowOperationError(_txtDelete, "Mảng phải còn ít nhất một phần tử."); return; }
        try
        {
            bool removed = _rdoDeleteValue.Checked ? _array.RemoveValue(value) : RemoveAt(value);
            if (!removed) { ShowOperationError(_txtDelete, $"Không tìm thấy giá trị {value}."); return; }
            RefreshArray(); ShowStatistics();
        }
        catch (ArgumentOutOfRangeException ex) { ShowOperationError(_txtDelete, ex.Message); }
    }

    private bool RemoveAt(int position) { _array!.RemoveAt(position); return true; }

    private void ReplaceItem(object? sender, EventArgs e)
    {
        if (!EnsureArray() || !TryReadInt(_txtReplaceTarget, out int target) || !TryReadInt(_txtReplacement, out int replacement)) return;
        try
        {
            if (_rdoReplaceValue.Checked)
            {
                int count = _array!.ReplaceValue(target, replacement);
                if (count == 0) { ShowOperationError(_txtReplaceTarget, $"Không tìm thấy giá trị {target}."); return; }
            }
            else _array!.ReplaceAt(target, replacement);
            RefreshArray(); ShowStatistics();
        }
        catch (ArgumentOutOfRangeException ex) { ShowOperationError(_txtReplaceTarget, ex.Message); }
    }

    private void ShowStatistics()
    {
        if (!EnsureArray(false)) return;
        _lblTotal.Text = _array!.Sum.ToString();
        _lblEven.Text = _array.EvenSum.ToString();
        _lblOdd.Text = _array.OddSum.ToString();
        _lblMax.Text = _array.Maximum.ToString();
        _lblMin.Text = _array.Minimum.ToString();
    }

    private void RefreshArray() => _txtResult.Text = _array?.ToString() ?? string.Empty;

    private bool EnsureArray(bool showMessage = true)
    {
        if (_array is not null) return true;
        if (showMessage) MessageBox.Show("Hãy nhập và nạp mảng trước khi thực hiện thao tác.", "Chưa có mảng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return false;
    }

    private bool TryReadInt(TextBox box, out int value)
    {
        _errors.SetError(box, string.Empty);
        if (int.TryParse(box.Text.Trim(), out value)) return true;
        ShowOperationError(box, "Vui lòng nhập số nguyên hợp lệ.");
        return false;
    }

    private void ShowOperationError(Control control, string message)
    {
        _errors.SetError(control, message);
        MessageBox.Show(message, "Không thể thực hiện", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    private void ResetForm()
    {
        _array = null;
        foreach (TextBox box in new[] { _txtInput, _txtResult, _txtSearch, _txtInsertValue, _txtInsertPosition, _txtDelete, _txtReplaceTarget, _txtReplacement }) box.Clear();
        _errors.Clear();
        _rdoAscending.Checked = _rdoFindValue.Checked = _rdoDeleteValue.Checked = _rdoReplaceValue.Checked = true;
        _lblTotal.Text = _lblEven.Text = _lblOdd.Text = _lblMax.Text = _lblMin.Text = "0";
        _txtInput.Focus();
    }

    private static GroupBox CreateGroup(string text, int x, int y, int width, int height) => new() { Text = text, Location = new Point(x, y), Size = new Size(width, height) };
    private static Button CreateButton(string text, int x, int y, int width, int height, EventHandler click) { Button button = new() { Text = text, Location = new Point(x, y), Size = new Size(width, height) }; button.Click += click; return button; }
    private void AddLabel(string text, int x, int y, Control? parent = null) { Label label = new() { AutoSize = true, Text = text, Location = new Point(x, y) }; (parent?.Controls ?? Controls).Add(label); }
    private void ConfigureValueLabel(Label value, string text, int x, int y, Control parent) { AddLabel(text, x, y, parent); value.AutoSize = true; value.Text = "0"; value.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold); value.ForeColor = Color.Navy; value.Location = new Point(145, y); parent.Controls.Add(value); }
    private static void ConfigureRadio(RadioButton radio, string text, int x, int y, bool selected = false) { radio.Text = text; radio.AutoSize = true; radio.Location = new Point(x, y); radio.Checked = selected; }
    private void ConfigureTextBox(TextBox box, int x, int y, int width, Control? parent = null) { box.Location = new Point(x, y); box.Size = new Size(width, 29); (parent?.Controls ?? Controls).Add(box); }
    private void ConfirmClosing(object? sender, FormClosingEventArgs e) { if (MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) e.Cancel = true; }
}
