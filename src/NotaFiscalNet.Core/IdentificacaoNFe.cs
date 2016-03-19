using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using NotaFiscalNet.Core.Validacao.Validators;
using System;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Responsável por armazenar as informações de Indentificação de uma Nota Fiscal Eletrônica.
    /// </summary>

    public sealed class IdentificacaoNFe : ISerializavel
    {
        private UfIBGE _ufEnvio = UfIBGE.NaoEspecificado;
        private int _codigoNF = new Random().Next(10000000, 99999999);
        private IndicadorFormaPagamento _formaPagamento = IndicadorFormaPagamento.AVista;
        private string _naturezaOperacao = string.Empty;
        private TipoModalidadeDocumentoFiscal _codigoModeloDocFiscal = TipoModalidadeDocumentoFiscal.NFe;
        private int _serie;
        private int _numeroNF;
        private DateTime _dataEmissao;
        private DateTime? _dataEntradaSaida;
        private TipoNotaFiscal _tipoNotaFiscal = TipoNotaFiscal.Saida;
        private TipoIdentificadorLocalDestinoOperacao _identificadorLocalDestinoOperacao;
        private int _codigoMunicipioFatoGerador;
        private TipoFormatoImpressaoDANFE _tipoImpressa = TipoFormatoImpressaoDANFE.Retrato;
        private TipoEmissaoNFe _tipoEmissaoNFe = TipoEmissaoNFe.Normal;
        private TipoAmbiente _tipoAmbiente = TipoAmbiente.Producao;
        private TipoFinalidade _finalidade = TipoFinalidade.Normal;
        private TipoProcessoEmissaoNFe _processoEmissao = TipoProcessoEmissaoNFe.AplicativoContribuinte;
        private string _versaoAplicativoEmissao = string.Empty;
        private ReferenciaDocFiscalCollection _nfRef = new ReferenciaDocFiscalCollection();

        public IdentificacaoNFe()
        {
            JustificativaEntradaContingencia = string.Empty;
            DataHoraEntradaContingencia = DateTime.MinValue;
        }

        /// <summary>
        /// [cUF] Retorna ou define o código UF(IBGE) do Emitente do Documento Fiscal.
        /// </summary>
        [NFeField(FieldName = "cUF", DataType = "TCodUfIBGE", ID = "B02")]
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido, DefaultValue = UfIBGE.NaoEspecificado)]
        public UfIBGE UFEnvio
        {
            get { return _ufEnvio; }
            set { _ufEnvio = ValidationUtil.ValidateEnum(value, "UfIBGE"); }
        }

        /// <summary>
        /// [cNF] Retorna o código numérico que compõe a Chave de Acesso. Número aleatório gerado
        /// pelo emitente para cada NF-e para evitar acessos indevidos da NF-e.
        /// </summary>
        [NFeField(FieldName = "cNF", DataType = "string", ID = "B03", Pattern = "[0-9]{8}")]
        [ValidateField(2, true)]
        public int CodigoNF
        {
            get { return _codigoNF; }
            set { _codigoNF = ValidationUtil.ValidateRange(value, 0, 99999999, "CodigoNF"); }
        }

        /// <summary>
        /// [natOp] Retorna ou define a descrição da Natureza da Operação.
        /// </summary>
        [NFeField(FieldName = "natOp", DataType = "TString", ID = "B04", MinLength = 1, MaxLength = 60)]
        [ValidateField(3, ChaveErroValidacao.CampoNaoPreenchido)]
        public string NaturezaOperacao
        {
            get { return _naturezaOperacao; }
            set { _naturezaOperacao = ValidationUtil.ValidateTString(value, 1, 60); }
        }

        /// <summary>
        /// [indPag] Retorna ou define o indicador da Natureza da Operação. Valor padrão 'AVista'.
        /// </summary>
        [NFeField(FieldName = "indPag", DataType = "string", ID = "B05")]
        [ValidateField(4, true)]
        public IndicadorFormaPagamento FormaPagamento
        {
            get { return _formaPagamento; }
            set { _formaPagamento = ValidationUtil.ValidateEnum<IndicadorFormaPagamento>(value, "FormaPagamento"); }
        }

        /// <summary>
        /// [mod] Retorna o Código do Modelo de Documento Fiscal. O valor padrão é 55, para
        /// identificação da NF-e, emitida em substituição ao modelo 1 e 1A.
        /// </summary>
        [NFeField(FieldName = "mod", DataType = "TMod", ID = "B06")]
        [ValidateField(5, true)]
        public TipoModalidadeDocumentoFiscal CodigoModeloDocFiscal
        {
            get { return _codigoModeloDocFiscal; }
            set
            {
                ValidationUtil.ValidateEnum(value, "CodigoModeloDocFiscal");
                _codigoModeloDocFiscal = value;
            }
        }

        /// <summary>
        /// [serie] Retorna ou define a Série do Documento Fiscal. Série Normal 0-889 - Avulsa Fisco
        /// - 890-899 - SCAN 900-999
        /// </summary>
        [NFeField(FieldName = "serie", DataType = "TSerie", ID = "B07", Pattern = "0|[1-9]{1}[0-9]{0,2}")]
        [ValidateField(6, true)]
        public int Serie
        {
            get { return _serie; }
            set
            {
                ValidationUtil.ValidateTSerie(value, "Serie");
                _serie = value;
            }
        }

        /// <summary>
        /// [nNF] Retorna ou define o Número da Nota Fiscal Eletrônica.
        /// </summary>
        [NFeField(FieldName = "nNF", DataType = "TNF", ID = "B08", Pattern = "[1-9]{1}[0-9]{0,8}")]
        [ValidateField(7, ChaveErroValidacao.CampoNaoPreenchido)]
        public int NumeroNF
        {
            get { return _numeroNF; }
            set
            {
                ValidationUtil.ValidateTNF(value, "NumeroNF");
                _numeroNF = value;
            }
        }

        /// <summary>
        /// [dhEmi] Retorna ou define a Data de Emissão da Nota Fiscal Eletrônica.
        /// </summary>
        [NFeField(FieldName = "dhEmi", DataType = "TDateTimeUTC", ID = "B09",
            Pattern =
                @"(((20(([02468][048])|([13579][26]))-02-29))|(20[0-9][0-9])-((((0[1-9])|(1[0-2]))-((0[1-9])|(1\d)|(2[0-8])))|((((0[13578])|(1[02]))-31)|(((0[1,3-9])|(1[0-2]))-(29|30)))))T(20|21|22|23|[0-1]\d):[0-5]\d:[0-5]\d-0[1-4]:00"
            )]
        [ValidateField(8, ChaveErroValidacao.CampoNaoPreenchido)]
        public DateTime DataEmissao
        {
            get { return _dataEmissao; }
            set
            {
                ValidationUtil.ValidateTDateTimeUtc(value, "DataEmissao");
                _dataEmissao = value;
            }
        }

        /// <summary>
        /// [dhSaiEnt] Retorna ou define a Data de Entrada/Saída da Mercadoria/Produto. Opcional.
        /// </summary>
        [NFeField(FieldName = "dhSaiEnt", DataType = "TDateTimeUTC", ID = "B10a",
            Pattern =
                @"(((20(([02468][048])|([13579][26]))-02-29))|(20[0-9][0-9])-((((0[1-9])|(1[0-2]))-((0[1-9])|(1\d)|(2[0-8])))|((((0[13578])|(1[02]))-31)|(((0[1,3-9])|(1[0-2]))-(29|30)))))T(20|21|22|23|[0-1]\d):[0-5]\d:[0-5]\d-0[1-4]:00"
            )]
        [ValidateField(9, true)]
        public DateTime? DataEntradaSaida
        {
            get { return _dataEntradaSaida; }
            set { _dataEntradaSaida = value; }
        }

        /// <summary>
        /// [tpNF] Retorna ou define o Tipo da Nota Fiscal. Valor padrão 'Saida'.
        /// </summary>
        [NFeField(FieldName = "tpNF", DataType = "string", ID = "B11")]
        [ValidateField(10, true)]
        public TipoNotaFiscal TipoNotaFiscal
        {
            get { return _tipoNotaFiscal; }
            set { _tipoNotaFiscal = ValidationUtil.ValidateEnum<TipoNotaFiscal>(value, "TipoNotaFiscal"); }
        }

        /// <summary>
        /// [idDest] Retorna ou define o Identificador de Local de destino da operação.
        /// </summary>
        [NFeField(ID = "B11a", FieldName = "idDest", DataType = "string")]
        [ValidateField(11, ChaveErroValidacao.CampoNaoPreenchido)]
        public TipoIdentificadorLocalDestinoOperacao IdentificadorLocalDestinoOperacao
        {
            get { return _identificadorLocalDestinoOperacao; }
            set
            {
                _identificadorLocalDestinoOperacao = ValidationUtil.ValidateEnum(value,
                    "IdentificadorLocalDestinoOperacao");
            }
        }

        /// <summary>
        /// [cMunFG] Retorna ou define o Código do Município de Ocorrência do Fator Gerador.
        /// </summary>
        [NFeField(FieldName = "cMunFG", DataType = "TCodMunIBGE", ID = "B12", Pattern = "[0-9]{7}")]
        [ValidateField(12, ChaveErroValidacao.CampoNaoPreenchido)]
        public int CodigoMunicipioFatoGerador
        {
            get { return _codigoMunicipioFatoGerador; }
            set
            {
                ValidationUtil.ValidateTCodMunIBGE(value, "CodigoMunicipioFatorGerador");
                _codigoMunicipioFatoGerador = value;
            }
        }

        /// <summary>
        /// [NFref] Retorna a lista de referências à Documentos Fiscais vinculadas ao Documento
        /// Fiscal atual.
        /// </summary>
        /// <remarks>
        /// Esta informação é utilizada nas hipóteses previstas na legislação (Devolução de
        /// Mercadorias, Substituição de NF cancelada, Complementação de NF, etc).
        /// </remarks>
        [NFeField(FieldName = "NFref", ID = "B12a")]
        [ValidateField(13, ChaveErroValidacao.CollectionMinValue, Validator = typeof (RangeCollectionValidator),
            MinLength = 0)]
        public ReferenciaDocFiscalCollection ReferenciasDocFiscais
        {
            get { return _nfRef; }
        }

        /// <summary>
        /// [tpImp] Retorna ou define o formato de impressão do DANFE. Valor padrão 'Retrato'.
        /// </summary>
        [NFeField(FieldName = "tpImp", DataType = "string", ID = "B21")]
        [ValidateField(14, true)]
        public TipoFormatoImpressaoDANFE TipoImpressao
        {
            get { return _tipoImpressa; }
            set { _tipoImpressa = ValidationUtil.ValidateEnum(value, "TipoImpressao"); }
        }

        /// <summary>
        /// [tpEmis] Retorna ou define o Tipo de Emissão da Nota Fiscal Eletrônica. Valor padrão 'Normal'.
        /// </summary>
        [NFeField(FieldName = "tpEmis", DataType = "string", ID = "B22")]
        [ValidateField(15, true)]
        public TipoEmissaoNFe TipoEmissao
        {
            get { return _tipoEmissaoNFe; }
            set { _tipoEmissaoNFe = ValidationUtil.ValidateEnum<TipoEmissaoNFe>(value, "TipoEmissao"); }
        }

        /// <summary>
        /// [tpAmb] Retorna ou define o valor indicando o ambiente em que a NF-e está rodando. Valor
        /// padrão 'Producao'.
        /// </summary>
        [NFeField(FieldName = "tpAmb", DataType = "TAmb", ID = "B24")]
        [ValidateField(16, true)]
        public TipoAmbiente Ambiente
        {
            get { return _tipoAmbiente; }
            set { _tipoAmbiente = ValidationUtil.ValidateEnum(value, "Ambiente"); }
        }

        /// <summary>
        /// [finNFe] Retorna ou define a Finalidade da NF-e. Valor padrão 'Normal'.
        /// </summary>
        [NFeField(FieldName = "finNFe", DataType = "TFinNFe", ID = "B25")]
        [ValidateField(17, true)]
        public TipoFinalidade Finalidade
        {
            get { return _finalidade; }
            set { _finalidade = ValidationUtil.ValidateEnum(value, "Finalidade"); }
        }

        /// <summary>
        /// [indFinal] Retorna ou define o valor indicando se a operação foi realizada com consumidor
        /// final (False = Não, True = Consumidor Final).
        /// </summary>
        [NFeField(ID = "B25a", FieldName = "indFinal", DataType = "string"), ValidateField(18, true)]
        public bool OperacaoConsumidorFinal { get; set; }

        /// <summary>
        /// [indPres] Retorna ou define o indicador de presença do comprador no estabelecimento
        /// comercial no momento da operação.
        /// </summary>
        [NFeField(ID = "B25b", FieldName = "indPres", DataType = "string"),
         ValidateField(19, ChaveErroValidacao.CampoNaoPreenchido)]
        public TipoIndicadorPresencaComprador IndicadorPresencaComprador { get; set; }

        /// <summary>
        /// [procEmi] Retorna ou define o Tipo de processo de emissão da NF-e. Valor padrão 'AplicativoContribuinte'.
        /// </summary>
        [NFeField(FieldName = "procEmi", DataType = "TProcEmi", ID = "B26")]
        [ValidateField(20, true)]
        public TipoProcessoEmissaoNFe TipoProcessoEmissao
        {
            get { return _processoEmissao; }
            set
            {
                _processoEmissao = ValidationUtil.ValidateEnum<TipoProcessoEmissaoNFe>(value, "TipoProcessoEmissao");
            }
        }

        /// <summary>
        /// [verProc] Retorna ou define a Versão do Aplicativo de Emissão da NF-e.
        /// </summary>
        [NFeField(FieldName = "verProc", DataType = "TString", ID = "B27", MinLength = 1, MaxLength = 20)]
        [ValidateField(21, ChaveErroValidacao.CampoNaoPreenchido)]
        public string VersaoAplicativoEmissao
        {
            get { return _versaoAplicativoEmissao; }
            set { _versaoAplicativoEmissao = ValidationUtil.TruncateString(value, 20); }
        }

        /// <summary>
        /// [dhCont] Retorna ou define a Data e Hora de entrada em contigência. Apenas se o tipo de
        /// emissão for diferente de normal.
        /// </summary>
        [NFeField(FieldName = "dhCont", DataType = "TDateTimeUTC", ID = "B28",
            Pattern =
                @"(((20(([02468][048])|([13579][26]))-02-29))|(20[0-9][0-9])-((((0[1-9])|(1[0-2]))-((0[1-9])|(1\d)|(2[0-8])))|((((0[13578])|(1[02]))-31)|(((0[1,3-9])|(1[0-2]))-(29|30)))))T(20|21|22|23|[0-1]\d):[0-5]\d:[0-5]\d"
            ),
         ValidateField(22, ChaveErroValidacao.CampoNaoPreenchido, Validator = typeof (EntradaContingenciaValidator))]
        public DateTime DataHoraEntradaContingencia { get; set; }

        /// <summary>
        /// [xJust] Retorna ou define a Justificativa para entrada em contingência. Apenas se o tipo
        /// de emissão for diferente de normal.
        /// </summary>
        [NFeField(FieldName = "xJust", ID = "B29"),
         ValidateField(23, ChaveErroValidacao.CampoNaoPreenchido, Validator = typeof (EntradaContingenciaValidator))]
        public string JustificativaEntradaContingencia { get; set; }

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("ide"); // Elemento 'ide'

            writer.WriteElementString("cUF", UFEnvio.GetEnumValue());
            writer.WriteElementString("cNF", CodigoNF.ToString("00000000"));
            writer.WriteElementString("natOp", NaturezaOperacao.ToToken());
            writer.WriteElementString("indPag", FormaPagamento.GetEnumValue());
            writer.WriteElementString("mod", CodigoModeloDocFiscal.GetEnumValue());
            writer.WriteElementString("serie", Serie.ToString());
            writer.WriteElementString("nNF", NumeroNF.ToString());
            writer.WriteElementString("dhEmi", DataEmissao.ToTDateTimeUtc());

            if (DataEntradaSaida.HasValue)
            {
                writer.WriteElementString("dhSaiEnt", DataEntradaSaida.Value.ToTDateTimeUtc());
            }
            writer.WriteElementString("tpNF", TipoNotaFiscal.GetEnumValue());
            writer.WriteElementString("idDest", IdentificadorLocalDestinoOperacao.GetEnumValue());
            writer.WriteElementString("cMunFG", CodigoMunicipioFatoGerador.ToString7());
            writer.WriteElementString("tpImp", TipoImpressao.GetEnumValue());
            writer.WriteElementString("tpEmis", TipoEmissao.GetEnumValue());
            writer.WriteElementString("cDV", nfe.ChaveAcessoDV.ToString());
            writer.WriteElementString("tpAmb", Ambiente.GetEnumValue());
            writer.WriteElementString("finNFe", Finalidade.GetEnumValue());
            writer.WriteElementString("indFinal", OperacaoConsumidorFinal ? "1" : "0");
            writer.WriteElementString("indPres", IndicadorPresencaComprador.GetEnumValue());
            writer.WriteElementString("procEmi", TipoProcessoEmissao.GetEnumValue());
            writer.WriteElementString("verProc", VersaoAplicativoEmissao.ToToken(20));

            if (TipoEmissao != TipoEmissaoNFe.Normal)
            {
                writer.WriteElementString("dhCont", DataHoraEntradaContingencia.ToTDateTimeUtc());
                writer.WriteElementString("xJust", JustificativaEntradaContingencia);
            }

            // Serializa as referência
            if (ReferenciasDocFiscais.Modificado)
                ((ISerializavel) ReferenciasDocFiscais).Serializar(writer, nfe);

            writer.WriteEndElement(); // fim do elemento 'ide'
        }
    }
}