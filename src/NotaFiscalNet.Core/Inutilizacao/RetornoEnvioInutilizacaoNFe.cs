using NotaFiscalNet.Core.Utils;
using System;
using System.Xml.Linq;

namespace NotaFiscalNet.Core.Inutilizacao
{
    /// <summary>
    /// Representa o retorna do envio do pedido de Inutilização de Faixa de Numeração de NF-e.
    /// </summary>
    public class RetornoEnvioInutilizacaoNFe
    {
        private RetornoEnvioInutilizacaoNFe()
        {
        }

        /// <summary>
        /// [versao]
        /// </summary>
        [NFeField(FieldName = "versao", DataType = "TVerInutNFe")]
        public string Versao { get; private set; }

        /// <summary>
        /// [Id] Retorna o ID da Inutilização.
        /// </summary>
        [NFeField(FieldName = "Id")]
        public string Id { get; private set; }

        /// <summary>
        /// [tpAmb] Retorna ou define o Ambiente do Documento Fiscal.
        /// </summary>
        [NFeField(FieldName = "tpAmb", DataType = "TAmb")]
        public TipoAmbiente Ambiente { get; set; }

        /// <summary>
        /// [verAplic] Retorna a versão do Aplicativo que processou a NF-e.
        /// </summary>
        [NFeField(FieldName = "verAplic", DataType = "TVerAplic")]
        public string VersaoAplicativo { get; private set; }

        /// <summary>
        /// [cStat] Retorna o Código do Status da mensagem enviada.
        /// </summary>
        [NFeField(FieldName = "cStat", DataType = "TStat")]
        public string CodigoStatus { get; private set; }

        /// <summary>
        /// [xMotivo] Retorna a descrição literal do status do serviço solicitado.
        /// </summary>
        [NFeField(FieldName = "xMotivo", DataType = "TMotivo")]
        public string Motivo { get; private set; }

        /// <summary>
        /// [cUF] Retorna o código da UF que atendeu a solicitação.
        /// </summary>
        [NFeField(FieldName = "cUF", DataType = "TCodUfIBGE")]
        public UfIBGE UF { get; private set; }

        /// <summary>
        /// [ano] Retorna o Ano de inutilização da numeração.
        /// </summary>
        [NFeField(FieldName = "ano", DataType = "Tano")]
        public int? Ano { get; private set; }

        /// <summary>
        /// [Cnpj] Retorna o Cnpj do emitente.
        /// </summary>
        [NFeField(FieldName = "Cnpj", DataType = "TCnpj")]
        public string Cnpj { get; private set; }

        /// <summary>
        /// [mod] Retorna o modelo da NF-e (55, 65, etc).
        /// </summary>
        [NFeField(FieldName = "mod", DataType = "TMod")]
        public TipoModalidadeDocumentoFiscal? CodigoModeloDocFiscal { get; private set; }

        /// <summary>
        /// [serie] Retorna a Série da NF-e.
        /// </summary>
        [NFeField(FieldName = "serie", DataType = "TSerie")]
        public int? Serie { get; private set; }

        /// <summary>
        /// [nNFIni] Retorna o Número da NF-e inicial.
        /// </summary>
        [NFeField(FieldName = "nNFIni", DataType = "TNF")]
        public int? NumeracaoInicial { get; set; }

        /// <summary>
        /// [nNFFin] Retorna o Número da NF-e final.
        /// </summary>
        [NFeField(FieldName = "nNFFin", DataType = "TNF")]
        public int? NumeracaoFinal { get; private set; }

        /// <summary>
        /// [dhRecbto] Retorna o Número da NF-e final.
        /// </summary>
        [NFeField(FieldName = "dhRecbto", DataType = "TDateTimeUTC")]
        public DateTime DataRecebimento { get; private set; }

        /// <summary>
        /// [nProt] Retorna o Número do Protocolo de Status da NF-e.
        /// </summary>
        [NFeField(FieldName = "nProt", DataType = "TProt")]
        public string NumeroProtocolo { get; set; }

        /// <summary>
        /// Retorna o xml completo utilizado no Parsing.
        /// </summary>
        public string RawXml { get; private set; }

        public static RetornoEnvioInutilizacaoNFe Parse(string text)
        {
            var xdoc = XDocument.Parse(text);
            return ReadFrom(xdoc);
        }

        public static RetornoEnvioInutilizacaoNFe ReadFrom(XDocument doc)
        {
            var ns = Constants.XNamespacePortalFiscalNFe;
            var ret = new RetornoEnvioInutilizacaoNFe();

            var retInutNFeEl = doc.Root; // retInutNFe

            retInutNFeEl.NFAttributeAsString("versao", value => ret.Versao = value);

            var infInutEl = retInutNFeEl.Element(ns + "infInut");

            infInutEl.NFAttributeAsString("Id", value => ret.Id = value);
            infInutEl.NFElementAsEnum<TipoAmbiente>("tpAmb", value => ret.Ambiente = value);
            infInutEl.NFElementAsString("verAplic", value => ret.VersaoAplicativo = value);
            infInutEl.NFElementAsString("cStat", value => ret.CodigoStatus = value);
            infInutEl.NFElementAsString("xMotivo", value => ret.Motivo = value);
            infInutEl.NFElementAsEnum<UfIBGE>("cUF", value => ret.UF = value);

            infInutEl.NFElementAsInt32("ano", value => ret.Ano = value);
            infInutEl.NFElementAsString("Cnpj", value => ret.Cnpj = value);
            infInutEl.NFElementAsEnum<TipoModalidadeDocumentoFiscal>("mod", value => ret.CodigoModeloDocFiscal = value);
            infInutEl.NFElementAsInt32("serie", value => ret.Serie = value);

            infInutEl.NFElementAsInt32("nNFIni", value => ret.NumeracaoInicial = value);
            infInutEl.NFElementAsInt32("nNFFin", value => ret.NumeracaoFinal = value);

            infInutEl.NFElementAsDateTime("dhRecbto", value => ret.DataRecebimento = value);
            infInutEl.NFElementAsString("nProt", value => ret.NumeroProtocolo = value);

            ret.RawXml = doc.ToString(SaveOptions.DisableFormatting);

            return ret;
        }
    }
}