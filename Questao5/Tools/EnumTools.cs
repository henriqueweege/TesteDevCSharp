using System.ComponentModel;

namespace Tools
{
    public static class EnumTools
    {
        public static string ToDescription<T>(this T @enum)
        {
            DescriptionAttribute[] description = (DescriptionAttribute[])@enum.GetType().GetField(@enum.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);

            return description.Length > 0 ? description[0].Description : String.Empty;
        }
    }
}
