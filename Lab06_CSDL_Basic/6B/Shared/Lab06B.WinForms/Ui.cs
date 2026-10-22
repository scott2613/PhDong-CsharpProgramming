using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab06B.WinForms;

/// <summary>Tạo điều khiển theo một kiểu thống nhất cho toàn bộ Lab06B.</summary>
public static class Ui
{
    public static readonly Color Primary = Color.FromArgb(31, 78, 121);
    public static readonly Color Accent = Color.FromArgb(221, 235, 247);

    public static void Configure(Form form, string title, int width = 1080, int height = 680)
    {
        form.Text = title;
        form.StartPosition = FormStartPosition.CenterScreen;
        form.ClientSize = new Size(width, height);
        form.MinimumSize = new Size(820, 520);
        form.Font = new Font("Segoe UI", 10F);
        form.BackColor = Color.White;
        form.AutoScaleMode = AutoScaleMode.Dpi;
    }

    public static Label Title(string text) => new()
    {
        Text = text,
        Dock = DockStyle.Top,
        Height = 64,
        TextAlign = ContentAlignment.MiddleCenter,
        Font = new Font("Segoe UI", 18F, FontStyle.Bold),
        ForeColor = Primary,
        BackColor = Accent
    };

    public static Label Hint(string database = "QLSinhVien") => new()
    {
        Text = $"CSDL {database} - tự dò .\\SQLEXPRESS hoặc (localdb)\\MSSQLLocalDB. Có thể đặt LAB06_CONNECTION_STRING.",
        Dock = DockStyle.Bottom,
        Height = 30,
        Padding = new Padding(8, 5, 8, 0),
        ForeColor = Color.DimGray,
        BackColor = Color.WhiteSmoke
    };

    public static Label Label(string text) => new()
    {
        Text = text,
        AutoSize = true,
        Margin = new Padding(7, 12, 5, 7)
    };

    public static TextBox TextBox(int width = 200) => new() { Width = width, Margin = new Padding(7) };

    public static ComboBox ComboBox(int width = 190) => new()
    {
        Width = width,
        DropDownStyle = ComboBoxStyle.DropDownList,
        Margin = new Padding(7)
    };

    public static Button Button(string text, EventHandler handler)
    {
        Button button = new()
        {
            Text = text,
            AutoSize = true,
            MinimumSize = new Size(108, 40),
            Margin = new Padding(7)
        };
        button.Click += handler;
        return button;
    }

    public static FlowLayoutPanel Row(params Control[] controls)
    {
        FlowLayoutPanel panel = new()
        {
            AutoSize = true,
            Dock = DockStyle.Top,
            Padding = new Padding(14, 10, 14, 6),
            WrapContents = true,
            BackColor = Color.White
        };
        panel.Controls.AddRange(controls);
        return panel;
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
        RowHeadersVisible = false
    };

    public static ListView List(params string[] columns)
    {
        ListView list = new()
        {
            Dock = DockStyle.Fill,
            View = View.Details,
            FullRowSelect = true,
            GridLines = true,
            HideSelection = false
        };
        foreach (string column in columns)
            list.Columns.Add(column, 150);
        return list;
    }
}
