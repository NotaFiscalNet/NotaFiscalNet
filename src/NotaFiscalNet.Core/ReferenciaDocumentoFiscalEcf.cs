using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    public sealed class ReferenciaDocumentoFiscalEcf : ISerializavel, IReferenciaDocumentoFiscal
    {
        /// <summary>
        /// [mod] Retorna o C�digo do Modelo do Documento Fiscal Refer�nciado. Preencher com "2B",
        /// quando se tratar de Cupom Fiscal emitido por m�quina registradora (n�o ECF), com "2C",
        /// quando se tratar de Cupom Fiscal PDV, ou "2D", quando se tratar de Cupom Fiscal (emitido
        /// por ECF)
        /// </summary>
        public string CodigoModelo { get; set; }

        /// <summary>
        /// [nECF] Retorna ou define o n�mero de ordem seq�encial do ECF que emitiu o Cupom Fiscal
        /// vinculado � NF-e.
        /// </summary>
        public int NumeroEcf { get; set; }

        /// <summary>
        /// [nCOO] Retorna ou define o N�mero do Contador de Ordem de Opera��o - COO vinculado � NF-e.
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