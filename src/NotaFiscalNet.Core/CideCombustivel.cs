using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa a Contribuição de Intervenção no Domínio Econômico do Combustível
    /// </summary>
    public sealed class CideCombustivel : ISerializavel, IModificavel
    {
        public void Serializar(XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("CIDE");

            writer.WriteStartElement("qBCProd", SerializationUtil.ToTDec_1204(BaseCalculo));
            writer.WriteStartElement("vAliqProd", SerializationUtil.ToTDec_1104(Aliquota));
            writer.WriteStartElement("vCIDE", SerializationUtil.ToTDec_1302(Valor));

            writer.WriteEndElement();
        }

        private decimal _baseCalculo;
        private decimal _aliquota;
        private decimal _valor;

        /// <summary>
        /// [qBCProd] Retorna ou define a Base de Cálculo do CIDE (Quantidade Comercializada)
        /// </summary>
        [NFeField(ID = "L106", FieldName = "qBCProd", DataType = "TDec_1204",
            Pattern = @"0|0\.[0-9]{4}|[1-9]{1}[0-9]{0,11}(\.[0-9]{4})?")]
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal BaseCalculo
        {
            get { return _baseCalculo; }
            set
            {
                ValidationUtil.ValidateTDec_1204(value, "BaseCalculo");
                _baseCalculo = value;
            }
        }

        /// <summary>
        /// [vAliqProd] Retorna ou define a Alíquota do CIDE em moeda corrente.
        /// </summary>
        [NFeField(ID = "L107", FieldName = "vAliqProd", DataType = "TDec_1104",
            Pattern = @"0|0\.[0-9]{4}|[1-9]{1}[0-9]{0,10}(\.[0-9]{4})?")]
        [ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal Aliquota
        {
            get { return _aliquota; }
            set
            {
                ValidationUtil.ValidateTDec_1104(value, "Aliquota");
                _aliquota = value;
            }
        }

        /// <summary>
        /// [vCIDE] Retorna ou define o Valor da CIDE
        /// </summary>
        [NFeField(ID = "L108", FieldName = "vCIDE", DataType = "TDec_1302",
            Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
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
        public bool Modificado => BaseCalculo != 0m ||
                                  Aliquota != 0m ||
                                  Valor != 0m;
    }
}