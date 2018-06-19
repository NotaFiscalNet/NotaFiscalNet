using NotaFiscalNet.Core.Interfaces;
using System;
using System.ComponentModel;
using System.Xml;

namespace NotaFiscalNet.Core
{
    public sealed class DetalheExportacaoCollection : BaseCollection<DetalheExportacao>, ISerializavel
    {
        private const int CAPACIDADE = 500;

        public void Serializar(XmlWriter writer, INFe nfe)
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
                throw new ApplicationException($"A capacidade máxima deste campo é de {CAPACIDADE} item(ns).");

            base.PreAdd(e, item);
        }
    }
}