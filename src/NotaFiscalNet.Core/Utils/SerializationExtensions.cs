using NotaFiscalNet.Core.Interfaces;
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace NotaFiscalNet.Core.Utils
{
    /// <summary>
    /// Classe de Suporte para Serialização
    /// </summary>
    public static class SerializationExtensions
    {
        public static void Serialize(this ISerializavel source, XmlWriter writer, INFe nfe)
        {
            if (source == null)
                throw new InvalidOperationException("Não foi possível serializar o objeto pois o mesmo encontra-se nulo.");
            source.Serializar(writer, nfe);
        }

        public static void SerializeIfNotNull(this ISerializavel source, XmlWriter writer, INFe nfe)
        {
            source?.Serializar(writer, nfe);
        }

        /// <summary>
        /// Remove os acentos do texto.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoverAcentos(this string value)
        {
            if (value == null) return null;
            var text = new StringBuilder();
            foreach (var t in value)
            {
                var c = t;

                switch (c)
                {
                    case 'á': c = 'a'; break;
                    case 'é': c = 'e'; break;
                    case 'í': c = 'i'; break;
                    case 'ó': c = 'o'; break;
                    case 'ú': c = 'u'; break;
                    case 'à': c = 'a'; break;
                    case 'è': c = 'e'; break;
                    case 'ì': c = 'i'; break;
                    case 'ò': c = 'o'; break;
                    case 'ù': c = 'u'; break;
                    case 'â': c = 'a'; break;
                    case 'ê': c = 'e'; break;
                    case 'î': c = 'i'; break;
                    case 'ô': c = 'o'; break;
                    case 'û': c = 'u'; break;
                    case 'ä': c = 'a'; break;
                    case 'ë': c = 'e'; break;
                    case 'ï': c = 'i'; break;
                    case 'ö': c = 'o'; break;
                    case 'ü': c = 'u'; break;
                    case 'ã': c = 'a'; break;
                    case 'õ': c = 'o'; break;
                    case 'ñ': c = 'n'; break;
                    case 'ç': c = 'c'; break;
                    case 'Á': c = 'A'; break;
                    case 'É': c = 'E'; break;
                    case 'Í': c = 'I'; break;
                    case 'Ó': c = 'O'; break;
                    case 'Ú': c = 'U'; break;
                    case 'À': c = 'A'; break;
                    case 'È': c = 'E'; break;
                    case 'Ì': c = 'I'; break;
                    case 'Ò': c = 'O'; break;
                    case 'Ù': c = 'U'; break;
                    case 'Â': c = 'A'; break;
                    case 'Ê': c = 'E'; break;
                    case 'Î': c = 'I'; break;
                    case 'Ô': c = 'O'; break;
                    case 'Û': c = 'U'; break;
                    case 'Ä': c = 'A'; break;
                    case 'Ë': c = 'E'; break;
                    case 'Ï': c = 'I'; break;
                    case 'Ö': c = 'O'; break;
                    case 'Ü': c = 'U'; break;
                    case 'Ã': c = 'A'; break;
                    case 'Õ': c = 'O'; break;
                    case 'Ñ': c = 'N'; break;
                    case 'Ç': c = 'C'; break;
                }

                text.Append(c);
            }
            return text.ToString();
        }

        public static string TratarCaracteresInvalidos(this string value)
        {
            if (value == null) return null;
            return value
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", " ");
        }

        public static string DecodificarCaracteresInvalidos(this string texto)
        {
            if (texto == null) return String.Empty;
            return texto
                .Replace("&amp;", "&")
                .Replace("&lt;", "<")
                .Replace("&gt;", ">")
                .Replace("&quot;", "\"");
        }

        /// <summary>
        /// Realiza o tratamento de "Tokenização" da string retornando apenas uma quantidade
        /// especificada de caracteres.
        /// </summary>
        /// <param name="value">Valor a ser tratado.</param>
        /// <param name="maxCharacters">Número máximo de caracteres a serem retornados.</param>
        /// <returns></returns>
        public static string ToToken(this string value, int maxCharacters)
        {
            if (value == null) return null;
            string handledValue = RemoverAcentos(ToToken(value));
            handledValue = TratarCaracteresInvalidos(handledValue);
            return handledValue.Substring(0, (handledValue.Length > maxCharacters) ? maxCharacters : handledValue.Length);
        }

        /// <summary>
        /// Formata um Token
        /// </summary>
        /// <param name="value">String Token</param>
        public static string ToToken(this string value)
        {
            if (value == null) return null;
            value = RemoverAcentos(value ?? string.Empty);
            //value = TratarCaracteresInvalidos(value);
            // 1. Retira os caracteres CHAR(9), CHAR(10) e CHAR(13) da expressão.
            // 2. Retira os espaços não significativos no inicio e no final da expressão.
            // 3. Retira os espaços duplos no meio da expressão.
            return Regex.Replace(value.Trim('\n', '\r', '\t').Trim(), @"\s{2,}", " ");
        }

        /// <summary>
        /// Formata um Token Int
        /// </summary>
        /// <param name="value">String Token</param>
        public static string ToToken(this int value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Formata um Token Long
        /// </summary>
        /// <param name="value">String Token</param>
        public static string ToToken(this long value)
        {
            return value.ToString();
        }

        public static string ToTDec_0803(this double value)
        {
            return value.ToString("f3", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_0804(this double value)
        {
            return value.ToString("f4", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_1204(this decimal value)
        {
            return value.ToString("f4", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_1110(this decimal value)
        {
            var formattedValue = value.ToString("f10", CultureInfo.InvariantCulture);
            formattedValue = formattedValue.TrimEnd('0');
            return formattedValue.EndsWith(".") ? formattedValue.Remove(formattedValue.Length - 1, 1) : formattedValue;
        }

        public static string ToTDec_1203(this double value)
        {
            return value.ToString("f3", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_1104(this double value)
        {
            return value.ToString("f4", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_1104(this decimal value)
        {
            return value.ToString("f4", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_1104v(this decimal value)
        {
            return value.ToString("f4", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_1302Opc(this decimal? value)
        {
            return !value.HasValue
                ? null
                : value.Value.ToString("f2", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_1302(this double value)
        {
            return value.ToString("f2", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_1302(this decimal value)
        {
            return value.ToString("f2", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_0302(this decimal value)
        {
            return value.ToString("f2", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_0302Max100(this decimal value)
        {
            return value.ToString("f2", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Tipo Decimal com até 3 dígitos inteiros, podendo ter de 2 até 4 decimais
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToTDec_0302a04(this decimal value)
        {
            // 0|0\.[0-9]{2,4}|[1-9]{1}[0-9]{0,2}(\.[0-9]{2,4})?
            decimal resto = (value - (long)value);

            if (resto < 0.100m)
                return value.ToString("f2", CultureInfo.InvariantCulture);
            else if (resto < 0.1000m)
                return value.ToString("f3", CultureInfo.InvariantCulture);
            else
                return value.TruncateDecimals(4).ToString("f4", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Tipo Decimal com até 3 dígitos inteiros, podendo ter de 2 até 4 decimais
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToTDec_0302a04Opc(this decimal? value)
        {
            // 0\.[0-9]{2,4}|[1-9]{1}[0-9]{0,2}(\.[0-9]{2,4})?

            if (!value.HasValue || value.Value == 0m)
                return string.Empty;

            decimal resto = (value.Value - (long)value);

            if (resto < 0.100m)
                return value.Value.ToString("f2", CultureInfo.InvariantCulture);
            else if (resto < 0.1000m)
                return value.Value.ToString("f3", CultureInfo.InvariantCulture);
            else
                return value.Value.TruncateDecimals(4).ToString("f4", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_0504(this double value)
        {
            return value.ToString("f2", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_0302(this double value)
        {
            return value.ToString("f2", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Retorna uma Data no formato AAAA-MM-DD.
        /// </summary>
        /// <param name="value">Objeto DateTime contendo a data.</param>
        /// <returns></returns>
        public static string ToTData(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumValue<T>(T value)
        {
            if (typeof(T).BaseType != typeof(Enum))
                throw new ArgumentException("T deve ser um tipo enumerador.");

            return value.GetHashCode().ToString();
        }

        public static bool IsDefined(this Enum value)
        {
            return Enum.IsDefined(value.GetType(), value);
        }

        public static string GetEnumValue(this Enum value)
        {
            return value.GetHashCode().ToString();
        }

        public static string ToString6(this int value)
        {
            return value.ToString("000000");
        }

        public static string ToString9(this int value)
        {
            return value.ToString("000000000");
        }

        public static string ToString4(this int value)
        {
            return value.ToString("0000");
        }

        public static string ToString7(this int value)
        {
            return value.ToString("0000000");
        }

        public static string ToString2(this int value)
        {
            return value.ToString("00");
        }

        public static string ToString3(this int value)
        {
            return value.ToString("000");
        }

        public static string ToTCnpj(this string value)
        {
            if (value == null) return null;
            return value.PadLeft(14, '0');
        }

        public static string ToTCpf(this string value)
        {
            if (value == null) return null;
            return value.PadLeft(11, '0');
        }

        public static string ToTString(this string value, int maxCharacters)
        {
            if (value == null) return null;
            value = RemoverAcentos(value);
            value = TratarCaracteresInvalidos(value);
            return value.Substring(0, (value.Length > maxCharacters) ? maxCharacters : value.Length);
        }

        /// <summary>
        /// Retorna um horário no formato HH:MM:SS.
        /// </summary>
        /// <param name="value">Objeto DateTime contendo a hora.</param>
        /// <returns></returns>
        public static string ToTTime(this DateTime value)
        {
            return value.ToString("HH:mm:ss");
        }

        /// <summary>
        /// Retorna uma data e hora no formato AAAA-MM-DDTHH:MM:SS.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToTDataHora(this DateTime value)
        {
            return value.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        /// <summary>
        /// Converte para o formato de data e hora com especificador de fuso-horário.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToTDateTimeUtc(this DateTime value)
        {
            return value.ToString("yyyy-MM-ddTHH:mm:sszzz");
        }
    }
}