using NotaFiscalNet.Core.Utils;
using System;
using System.Xml;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Duplicata de Cobrança da Nota Fiscal Eletrônica
    /// </summary>
    public sealed class Duplicata : ISerializavel, IModificavel
    {
        void ISerializavel.Serializar(XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("dup"); // Elemento 'dup'
            if (!string.IsNullOrEmpty(Numero))
                writer.WriteElementString("nDup", SerializationUtil.ToToken(Numero, 60));
            if (DataVencimento > DateTime.MinValue)
                writer.WriteElementString("dVenc", SerializationUtil.ToTData(DataVencimento));
            if (Valor > 0m)
                writer.WriteElementString("vDup", SerializationUtil.ToTDec_1302(Valor));
            writer.WriteEndElement(); // Elemento 'dup'
        }

        private string _numero = string.Empty;
        private DateTime _dataVencimento;
        private decimal _valor;

        /// <summary>
        /// Retorna ou define o Número da Duplicata. Opcional.
        /// </summary>
        [NFeField(ID = "Y08", FieldName = "nDup", DataType = "token", MinLength = 0, MaxLength = 60, Opcional = true)]
        public string Numero
        {
            get { return _numero; }
            set { _numero = ValidationUtil.TruncateString(value, 60); }
        }

        /// <summary>
        /// Retorna ou define a Data de Vencimento da Fatura. Opcional.
        /// </summary>
        /// <remarks>Formato AAAA-MM-DD</remarks>
        [NFeField(ID = "Y09", FieldName = "dVenc", DataType = "TData", Pattern = @"\d{4}-\d{2}-\d{2}")]
        public DateTime DataVencimento
        {
            get { return _dataVencimento; }
            set
            {
                ValidationUtil.ValidateTData(value, "DataVencimento");

                _dataVencimento = value;
            }
        }

        /// <summary>
        /// Retorna ou define o Valor da Duplicata. Opcional.
        /// </summary>
        [NFeField(ID = "Y09", FieldName = "vDup", DataType = "TDev_1302Opc",
            Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?", Opcional = true)]
        public decimal Valor
        {
            get { return _valor; }
            set
            {
                _valor = ValidationUtil.ValidateTDec_1302(value, "Valor");
                ;
            }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado
        {
            get
            {
                return
                    DataVencimento != DateTime.MinValue ||
                    !string.IsNullOrEmpty(Numero) ||
                    Valor != 0m;
            }
        }
    }
}