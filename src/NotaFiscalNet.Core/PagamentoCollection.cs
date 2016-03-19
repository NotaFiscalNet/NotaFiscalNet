using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    
    
    
    public sealed class PagamentoCollection : BaseCollection<Pagamento>,  INFeSerializable
    {
        private const int Capacidade = 100;

        protected override void PreAdd(CancelEventArgs e, Pagamento item)
        {
            if (Count == Capacidade)
                throw new ApplicationException(string.Format("A capacidade máxima deste campo é de {0} pagamentos.", Capacidade));

            base.PreAdd(e, item);
        }

        #region INFeSerializable implementation

        void INFeSerializable.Serialize(System.Xml.XmlWriter writer, NFe nfe)
        {
            foreach (var pagamento in this)
            {
                if (!pagamento.IsDirty)
                    continue;

                INFeSerializable obj = pagamento;
                obj.Serialize(writer, nfe);
            }
        }

        #endregion

      
    }
}