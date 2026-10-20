// Mô hình đếm ngược độc lập với Timer để có thể kiểm tra tự động.
using System;

namespace Lab05D.Core;

public sealed class CountdownModel
{
    public int RemainingSeconds { get; private set; }
    public bool IsRunning { get; private set; }
    public string Display => TimeSpan.FromSeconds(RemainingSeconds).ToString(@"mm\:ss");

    public bool Start(string? minutesText, string? secondsText, out string error)
    {
        if (!int.TryParse(minutesText, out int minutes) || !int.TryParse(secondsText, out int seconds) ||
            minutes < 0 || seconds < 0 || seconds > 59 || minutes + seconds == 0)
        {
            error = "Thời gian phải hợp lệ và lớn hơn 0 giây.";
            return false;
        }
        RemainingSeconds = checked(minutes * 60 + seconds);
        IsRunning = true;
        error = string.Empty;
        return true;
    }

    public void Start(int minutes, int seconds)
    {
        if (!Start(minutes.ToString(), seconds.ToString(), out string error))
            throw new ArgumentOutOfRangeException(nameof(minutes), error);
    }

    public bool Tick()
    {
        if (!IsRunning) return false;
        if (RemainingSeconds > 0) RemainingSeconds--;
        if (RemainingSeconds == 0) IsRunning = false;
        return IsRunning;
    }

    public void Stop() => IsRunning = false;
}
