using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Validacao;
using System;

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
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public string ChaveAcessoNFe { get; set; }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => !string.IsNullOrEmpty(ChaveAcessoNFe);

        public void Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteElementString("refNFe", ChaveAcessoNFe);
        }
    }
}