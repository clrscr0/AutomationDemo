using System.Globalization;

public static class StringExtension
{
    /// <summary>
    /// Convert to datetime from string
    /// </summary>
    /// <param name="datetimeString">Datetime text value</param>
    /// <param name="dateTimeFormat">Datetime format</param>
    /// <param name="timezoneInfo">Timezone info i.e. TimeZoneInfo.Local, userData.GetTimezone().ToTimeZoneInfo()</param>
    /// <returns></returns>
    public static DateTime ConvertToDateTime(this string datetimeString, DateTimeFormat dateTimeFormat = DateTimeFormat.CommonDate, TimeZoneInfo? timezoneInfo = null)
    {
        DateTime parsedDate = DateTime.ParseExact(datetimeString, dateTimeFormat.GetDescription(), CultureInfo.InvariantCulture);

        if (timezoneInfo == TimeZoneInfo.Utc)
            return DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc); // Set the Kind to Utc
        else if (timezoneInfo == TimeZoneInfo.Local || timezoneInfo == null)
            return DateTime.SpecifyKind(parsedDate, DateTimeKind.Local); // Set the Kind to Utc
        else
        {
            parsedDate = DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);
            return TimeZoneInfo.ConvertTimeFromUtc(parsedDate, timezoneInfo);
        }
    }
}