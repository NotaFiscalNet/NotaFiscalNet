using System;

namespace NotaFiscalNet.Core.Utils
{
    public static class DecimalExtensions
    {
        public static decimal TruncateDecimals(this decimal value, int digits)
        {
            var maxDigits = (decimal)Math.Pow(10.0, digits);
            return decimal.Truncate(value * maxDigits) / maxDigits;
        }
    }
}