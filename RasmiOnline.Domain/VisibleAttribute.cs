namespace RasmiOnline.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class VisibleAttribute : Attribute
    {
    }

   public static class EnumExtension
    {
        public static IEnumerable<TEnum> FilterEnumWithAttributeOf<TEnum, TAttribute>() where TEnum : struct where TAttribute : class
        {
            foreach (var field in
                typeof(TEnum).GetFields(BindingFlags.GetField |
                                         BindingFlags.Public |
                                         BindingFlags.Static))
            {
                if (field.GetCustomAttributes(typeof(TAttribute), false).Length > 0)
                    yield return (TEnum)field.GetValue(null);
            }
        }
    }
}