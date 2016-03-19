using System;

namespace NotaFiscalNet.Core
{
    public static class TipoEmissaoNFeEx
    {
        public static string GetCustomValue(this TipoEmissaoNFe source)
        {
            return ((int)source).ToString();
        }

        public static TipoEmissaoNFe Parse(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return (TipoEmissaoNFe)Enum.Parse(typeof(TipoEmissaoNFe), value);
        }
    }
}