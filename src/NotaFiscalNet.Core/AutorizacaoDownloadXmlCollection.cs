using System;
using System.ComponentModel;
using System.Xml;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    public sealed class AutorizacaoDownloadXmlCollection : BaseCollection<AutorizacaoDownloadXml>, ISerializavel
    {
        private const int CAPACIDADE = 10;

        void ISerializavel.Serializar(XmlWriter writer, NFe nfe)
        {
            foreach (var autorizacao in this)
            {
                ISerializavel obj = autorizacao;
                obj.Serializar(writer, nfe);
            }
        }

        protected override void PreAdd(CancelEventArgs e, AutorizacaoDownloadXml item)
        {
            if (Count == CAPACIDADE)
                throw new ApplicationException(
                    $"A capacidade máxima deste campo é de {CAPACIDADE} autorização/autorizações.");

            base.PreAdd(e, item);
        }
    }
}