using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    public sealed class ReferenciaDocumentoFiscalCte : ISerializavel, IReferenciaDocumentoFiscal
    {
        /// <summary>
        /// Retorna ou define a referência de um CT-e emitido anteriormente vinculada com esta NFe.
        /// </summary>
        public string ReferenciaCte { get; set; }

        public void Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteElementString("refCTe", ReferenciaCte);
        }
    }
}