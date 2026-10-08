// Form của bài mẫu định dạng Label: nhận thao tác người dùng, kiểm tra dữ liệu và hiển thị kết quả.
using System;
using System.Drawing;
using System.Windows.Forms;
using Lab04D.Core;

namespace BaiMau02_DinhDangLabel;

/// <summary>Minh họa CheckBox đổi FontStyle và RadioButton đổi màu chữ.</summary>
public sealed class FrmDinhDangLabel : Form
{
    private readonly Label _preview = new();
    private readonly CheckBox _chkRegular = new();
    private readonly CheckBox _chkBold = new();
    private readonly CheckBox _chkItalic = new();
    private readonly CheckBox _chkBoldItalic = new();
    private readonly RadioButton _rdoAuto = new();
    private readonly RadioButton _rdoRed = new();
    private readonly RadioButton _rdoGreen = new();
    private readonly RadioButton _rdoBlue = new();

    // Khởi tạo bố cục, thiết lập phím tắt và đăng ký các sự kiện của form.
    public FrmDinhDangLabel()
    {
        Text = "Định dạng Label";
        ClientSize = new Size(700, 455);
        Font = new Font("Segoe UI", 11F);
        BackColor = Color.White;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;

        _preview.Text = "TRƯỜNG ĐẠI HỌC SÀI GÒN\nKHOA CÔNG NGHỆ THÔNG TIN";
        _preview.TextAlign = ContentAlignment.MiddleCenter;
        _preview.Font = new Font("Segoe UI", 15F, FontStyle.Regular);
        _preview.ForeColor = SystemColors.ControlText;
        _preview.BorderStyle = BorderStyle.FixedSingle;
        _preview.Location = new Point(70, 32);
        _preview.Size = new Size(560, 90);
        Controls.Add(_preview);

        GroupBox fontGroup = new() { Text = "Font Style", Location = new Point(70, 155), Size = new Size(255, 205) };
        ConfigureCheckBox(_chkRegular, "Regular", 28, 38);
        ConfigureCheckBox(_chkBold, "Bold", 28, 78);
        ConfigureCheckBox(_chkItalic, "Italic", 28, 118);
        ConfigureCheckBox(_chkBoldItalic, "Bold and Italic", 28, 158);
        fontGroup.Controls.AddRange(new Control[] { _chkRegular, _chkBold, _chkItalic, _chkBoldItalic });

        GroupBox colorGroup = new() { Text = "Color", Location = new Point(375, 155), Size = new Size(255, 205) };
        ConfigureRadio(_rdoAuto, "AutoColor", 28, 38, SystemColors.ControlText);
        ConfigureRadio(_rdoRed, "Red", 28, 78, Color.Red);
        ConfigureRadio(_rdoGreen, "Green", 28, 118, Color.Green);
        ConfigureRadio(_rdoBlue, "Blue", 28, 158, Color.Blue);
        colorGroup.Controls.AddRange(new Control[] { _rdoAuto, _rdoRed, _rdoGreen, _rdoBlue });
        Controls.AddRange(new Control[] { fontGroup, colorGroup });

        Button btnExit = new() { Text = "E&xit", Location = new Point(505, 387), Size = new Size(125, 42) };
        btnExit.Click += (_, _) => Close(); Controls.Add(btnExit); CancelButton = btnExit;
        _chkRegular.Checked = true; _rdoAuto.Checked = true;
        FormClosing += ConfirmClosing;
    }

    private void ConfigureCheckBox(CheckBox checkBox, string text, int x, int y)
    {
        checkBox.Text = text; checkBox.AutoSize = true; checkBox.Location = new Point(x, y);
        checkBox.CheckedChanged += (_, _) => ApplyFormatting();
    }

    private void ConfigureRadio(RadioButton radio, string text, int x, int y, Color color)
    {
        radio.Text = text; radio.Tag = color; radio.AutoSize = true; radio.Location = new Point(x, y);
        radio.CheckedChanged += (_, _) => { if (radio.Checked) ApplyFormatting(); };
    }

    private void ApplyFormatting()
    {
        LabelTextStyle style = LabelStyleResolver.Resolve(_chkRegular.Checked, _chkBold.Checked, _chkItalic.Checked, _chkBoldItalic.Checked);
        FontStyle fontStyle = style switch
        {
            LabelTextStyle.Bold => FontStyle.Bold,
            LabelTextStyle.Italic => FontStyle.Italic,
            LabelTextStyle.BoldItalic => FontStyle.Bold | FontStyle.Italic,
            _ => FontStyle.Regular
        };
        _preview.Font = new Font(_preview.Font.FontFamily, 15F, fontStyle);

        RadioButton? selectedColor = _rdoRed.Checked ? _rdoRed : _rdoGreen.Checked ? _rdoGreen : _rdoBlue.Checked ? _rdoBlue : _rdoAuto;
        _preview.ForeColor = selectedColor.Tag is Color color ? color : SystemColors.ControlText;
    }

    // Hỏi lại người dùng trước khi đóng để tránh mất dữ liệu đang nhập.
    private void ConfirmClosing(object? sender, FormClosingEventArgs e)
    {
        DialogResult result = MessageBox.Show("Bạn có muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        e.Cancel = result == DialogResult.No;
    }
}
