using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace NotaFiscalNet.Core.Utils
{
    public static class EnumEx
    {
        private static Type TypeOfDescriptionAttribute = typeof(DescriptionAttribute);
        private static Type TypeOfFlagsAttribute = typeof(FlagsAttribute);

        public static string[] GetEnumDescriptionArray(this Enum value)
        {
            var items = new List<Enum>();

            var typeOfValue = value.GetType();
            if (typeOfValue.IsDefined(TypeOfFlagsAttribute, false))
            {
                if (Convert.ToUInt64(value) == 0)
                    return new[] { GetEnumDescriptionItem(value) };

                foreach (Enum enumValue in Enum.GetValues(typeOfValue))
                {
                    if (value.HasFlag(enumValue))
                    {
                        // Quando trabalhando com flags, se há valor 0 na lista do enum, ele sempre 
                        // dá que foi definido (bit mask).
                        if (Convert.ToUInt64(enumValue) == 0)
                            continue; // ignora o item da lista com valor igual a zero.

                        items.Add(enumValue);
                    }
                }
            }
            else
            {
                items.Add(value);
            }

            return items
                .Select(GetEnumDescriptionItem)
                .ToArray();
        }

        public static string GetEnumDescription(this Enum value)
        {
            return GetEnumDescriptionArray(value)
                .Aggregate((acum, next) => acum + ", " + next);
        }

        private static string GetEnumDescriptionItem(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(TypeOfDescriptionAttribute, false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public static string GetEnumDescription(this Enum value, params object[] args)
        {
            var description = GetEnumDescription(value);
            return String.Format(description, args);
        }

        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", nameof(description));
        }
    }
}
