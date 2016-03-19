using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using NotaFiscalNet.Core.Validacao.Validators;
using System;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Responsável por armazenar as informações de Indentificação de um Documento Fiscal.
    /// </summary>
    public sealed class IdentificacaoDocumentoFiscal : ISerializavel
    {
        private UfIBGE _unidadeFederativaEmitente = UfIBGE.NaoEspecificado;
        private int _codigoNumerico = new Random().Next(10000000, 99999999);
        private IndicadorFormaPagamento _formaPagamento = IndicadorFormaPagamento.AVista;
        private string _naturezaOperacao;
        private TipoModalidadeDocumentoFiscal _modalidadeDocumentoFiscal = TipoModalidadeDocumentoFiscal.Nfe;
        private int _serie;
        private int _numeroDocumentoFiscal;
        private DateTime _dataEmissao;
        private DateTime? _dataEntradaSaida;
        private TipoNotaFiscal _tipoNotaFiscal = TipoNotaFiscal.Saida;
        private TipoIdentificadorLocalDestinoOperacao _identificadorLocalDestinoOperacao;
        private int _codigoMunicipioFatoGerador;
        private TipoFormatoImpressaoDanfe _tipoImpressa = TipoFormatoImpressaoDanfe.Retrato;
        private TipoEmissaoNFe _tipoEmissaoNFe = TipoEmissaoNFe.Normal;
        private TipoAmbiente _tipoAmbiente = TipoAmbiente.Producao;
        private TipoFinalidade _finalidade = TipoFinalidade.Normal;
        private TipoProcessoEmissaoNFe _tipoProcessoEmissao = TipoProcessoEmissaoNFe.AplicativoContribuinte;
        private string _versaoAplicativoEmissao;
        private TipoIndicadorPresencaComprador _indicadorPresencaComprador;
        private string _justificativaEntradaContingencia;
        private DateTime _dataHoraEntradaContingencia = DateTime.MinValue;

        /// <summary>
        /// [cUF] Retorna ou define o código Unidade Federativa (IBGE) do Emitente do Documento Fiscal.
        /// </summary>
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido, DefaultValue = UfIBGE.NaoEspecificado)]
        public UfIBGE UnidadeFederativaEmitente
        {
            get { return _unidadeFederativaEmitente; }
            set { _unidadeFederativaEmitente = ValidationUtil.ValidateEnum(value, "UnidadeFederativaEmitente"); }
        }

        /// <summary>
        /// [cNF] Retorna o código numérico que compõe a Chave de Acesso. Número aleatório gerado
        /// pelo emitente para cada NF-e para evitar acessos indevidos da NF-e.
        /// </summary>
        [ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public int CodigoNumerico
        {
            get { return _codigoNumerico; }
            set { _codigoNumerico = ValidationUtil.ValidateRange(value, 10000000, 99999999, "CodigoNumerico"); }
        }

        /// <summary>
        /// [natOp] Retorna ou define a descrição da Natureza da Operação.
        /// </summary>
        [ValidateField(3, ChaveErroValidacao.CampoNaoPreenchido)]
        public string NaturezaOperacao
        {
            get { return _naturezaOperacao; }
            set { _naturezaOperacao = ValidationUtil.ValidateTString(value, 1, 60); }
        }

        /// <summary>
        /// [indPag] Retorna ou define o indicador da Forma de Pagamento. Valor padrão 'AVista'.
        /// </summary>
        [ValidateField(4, true)]
        public IndicadorFormaPagamento FormaPagamento
        {
            get { return _formaPagamento; }
            set { _formaPagamento = ValidationUtil.ValidateEnum(value, "FormaPagamento"); }
        }

        /// <summary>
        /// [mod] Código do modelo do Documento Fiscal. 55 = NF-e; 65 = NFC-e.
        /// Valor padrão: NF-e
        /// </summary>
        [ValidateField(5, ChaveErroValidacao.CampoNaoPreenchido)]
        public TipoModalidadeDocumentoFiscal ModalidadeDocumentoFiscal
        {
            get { return _modalidadeDocumentoFiscal; }
            set
            {
                ValidationUtil.ValidateEnum(value, "CodigoModeloDocumentoFiscal");
                _modalidadeDocumentoFiscal = value;
            }
        }

        /// <summary>
        /// [serie] Retorna ou define a Série do Documento Fiscal. 
        /// Série Normal - 0-889
        /// Avulsa Fisco - 890-899
        /// SCAN - 900-999
        /// </summary>
        [ValidateField(6, ChaveErroValidacao.CampoNaoPreenchido)]
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
        [ValidateField(7, ChaveErroValidacao.CampoNaoPreenchido)]
        public int NumeroDocumentoFiscal
        {
            get { return _numeroDocumentoFiscal; }
            set
            {
                ValidationUtil.ValidateTNF(value, "NumeroDocumentoFiscal");
                _numeroDocumentoFiscal = value;
            }
        }

        /// <summary>
        /// [dhEmi] Retorna ou define a Data e Hora de emissão do Documento Fiscal
        /// </summary>
        [ValidateField(8, ChaveErroValidacao.CampoNaoPreenchido)]
        public DateTime DataEmissao
        {
            get { return _dataEmissao; }
            set
            {
                _dataEmissao = ValidationUtil.ValidateTDateTimeUtc(value, "DataEmissao");
            }
        }

        /// <summary>
        /// [dhSaiEnt] Retorna ou define a Data de Entrada/Saída da Mercadoria/Produto. Opcional.
        /// </summary>
        [ValidateField(9, true)]
        public DateTime? DataEntradaSaida
        {
            get { return _dataEntradaSaida; }
            set
            {
                if (value.HasValue)
                    ValidationUtil.ValidateTDateTimeUtc(value.Value, "DataEntradaSaida");
                _dataEntradaSaida = value;
            }
        }

        /// <summary>
        /// [tpNF] Retorna ou define o Tipo da Nota Fiscal. Valor padrão 'Saida'.
        /// </summary>
        [ValidateField(10, true)]
        public TipoNotaFiscal TipoNotaFiscal
        {
            get { return _tipoNotaFiscal; }
            set { _tipoNotaFiscal = ValidationUtil.ValidateEnum(value, "TipoNotaFiscal"); }
        }

        /// <summary>
        /// [idDest] Retorna ou define o Identificador de Local de destino da operação.
        /// </summary>
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
        /// [tpImp] Retorna ou define o formato de impressão do DANFE. 
        /// Valor padrão 'Retrato'.
        /// </summary>
        [ValidateField(14, true)]
        public TipoFormatoImpressaoDanfe TipoImpressao
        {
            get { return _tipoImpressa; }
            set { _tipoImpressa = ValidationUtil.ValidateEnum(value, "TipoImpressao"); }
        }

        /// <summary>
        /// [tpEmis] Retorna ou define o Tipo de Emissão da Nota Fiscal Eletrônica. Valor padrão 'Normal'.
        /// </summary>
        [ValidateField(15, true)]
        public TipoEmissaoNFe TipoEmissao
        {
            get { return _tipoEmissaoNFe; }
            set { _tipoEmissaoNFe = ValidationUtil.ValidateEnum(value, "TipoEmissao"); }
        }

        /// <summary>
        /// [tpAmb] Retorna ou define o valor indicando o ambiente do Documento Fiscal. 
        /// Valor padrão 'Producao'.
        /// </summary>
        [ValidateField(16, true)]
        public TipoAmbiente Ambiente
        {
            get { return _tipoAmbiente; }
            set { _tipoAmbiente = ValidationUtil.ValidateEnum(value, "Ambiente"); }
        }

        /// <summary>
        /// [finNFe] Retorna ou define a Finalidade do Documento Fiscal. Valor padrão 'Normal'.
        /// </summary>
        [ValidateField(17, true)]
        public TipoFinalidade Finalidade
        {
            get { return _finalidade; }
            set { _finalidade = ValidationUtil.ValidateEnum(value, "Finalidade"); }
        }

        /// <summary>
        /// [indFinal] Retorna ou define o valor indicando se a operação foi realizada com consumidor final.
        /// </summary>
        [ValidateField(19, true)]
        public bool OperacaoConsumidorFinal { get; set; }

        /// <summary>
        /// [indPres] Retorna ou define o indicador de presença do comprador no estabelecimento
        /// comercial no momento da operação.
        /// </summary>
        [ValidateField(19, true)]
        public TipoIndicadorPresencaComprador IndicadorPresencaComprador
        {
            get { return _indicadorPresencaComprador; }
            set { _indicadorPresencaComprador = ValidationUtil.ValidateEnum(value, "IndicadorPresencaComprador"); }
        }

        /// <summary>
        /// [procEmi] Retorna ou define o Tipo de processo de emissão da NF-e. Valor padrão 'AplicativoContribuinte'.
        /// </summary>
        [ValidateField(20, true)]
        public TipoProcessoEmissaoNFe TipoProcessoEmissao
        {
            get { return _tipoProcessoEmissao; }
            set
            {
                _tipoProcessoEmissao = ValidationUtil.ValidateEnum(value, "TipoProcessoEmissao");
            }
        }

        /// <summary>
        /// [verProc] Retorna ou define a Versão do Aplicativo de Emissão da NF-e.
        /// </summary>
        [ValidateField(21, ChaveErroValidacao.CampoNaoPreenchido)]
        public string VersaoAplicativoEmissao
        {
            get { return _versaoAplicativoEmissao; }
            set { _versaoAplicativoEmissao = ValidationUtil.ValidateTString(value, 1, 20); }
        }

        /// <summary>
        /// [dhCont] Retorna ou define a Data e Hora de entrada em contigência. Apenas se o tipo de
        /// emissão for diferente de normal.
        /// </summary>
        [ValidateField(22, ChaveErroValidacao.CampoNaoPreenchido, Validator = typeof (EntradaContingenciaValidator))]
        public DateTime DataHoraEntradaContingencia
        {
            get { return _dataHoraEntradaContingencia; }
            set { _dataHoraEntradaContingencia = ValidationUtil.ValidateTDateTimeUtc(value, "DataHoraEntradaContingencia"); }
        }

        /// <summary>
        /// [xJust] Retorna ou define a Justificativa para entrada em contingência. Apenas se o tipo
        /// de emissão for diferente de normal.
        /// </summary>
        [ValidateField(23, ChaveErroValidacao.CampoNaoPreenchido, Validator = typeof (EntradaContingenciaValidator))]
        public string JustificativaEntradaContingencia
        {
            get { return _justificativaEntradaContingencia; }
            set { _justificativaEntradaContingencia = ValidationUtil.ValidateTString(value, 15, 256); }
        }

        /// <summary>
        /// [NFref] Retorna a lista de referências à Documentos Fiscais vinculadas ao Documento
        /// Fiscal atual.
        /// </summary>
        /// <remarks>
        /// Esta informação é utilizada nas hipóteses previstas na legislação (Devolução de
        /// Mercadorias, Substituição de NF cancelada, Complementação de NF, etc).
        /// </remarks>
        [ValidateField(13, ChaveErroValidacao.CollectionMinValue, Validator = typeof(RangeCollectionValidator),
            MinLength = 0, MaxLength = 500)]
        public ReferenciaDocumentoFiscalCollection ReferenciasDocumentoFiscais { get; } = new ReferenciaDocumentoFiscalCollection();

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ide");

            writer.WriteElementString("cUF", UnidadeFederativaEmitente.GetEnumValue());
            writer.WriteElementString("cNF", CodigoNumerico.ToString("00000000"));
            writer.WriteElementString("natOp", NaturezaOperacao.ToToken());
            writer.WriteElementString("indPag", FormaPagamento.GetEnumValue());
            writer.WriteElementString("mod", ModalidadeDocumentoFiscal.GetEnumValue());
            writer.WriteElementString("serie", Serie.ToString());
            writer.WriteElementString("nNF", NumeroDocumentoFiscal.ToString());
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
            writer.WriteElementString("cDV", nfe.DigitoVerificadorChaveAcesso.ToString());
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

            if (ReferenciasDocumentoFiscais.Modificado)
                ReferenciasDocumentoFiscais.Serializar(writer, nfe);

            writer.WriteEndElement();
        }
    }
}