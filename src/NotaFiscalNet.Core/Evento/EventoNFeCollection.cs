using System;
using System.ComponentModel;

namespace NotaFiscalNet.Core.Evento
{
    public class EventoNFeCollection<TEventoNFe> : BaseCollection<TEventoNFe>
        where TEventoNFe : EventoNFe
    {
        private const int MaxItensPorEnvioEvento = 20;

        protected override void PreAdd(CancelEventArgs e, TEventoNFe item)
        {
            if (Count >= MaxItensPorEnvioEvento)
                throw new ApplicationException(String.Format("A quantdade máxima de itens no envio de evento é {0}.", MaxItensPorEnvioEvento));
        }
    }
}