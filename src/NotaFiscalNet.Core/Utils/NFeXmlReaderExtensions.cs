using System;
using System.Globalization;
using System.Xml.Linq;

namespace NotaFiscalNet.Core.Utils
{
    static class NFeXmlReaderExtensions
    {
        private static readonly XNamespace ns = Constants.NamespacePortalFiscalNFe;

        public static string NFAttributeAsString(this XElement source, string name)
        {
            return source.Attribute(name).Value;
        }

        public static bool NFAttributeAsString(this XElement source, string name, Action<string> setter)
        {
            var attr = source.Attribute(ns + name);
            if (attr == null)
                return false;

            setter(attr.Value);
            return true;
        }

        public static int NFAttributeAsInt32(this XElement source, string name)
        {
            return int.Parse(source.Attribute(name).Value);
        }

        public static bool NFAttributeAsInt32(this XElement source, string name, Action<int> setter)
        {
            var attr = source.Attribute(ns + name);
            if (attr == null)
                return false;

            setter(int.Parse(attr.Value));
            return true;
        }


        public static int NFElementAsInt32(this XElement source, string name)
        {
            return int.Parse(source.Element(ns + name).Value);
        }

        public static bool NFElementAsInt32(this XElement source, string name, Action<int> setter)
        {
            var el = source.Element(ns + name);
            if (el == null) 
                return false;

            var value = int.Parse(el.Value);
            setter(value);
            return true;
        }

        public static long NFElementAsInt64(this XElement source, string name)
        {
            return long.Parse(source.Element(ns + name).Value);
        }

        public static bool NFElementAsInt64(this XElement source, string name, Action<long> setter)
        {
            var el = source.Element(ns + name);
            if (el == null)
                return false;

            var value = long.Parse(el.Value);
            setter(value);
            return true;
        }

        public static decimal NFElementAsDecimal(this XElement source, string name)
        {
            return decimal.Parse(source.Element(ns + name).Value, CultureInfo.InvariantCulture);
        }

        public static bool NFElementAsDecimal(this XElement source, string name, Action<decimal> setter)
        {
            var el = source.Element(ns + name);
            if (el == null)
                return false;

            var value = decimal.Parse(el.Value, CultureInfo.InvariantCulture);
            setter(value);
            return true;
        }

        public static string NFElementAsString(this XElement source, string name)
        {
            return source.Element(ns + name).Value;
        }

        public static bool NFElementAsString(this XElement source, string name, Action<string> setter)
        {
            var el = source.Element(ns + name);
            if (el == null)
                return false;

            setter(el.Value);
            return true;
        }



        public static DateTime NFElementAsDateTime(this XElement source, string name)
        {
            return DateTime.Parse(source.Element(ns + name).Value);
        }

        public static bool NFElementAsDateTime(this XElement source, string name, Action<DateTime> setter)
        {
            var el = source.Element(ns + name);
            if (el == null)
                return false;

            var value = DateTime.Parse(el.Value);
            setter(value);
            return true;
        }

        public static TEnum NFElementAsEnum<TEnum>(this XElement source, string name) where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("TEnum must be an enumerated type");

            string value = source.Element(ns + name).Value;

            TEnum enumValue;
            if (!Enum.TryParse(value, out enumValue))
                throw new ArgumentException("O valor não é válido para o enum especificado.");

            return enumValue;
        }

        public static bool NFElementAsEnum<TEnum>(this XElement source, string name, Action<TEnum> setter) where TEnum : struct, IConvertible
        {
            var el = source.Element(ns + name);
            if (el == null)
                return false;

            TEnum enumValue;
            if (!Enum.TryParse(el.Value, out enumValue))
                throw new ArgumentException("O valor não é válido para o enum especificado.");

            setter(enumValue);
            return true;
        }

    }
}
