// Form từ điển hai chiều: gợi ý theo tiền tố và hiển thị nghĩa khi nhấn Enter hoặc nhấp đúp.
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lab05C.Core;

namespace BaiVeNha01_TuDien;

public sealed class FrmTuDien : Form
{
    private readonly BilingualDictionary _dictionary = new();

    public FrmTuDien()
    {
        Text = "Từ điển Anh Việt - Việt Anh";
        ClientSize = new Size(760, 540);
        Font = new Font("Segoe UI", 10.5F);
        BackColor = Color.White;
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        Controls.Add(new Label { Text = "TỪ ĐIỂN ANH VIỆT VÀ VIỆT ANH", AutoSize = true, Font = new Font("Segoe UI", 19F, FontStyle.Bold), ForeColor = Color.Firebrick, Location = new Point(145, 18) });
        TabControl tabs = new() { Location = new Point(35, 75), Size = new Size(690, 390) };
        tabs.TabPages.Add(CreateDictionaryTab("ANH - VIỆT", DictionaryDirection.EnglishToVietnamese, "Tiếng Anh", "Tiếng Việt"));
        tabs.TabPages.Add(CreateDictionaryTab("VIỆT - ANH", DictionaryDirection.VietnameseToEnglish, "Tiếng Việt", "Tiếng Anh"));
        Controls.Add(tabs);
        Button exit = new() { Text = "T&hoát", Location = new Point(545, 485), Size = new Size(180, 40) }; exit.Click += (_, _) => Close(); Controls.Add(exit);
    }

    private TabPage CreateDictionaryTab(string title, DictionaryDirection direction, string sourceLabel, string targetLabel)
    {
        TabPage page = new(title);
        page.Controls.Add(new Label { Text = sourceLabel, AutoSize = true, Location = new Point(25, 25) });
        page.Controls.Add(new Label { Text = targetLabel, AutoSize = true, Location = new Point(355, 25) });
        ComboBox input = new() { Location = new Point(25, 55), Size = new Size(285, 33), DropDownStyle = ComboBoxStyle.DropDown };
        ListBox suggestions = new() { Location = new Point(25, 100), Size = new Size(285, 230) };
        TextBox meaning = new() { Location = new Point(355, 55), Size = new Size(285, 275), Multiline = true, ReadOnly = true, BackColor = Color.White, ScrollBars = ScrollBars.Vertical };
        suggestions.Items.AddRange(_dictionary.Words(direction).Cast<object>().ToArray());

        input.TextChanged += (_, _) =>
        {
            suggestions.BeginUpdate(); suggestions.Items.Clear();
            suggestions.Items.AddRange(_dictionary.Suggest(direction, input.Text).Cast<object>().ToArray());
            suggestions.EndUpdate();
        };
        input.KeyDown += (_, e) => { if (e.KeyCode == Keys.Enter) { Translate(direction, input.Text, meaning); e.SuppressKeyPress = true; } };
        suggestions.DoubleClick += (_, _) => { if (suggestions.SelectedItem is string word) { input.Text = word; Translate(direction, word, meaning); } };
        suggestions.SelectedIndexChanged += (_, _) => { if (suggestions.SelectedItem is string word) input.Text = word; };
        page.Controls.AddRange(new Control[] { input, suggestions, meaning });
        return page;
    }

    private void Translate(DictionaryDirection direction, string word, TextBox output)
    {
        _dictionary.TryTranslate(direction, word, out string meaning);
        output.Text = meaning;
    }
}
