using NotaFiscalNet.Core.Interfaces;
using System;
using System.ComponentModel;
using System.Xml;

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
                    $"A capacidade m�xima deste campo � de {CAPACIDADE} autoriza��o/autoriza��es.");

            base.PreAdd(e, item);
        }
    }
}