using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    public sealed class ReferenciaDocumentoFiscalEcf : ISerializavel, IReferenciaDocumentoFiscal
    {
        /// <summary>
        /// [mod] Retorna o Código do Modelo do Documento Fiscal Referênciado. Preencher com "2B",
        /// quando se tratar de Cupom Fiscal emitido por máquina registradora (não ECF), com "2C",
        /// quando se tratar de Cupom Fiscal PDV, ou "2D", quando se tratar de Cupom Fiscal (emitido
        /// por ECF)
        /// </summary>
        public string CodigoModelo { get; set; }

        /// <summary>
	    /// [nECF] Retorna ou define o número de ordem seqüencial do ECF que emitiu o Cupom Fiscal
        /// vinculado à NF-e.
        /// </summary>
        public int NumeroEcf { get; set; }

        /// <summary>
        /// [nCOO] Retorna ou define o Número do Contador de Ordem de Operação - COO vinculado à NF-e.
        /// </summary>
        public int NumeroContadorOrdemOperacao { get; set; }

        public void Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("refECF");
            writer.WriteElementString("mod", CodigoModelo);
            writer.WriteElementString("nECF", NumeroEcf.ToString());
            writer.WriteElementString("nCOO", NumeroContadorOrdemOperacao.ToString());
            writer.WriteEndElement();
        }
    }
}