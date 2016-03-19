using System;
using System.ComponentModel;

namespace NotaFiscalNet.Core
{
    public static class TipoAmbienteEx
    {
        public static string GetCustomValue(this TipoAmbiente source)
        {
            switch (source)
            {
                case TipoAmbiente.Producao:
                    return "P";

                case TipoAmbiente.Homologacao:
                    return "H";

                default:
                    throw new InvalidEnumArgumentException("Valor inválido para o enum (" + (int)source + ").");
            }
        }

        public static TipoAmbiente FromCustomValue(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            switch (value.ToUpper())
            {
                case "P": return TipoAmbiente.Producao;
                case "H": return TipoAmbiente.Homologacao;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, "O valor informado não é um ambiente de Documento Fiscal válido.");
            }
        }
    }
}