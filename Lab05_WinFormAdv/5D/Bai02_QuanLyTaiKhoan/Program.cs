using System;
using System.Globalization;
using System.Windows.Forms;
using Lab05D.Core;
using Lab05D.WinForms;

namespace Bai02_QuanLyTaiKhoan;

internal static class Program
{
    [STAThread] private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmQuanLyTaiKhoan());
    }
}

public sealed class FrmQuanLyTaiKhoan : Form
{
    private readonly TextBox _number = Ui.TextBox(160);
    private readonly TextBox _name = Ui.TextBox(220);
    private readonly TextBox _address = Ui.TextBox(250);
    private readonly TextBox _balance = Ui.TextBox(150);
    private readonly ListView _list = Ui.ListView(("STT", 65), ("Số tài khoản", 145), ("Khách hàng", 220), ("Địa chỉ", 250), ("Số dư", 140));
    private readonly Label _total = Ui.Label("Tổng tiền: 0 VNĐ");
    private readonly BankAccountService _service = new();
    private readonly Button _save;
    private bool _adding;

    public FrmQuanLyTaiKhoan()
    {
        Ui.Configure(this, "Bài 2 - Quản lý tài khoản", 980, 620);
        _save = Ui.Button("Lưu", (_, _) => Save());
        Button add = Ui.Button("Thêm / Hủy", (_, _) => ToggleAdd());
        Controls.Add(_list);
        Controls.Add(Ui.Row(add, _save, Ui.Button("Xóa", (_, _) => Delete()), Ui.Button("Thoát", (_, _) => Close()), _total));
        Controls.Add(Ui.Row(Ui.Label("Địa chỉ:"), _address, Ui.Label("Số dư:"), _balance));
        Controls.Add(Ui.Row(Ui.Label("Số tài khoản:"), _number, Ui.Label("Tên khách hàng:"), _name));
        Controls.Add(Ui.Title("QUẢN LÝ TÀI KHOẢN NGÂN HÀNG"));
        SetEditMode(false);
        _list.SelectedIndexChanged += (_, _) => LoadSelected();
    }

    private void ToggleAdd()
    {
        _adding = !_adding;
        SetEditMode(_adding);
        ClearInputs();
    }

    private void SetEditMode(bool enabled)
    {
        _number.Enabled = _name.Enabled = _address.Enabled = _balance.Enabled = enabled;
        _save.Enabled = enabled;
    }

    private void Save()
    {
        if (!_service.TryAdd(_number.Text, _name.Text, _address.Text, _balance.Text, out string error))
        { MessageBox.Show(error, "Dữ liệu chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
        _adding = false; SetEditMode(false); RefreshList(); ClearInputs();
    }

    private void Delete()
    {
        if (_list.SelectedItems.Count == 0) { MessageBox.Show("Hãy chọn tài khoản cần xóa."); return; }
        _service.Remove(_list.SelectedItems[0].SubItems[1].Text); RefreshList(); ClearInputs();
    }

    private void LoadSelected()
    {
        if (_list.SelectedItems.Count == 0) return;
        ListViewItem item = _list.SelectedItems[0];
        _number.Text = item.SubItems[1].Text; _name.Text = item.SubItems[2].Text;
        _address.Text = item.SubItems[3].Text; _balance.Text = item.SubItems[4].Text.Replace(" VNĐ", "").Replace(".", "");
    }

    private void RefreshList()
    {
        _list.Items.Clear();
        int index = 1;
        foreach (BankAccount account in _service.Items)
            _list.Items.Add(new ListViewItem(new[] { (index++).ToString(), account.Number, account.CustomerName, account.Address, account.Balance.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " VNĐ" }));
        _total.Text = "Tổng tiền: " + _service.TotalBalance.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " VNĐ";
    }

    private void ClearInputs() { _number.Clear(); _name.Clear(); _address.Clear(); _balance.Clear(); if (_adding) _number.Focus(); }
}
