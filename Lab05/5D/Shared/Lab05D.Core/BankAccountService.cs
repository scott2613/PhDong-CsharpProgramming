// Mô hình tài khoản ngân hàng và các quy tắc thêm, xóa, tính tổng tiền.
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Lab05D.Core;

public sealed class BankAccount
{
    public BankAccount(string number, string customerName, string address, decimal balance)
    {
        Number = number;
        CustomerName = customerName;
        Address = address;
        Balance = balance;
    }

    public string Number { get; }
    public string CustomerName { get; }
    public string Address { get; }
    public decimal Balance { get; }
}

public sealed class BankAccountService
{
    private readonly List<BankAccount> _accounts = new List<BankAccount>();
    public IReadOnlyList<BankAccount> Accounts => _accounts;
    public IReadOnlyList<BankAccount> Items => _accounts;
    public decimal TotalBalance => _accounts.Sum(item => item.Balance);

    public bool Add(string? number, string? customerName, string? address, string? balanceText, out string error)
    {
        string normalizedNumber = (number ?? string.Empty).Trim();
        if (normalizedNumber.Length == 0 || string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(address))
        {
            error = "Số tài khoản, tên khách hàng và địa chỉ không được để trống.";
            return false;
        }
        if (_accounts.Any(item => item.Number.Equals(normalizedNumber, StringComparison.OrdinalIgnoreCase)))
        {
            error = "Số tài khoản đã tồn tại.";
            return false;
        }
        if (!decimal.TryParse(balanceText, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal balance) || balance < 0)
        {
            error = "Số tiền phải là số không âm.";
            return false;
        }

        _accounts.Add(new BankAccount(normalizedNumber, customerName!.Trim(), address!.Trim(), balance));
        error = string.Empty;
        return true;
    }

    public bool TryAdd(string? number, string? customerName, string? address, string? balanceText, out string error) =>
        Add(number, customerName, address, balanceText, out error);

    public bool Remove(string number)
    {
        int index = _accounts.FindIndex(item => item.Number.Equals(number, StringComparison.OrdinalIgnoreCase));
        return RemoveAt(index);
    }

    public bool RemoveAt(int index)
    {
        if (index < 0 || index >= _accounts.Count) return false;
        _accounts.RemoveAt(index);
        return true;
    }
}
