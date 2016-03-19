using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o Imposto Programa de Integração Social ST
    /// </summary>
    public sealed class ImpostoPISST : ISerializavel, IModificavel
    {
        private TipoCalculoPIS _tipo = TipoCalculoPIS.PercentualValor;
        private decimal _baseCalculo;
        private decimal _aliquota;
        private decimal _valor;

        private readonly ImpostoProduto _imposto;

        internal ImpostoPISST(ImpostoProduto imposto)
        {
            _imposto = imposto;
        }

        /// <summary>
        /// Retorna a referência para o objeto ImpostoProduto no qual o Imposto se refere.
        /// </summary>
        internal ImpostoProduto Imposto => _imposto;

        /// <summary>
        /// Retorna ou define o Tipo da Alíquota e da Base de Cálculo.
        /// </summary>
        public TipoCalculoPIS TipoCalculo
        {
            get { return _tipo; }
            set
            {
                ValidationUtil.ValidateEnum(value, "TipoCalculo");
                _tipo = value;
            }
        }

        /// <summary>
        /// [vBC,qBCProd] Retorna ou define a Base de Cálculo do PIS ST.
        /// </summary>
        [NFeField(ID = "R02", FieldName = "vBC", DataType = "TDec_1302Opc", Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [NFeField(ID = "R04", FieldName = "qBCProd", DataType = "TDec_1204Opc", Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,2}(\.[0-9]{2})?")]
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal BaseCalculo
        {
            get { return _baseCalculo; }
            set
            {
                switch (TipoCalculo)
                {
                    case TipoCalculoPIS.PercentualValor:
                        ValidationUtil.ValidateTDec_1302(value, "BaseCalculo");
                        break;

                    case TipoCalculoPIS.ValorQuantidade:
                        ValidationUtil.ValidateTDec_1204(value, "BaseCalculo");
                        break;
                }
                _baseCalculo = value;
            }
        }

        /// <summary>
        /// [pPIS,vAliqProd] Retorna ou define a Alíquota do PIS ST.
        /// </summary>
        [NFeField(ID = "R03", FieldName = "pPIS", DataType = "TDec_0302Opc", Pattern = @"0\.[1-9]{1}[0-9]{3}|0\.[0-9]{3}[1-9]{1}|0\.[0-9]{2}[1-9]{1}[0-9]{1}|0\.[0-9]{1}[1-9]{1}[0-9]{2}|[1-9]{1}[0-9]{0,11}(\.[0-9]{4})?")]
        [NFeField(ID = "R05", FieldName = "vAliqProd", DataType = "TDec_1104Opc", Pattern = @"0\.[1-9]{1}[0-9]{3}|0\.[0-9]{3}[1-9]{1}|0\.[0-9]{2}[1-9]{1}[0-9]{1}|0\.[0-9]{1}[1-9]{1}[0-9]{2}|[1-9]{1}[0-9]{0,10}(\.[0-9]{4})?")]
        [ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal Aliquota
        {
            get { return _aliquota; }
            set
            {
                switch (TipoCalculo)
                {
                    case TipoCalculoPIS.PercentualValor:
                        ValidationUtil.ValidateTDec_0302(value, "Aliquota");
                        break;

                    case TipoCalculoPIS.ValorQuantidade:
                        ValidationUtil.ValidateTDec_1104(value, "Aliquota");
                        break;
                }
                _aliquota = value;
            }
        }

        /// <summary>
        /// Retorna ou define o Valor do PIS ST.
        /// </summary>
        [NFeField(ID = "R06", FieldName = "vPIS", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [ValidateField(3, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal Valor
        {
            get { return _valor; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "Valor");
                _valor = value;
            }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => Aliquota != 0m ||
                                  BaseCalculo != 0m ||
                                  Valor != 0m;

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("PISST"); // Elemento 'PISST'
            switch (TipoCalculo)
            {
                case TipoCalculoPIS.PercentualValor:
                    writer.WriteElementString("vBC", SerializationUtil.ToTDec_1302(BaseCalculo));
                    writer.WriteElementString("pPIS", SerializationUtil.ToTDec_0302(Aliquota));
                    break;

                case TipoCalculoPIS.ValorQuantidade:
                    writer.WriteElementString("qBCProd", SerializationUtil.ToTDec_1204(BaseCalculo));
                    writer.WriteElementString("vAliqProd", SerializationUtil.ToTDec_1104(Aliquota));
                    break;
            }
            writer.WriteElementString("vPIS", SerializationUtil.ToTDec_1302(Valor));
            writer.WriteEndElement(); // Elemento 'PISST'
        }
    }
}