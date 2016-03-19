using System;
using System.Xml.Linq;

namespace NotaFiscalNet.Core
{
    public static class Constants
    {
        /// <summary>
        /// Versão do Leiaute NF-e.
        /// </summary>
        public const string VersaoLeiaute = "3.10";

        /// <summary>
        /// Versão do leiaute do Lote NF-e.
        /// </summary>
        public const string VersaoLeiauteLote = "3.10";

        /// <summary>
        /// Versão do leiaute da Consulta de Lote.
        /// </summary>
        public const string VersaoLeiauteConsultaLote = "2.00";

        /// <summary>
        /// Versão do leiaute de Cancelamento.
        /// </summary>
        public const string VersaoLeiauteCancelamento = "2.00";

        /// <summary>
        /// Versão do leiaute de Inutilização
        /// </summary>
        public const string VersaoLeiauteInutilizacao = "2.00";

        /// <summary>
        /// Versão do leiaute de Inutilização
        /// </summary>
        public const string VersaoLeiauteInutilizacaoNFCe = "3.10";

        /// <summary>
        /// Versão do leiaute de Eventos.
        /// </summary>
        public const string VersaoLeiauteEventos = "1.00";

        /// <summary>
        /// Namespace padrão para extrair recursos armazenados no assembly.
        /// </summary>
        public const string DefaultResourceNamespace = "MaxNFe";

        /// <summary>
        /// Código do Modelo do Documento Fiscal para uma Nota Fiscal Eletrônica
        /// </summary>
        public const string CodigoModeloDocFiscalNFe = "55";

        /// <summary>
        /// Código do Modelo do Documento Fiscal para uma Nota Fiscal de Consumidor Eletrônica
        /// </summary>
        public const string CodigoModeloDocFiscalNFCe = "65";

        /// <summary>
        /// Xml namespace padrão para NF-e (http://www.portalfiscal.inf.br/nfe).
        /// </summary>
        public const string NamespacePortalFiscalNFe = "http://www.portalfiscal.inf.br/nfe";

        /// <summary>
        /// Retorna o nome do arquivo xsd utilizado para validar o xml do arquivo de licença.
        /// </summary>
        public const string InfomaxXmlSchemaFilename = "license.xsd";

        public const string InfomaxXmlSchemaBasicoFilename = "basicos.xsd";
        public const string InformaxXmlSchemaXmldsigFilename = "xmldsig.xsd";

        /// <summary>
        /// Namespace do padrão xmldsig#.
        /// </summary>
        public const string NamespaceXmldsig = "http://www.w3.org/2000/09/xmldsig#";

        public const string NamespaceSchemaXsi = "http://www.w3.org/2001/XMLSchema-instance";

        public const string DeclaracaoXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

        /// <summary>
        /// Quantidade máxima de itens em um Lote de Notas Fiscais Eletrônicas.
        /// </summary>
        public const int MaxLoteItens = 50;

        /// <summary>
        /// Tamanho máximo xml web services.
        /// </summary>
        public const int MaxXmlLength = 512000; // 500 KB

        public const int SignatureAvgLength = 3072; // 3KB

        /// <summary>
        /// Tempo de espera (em milisegundos) para resposta do web service da Sefaz (1 minuto).
        /// </summary>
        [Obsolete("Usar 'MaxNFe.Processor.Configuration.AppSettingsHelper.NFeProcessorSettings.TimeoutWsAutorizacao'")]
        public const int RequestTimeoutRetornoSefaz = 60000; // 1 Minuto

        /// <summary>
        /// Tempo de espera (em milisegundos) para resposta do web service da Sefaz (15 segundos).
        /// </summary>
        [Obsolete(
            "Usar 'MaxNFe.Processor.Configuration.AppSettingsHelper.NFeProcessorSettings.TimeoutWsConsultaSituacao'")]
        public const int ConsultaTimeoutRetornoSefaz = 15000; // 15 Segundos

        public static readonly XNamespace XNamespacePortalFiscalNFe = NamespacePortalFiscalNFe;

        /// <summary>
        /// Namespace do padrão xmldsig# como objeto XNamespace.
        /// </summary>
        public static readonly XNamespace XNamespaceXmldsig = NamespaceXmldsig;
    }
}