public static class ScoreFormatter
{
    private static string[] suffixes = { "", "K", "M", "B", "T", "Qd", "Qn", "Sx", "Sp" };

    /// <summary>
    /// Returns pretty formatted value taking into account the lowest digit to show
    /// </summary>
    /// <param name="value"></param>
    /// <param name="delta"></param>
    /// <returns></returns>
    public static string Format(ulong value, ulong delta)
    {
        ulong divider = 1000;
        int suffixIndex = 0;
        ulong newValue = value;

        while (value / divider > 1 && (delta / divider > 1 || delta == 0))
        {
            suffixIndex += 1;
            divider *= 1000;
            newValue /= 1000;
        }

        var result = $"{(newValue)} {suffixes[suffixIndex]}";
        return result;
    }
}