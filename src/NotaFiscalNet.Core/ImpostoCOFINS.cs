using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;

using System;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o Imposto Contribuição para o Financiamento da Seguridade Social
    /// </summary>
    public sealed class ImpostoCOFINS : ISerializavel, IModificavel
    {
        private SituacaoTributariaCOFINS _situacaoTributaria = SituacaoTributariaCOFINS.NaoEspecificado;
        private decimal _baseCalculo;
        private decimal _aliquota;
        private TipoCalculoCOFINS _tipo = TipoCalculoCOFINS.PercentualValor;
        private decimal _valor;
        private bool _canChangeTipo = true;

        private readonly ImpostoProduto _imposto;

        internal ImpostoCOFINS(ImpostoProduto imposto)
        {
            _imposto = imposto;
        }

        /// <summary>
        /// Retorna a referência para o objeto ImpostoProduto no qual o Imposto se refere.
        /// </summary>
        internal ImpostoProduto Imposto => _imposto;

        /// <summary>
        /// [CST] Retorna ou define a Situação Tributária.
        /// </summary>
        [NFeField(ID = "S06", FieldName = "CST", DataType = "token")]
        [CampoValidavel(1, ChaveErroValidacao.CampoNaoPreenchido, ValorNaoPreenchido = SituacaoTributariaCOFINS.NaoEspecificado)]
        public SituacaoTributariaCOFINS SituacaoTributaria
        {
            get { return _situacaoTributaria; }
            set
            {
                ValidationUtil.ValidateEnum(value, "SituacaoTributaria");

                switch (value)
                {
                    case SituacaoTributariaCOFINS.Cst01:
                    case SituacaoTributariaCOFINS.Cst02:
                        _tipo = TipoCalculoCOFINS.PercentualValor;
                        _canChangeTipo = false;
                        break;

                    case SituacaoTributariaCOFINS.Cst03:
                        _tipo = TipoCalculoCOFINS.ValorQuantidade;
                        _canChangeTipo = false;
                        break;

                    case SituacaoTributariaCOFINS.Cst04:
                    case SituacaoTributariaCOFINS.Cst06:
                    case SituacaoTributariaCOFINS.Cst07:
                    case SituacaoTributariaCOFINS.Cst08:
                    case SituacaoTributariaCOFINS.Cst09:
                        _baseCalculo = 0;
                        _aliquota = 0;
                        _valor = 0;
                        _canChangeTipo = false;
                        break;

                    case SituacaoTributariaCOFINS.Cst49:
                    case SituacaoTributariaCOFINS.Cst50:
                    case SituacaoTributariaCOFINS.Cst51:
                    case SituacaoTributariaCOFINS.Cst52:
                    case SituacaoTributariaCOFINS.Cst53:
                    case SituacaoTributariaCOFINS.Cst54:
                    case SituacaoTributariaCOFINS.Cst55:
                    case SituacaoTributariaCOFINS.Cst56:
                    case SituacaoTributariaCOFINS.Cst60:
                    case SituacaoTributariaCOFINS.Cst61:
                    case SituacaoTributariaCOFINS.Cst62:
                    case SituacaoTributariaCOFINS.Cst63:
                    case SituacaoTributariaCOFINS.Cst64:
                    case SituacaoTributariaCOFINS.Cst65:
                    case SituacaoTributariaCOFINS.Cst66:
                    case SituacaoTributariaCOFINS.Cst67:
                    case SituacaoTributariaCOFINS.Cst70:
                    case SituacaoTributariaCOFINS.Cst71:
                    case SituacaoTributariaCOFINS.Cst72:
                    case SituacaoTributariaCOFINS.Cst73:
                    case SituacaoTributariaCOFINS.Cst74:
                    case SituacaoTributariaCOFINS.Cst75:
                    case SituacaoTributariaCOFINS.Cst98:
                    case SituacaoTributariaCOFINS.Cst99:
                        _canChangeTipo = true;
                        break;
                }
                _situacaoTributaria = value;
            }
        }

        /// <summary>
        /// Retorna ou define o Tipo da Alíquota e da Base de Cálculo.
        /// </summary>
        public TipoCalculoCOFINS TipoCalculo
        {
            get { return _tipo; }
            set
            {
                if (!_canChangeTipo)
                    throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeTipoCalculo, SituacaoTributaria));
                ValidationUtil.ValidateEnum(value, "TipoCalculo");
                _tipo = value;
            }
        }

        /// <summary>
        /// [vBC,qBCProd] Retorna ou define a Base de Cálculo do COFINS.
        /// </summary>
        [NFeField(ID = "S07", FieldName = "vBC", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [NFeField(ID = "S09", FieldName = "qBCProd", DataType = "TDec_1204", Pattern = @"0|0\.[0-9]{4}|[1-9]{1}[0-9]{0,11}(\.[0-9]{4})?")]
        [CampoValidavel(2)]
        public decimal BaseCalculo
        {
            get { return _baseCalculo; }
            set
            {
                switch (SituacaoTributaria)
                {
                    case SituacaoTributariaCOFINS.Cst01:
                    case SituacaoTributariaCOFINS.Cst02:
                        ValidationUtil.ValidateTDec_1302(value, "BaseCalculo");
                        break;

                    case SituacaoTributariaCOFINS.Cst03:
                        ValidationUtil.ValidateTDec_1204(value, "BaseCalculo");
                        break;

                    case SituacaoTributariaCOFINS.Cst04:
                    case SituacaoTributariaCOFINS.Cst06:
                    case SituacaoTributariaCOFINS.Cst07:
                    case SituacaoTributariaCOFINS.Cst08:
                    case SituacaoTributariaCOFINS.Cst09:
                        throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeBaseCalculo, SituacaoTributaria));
                    case SituacaoTributariaCOFINS.Cst49:
                    case SituacaoTributariaCOFINS.Cst50:
                    case SituacaoTributariaCOFINS.Cst51:
                    case SituacaoTributariaCOFINS.Cst52:
                    case SituacaoTributariaCOFINS.Cst53:
                    case SituacaoTributariaCOFINS.Cst54:
                    case SituacaoTributariaCOFINS.Cst55:
                    case SituacaoTributariaCOFINS.Cst56:
                    case SituacaoTributariaCOFINS.Cst60:
                    case SituacaoTributariaCOFINS.Cst61:
                    case SituacaoTributariaCOFINS.Cst62:
                    case SituacaoTributariaCOFINS.Cst63:
                    case SituacaoTributariaCOFINS.Cst64:
                    case SituacaoTributariaCOFINS.Cst65:
                    case SituacaoTributariaCOFINS.Cst66:
                    case SituacaoTributariaCOFINS.Cst67:
                    case SituacaoTributariaCOFINS.Cst70:
                    case SituacaoTributariaCOFINS.Cst71:
                    case SituacaoTributariaCOFINS.Cst72:
                    case SituacaoTributariaCOFINS.Cst73:
                    case SituacaoTributariaCOFINS.Cst74:
                    case SituacaoTributariaCOFINS.Cst75:
                    case SituacaoTributariaCOFINS.Cst98:
                    case SituacaoTributariaCOFINS.Cst99:
                        switch (TipoCalculo)
                        {
                            case TipoCalculoCOFINS.PercentualValor:
                                ValidationUtil.ValidateTDec_1302(value, "BaseCalculo");
                                break;

                            case TipoCalculoCOFINS.ValorQuantidade:
                                ValidationUtil.ValidateTDec_1204(value, "BaseCalculo");
                                break;
                        }
                        break;
                }
                _baseCalculo = value;
            }
        }

        /// <summary>
        /// [pCOFINS,vAliqProd] Retorna ou define a Alíquota do COFINS.
        /// </summary>
        [NFeField(ID = "S08", FieldName = "pCOFINS", DataType = "TDec_0302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,2}(\.[0-9]{2})?")]
        [NFeField(ID = "S10", FieldName = "vAliqProd", DataType = "TDec_1104", Pattern = @"0|0\.[0-9]{4}|[1-9]{1}[0-9]{0,10}(\.[0-9]{4})?")]
        [CampoValidavel(3)]
        public decimal Aliquota
        {
            get { return _aliquota; }
            set
            {
                switch (SituacaoTributaria)
                {
                    case SituacaoTributariaCOFINS.Cst01:
                    case SituacaoTributariaCOFINS.Cst02:
                        ValidationUtil.ValidateTDec_0302(value, "Aliquota");
                        break;

                    case SituacaoTributariaCOFINS.Cst03:
                        ValidationUtil.ValidateTDec_1104(value, "Aliquota");
                        break;

                    case SituacaoTributariaCOFINS.Cst04:
                    case SituacaoTributariaCOFINS.Cst06:
                    case SituacaoTributariaCOFINS.Cst07:
                    case SituacaoTributariaCOFINS.Cst08:
                    case SituacaoTributariaCOFINS.Cst09:
                        throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeAliquota, SituacaoTributaria));
                    case SituacaoTributariaCOFINS.Cst49:
                    case SituacaoTributariaCOFINS.Cst50:
                    case SituacaoTributariaCOFINS.Cst51:
                    case SituacaoTributariaCOFINS.Cst52:
                    case SituacaoTributariaCOFINS.Cst53:
                    case SituacaoTributariaCOFINS.Cst54:
                    case SituacaoTributariaCOFINS.Cst55:
                    case SituacaoTributariaCOFINS.Cst56:
                    case SituacaoTributariaCOFINS.Cst60:
                    case SituacaoTributariaCOFINS.Cst61:
                    case SituacaoTributariaCOFINS.Cst62:
                    case SituacaoTributariaCOFINS.Cst63:
                    case SituacaoTributariaCOFINS.Cst64:
                    case SituacaoTributariaCOFINS.Cst65:
                    case SituacaoTributariaCOFINS.Cst66:
                    case SituacaoTributariaCOFINS.Cst67:
                    case SituacaoTributariaCOFINS.Cst70:
                    case SituacaoTributariaCOFINS.Cst71:
                    case SituacaoTributariaCOFINS.Cst72:
                    case SituacaoTributariaCOFINS.Cst73:
                    case SituacaoTributariaCOFINS.Cst74:
                    case SituacaoTributariaCOFINS.Cst75:
                    case SituacaoTributariaCOFINS.Cst98:
                    case SituacaoTributariaCOFINS.Cst99:
                        switch (TipoCalculo)
                        {
                            case TipoCalculoCOFINS.PercentualValor:
                                ValidationUtil.ValidateTDec_0302(value, "Aliquota");
                                break;

                            case TipoCalculoCOFINS.ValorQuantidade:
                                ValidationUtil.ValidateTDec_1104(value, "Aliquota");
                                break;
                        }
                        break;
                }
                _aliquota = value;
            }
        }

        /// <summary>
        /// [vCOFINS] Retorna ou define o Valor do COFINS
        /// </summary>
        [NFeField(ID = "S11", FieldName = "vCOFINS", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [CampoValidavel(4)]
        public decimal Valor
        {
            get { return _valor; }
            set
            {
                switch (SituacaoTributaria)
                {
                    case SituacaoTributariaCOFINS.Cst01:
                    case SituacaoTributariaCOFINS.Cst02:
                    case SituacaoTributariaCOFINS.Cst03:
                    case SituacaoTributariaCOFINS.Cst99:
                        ValidationUtil.ValidateTDec_1302(value, "Valor");
                        break;

                    case SituacaoTributariaCOFINS.Cst04:
                    case SituacaoTributariaCOFINS.Cst06:
                    case SituacaoTributariaCOFINS.Cst07:
                    case SituacaoTributariaCOFINS.Cst08:
                    case SituacaoTributariaCOFINS.Cst09:
                        throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeValor, SituacaoTributaria));
                }
                _valor = value;
            }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => Aliquota != 0.0m ||
                                  BaseCalculo != 0.0m ||
                                  SituacaoTributaria != SituacaoTributariaCOFINS.NaoEspecificado ||
                                  Valor != 0.0m;

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("COFINS"); // Elemento 'COFINS'

            switch (SituacaoTributaria)
            {
                case SituacaoTributariaCOFINS.Cst01:
                case SituacaoTributariaCOFINS.Cst02:
                    SerializeCOFINSAliq(writer, nfe);
                    break;

                case SituacaoTributariaCOFINS.Cst03:
                    SerializeCOFINSQtde(writer, nfe);
                    break;

                case SituacaoTributariaCOFINS.Cst04:
                case SituacaoTributariaCOFINS.Cst06:
                case SituacaoTributariaCOFINS.Cst07:
                case SituacaoTributariaCOFINS.Cst08:
                case SituacaoTributariaCOFINS.Cst09:
                    SerializeCOFINSNT(writer, nfe);
                    break;

                case SituacaoTributariaCOFINS.Cst49:
                case SituacaoTributariaCOFINS.Cst50:
                case SituacaoTributariaCOFINS.Cst51:
                case SituacaoTributariaCOFINS.Cst52:
                case SituacaoTributariaCOFINS.Cst53:
                case SituacaoTributariaCOFINS.Cst54:
                case SituacaoTributariaCOFINS.Cst55:
                case SituacaoTributariaCOFINS.Cst56:
                case SituacaoTributariaCOFINS.Cst60:
                case SituacaoTributariaCOFINS.Cst61:
                case SituacaoTributariaCOFINS.Cst62:
                case SituacaoTributariaCOFINS.Cst63:
                case SituacaoTributariaCOFINS.Cst64:
                case SituacaoTributariaCOFINS.Cst65:
                case SituacaoTributariaCOFINS.Cst66:
                case SituacaoTributariaCOFINS.Cst67:
                case SituacaoTributariaCOFINS.Cst70:
                case SituacaoTributariaCOFINS.Cst71:
                case SituacaoTributariaCOFINS.Cst72:
                case SituacaoTributariaCOFINS.Cst73:
                case SituacaoTributariaCOFINS.Cst74:
                case SituacaoTributariaCOFINS.Cst75:
                case SituacaoTributariaCOFINS.Cst98:
                case SituacaoTributariaCOFINS.Cst99:
                    SerializeCOFINSOutr(writer, nfe);
                    break;
            }

            writer.WriteEndElement(); // Elemento 'COFINS'
        }

        /// <summary>
        /// Serializa Elemento 'COFINSAliq'
        /// </summary>
        private void SerializeCOFINSAliq(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("COFINSAliq"); // Elemento 'COFINSAliq'
            writer.WriteElementString("CST", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue<SituacaoTributariaCOFINS>(SituacaoTributaria))));
            writer.WriteElementString("vBC", BaseCalculo.ToTDec_1302());
            writer.WriteElementString("pCOFINS", Aliquota.ToTDec_1302());
            writer.WriteElementString("vCOFINS", Valor.ToTDec_1302());
            writer.WriteEndElement(); // Elemento 'COFINSAliq'
        }

        /// <summary>
        /// Serializa Elemento 'COFINSQtde'
        /// </summary>
        private void SerializeCOFINSQtde(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("COFINSQtde"); // Elemento 'COFINSQtde'
            writer.WriteElementString("CST", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue(SituacaoTributaria))));
            writer.WriteElementString("qBCProd", BaseCalculo.ToTDec_1204());
            writer.WriteElementString("vAliqProd", Aliquota.ToTDec_1104());
            writer.WriteElementString("vCOFINS", Valor.ToTDec_1302());
            writer.WriteEndElement(); // Elemento 'COFINSQtde'
        }

        /// <summary>
        /// Serializa Elemento 'COFINSNT'
        /// </summary>
        private void SerializeCOFINSNT(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("COFINSNT"); // Elemento 'COFINSNT'
            writer.WriteElementString("CST", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue(SituacaoTributaria))));
            writer.WriteEndElement(); // Elemento 'COFINSNT'
        }

        /// <summary>
        /// Serializa Elemento 'COFINSOutr'
        /// </summary>
        private void SerializeCOFINSOutr(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("COFINSOutr"); // Elemento 'COFINSOutr'
            writer.WriteElementString("CST", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue(SituacaoTributaria))));
            switch (TipoCalculo)
            {
                case TipoCalculoCOFINS.PercentualValor:
                    writer.WriteElementString("vBC", BaseCalculo.ToTDec_1302());
                    writer.WriteElementString("pCOFINS", Aliquota.ToTDec_0302());
                    break;

                case TipoCalculoCOFINS.ValorQuantidade:
                    writer.WriteElementString("qBCProd", BaseCalculo.ToTDec_1204());
                    writer.WriteElementString("vAliqProd", Aliquota.ToTDec_1104());
                    break;
            }
            writer.WriteElementString("vCOFINS", Valor.ToTDec_1302());
            writer.WriteEndElement(); // Elemento 'COFINSOutr'
        }
    }
}