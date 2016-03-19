using NotaFiscalNet.Core.Interfaces;
using System;
using System.ComponentModel;

namespace NotaFiscalNet.Core
{
    public sealed class PagamentoCollection : BaseCollection<Pagamento>, ISerializavel
    {
        private const int Capacidade = 100;

        protected override void PreAdd(CancelEventArgs e, Pagamento item)
        {
            if (Count == Capacidade)
                throw new ApplicationException($"A capacidade máxima deste campo é de {Capacidade} pagamentos.");

            base.PreAdd(e, item);
        }

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            foreach (var pagamento in this)
            {
                if (!pagamento.Modificado)
                    continue;

                ISerializavel obj = pagamento;
                obj.Serializar(writer, nfe);
            }
        }
    }
}