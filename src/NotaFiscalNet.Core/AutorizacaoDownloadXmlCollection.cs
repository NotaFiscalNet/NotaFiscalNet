using System;
using System.ComponentModel;
using System.Xml;

namespace NotaFiscalNet.Core
{
    public sealed class AutorizacaoDownloadXmlCollection : BaseCollection<AutorizacaoDownloadXml>, INFeSerializable
    {
        private const int CAPACIDADE = 10;

        void INFeSerializable.Serialize(XmlWriter writer, NFe nfe)
        {
            foreach (var autorizacao in this)
            {
                INFeSerializable obj = autorizacao;
                obj.Serialize(writer, nfe);
            }
        }

        protected override void PreAdd(CancelEventArgs e, AutorizacaoDownloadXml item)
        {
            if (Count == CAPACIDADE)
                throw new ApplicationException(
                    string.Format("A capacidade máxima deste campo é de {0} autorização/autorizações.", CAPACIDADE));

            base.PreAdd(e, item);
        }

        private void Add(AutorizacaoDownloadXml item)
        {
            Add(item);
        }

        private void Clear()
        {
            Clear();
        }

        private bool Contains(AutorizacaoDownloadXml item)
        {
            return Contains(item);
        }

        private void Remove(AutorizacaoDownloadXml item)
        {
            Remove(item);
        }

        private int Count
        {
            get { return Count; }
        }
    }
}