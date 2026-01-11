using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;


public static class EnumExtension
{
    public static string? GetDescription(this Enum @enum)
    {
        if (@enum == null)
        {
            return null;
        }
        else
        {
            var type = @enum.GetType();
            var memInfo = type.GetMember(@enum.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            var stringValue = ((DescriptionAttribute)attributes[0]).Description;
            return stringValue;
        }
    }
}
