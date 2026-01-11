using System.ComponentModel;

public static class ObjectExtension
{
    public static string Describe(this object obj, bool addNewLine = true)
    {
        string description = "";
        foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
        {
            string name = descriptor.Name;
            object value = descriptor.GetValue(obj);
            description += string.Format("{0}.{1} [{2}]", obj.GetType().Name, name, value);
            if (addNewLine)
                description += Environment.NewLine;
            else description += " | ";
        }
        return description;
    }
}