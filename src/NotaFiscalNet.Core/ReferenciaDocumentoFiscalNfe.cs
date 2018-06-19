using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    public sealed class ReferenciaDocumentoFiscalNfe : ISerializavel, IReferenciaDocumentoFiscal
    {
        /// <summary>
        /// Retorna ou define a Chave de Acesso da Nota Fiscal Eletrônica referenciada (emitida
        /// anteriormente, vinculada a atual NF-e).
        /// </summary>
        /// <remarks>
        /// Este campo deve ser preenchido apenas caso o Documento Fiscal referenciado seja uma Nota
        /// Fiscal Eletrônica. Esta informação será utilizada nas hipóteses previstas na legislação
        /// (ex.: Devolução de Mercadorias, Substituição de NF cancelada, Complementação de NF, etc).
        /// </remarks>
        public string ChaveAcessoNFe { get; set; }

        public void Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteElementString("refNFe", ChaveAcessoNFe);
        }
    }
}