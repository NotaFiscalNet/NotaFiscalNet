using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using System;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o Total de ISSQN da Nota Fiscal Eletrônica.
    /// </summary>
    public sealed class TotalISSQN : ISerializavel, IModificavel
    {
        private decimal _valorTotalServicos;
        private decimal _baseCalculo;
        private decimal _valorTotalISS;
        private decimal _valorPIS;
        private decimal _valorCOFINS;

        /// <summary>
        /// [vServ] Retorna ou define o Valor Total dos Serviços sob não-incidência ou não tributados
        /// pelo ICMS. Opcional.
        /// </summary>
        [NFeField(ID = "W18", FieldName = "vServ", DataType = "TDec_1302Opc", Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?", Opcional = true)]
        public decimal ValorTotalServicos
        {
            get { return _valorTotalServicos; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorTotalServicos");
                _valorTotalServicos = value;
            }
        }

        /// <summary>
        /// [vBC] Retorna ou define a Base de Cálculo do ISS. Opcional.
        /// </summary>
        [NFeField(ID = "W19", FieldName = "vBC", DataType = "TDec_1302Opc", Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?", Opcional = true)]
        public decimal BaseCalculo
        {
            get { return _baseCalculo; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "BaseCalculo");
                _baseCalculo = value;
            }
        }

        /// <summary>
        /// [vISS] Retorna ou define o Valor Total do ISS. Opcional.
        /// </summary>
        [NFeField(ID = "W20", FieldName = "vISS", DataType = "TDec_1302Opc", Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?", Opcional = true)]
        public decimal ValorTotalISS
        {
            get { return _valorTotalISS; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorTotalISS");
                _valorTotalISS = value;
            }
        }

        /// <summary>
        /// [vPIS] Retorna ou define o Valor do PIS. Opcional.
        /// </summary>
        [NFeField(ID = "W21", FieldName = "vPIS", DataType = "TDec_1302Opc", Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?", Opcional = true)]
        public decimal ValorPIS
        {
            get { return _valorPIS; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorPIS");
                _valorPIS = value;
            }
        }

        /// <summary>
        /// [vCOFINS] Retorna ou define o Valor do COFINS. Opcional.
        /// </summary>
        [NFeField(ID = "W22", FieldName = "vCOFINS", DataType = "TDec_1302Opc", Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?", Opcional = true)]
        public decimal ValorCOFINS
        {
            get { return _valorCOFINS; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorCOFINS");
                _valorCOFINS = value;
            }
        }

        /// <summary>
        /// [dCompet] Retorna ou define a Data da Prestação do Serviço.
        /// </summary>
        [NFeField(ID = "W22a", FieldName = "dCompet", DataType = "TData")]
        public DateTime DataCompetencia { get; set; }

        /// <summary>
        /// [vDeducao] Retorna ou define o Valor total dedução para redução da base de cálculo.
        /// </summary>
        [NFeField(ID = "W22b", FieldName = "vDeducao", DataType = "TDec_1302Opc", Opcional = true)]
        public decimal ValorDeducao { get; set; }

        /// <summary>
        /// [vOutro] Retorna ou define o Valor total outras retenções.
        /// </summary>
        [NFeField(ID = "W22c", FieldName = "vOutro", DataType = "TDec_1302Opc", Opcional = true)]
        public decimal ValorOutrasRetencoes { get; set; }

        /// <summary>
        /// [vDescIncond] Retorna ou define o Valor total desconto incondicionado.
        /// </summary>
        [NFeField(ID = "W22d", FieldName = "vDescIncond", DataType = "TDec_1302Opc", Opcional = true)]
        public decimal ValorDescontoIncondicionado { get; set; }

        /// <summary>
        /// [vDescCond] Retorna ou define o Valor total desconto condicionado.
        /// </summary>
        [NFeField(ID = "W22e", FieldName = "vDescCond", DataType = "TDec_1302Opc", Opcional = true)]
        public decimal ValorDescontoCondicionado { get; set; }

        /// <summary>
        /// [vISSRet] Retorna ou define o Valor total retenção ISS.
        /// </summary>
        [NFeField(ID = "W22f", FieldName = "vISSRet", DataType = "TDec_1302Opc", Opcional = true)]
        public decimal ValorIssRetido { get; set; }

        /// <summary>
        /// [cRegTrib] Retorna ou define o Valor total retenção ISS.
        /// </summary>
        [NFeField(ID = "W22g", FieldName = "cRegTrib", DataType = "TDec_1302Opc", Opcional = true)]
        public int CodigoRegimeTributacao { get; set; }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => ValorTotalServicos != 0m ||
                                  BaseCalculo > 0m ||
                                  ValorTotalISS > 0m ||
                                  ValorPIS > 0m ||
                                  ValorCOFINS > 0m ||
                                  DataCompetencia != DateTime.MinValue ||
                                  ValorDescontoCondicionado > 0m ||
                                  ValorOutrasRetencoes > 0m ||
                                  ValorDescontoIncondicionado > 0m ||
                                  ValorDescontoCondicionado > 0m ||
                                  ValorIssRetido > 0m ||
                                  CodigoRegimeTributacao != 0;

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ISSQNtot");

            if (ValorTotalServicos > 0m)
                writer.WriteElementString("vServ", SerializationUtil.ToTDec_1302(ValorTotalServicos));

            if (BaseCalculo > 0m)
                writer.WriteElementString("vBC", SerializationUtil.ToTDec_1302(BaseCalculo));

            if (ValorTotalISS > 0m)
                writer.WriteElementString("vISS", SerializationUtil.ToTDec_1302(ValorTotalISS));

            if (ValorPIS > 0m)
                writer.WriteElementString("vPIS", SerializationUtil.ToTDec_1302(ValorPIS));

            if (ValorCOFINS > 0m)
                writer.WriteElementString("vCOFINS", SerializationUtil.ToTDec_1302(ValorCOFINS));

            writer.WriteElementString("dCompet", DataCompetencia.ToTData());

            if (ValorDeducao > 0m)
                writer.WriteElementString("vDeducao", ValorDeducao.ToTDec_1302());

            if (ValorOutrasRetencoes > 0m)
                writer.WriteElementString("vOutro", ValorOutrasRetencoes.ToTDec_1302());

            if (ValorDescontoIncondicionado > 0m)
                writer.WriteElementString("vDescIncond", ValorDescontoIncondicionado.ToTDec_1302());

            if (ValorDescontoCondicionado > 0m)
                writer.WriteElementString("vDescCond", ValorDescontoCondicionado.ToTDec_1302());

            if (ValorIssRetido > 0m)
                writer.WriteElementString("vISSRet", ValorIssRetido.ToTDec_1302());

            if (CodigoRegimeTributacao != 0m)
                writer.WriteElementString("cRegTrib", CodigoRegimeTributacao.ToString());

            writer.WriteEndElement(); // fim do elemento 'ISSQNtot'
        }
    }
}