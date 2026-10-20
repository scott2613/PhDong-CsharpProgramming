// Form bài mẫu ListBox: chuyển một, nhiều hoặc toàn bộ phần tử giữa hai danh sách.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lab05C.Core;

namespace BaiMau01_ListBox;

public sealed class FrmListBox : Form
{
    private readonly ListBox _leftList = new();
    private readonly ListBox _rightList = new();
    private readonly List<string> _left = new() { "Cóc", "Ổi", "Xoài", "Me", "Bưởi", "Cam" };
    private readonly List<string> _right = new();

    public FrmListBox()
    {
        Text = "Sử dụng ListBox";
        ClientSize = new Size(720, 430);
        Font = new Font("Segoe UI", 11F);
        BackColor = Color.White;
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        Controls.Add(new Label { Text = "CHUYỂN PHẦN TỬ LISTBOX", AutoSize = true, Font = new Font("Segoe UI", 20F, FontStyle.Bold), ForeColor = Color.Firebrick, Location = new Point(170, 25) });
        ConfigureList(_leftList, 45, 95);
        ConfigureList(_rightList, 455, 95);
        _leftList.SelectionMode = SelectionMode.MultiExtended;
        _rightList.SelectionMode = SelectionMode.MultiExtended;

        Controls.Add(CreateButton(">", 310, 105, (_, _) => MoveOne(_left, _right, _leftList.SelectedIndex)));
        Controls.Add(CreateButton(">>", 310, 160, (_, _) => MoveAll(_left, _right)));
        Controls.Add(CreateButton("<", 310, 215, (_, _) => MoveOne(_right, _left, _rightList.SelectedIndex)));
        Controls.Add(CreateButton("<<", 310, 270, (_, _) => MoveAll(_right, _left)));
        Controls.Add(CreateButton("Chuyển tùy ý", 270, 335, (_, _) => MoveSelected()));
        RefreshLists();
        FormClosing += ConfirmClosing;
    }

    private void MoveOne(List<string> source, List<string> destination, int index)
    {
        if (!TransferService.MoveOne(source, destination, index))
        {
            MessageBox.Show("Vui lòng chọn một phần tử cần chuyển.", "Thiếu lựa chọn", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        RefreshLists();
    }

    private void MoveAll(List<string> source, List<string> destination)
    {
        TransferService.MoveAll(source, destination);
        RefreshLists();
    }

    private void MoveSelected()
    {
        int[] indices = _leftList.SelectedIndices.Cast<int>().ToArray();
        if (TransferService.MoveSelected(_left, _right, indices) == 0)
        {
            MessageBox.Show("Hãy chọn một hoặc nhiều phần tử ở danh sách bên trái.", "Thiếu lựa chọn", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        RefreshLists();
    }

    private void RefreshLists()
    {
        _leftList.DataSource = null; _leftList.DataSource = _left.ToList();
        _rightList.DataSource = null; _rightList.DataSource = _right.ToList();
    }

    private void ConfigureList(ListBox list, int x, int y)
    {
        list.Location = new Point(x, y);
        list.Size = new Size(220, 220);
        Controls.Add(list);
    }

    private static Button CreateButton(string text, int x, int y, EventHandler click)
    {
        Button button = new() { Text = text, Location = new Point(x, y), Size = new Size(text.Length > 2 ? 180 : 100, 42) };
        button.Click += click;
        return button;
    }

    private static void ConfirmClosing(object? sender, FormClosingEventArgs e)
    {
        if (MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) e.Cancel = true;
    }
}
