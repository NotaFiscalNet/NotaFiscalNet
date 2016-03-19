using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Validacao;
using System;

namespace NotaFiscalNet.Core
{
    public sealed class ReferenciaDocumentoFiscalCte : ISerializavel, IReferenciaDocumentoFiscal
    {
        /// <summary>
        /// Retorna ou define a referência de um CT-e emitido anteriormente vinculada com esta NFe.
        /// </summary>
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public string ReferenciaCte { get; set; }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => !string.IsNullOrWhiteSpace(ReferenciaCte);

        public void Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteElementString("refCTe", ReferenciaCte);
        }
    }
}