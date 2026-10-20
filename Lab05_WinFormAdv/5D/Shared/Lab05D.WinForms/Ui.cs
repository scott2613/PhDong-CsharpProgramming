using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab05D.WinForms;

/// <summary>Các hàm dùng chung giúp những biểu mẫu trong Lab05D có bố cục đồng nhất.</summary>
public static class Ui
{
    public static readonly Color Primary = Color.FromArgb(35, 88, 138);
    public static readonly Color Accent = Color.FromArgb(231, 242, 251);

    public static void Configure(Form form, string title, int width = 980, int height = 650)
    {
        form.Text = title;
        form.StartPosition = FormStartPosition.CenterScreen;
        form.ClientSize = new Size(width, height);
        form.Font = new Font("Segoe UI", 10F);
        form.BackColor = Color.White;
        form.AutoScaleMode = AutoScaleMode.Dpi;
    }

    public static Label Title(string text) => new()
    {
        Text = text,
        Dock = DockStyle.Top,
        Height = 62,
        TextAlign = ContentAlignment.MiddleCenter,
        Font = new Font("Segoe UI", 18F, FontStyle.Bold),
        ForeColor = Primary,
        BackColor = Accent
    };

    public static Button Button(string text, EventHandler handler)
    {
        Button button = new() { Text = text, AutoSize = true, MinimumSize = new Size(112, 40), Margin = new Padding(6) };
        button.Click += handler;
        return button;
    }

    public static TextBox TextBox(int width = 220) => new() { Width = width, Margin = new Padding(6) };

    public static Label Label(string text) => new() { Text = text, AutoSize = true, Margin = new Padding(6, 11, 6, 6) };

    public static FlowLayoutPanel Row(params Control[] controls)
    {
        FlowLayoutPanel panel = new() { AutoSize = true, Dock = DockStyle.Top, Padding = new Padding(12, 8, 12, 4), WrapContents = true };
        panel.Controls.AddRange(controls);
        return panel;
    }

    public static ListView ListView(params (string Name, int Width)[] columns)
    {
        ListView list = new() { Dock = DockStyle.Fill, View = View.Details, FullRowSelect = true, GridLines = true, MultiSelect = false };
        foreach ((string name, int width) in columns) list.Columns.Add(name, width);
        return list;
    }

    public static DataGridView Grid() => new()
    {
        Dock = DockStyle.Fill,
        ReadOnly = true,
        AllowUserToAddRows = false,
        AllowUserToDeleteRows = false,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        MultiSelect = false,
        BackgroundColor = Color.White,
        BorderStyle = BorderStyle.Fixed3D
    };
}
