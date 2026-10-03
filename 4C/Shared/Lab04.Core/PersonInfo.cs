using System;

namespace Lab04.Core;

public static class PersonInfo
{
    public static bool TryCalculateAge(string? birthYearText, int currentYear, out int age)
    {
        age = 0;
        if (!int.TryParse(birthYearText, out int birthYear) || birthYear < 1900 || birthYear > currentYear)
            return false;

        age = currentYear - birthYear;
        return true;
    }
}
