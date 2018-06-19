using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa a Fatura de Cobrança da Nota Fiscal Eletrônica
    /// </summary>
    public sealed class Fatura : ISerializavel, IModificavel
    {
        public void Serializar(XmlWriter writer, INFe nfe)
        {
            if (Modificado)
            {
                writer.WriteStartElement("fat"); // Elemento 'fat'

                if (!string.IsNullOrEmpty(Numero))
                    writer.WriteElementString("nFat", SerializationUtil.ToToken(Numero, 60));
                if (ValorOriginal > 0)
                    writer.WriteElementString("vOrig", SerializationUtil.ToTDec_1302(ValorOriginal));
                if (ValorDesconto > 0)
                    writer.WriteElementString("vDesc", SerializationUtil.ToTDec_1302(ValorDesconto));
                if (ValorLiquido > 0)
                    writer.WriteElementString("vLiq", SerializationUtil.ToTDec_1302(ValorLiquido));

                writer.WriteEndElement(); // Elemento 'fat'
            }
        }

        private string _numero = string.Empty;
        private decimal _valorOriginal;
        private decimal _valorDesconto;
        private decimal _valorLiquido;

        /// <summary>
        /// [nFat] Retorna ou define o Número da Fatura. Opcional.
        /// </summary>
        [NFeField(ID = "Y03", FieldName = "nFat", DataType = "token", MinLength = 1, MaxLength = 60, Opcional = true)]
        public string Numero
        {
            get => _numero;
	        set => _numero = ValidationUtil.TruncateString(value, 60);
        }

        /// <summary>
        /// [vOrig] Retorna ou define o Valor Original Total da Fatura. Opcional.
        /// </summary>
        [NFeField(ID = "Y04", FieldName = "vOrig", DataType = "TDev_1302Opc",
            Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?", Opcional = true)]
        public decimal ValorOriginal
        {
            get => _valorOriginal;
	        set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorOriginal");
                _valorOriginal = value;
            }
        }

        /// <summary>
        /// [vDesc] Retorna ou define o Valor de Desconto da Fatura. Opcional
        /// </summary>
        [NFeField(ID = "Y05", FieldName = "vDesc", DataType = "TDev_1302Opc",
            Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?", Opcional = true)]
        public decimal ValorDesconto
        {
            get => _valorDesconto;
	        set
            {
                ValidationUtil.ValidateTDec_1302Opc(value, "ValorDesconto");
                _valorDesconto = value;
            }
        }

        /// <summary>
        /// [vLiq] Retorna ou define o Valor Líquido da Fatura. Opcional.
        /// </summary>
        [NFeField(ID = "Y06", FieldName = "vLiq", DataType = "TDev_1302Opc",
            Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?", Opcional = true)]
        public decimal ValorLiquido
        {
            get => _valorLiquido;
	        set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorLiquido");
                _valorLiquido = value;
            }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => !string.IsNullOrEmpty(Numero) ||
                                  ValorOriginal != 0m ||
                                  ValorLiquido != 0m ||
                                  ValorDesconto != 0m;
    }
}