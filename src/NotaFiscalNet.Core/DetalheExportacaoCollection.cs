using System;
using System.ComponentModel;
using System.Xml;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    public sealed class DetalheExportacaoCollection : BaseCollection<DetalheExportacao>, ISerializavel
    {
        private const int CAPACIDADE = 500;

        void ISerializavel.Serializar(XmlWriter writer, NFe nfe)
        {
            foreach (var item in this)
            {
                ISerializavel obj = item;
                obj.Serializar(writer, nfe);
            }
        }

        protected override void PreAdd(CancelEventArgs e, DetalheExportacao item)
        {
            if (Count == CAPACIDADE)
                throw new ApplicationException(string.Format("A capacidade máxima deste campo é de {0} item(ns).",
                    CAPACIDADE));

            base.PreAdd(e, item);
        }
    }
}