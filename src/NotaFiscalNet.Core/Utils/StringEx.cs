namespace NotaFiscalNet.Core.Utils
{
    public static class StringEx
    {
        /// <summary>
        /// Retorna o texto com a quantidade máxima de caracteres especificados.
        /// </summary>
        /// <param name="value">Texto que será (ou não) cortado.</param>
        /// <param name="maxLength">Tamanho máximo do texto a ser retornado.</param>
        /// <returns></returns>
        public static string Truncate(this string value, int maxLength)
        {
            if (value == null) return null;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}
