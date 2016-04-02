using NotaFiscalNet.Core.Interfaces;
using System;
using System.ComponentModel;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma coleção de autorizações para acesso ao xml do documento fiscal
    /// </summary>
    /// <remarks>
    /// Essa classe é usada internamente pela dll para controlar a capacidade do nó serializável
    /// </remarks>
    public sealed class AutorizacaoDownloadXmlCollection : BaseCollection<AutorizacaoDownloadXml>, ISerializavel
    {
        private const int CAPACIDADE = 10;

        /// <summary>
        /// Serializa a coleção
        /// </summary>
        /// <param name="writer">Escritor</param>
        /// <param name="nfe">Documento Fiscal</param>
        public void Serializar(XmlWriter writer, INFe nfe)
        {
            foreach (var autorizacao in this)
            {
                ISerializavel obj = autorizacao;
                obj.Serializar(writer, nfe);
            }
        }

        /// <summary>
        /// Executa antes de adicionar a coleção
        /// </summary>
        /// <param name="e">Evento</param>
        /// <param name="item">Autorização</param>
        protected override void PreAdd(CancelEventArgs e, AutorizacaoDownloadXml item)
        {
            if (Count == CAPACIDADE)
                throw new ApplicationException(
                    $"A capacidade máxima deste campo é de {CAPACIDADE} autorização/autorizações.");

            base.PreAdd(e, item);
        }
    }
}