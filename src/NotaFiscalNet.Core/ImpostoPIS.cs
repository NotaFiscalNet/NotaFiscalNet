using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using System;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o Imposto Programa de Integração Social
    /// </summary>
    public sealed class ImpostoPIS : ISerializavel, IModificavel
    {
        private SituacaoTributariaPIS _situacaoTributaria = SituacaoTributariaPIS.NaoEspecificado;
        private decimal _baseCalculo;
        private decimal _aliquota;
        private TipoCalculoPIS _tipo = TipoCalculoPIS.PercentualValor;
        private decimal _valor;
        private bool _canChangeTipo = true;

        private readonly ImpostoProduto _imposto;

        internal ImpostoPIS(ImpostoProduto imposto)
        {
            _imposto = imposto;
        }

        /// <summary>
        /// Retorna a referência para o objeto ImpostoProduto no qual o Imposto se refere.
        /// </summary>
        internal ImpostoProduto Imposto => _imposto;

        /// <summary>
        /// Retorna ou define a Situação Tributária.
        /// </summary>
        [NFeField(ID = "Q06", FieldName = "CST", DataType = "token")]
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido, DefaultValue = SituacaoTributariaPIS.NaoEspecificado)]
        public SituacaoTributariaPIS SituacaoTributaria
        {
            get { return _situacaoTributaria; }
            set
            {
                ValidationUtil.ValidateEnum<SituacaoTributariaPIS>(value, "SituacaoTributaria");

                switch (value)
                {
                    case SituacaoTributariaPIS.Cst01:
                    case SituacaoTributariaPIS.Cst02:
                        _tipo = TipoCalculoPIS.PercentualValor;
                        _canChangeTipo = false;
                        break;

                    case SituacaoTributariaPIS.Cst03:
                        _tipo = TipoCalculoPIS.ValorQuantidade;
                        _canChangeTipo = false;
                        break;

                    case SituacaoTributariaPIS.Cst04:
                    case SituacaoTributariaPIS.Cst06:
                    case SituacaoTributariaPIS.Cst07:
                    case SituacaoTributariaPIS.Cst08:
                    case SituacaoTributariaPIS.Cst09:
                        _baseCalculo = 0.0m;
                        _aliquota = 0.0m;
                        _valor = 0.0m;
                        _canChangeTipo = false;
                        break;

                    case SituacaoTributariaPIS.Cst49:
                    case SituacaoTributariaPIS.Cst50:
                    case SituacaoTributariaPIS.Cst51:
                    case SituacaoTributariaPIS.Cst52:
                    case SituacaoTributariaPIS.Cst53:
                    case SituacaoTributariaPIS.Cst54:
                    case SituacaoTributariaPIS.Cst55:
                    case SituacaoTributariaPIS.Cst56:
                    case SituacaoTributariaPIS.Cst60:
                    case SituacaoTributariaPIS.Cst61:
                    case SituacaoTributariaPIS.Cst62:
                    case SituacaoTributariaPIS.Cst63:
                    case SituacaoTributariaPIS.Cst64:
                    case SituacaoTributariaPIS.Cst65:
                    case SituacaoTributariaPIS.Cst66:
                    case SituacaoTributariaPIS.Cst67:
                    case SituacaoTributariaPIS.Cst70:
                    case SituacaoTributariaPIS.Cst71:
                    case SituacaoTributariaPIS.Cst72:
                    case SituacaoTributariaPIS.Cst73:
                    case SituacaoTributariaPIS.Cst74:
                    case SituacaoTributariaPIS.Cst75:
                    case SituacaoTributariaPIS.Cst98:
                    case SituacaoTributariaPIS.Cst99:
                        _canChangeTipo = true;
                        break;
                }
                _situacaoTributaria = value;
            }
        }

        /// <summary>
        /// Retorna ou define o Tipo da Alíquota e da Base de Cálculo.
        /// </summary>
        public TipoCalculoPIS TipoCalculo
        {
            get { return _tipo; }
            set
            {
                if (!_canChangeTipo)
                    throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeTipoCalculo, SituacaoTributaria));
                ValidationUtil.ValidateEnum<TipoCalculoPIS>(value, "TipoCalculo");
                _tipo = value;
            }
        }

        /// <summary>
        /// [vBC,qBCProd] Retorna ou define a Base de Cálculo do PIS.
        /// </summary>
        [NFeField(ID = "Q07", FieldName = "vBC", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [NFeField(ID = "Q10", FieldName = "qBCProd", DataType = "TDec_1204", Pattern = @"0|0\.[0-9]{4}|[1-9]{1}[0-9]{0,11}(\.[0-9]{4})?")]
        //[ValidateField(2, Validator=typeof(ImpostoPISValidator))]
        public decimal BaseCalculo
        {
            get { return _baseCalculo; }
            set
            {
                switch (SituacaoTributaria)
                {
                    case SituacaoTributariaPIS.Cst01:
                    case SituacaoTributariaPIS.Cst02:
                        ValidationUtil.ValidateTDec_1302(value, "BaseCalculo");
                        break;

                    case SituacaoTributariaPIS.Cst03:
                        ValidationUtil.ValidateTDec_1204(value, "BaseCalculo");
                        break;

                    case SituacaoTributariaPIS.Cst04:
                    case SituacaoTributariaPIS.Cst06:
                    case SituacaoTributariaPIS.Cst07:
                    case SituacaoTributariaPIS.Cst08:
                    case SituacaoTributariaPIS.Cst09:
                        throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeBaseCalculo, SituacaoTributaria));
                    case SituacaoTributariaPIS.Cst49:
                    case SituacaoTributariaPIS.Cst50:
                    case SituacaoTributariaPIS.Cst51:
                    case SituacaoTributariaPIS.Cst52:
                    case SituacaoTributariaPIS.Cst53:
                    case SituacaoTributariaPIS.Cst54:
                    case SituacaoTributariaPIS.Cst55:
                    case SituacaoTributariaPIS.Cst56:
                    case SituacaoTributariaPIS.Cst60:
                    case SituacaoTributariaPIS.Cst61:
                    case SituacaoTributariaPIS.Cst62:
                    case SituacaoTributariaPIS.Cst63:
                    case SituacaoTributariaPIS.Cst64:
                    case SituacaoTributariaPIS.Cst65:
                    case SituacaoTributariaPIS.Cst66:
                    case SituacaoTributariaPIS.Cst67:
                    case SituacaoTributariaPIS.Cst70:
                    case SituacaoTributariaPIS.Cst71:
                    case SituacaoTributariaPIS.Cst72:
                    case SituacaoTributariaPIS.Cst73:
                    case SituacaoTributariaPIS.Cst74:
                    case SituacaoTributariaPIS.Cst75:
                    case SituacaoTributariaPIS.Cst98:
                    case SituacaoTributariaPIS.Cst99:
                        switch (TipoCalculo)
                        {
                            case TipoCalculoPIS.PercentualValor:
                                ValidationUtil.ValidateTDec_1302(value, "BaseCalculo");
                                break;

                            case TipoCalculoPIS.ValorQuantidade:
                                ValidationUtil.ValidateTDec_1204(value, "BaseCalculo");
                                break;
                        }
                        break;
                }
                _baseCalculo = value;
            }
        }

        /// <summary>
        /// [pPIS,vAliqProd] Retorna ou define a Alíquota do PIS.
        /// </summary>
        [NFeField(ID = "Q08", FieldName = "pPIS", DataType = "TDec_0302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,2}(\.[0-9]{2})?")]
        [NFeField(ID = "Q11", FieldName = "vAliqProd", DataType = "TDec_1104", Pattern = @"0|0\.[0-9]{4}|[1-9]{1}[0-9]{0,10}(\.[0-9]{4})?")]
        //[ValidateField(3, Validator = typeof(ImpostoPISValidator))]
        public decimal Aliquota
        {
            get { return _aliquota; }
            set
            {
                switch (SituacaoTributaria)
                {
                    case SituacaoTributariaPIS.Cst01:
                    case SituacaoTributariaPIS.Cst02:
                        ValidationUtil.ValidateTDec_0302(value, "Aliquota");
                        break;

                    case SituacaoTributariaPIS.Cst03:
                        ValidationUtil.ValidateTDec_1104(value, "Aliquota");
                        break;

                    case SituacaoTributariaPIS.Cst04:
                    case SituacaoTributariaPIS.Cst06:
                    case SituacaoTributariaPIS.Cst07:
                    case SituacaoTributariaPIS.Cst08:
                    case SituacaoTributariaPIS.Cst09:
                        throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeAliquota, SituacaoTributaria));
                    case SituacaoTributariaPIS.Cst99:
                        switch (TipoCalculo)
                        {
                            case TipoCalculoPIS.PercentualValor:
                                ValidationUtil.ValidateTDec_0302(value, "Aliquota");
                                break;

                            case TipoCalculoPIS.ValorQuantidade:
                                ValidationUtil.ValidateTDec_1104(value, "Aliquota");
                                break;
                        }
                        break;
                }
                _aliquota = value;
            }
        }

        /// <summary>
        /// [vPIS] Retorna ou define o Valor do PIS
        /// </summary>
        [NFeField(ID = "Q09", FieldName = "vPIS", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        //[ValidateField(4, Validator = typeof(ImpostoPISValidator))]
        public decimal Valor
        {
            get { return _valor; }
            set
            {
                switch (SituacaoTributaria)
                {
                    case SituacaoTributariaPIS.Cst01:
                    case SituacaoTributariaPIS.Cst02:
                    case SituacaoTributariaPIS.Cst03:
                    case SituacaoTributariaPIS.Cst49:
                    case SituacaoTributariaPIS.Cst50:
                    case SituacaoTributariaPIS.Cst51:
                    case SituacaoTributariaPIS.Cst52:
                    case SituacaoTributariaPIS.Cst53:
                    case SituacaoTributariaPIS.Cst54:
                    case SituacaoTributariaPIS.Cst55:
                    case SituacaoTributariaPIS.Cst56:
                    case SituacaoTributariaPIS.Cst60:
                    case SituacaoTributariaPIS.Cst61:
                    case SituacaoTributariaPIS.Cst62:
                    case SituacaoTributariaPIS.Cst63:
                    case SituacaoTributariaPIS.Cst64:
                    case SituacaoTributariaPIS.Cst65:
                    case SituacaoTributariaPIS.Cst66:
                    case SituacaoTributariaPIS.Cst67:
                    case SituacaoTributariaPIS.Cst70:
                    case SituacaoTributariaPIS.Cst71:
                    case SituacaoTributariaPIS.Cst72:
                    case SituacaoTributariaPIS.Cst73:
                    case SituacaoTributariaPIS.Cst74:
                    case SituacaoTributariaPIS.Cst75:
                    case SituacaoTributariaPIS.Cst98:
                    case SituacaoTributariaPIS.Cst99:
                        ValidationUtil.ValidateTDec_1302(value, "Valor");
                        break;

                    case SituacaoTributariaPIS.Cst04:
                    case SituacaoTributariaPIS.Cst06:
                    case SituacaoTributariaPIS.Cst07:
                    case SituacaoTributariaPIS.Cst08:
                    case SituacaoTributariaPIS.Cst09:
                        throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeValor, SituacaoTributaria));
                }
                _valor = value;
            }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => Aliquota != 0m ||
                                  BaseCalculo != 0m ||
                                  SituacaoTributaria != SituacaoTributariaPIS.NaoEspecificado ||
                                  Valor != 0m;

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("PIS"); // Elemento 'PIS'
            switch (SituacaoTributaria)
            {
                case SituacaoTributariaPIS.Cst01:
                case SituacaoTributariaPIS.Cst02:
                    SerializePISAliq(writer, nfe);
                    break;

                case SituacaoTributariaPIS.Cst03:
                    SerializePISQtde(writer, nfe);
                    break;

                case SituacaoTributariaPIS.Cst04:
                case SituacaoTributariaPIS.Cst06:
                case SituacaoTributariaPIS.Cst07:
                case SituacaoTributariaPIS.Cst08:
                case SituacaoTributariaPIS.Cst09:
                    SerializePISNT(writer, nfe);
                    break;

                case SituacaoTributariaPIS.Cst49:
                case SituacaoTributariaPIS.Cst50:
                case SituacaoTributariaPIS.Cst51:
                case SituacaoTributariaPIS.Cst52:
                case SituacaoTributariaPIS.Cst53:
                case SituacaoTributariaPIS.Cst54:
                case SituacaoTributariaPIS.Cst55:
                case SituacaoTributariaPIS.Cst56:
                case SituacaoTributariaPIS.Cst60:
                case SituacaoTributariaPIS.Cst61:
                case SituacaoTributariaPIS.Cst62:
                case SituacaoTributariaPIS.Cst63:
                case SituacaoTributariaPIS.Cst64:
                case SituacaoTributariaPIS.Cst65:
                case SituacaoTributariaPIS.Cst66:
                case SituacaoTributariaPIS.Cst67:
                case SituacaoTributariaPIS.Cst70:
                case SituacaoTributariaPIS.Cst71:
                case SituacaoTributariaPIS.Cst72:
                case SituacaoTributariaPIS.Cst73:
                case SituacaoTributariaPIS.Cst74:
                case SituacaoTributariaPIS.Cst75:
                case SituacaoTributariaPIS.Cst98:
                case SituacaoTributariaPIS.Cst99:
                    SerializePISOutr(writer, nfe);
                    break;
            }

            writer.WriteEndElement(); // Elemento 'PIS'
        }

        /// <summary>
        /// Serializa Elemento 'PISAliq'
        /// </summary>
        private void SerializePISAliq(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("PISAliq"); // Elemento 'PISAliq'
            writer.WriteElementString("CST", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue<SituacaoTributariaPIS>(SituacaoTributaria))));
            writer.WriteElementString("vBC", SerializationUtil.ToTDec_1302(BaseCalculo));
            writer.WriteElementString("pPIS", SerializationUtil.ToTDec_0302(Aliquota));
            writer.WriteElementString("vPIS", SerializationUtil.ToTDec_1302(Valor));
            writer.WriteEndElement(); // Elemento 'PISAliq'
        }

        /// <summary>
        /// Serializa Elemento 'PISQtde'
        /// </summary>
        private void SerializePISQtde(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("PISQtde"); // Elemento 'PISQtde'
            writer.WriteElementString("CST", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue<SituacaoTributariaPIS>(SituacaoTributaria))));
            writer.WriteElementString("qBCProd", SerializationUtil.ToTDec_1204(BaseCalculo));
            writer.WriteElementString("vAliqProd", SerializationUtil.ToTDec_1104(Aliquota));
            writer.WriteElementString("vPIS", SerializationUtil.ToTDec_1302(Valor));
            writer.WriteEndElement(); // Elemento 'PISQtde'
        }

        /// <summary>
        /// Serializa Elemento 'PISNT'
        /// </summary>
        private void SerializePISNT(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("PISNT"); // Elemento 'PISNT'
            writer.WriteElementString("CST", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue<SituacaoTributariaPIS>(SituacaoTributaria))));
            writer.WriteEndElement(); // Elemento 'PISNT'
        }

        /// <summary>
        /// Serializa Elemento 'PISOutr'
        /// </summary>
        private void SerializePISOutr(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("PISOutr"); // Elemento 'PISOutr'
            writer.WriteElementString("CST", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue<SituacaoTributariaPIS>(SituacaoTributaria))));
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
            writer.WriteEndElement(); // Elemento 'PISOutr'
        }
    }
}