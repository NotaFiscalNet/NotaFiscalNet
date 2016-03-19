using System;
using System.ComponentModel;
using System.Xml;

namespace NotaFiscalNet.Core
{
    public sealed class DetalheExportacaoCollection : BaseCollection<DetalheExportacao>, INFeSerializable
    {
        private const int CAPACIDADE = 500;

        void INFeSerializable.Serialize(XmlWriter writer, NFe nfe)
        {
            foreach (var item in this)
            {
                INFeSerializable obj = item;
                obj.Serialize(writer, nfe);
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