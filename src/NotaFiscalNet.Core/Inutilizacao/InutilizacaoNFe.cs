using NotaFiscalNet.Core.Utils;
using System;
using System.IO;
using System.Text;
using System.Xml;

namespace NotaFiscalNet.Core.Inutilizacao
{
    public class InutilizacaoNFe
    {
        private int _ano;

        /// <summary>
        /// [versao]
        /// </summary>
        [NFeField(FieldName = "versao", DataType = "TVerInutNFe")]
        public string Versao => Constants.VersaoLeiaute;

        /// <summary>
        /// [Id] Retorna o ID da Inutilização.
        /// </summary>
        [NFeField(FieldName = "Id")]
        public string Id => $"ID{(int)UF}{Ano.ToString("D2")}{Cnpj}{(int)CodigoModeloDocFiscal}{Serie.ToString("D3")}{NumeracaoInicial.ToString("D9")}{NumeracaoFinal.ToString("D9")}";

        /// <summary>
        /// [tpAmb] Retorna ou define o Ambiente do Documento Fiscal.
        /// </summary>
        [NFeField(FieldName = "tpAmb", DataType = "TAmb")]
        public TipoAmbiente Ambiente { get; set; }

        /// <summary>
        /// [xServ] Retorna a descrição do serviço.
        /// </summary>
        [NFeField(FieldName = "xServ")]
        public string Servico => "INUTILIZAR";

        /// <summary>
        /// [cUF] Retorna ou define o Código da UF do emitente.
        /// </summary>
        [NFeField(FieldName = "cUF", DataType = "TCodUfIBGE")]
        public UfIBGE UF { get; set; }

        /// <summary>
        /// [ano] Retorna ou define o ano de inutilização da numeração.
        /// </summary>
        [NFeField(FieldName = "ano", DataType = "Tano")]
        public int Ano
        {
            get => _ano;
	        set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Ano", value, "Ano deve ser maior ou igual a 00.");

                if (value > 99)
                    throw new ArgumentOutOfRangeException("Ano", value, "Ano deve ser menor ou igual a 99.");

                _ano = value;
            }
        }

        /// <summary>
        /// [Cnpj] Retorna ou define o ano de inutilização da numeração.
        /// </summary>
        [NFeField(FieldName = "Cnpj", DataType = "TCnpj")]
        public string Cnpj { get; set; }

        /// <summary>
        /// [mod] Retorna ou define o modelo da NF-e (55, 65, etc).
        /// </summary>
        [NFeField(FieldName = "mod", DataType = "TMod")]
        public TipoModalidadeDocumentoFiscal CodigoModeloDocFiscal { get; set; }

        /// <summary>
        /// [serie] Retorna ou define a Série da NF-e.
        /// </summary>
        [NFeField(FieldName = "serie", DataType = "TSerie")]
        public int Serie { get; set; }

        /// <summary>
        /// [nNFIni] Retorna ou define o Número da NF-e inicial.
        /// </summary>
        [NFeField(FieldName = "nNFIni", DataType = "TNF")]
        public int NumeracaoInicial { get; set; }

        /// <summary>
        /// [nNFFin] Retorna ou define o Número da NF-e final.
        /// </summary>
        [NFeField(FieldName = "nNFFin", DataType = "TNF")]
        public int NumeracaoFinal { get; set; }

        /// <summary>
        /// [xJust] Retorna ou define a Justificativa do pedido de inutilização.
        /// </summary>
        [NFeField(FieldName = "xJust", DataType = "TJust")]
        public string Justificativa { get; set; }

        public void Serialize(XmlWriter writer)
        {
            writer.WriteStartElement("inutNFe", Constants.NamespacePortalFiscalNFe);
            writer.WriteAttributeString("versao", Constants.VersaoLeiaute);

            writer.WriteStartElement("infInut");
            writer.WriteAttributeString("Id", Id);
            writer.WriteElementString("tpAmb", Ambiente.GetEnumValue());
            writer.WriteElementString("xServ", Servico);
            writer.WriteElementString("cUF", UF.GetEnumValue());
            writer.WriteElementString("ano", Ano.ToString("D2"));
            writer.WriteElementString("Cnpj", Cnpj);
            writer.WriteElementString("mod", CodigoModeloDocFiscal.GetEnumValue());
            writer.WriteElementString("serie", Serie.ToString());
            writer.WriteElementString("nNFIni", NumeracaoInicial.ToString());
            writer.WriteElementString("nNFFin", NumeracaoFinal.ToString());
            writer.WriteElementString("xJust", SerializationUtil.ToTString(Justificativa, 255));

            writer.WriteEndElement(); // infInut
            writer.WriteEndElement(); // inutNFe
        }

        public string GerarXmlNaoAssinado()
        {
            return GerarXmlNaoAssinado(true);
        }

        private string GerarXmlNaoAssinado(bool validarXml)
        {
            var settings = new XmlWriterSettings { Encoding = new UTF8Encoding(false), OmitXmlDeclaration = true };

            string xml;

            using (var sw = new StringWriter())
            using (var writer = XmlWriter.Create(sw, settings))
            {
                Serialize(writer);
                writer.Flush();
                xml = sw.ToString();
            }

            return xml;
        }
    }
}