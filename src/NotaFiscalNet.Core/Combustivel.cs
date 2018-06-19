using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa as informações de detalhamento de um Item de Produto da Nota Fiscal referente a
    /// combustíveis líquidos.
    /// </summary>
    public sealed class Combustivel : ISerializavel, IModificavel
    {
        internal Combustivel(Produto produto)
        {
            Produto = produto;
        }

        public void Serializar(XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("comb");

            writer.WriteElementString("cProdANP", CodigoProdutoANP.ToString());

            if (!string.IsNullOrEmpty(CodigoCODIF))
                writer.WriteElementString("CODIF", SerializationUtil.ToToken(CodigoCODIF, 21));

            if (QuantidadeCombustivelFaturadaTempAmbiente > 0)
                writer.WriteElementString("qTemp",
                    SerializationUtil.ToTDec_1204(QuantidadeCombustivelFaturadaTempAmbiente));

            writer.WriteElementString("UFCons", UFConsumo.ToString());

            if (CIDE != null)
                CIDE.Serializar(writer, nfe);

            writer.WriteEndElement(); // fim do elemento 'comb'
        }

        private int _codigoProdutoANP;
        private string _codif = string.Empty;
        private decimal _quantidadeCombustivelTempAmbiente;
        private SiglaUF _UFConsumo = SiglaUF.NaoEspecificado;

        private decimal? _percentualMixGn;

        /// <summary>
        /// [cProdANP] Retorna ou define o Código de produto da ANP. Código de produto da ANP.
        /// Utilizar a codificação de produtos do Sistema de Informações de Movimentação de produtos
        /// SIMP(http://www.anp.gov.br/simp/index.htm), somente informar 999999999 quando não se
        /// tratar de produtos não regulados pela ANP - Agência Nacional do Petróleo
        /// </summary>
        [NFeField(FieldName = "cProdANP", DataType = "token", ID = "L102", Pattern = @"[0-9]{9}")]
        [CampoValidavel(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public int CodigoProdutoANP
        {
            get => _codigoProdutoANP;
	        set => _codigoProdutoANP = ValidationUtil.ValidateRange(value, 0, 999999999, "CodigoProdutoANP");
        }

        /// <summary>
        /// [pMixGN] Retorna ou define o Percentual de Gás Natural para o produto GLP
        /// (CodigoProdutoANP = 210203001).
        /// </summary>
        [NFeField(FieldName = "pMixGN", DataType = "TDec_0204v", ID = "L102a", Opcional = true)]
        [CampoValidavel(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal? PercentualMixGN
        {
            get => _percentualMixGn;
	        set => _percentualMixGn = ValidationUtil.ValidateTDec_0204v(value, "PercentualMixGN");
        }

        /// <summary>
        /// [CODIF] Retorna ou define o CODIF (Sistema de Controle do Diferimento do Imposto nas
        /// Operações com AEAC - Álcool Etílico Anidro Combustível). Informar apenas quando a UF
        /// utilizar o CODIF. Apenas números (máximo 21 caracteres).
        /// </summary>
        [NFeField(FieldName = "CODIF", DataType = "token", ID = "L103", Pattern = @"[0-9]{0,21}", Opcional = true)]
        [CampoValidavel(2, Opcional = true)]
        public string CodigoCODIF
        {
            get => _codif;
	        set => _codif = ValidationUtil.ValidateRange(SerializationUtil.ToToken(value), 0, 21, "CodigoCODIF");
        }

        /// <summary>
        /// [qTemp] Retorna ou define a Quantidade de Combustível Faturada à Temperatura Ambiente.
        /// </summary>
        /// <remarks>
        /// Informar apenas quando o valor do campo Quantidade da classe Produto Informar quando a
        /// quantidade faturada informada no campo Quantidade, da classe Produto, tiver sido ajustada
        /// para uma temperatura diferente da ambiente.
        /// </remarks>
        [NFeField(FieldName = "qTemp", DataType = "TDec_1204Opc", ID = "L104",
            Pattern =
                @"0\.[1-9]{1}[0-9]{3}|0\.[0-9]{3}[1-9]{1}|0\.[0-9]{2}[1-9]{1}[0-9]{1}|0\.[0-9]{1}[1-9]{1}[0-9]{2}|[1-9]{1}[0-9]{0,11}(\.[0-9]{4})?"
            )]
        [CampoValidavel(3, Opcional = true)]
        public decimal QuantidadeCombustivelFaturadaTempAmbiente
        {
            get => _quantidadeCombustivelTempAmbiente;
	        set
            {
                if (value == 0m)
                    _quantidadeCombustivelTempAmbiente = value;
                else
                    _quantidadeCombustivelTempAmbiente = ValidationUtil.ValidateTDec_1204(value,
                        "QuantidadeCombustivelFaturadaTempAmbiente");
            }
        }

        /// <summary>
        /// [UFCons] Retorna ou define a Sigla da UF de Destino
        /// </summary>
        [NFeField(ID = "L120", FieldName = "UFCons", DataType = "TUf")]
        [CampoValidavel(3, ChaveErroValidacao.CampoNaoPreenchido, ValorNaoPreenchido = SiglaUF.NaoEspecificado)]
        public SiglaUF UFConsumo
        {
            get => _UFConsumo;
	        set
            {
                ValidationUtil.ValidateEnum(value, "UFConsumo");
                _UFConsumo = value;
            }
        }

        /// <summary>
        /// [CIDE] Retorna as informações de CIDE do combustível. Opcional.
        /// </summary>
        [NFeField(FieldName = "CIDE", ID = "L105", Opcional = true)]
        [CampoValidavel(4, Opcional = true)]
        public CideCombustivel CIDE { get; } = new CideCombustivel();

        /// <summary>
        /// Retorna a referência para o objeto Produto no qual o Combustivel se refere.
        /// </summary>
        internal Produto Produto { get; }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => CodigoProdutoANP != 0 ||
                                  !string.IsNullOrEmpty(CodigoCODIF) ||
                                  QuantidadeCombustivelFaturadaTempAmbiente != 0m ||
                                  CIDE.Modificado ||
                                  UFConsumo != SiglaUF.NaoEspecificado;
    }
}