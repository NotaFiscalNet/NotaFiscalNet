using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o Total de ICMS da Nota Fiscal Eletrônica.
    /// </summary>

    public sealed class TotalICMS : ISerializavel
    {
        private decimal _baseCalculo;
        private decimal _valorTotalICMS;
        private decimal _valorTotalICMSDesonerado;
        private decimal _baseCalculoST;
        private decimal _valorTotalICMSST;
        private decimal _valorTotalProdutos;
        private decimal _valorTotalFrete;
        private decimal _valorTotalSeguro;
        private decimal _valorTotalDesconto;
        private decimal _valorTotalII;
        private decimal _valorTotalIPI;
        private decimal _valorTotalPIS;
        private decimal _valorTotalCOFINS;
        private decimal _valorOutrasDespesas;
        private decimal _valorTotalNFe;
        private decimal? _valorTotalTributos;

        /// <summary>
        /// [vBC] Retorna ou define a Base de Cálculo do ICMS.
        /// </summary>
        [NFeField(ID = "W03", FieldName = "vBC", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [CampoValidavel(1, Opcional = true)]
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
        /// [vICMS] Retorna ou define o Valor Total do ICMS.
        /// </summary>
        [NFeField(ID = "W04", FieldName = "vICMS", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [CampoValidavel(2, Opcional = true)]
        public decimal ValorTotalICMS
        {
            get { return _valorTotalICMS; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorTotalICMS");
                _valorTotalICMS = value;
            }
        }

        /// <summary>
        /// [vICMSDeson] Retorna ou define o Valor Total do ICMS desonerado
        /// </summary>
        [NFeField(ID = "W04", FieldName = "vICMS", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [CampoValidavel(2, Opcional = true)]
        public decimal ValorTotalICMSDesonerado
        {
            get { return _valorTotalICMSDesonerado; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorTotalICMSDesonerado");
                _valorTotalICMSDesonerado = value;
            }
        }

        /// <summary>
        /// [vBCST] Retorna ou define a Base de Cálculo do ICMS do Substituto Tributário.
        /// </summary>
        [NFeField(ID = "W05", FieldName = "vBCST", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [CampoValidavel(3, Opcional = true)]
        public decimal BaseCalculoST
        {
            get { return _baseCalculoST; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "BaseCalculoST");
                _baseCalculoST = value;
            }
        }

        /// <summary>
        /// [vST] Retorna ou define o Valor Total do ICMS do Substituto Tributário.
        /// </summary>
        [NFeField(ID = "W06", FieldName = "vST", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [CampoValidavel(4, Opcional = true)]
        public decimal ValorTotalICMSST
        {
            get { return _valorTotalICMSST; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorTotalICMSST");
                _valorTotalICMSST = value;
            }
        }

        /// <summary>
        /// [vProd] Retorna ou define o Valor Total dos Produtos e Serviços.
        /// </summary>
        [NFeField(ID = "W07", FieldName = "vProd", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [CampoValidavel(5, Opcional = true)]
        public decimal ValorTotalProdutos
        {
            get { return _valorTotalProdutos; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorTotalProdutos");
                _valorTotalProdutos = value;
            }
        }

        /// <summary>
        /// [vFrete] Retorna ou define o Valor Total do Frete.
        /// </summary>
        [NFeField(ID = "W08", FieldName = "vFrete", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [CampoValidavel(6, Opcional = true)]
        public decimal ValorTotalFrete
        {
            get { return _valorTotalFrete; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorTotalFrete");
                _valorTotalFrete = value;
            }
        }

        /// <summary>
        /// [vSeg] Retorna ou define o Valor Total do Seguro.
        /// </summary>
        [NFeField(ID = "W09", FieldName = "vSeg", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [CampoValidavel(7, Opcional = true)]
        public decimal ValorTotalSeguro
        {
            get { return _valorTotalSeguro; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorTotalSeguro");
                _valorTotalSeguro = value;
            }
        }

        /// <summary>
        /// [vDesc] Retorna ou define o Valor Total do Desconto.
        /// </summary>
        [NFeField(ID = "W10", FieldName = "vDesc", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [CampoValidavel(8, Opcional = true)]
        public decimal ValorTotalDesconto
        {
            get { return _valorTotalDesconto; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorTotalDesconto");
                _valorTotalDesconto = value;
            }
        }

        /// <summary>
        /// [vII] Retorna ou define o Valor Total do II.
        /// </summary>
        [NFeField(ID = "W11", FieldName = "vII", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [CampoValidavel(9, Opcional = true)]
        public decimal ValorTotalII
        {
            get { return _valorTotalII; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorTotalII");
                _valorTotalII = value;
            }
        }

        /// <summary>
        /// [vIPI] Retorna ou define o Valor Total do IPI.
        /// </summary>
        [NFeField(ID = "W12", FieldName = "vIPI", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [CampoValidavel(10, Opcional = true)]
        public decimal ValorTotalIPI
        {
            get { return _valorTotalIPI; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorTotalIPI");
                _valorTotalIPI = value;
            }
        }

        /// <summary>
        /// [vPIS] Retorna ou define o Valor Total do PIS.
        /// </summary>
        [NFeField(ID = "W13", FieldName = "vPIS", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [CampoValidavel(11, Opcional = true)]
        public decimal ValorTotalPIS
        {
            get { return _valorTotalPIS; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorTotalPIS");
                _valorTotalPIS = value;
            }
        }

        /// <summary>
        /// [vCOFINS] Retorna ou define o Valor Total do COFINS.
        /// </summary>
        [NFeField(ID = "W14", FieldName = "vCOFINS", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [CampoValidavel(12, Opcional = true)]
        public decimal ValorTotalCOFINS
        {
            get { return _valorTotalCOFINS; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorTotalCOFINS");
                _valorTotalCOFINS = value;
            }
        }

        /// <summary>
        /// [vOutro] Retorna ou define o Valor de Outras Despesas Acessórias.
        /// </summary>
        [NFeField(ID = "W15", FieldName = "vOutro", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [CampoValidavel(13, Opcional = true)]
        public decimal ValorOutrasDespesas
        {
            get { return _valorOutrasDespesas; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorOutrasDespesas");
                _valorOutrasDespesas = value;
            }
        }

        /// <summary>
        /// [vNF] Retorna ou define o Valor Total da Nota Fiscal Eletrônica.
        /// </summary>
        [NFeField(ID = "W16", FieldName = "vNF", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [CampoValidavel(14, Opcional = true)]
        public decimal ValorTotalNFe
        {
            get { return _valorTotalNFe; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorTotalNFe");
                _valorTotalNFe = value;
            }
        }

        /// <summary>
        /// [vTotTrib] Retorna ou define o Valor Total da Nota Fiscal Eletrônica.
        /// </summary>
        [NFeField(FieldName = "vTotTrib", DataType = TiposBasicos.TDec_1302)]
        [CampoValidavel(15, Opcional = true)]
        public decimal? ValorTotalTributos
        {
            get { return _valorTotalTributos; }
            set
            {
                _valorTotalTributos = value.HasValue
                    ? ValidationUtil.ValidateTDec_1302(value.Value, "ValorTotalTributos")
                    : (decimal?)null;
            }
        }

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMSTot");
            writer.WriteElementString("vBC", SerializationUtil.ToTDec_1302(BaseCalculo));
            writer.WriteElementString("vICMS", SerializationUtil.ToTDec_1302(ValorTotalICMS));
            writer.WriteElementString("vICMSDeson", SerializationUtil.ToTDec_1302(ValorTotalICMSDesonerado));
            writer.WriteElementString("vBCST", SerializationUtil.ToTDec_1302(BaseCalculoST));
            writer.WriteElementString("vST", SerializationUtil.ToTDec_1302(ValorTotalICMSST));
            writer.WriteElementString("vProd", SerializationUtil.ToTDec_1302(ValorTotalProdutos));
            writer.WriteElementString("vFrete", SerializationUtil.ToTDec_1302(ValorTotalFrete));
            writer.WriteElementString("vSeg", SerializationUtil.ToTDec_1302(ValorTotalSeguro));
            writer.WriteElementString("vDesc", SerializationUtil.ToTDec_1302(ValorTotalDesconto));
            writer.WriteElementString("vII", SerializationUtil.ToTDec_1302(ValorTotalII));
            writer.WriteElementString("vIPI", SerializationUtil.ToTDec_1302(ValorTotalIPI));
            writer.WriteElementString("vPIS", SerializationUtil.ToTDec_1302(ValorTotalPIS));
            writer.WriteElementString("vCOFINS", SerializationUtil.ToTDec_1302(ValorTotalCOFINS));
            writer.WriteElementString("vOutro", SerializationUtil.ToTDec_1302(ValorOutrasDespesas));
            writer.WriteElementString("vNF", SerializationUtil.ToTDec_1302(ValorTotalNFe));
            if (ValorTotalTributos.HasValue)
                writer.WriteElementString("vTotTrib", SerializationUtil.ToTDec_1302(ValorTotalTributos.Value));
            writer.WriteEndElement(); // fim do elemento 'ICMSTot'        }
        }
    }
}