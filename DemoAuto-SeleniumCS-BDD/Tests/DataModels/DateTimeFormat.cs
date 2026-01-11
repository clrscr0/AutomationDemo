using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;
public enum DateTimeFormat
{
    [Description("o")] Iso8601,
    [Description("yyyy-MM-dd HH:mm:ss")] StandardDateTime,
    [Description("MM/dd/yyyy")] CommonDate,
    [Description("dd-MMM-yyyy")] DayMonthYear,
    [Description("ddd, dd MMM yyyy")] DayOfWeekDate,
    [Description("HH:mm:ss")] Time24Hour,
    [Description("hh:mm:ss tt")] Time12Hour,
    [Description("u")] UniversalSortable,
    [Description("dd/MM/yyyy HH:mm")] CustomFormat1,
    [Description("dddd, MMMM dd, yyyy")] CustomFormat2,
    [Description("yyyy-MM-dd HH:mm:ss.fff")] Milliseconds,
    [Description("ShortDatePattern")] ShortDate, 
    [Description("ShortTimePattern")] ShortTime,
    [Description("yyyy-MM-ddTHH:mm:ss zzz (K)")] DateTimeWithTimezoneFormat
}