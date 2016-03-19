using NotaFiscalNet.Core.Interfaces;
using System;
using System.ComponentModel;
using System.Linq;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma lista de referências à Documentos Fiscais.
    /// </summary>
    public sealed class ReferenciaDocumentoFiscalCollection : BaseCollection<IReferenciaDocumentoFiscal>, ISerializavel, IModificavel
    {
        private const int Capacidade = 500;

        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool Modificado => this.Any(item => item.Modificado);

        public void Serializar(XmlWriter writer, INFe nfe)
        {
            if (!Modificado)
                return;

            writer.WriteStartElement("NFref");

            foreach (var referencia in this.Where(referencia => referencia.Modificado))
            {
                ((ISerializavel)referencia).Serializar(writer, nfe);
            }

            writer.WriteEndElement();
        }

        protected override void PreAdd(CancelEventArgs e, IReferenciaDocumentoFiscal item)
        {
            if (Count == Capacidade)
                throw new ApplicationException($"A capacidade máxima deste campo é de {Capacidade} item(ns).");

            base.PreAdd(e, item);
        }
    }
}