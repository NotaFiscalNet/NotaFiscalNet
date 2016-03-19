using NotaFiscalNet.Core.Interfaces;
using System;
using System.ComponentModel;
using System.Xml;

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
                throw new ApplicationException($"A capacidade m�xima deste campo � de {CAPACIDADE} item(ns).");

            base.PreAdd(e, item);
        }
    }
}