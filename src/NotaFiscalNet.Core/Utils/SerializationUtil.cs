using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace NotaFiscalNet.Core.Utils
{
    /// <summary>
    /// Classe de Suporte para Serialização
    /// </summary>
    public static class SerializationUtil
    {
        /// <summary>
        /// Remove os acentos do texto.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string RemoverAcentos(string texto)
        {
            var text = new StringBuilder();
            for ( int i = 0; i < texto.Length; i++ )
            {
                char c = texto[i];
                #region Switch
                switch ( c )
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
                #endregion Switch
                text.Append(c);
            }
            return text.ToString();
        }

        public static string CodificarCaracteresInvalidos(string texto)
        {
            return texto
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", " ");
        }

        public static string DecodificarCaracteresInvalidos(string texto)
        {
            return texto
                .Replace("&amp;", "&")
                .Replace("&lt;", "<")
                .Replace("&gt;", ">")
                .Replace("&quot;", "\"");
        }

        /// <summary>
        /// Realiza o tratamento de "Tokenização" da string retornando apenas uma quantidade especificada de caracteres.
        /// </summary>
        /// <param name="value">Valor a ser tratado.</param>
        /// <param name="maxCharacters">Número máximo de caracteres a serem retornados.</param>
        /// <returns></returns>
        public static string ToToken(string value, int maxCharacters)
        {

            string handledValue = RemoverAcentos(ToToken(value));
            handledValue = CodificarCaracteresInvalidos(handledValue);
            return handledValue.Substring(0, (handledValue.Length > maxCharacters) ? maxCharacters : handledValue.Length);
        }

        /// <summary>
        /// Formata um Token
        /// </summary>
        /// <param name="value">String Token</param>
        public static string ToToken(string value)
        {
            value = RemoverAcentos(value ?? string.Empty);
            // value = TratarCaracteresInvalidos(value);
            // 1. Retira os caracteres CHAR(9), CHAR(10) e CHAR(13) da expressão.
            // 2. Retira os espaços não significativos no inicio e no final da expressão.
            // 3. Retira os espaços duplos no meio da expressão.
            return Regex.Replace(value.Trim('\n', '\r', '\t').Trim(), @"\s{2,}", " ");
        }

        /// <summary>
        /// Formata um Token Int
        /// </summary>
        /// <param name="value">String Token</param>
        public static string ToToken(int value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Formata um Token Long
        /// </summary>
        /// <param name="value">String Token</param>
        public static string ToToken(long value)
        {
            return value.ToString();
        }

        public static string ToTDec_0803(double value)
        {
            return value.ToString("f3", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_0803(decimal value)
        {
            return value.ToString("f3", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_0804(double value)
        {
            return value.ToString("f4", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_1204(decimal value)
        {
            return value.ToString("f4", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_1110(decimal value)
        {
            var formattedValue = value.ToString("f10", CultureInfo.InvariantCulture);
            formattedValue = formattedValue.TrimEnd('0');
            return formattedValue.EndsWith(".") ? formattedValue.Remove(formattedValue.Length - 1, 1) : formattedValue;
        }

        public static string ToTDec_1203(double value)
        {
            return value.ToString("f3", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_1203(decimal value)
        {
            return value.ToString("f3", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_1104(double value)
        {
            return value.ToString("f4", CultureInfo.InvariantCulture);
        }
        public static string ToTDec_1104(decimal value)
        {
            return value.ToString("f4", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_1104v(double value)
        {
            return value.ToString("f4", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_1302(double value)
        {
            return value.ToString("f2", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_1302(decimal value)
        {
            return value.ToString("f2", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_0504(double value)
        {
            return value.ToString("f2", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_0504(decimal value)
        {
            return value.ToString("f2", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_0204v(decimal value)
        {
            return value.ToString("f4", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_0302(double value)
        {
            return value.ToString("f2", CultureInfo.InvariantCulture);
        }

        public static string ToTDec_0302(decimal value)
        {
            return value.ToString("f2", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Retorna uma Data no formato AAAA-MM-DD.
        /// </summary>
        /// <param name="value">Objeto DateTime contendo a data.</param>
        /// <returns></returns>
        public static string ToTData(DateTime value)
        {
            return value.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumValue<T>(T value)
        {
            if (typeof(T).BaseType != typeof(Enum))
                throw new ArgumentException("T deve ser um tipo enumerador.");

            return value.GetHashCode().ToString();
        }

        public static string GetEnumValue(Enum value)
        {
            return value.GetHashCode().ToString();
        }

        public static string ToString6(int value)
        {
            return value.ToString("000000");
        }

        public static string ToString9(int value)
        {
            return value.ToString("000000000");
        }

        public static string ToString4(int value)
        {
            return value.ToString("0000");
        }

        public static string ToString7(int value)
        {
            return value.ToString("0000000");
        }

        public static string ToString2(int value)
        {
            return value.ToString("00");
        }

        public static string ToString3(int value)
        {
            return value.ToString("000");
        }

        public static string ToCNPJ(string value)
        {
            return value.PadLeft(14, '0');
        }

        public static string ToCPF(string value)
        {
            return value.PadLeft(11, '0');
        }

        public static string ToTString(string value, int maxCharacters)
        {
            value = RemoverAcentos(value);
            value = CodificarCaracteresInvalidos(value);
            return value.Substring(0, (value.Length > maxCharacters) ? maxCharacters : value.Length);
        }

        /// <summary>
        /// Retorna um horário no formato HH:MM:SS.
        /// </summary>
        /// <param name="value">Objeto DateTime contendo a hora.</param>
        /// <returns></returns>
        public static string ToTTime(DateTime value)
        {
            return value.ToString("HH:mm:ss");
        }

        /// <summary>
        /// Retorna uma data e hora no formato AAAA-MM-DDTHH:MM:SS.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToTDataHora(DateTime value)
        {
            return value.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        
    }
}
