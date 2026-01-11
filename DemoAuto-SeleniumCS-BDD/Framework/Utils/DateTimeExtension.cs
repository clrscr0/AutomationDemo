using System.Globalization;

public static class DateTimeExtension
{
    /// <summary>
    /// Convert to new timezone
    /// </summary>
    /// <param name="dateTime">datetime value to convert</param>
    /// <param name="timezone">if null, convert to Utc. else, convert to specified timezone</param>
    /// <returns></returns>
    public static DateTime ConvertToAnotherTimezone(this DateTime dateTime, TimeZoneInfo? timezoneInfo = null)
    {
        if (timezoneInfo == TimeZoneInfo.Utc || timezoneInfo == null)
            return dateTime.ToUniversalTime();
        else if (timezoneInfo == TimeZoneInfo.Local)
            return dateTime.ToLocalTime();
        else
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime.ToUniversalTime(), timezoneInfo!);
    }

    /// <summary>
    /// Convert to string format
    /// </summary>
    /// <param name="dateTime">Datetime value </param>
    /// <param name="dateTimeFormat">Expected string format of date time i.e. DateTimeFormat.ShortDate</param>
    /// <returns></returns>
    public static string ConvertToString(this DateTime dateTime, DateTimeFormat dateTimeFormat = DateTimeFormat.Milliseconds)
    {
        return dateTime.ToString(dateTimeFormat.GetDescription());
    }
}