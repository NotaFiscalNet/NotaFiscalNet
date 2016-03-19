using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using NotaFiscalNet.Core.Validacao.Validators;
using System;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Declaração de Importação do Produto
    /// </summary>
    public sealed class DeclaracaoImportacao : ISerializavel, IModificavel
    {
        private SiglaUF _ufTerceiro;

        /// <summary>
        /// Inicializa uma nova instância da classe DeclaracaoImportacao
        /// </summary>
        public DeclaracaoImportacao()
        {
            CodigoExportador = string.Empty;
            LocalDesembaracoAduaneiro = string.Empty;
            Numero = string.Empty;
            UFDesembaracoAduaneiro = SiglaUF.NaoEspecificado;
            UFTerceiro = SiglaUF.NaoEspecificado;

            Adicoes = new DeclaracaoImportacaoAdicaoCollection();
        }

        public void Serializar(XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("DI");

            writer.WriteStartElement("nID", SerializationUtil.ToToken(Numero, 10));
            writer.WriteStartElement("dDI", SerializationUtil.ToTData(DataRegistro));
            writer.WriteStartElement("xLocDesemb", SerializationUtil.ToToken(LocalDesembaracoAduaneiro, 60));
            writer.WriteStartElement("UFDesemb", UFDesembaracoAduaneiro.ToString());
            writer.WriteStartElement("dDesemb", SerializationUtil.ToTData(DataDesembaracoAduaneiro));
            writer.WriteStartElement("tpViaTransp", TipoViaTransporte.GetEnumValue());

            if (ValorAFRMM.HasValue)
                writer.WriteStartElement("vAFRMM", SerializationUtil.ToTDec_1302(ValorAFRMM.Value));

            writer.WriteStartElement("tpIntermedio", TipoIntermedio.GetEnumValue());

            if (!string.IsNullOrEmpty(CNPJ))
                writer.WriteStartElement("Cnpj", CNPJ);

            if (UFTerceiro.IsDefined())
                writer.WriteStartElement("UFTerceiro", UFTerceiro.GetEnumValue());

            writer.WriteStartElement("cExportador", SerializationUtil.ToToken(CodigoExportador, 60));

            /// renderiza os elementos 'adi'
            ((ISerializavel)Adicoes).Serializar(writer, nfe);

            writer.WriteEndElement(); // fim do elemento 'DI'
        }

        /// <summary>
        /// [nDI] Retorna ou define o Número da Declaração de Importação DI/DSI/DA
        /// </summary>
        [NFeField(ID = "I19", FieldName = "nDI", DataType = "token", MinLength = 1, MaxLength = 10),
         ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public string Numero { get; set; }

        /// <summary>
        /// [dDI] Retorna ou define a Data do Registro da DI/DSI/DA
        /// </summary>
        /// <remarks>Formato AAAA-MM-DD</remarks>
        [NFeField(ID = "I20", FieldName = "dDI", DataType = "TData", Pattern = @"\d{4}-\d{2}-\d{2}"),
         ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public DateTime DataRegistro { get; set; }

        /// <summary>
        /// [xLocDesemb] Retorna ou define o Local de Desembaraço Aduaneiro.
        /// </summary>
        [NFeField(ID = "I21", FieldName = "xLocDesemb", DataType = "token", MinLength = 1, MaxLength = 60),
         ValidateField(3, ChaveErroValidacao.CampoNaoPreenchido)]
        public string LocalDesembaracoAduaneiro { get; set; }

        /// <summary>
        /// [UFDesemb] Retorna ou define a Sigla da UF do Local de Desembaraço Aduaneiro.
        /// </summary>
        [NFeField(ID = "I22", FieldName = "UFDesemb"),
         ValidateField(4, ChaveErroValidacao.CampoNaoPreenchido, DefaultValue = SiglaUF.NaoEspecificado)]
        public SiglaUF UFDesembaracoAduaneiro { get; set; }

        /// <summary>
        /// [dDesemb] Retorna ou define a Data do Desembaraço Aduaneiro.
        /// </summary>
        /// <remarks>Formato AAAA-MM-DD</remarks>
        [NFeField(ID = "I23", FieldName = "dDesemb", DataType = "TData"),
         ValidateField(5, ChaveErroValidacao.CampoNaoPreenchido)]
        public DateTime DataDesembaracoAduaneiro { get; set; }

        /// <summary>
        /// [tpViaTransp] Retorna ou define o Tipo da Via Internacional declarada na Declaração de Importação.
        /// </summary>
        [NFeField(ID = "I23a", FieldName = "tpViaTransp"), ValidateField(6, ChaveErroValidacao.CampoNaoPreenchido)]
        public TipoViaTransporteInternacional TipoViaTransporte { get; set; }

        /// <summary>
        /// [vAFRMM] Retorna ou define o Valor Adicional ao Frete para Renovação da Marinha Mercante.
        /// </summary>
        [NFeField(ID = "I23b", FieldName = "vAFRMM"), ValidateField(7, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal? ValorAFRMM { get; set; }

        /// <summary>
        /// [tpIntermedio] Retorna ou define a Forma de importação quanto a intermediação.
        /// </summary>
        [NFeField(ID = "I23c", FieldName = "tpIntermedio"), ValidateField(8, ChaveErroValidacao.CampoNaoPreenchido)]
        public TipoIntermedioImportacao TipoIntermedio { get; set; }

        /// <summary>
        /// [Cnpj] Retorna ou define o Cnpj do adquirinte ou do encomendante.
        /// </summary>
        /// <remarks>
        /// Informação obrigatória no caso de importação por conta e ordem ou por encomenda. Informar
        /// os zeros não significativos.
        /// </remarks>
        [NFeField(ID = "I23d", FieldName = "Cnpj", DataType = "TCnpj"),
         ValidateField(9, ChaveErroValidacao.CampoNaoPreenchido)]
        public string CNPJ { get; set; }

        /// <summary>
        /// [UFTerceiro] Retorna ou define a sigla UF do adquirinte ou do encomendante.
        /// </summary>
        [NFeField(ID = "I23e", FieldName = "UFTerceiro", DataType = "TUfEmi"),
         ValidateField(10, ChaveErroValidacao.CampoNaoPreenchido)]
        public SiglaUF UFTerceiro
        {
            get { return _ufTerceiro; }
            set
            {
                if (value == SiglaUF.EX)
                    throw new ApplicationException("O valor informado ('EX') não é permitido neste contexto.");
                _ufTerceiro = value;
            }
        }

        /// <summary>
        /// [cExportador] Retorna ou define o Código do Exportador, usado nos sistemas internos de
        /// informação do emitente da NFe
        /// </summary>
        [NFeField(ID = "I24", FieldName = "cExportador", DataType = "token"),
         ValidateField(11, ChaveErroValidacao.CampoNaoPreenchido)]
        public string CodigoExportador { get; set; }

        /// <summary>
        /// [adi] Retorna as Adições na Declaração de Importação
        /// </summary>
        [NFeField(ID = "I25", FieldName = "adi")]
        [ValidateField(12, ChaveErroValidacao.CampoNaoPreenchido, Validator = typeof(RangeCollectionValidator),
            MinLength = 1)]
        public DeclaracaoImportacaoAdicaoCollection Adicoes { get; }

        /// <summary>
        /// Retorna ou define a referência para o objeto Produto este objeto está contido.
        /// </summary>
        /// <remarks>O valor deste campo é definido ao ser adicionado na coleção.</remarks>
        internal Produto Produto { get; set; }

        /// <summary>
        /// Retorna o valor indicando se a Nota Fiscal Eletrônica está em modo somente-leitura.
        /// </summary>
        /// <remarks>
        /// A Nota Fiscal Eletrônica estará em modo somente-leitura quando for instanciada a partir
        /// de um arquivo assinado digitalmente.
        /// </remarks>
        public bool IsReadOnly { get; private set; }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => !string.IsNullOrEmpty(Numero) ||
                                  DataRegistro != DateTime.MinValue ||
                                  !string.IsNullOrEmpty(LocalDesembaracoAduaneiro) ||
                                  UFDesembaracoAduaneiro != SiglaUF.NaoEspecificado ||
                                  DataDesembaracoAduaneiro != DateTime.MinValue ||
                                  TipoViaTransporte.IsDefined() ||
                                  ValorAFRMM.HasValue ||
                                  TipoIntermedio.IsDefined() ||
                                  !string.IsNullOrEmpty(CNPJ) ||
                                  UFTerceiro != SiglaUF.NaoEspecificado ||
                                  !string.IsNullOrEmpty(CodigoExportador) ||
                                  Adicoes.Modificado;
    }
}