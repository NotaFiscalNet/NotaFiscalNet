using NotaFiscalNet.Core.Utils;
using System;
using System.Xml.Linq;

namespace NotaFiscalNet.Core.Evento
{
    public class EventoNFeFactory
    {
        /// <summary>
        /// Fabrica uma instância do Evento NFe com base no xml do tipo complexto 'TEvento'.
        /// </summary>
        /// <param name="eventoEl">Representa o tipo complexto 'TEVento'.</param>
        /// <returns></returns>
        public EventoNFe Create(XElement eventoEl)
        {
            if (eventoEl == null)
                throw new ArgumentNullException(nameof(eventoEl));

            var ns = Constants.XNamespacePortalFiscalNFe;

            var infEventoEl = eventoEl.Element(ns + "infEvento");
            if (infEventoEl == null || infEventoEl.Name != (ns + "infEvento"))
                throw new Exception("O xml informado não é um TEvento válido.");

            var tpEvento = infEventoEl.Element(ns + "tpEvento").Value;

            EventoNFe evento = null;

            switch (tpEvento)
            {
                case "110111": // Cancelamento
                    evento = CreateEventoCancelamento(infEventoEl);
                    break;
            }

            return evento;
        }

        private EventoNFe CreateEventoCancelamento(XElement infEventoEl)
        {
            var evento = new EventoCancelamentoNFe();

            PreencherEventoBase(infEventoEl, evento);

            var detEventoEl = infEventoEl.Element(Constants.XNamespacePortalFiscalNFe + "detEvento");

            detEventoEl.NFElementAsString("nProt", value => evento.NumeroProtocolo = value);
            detEventoEl.NFElementAsString("xJust", value => evento.Justificativa = value);

            return evento;
        }

        private void PreencherEventoBase(XElement infEventoEl, EventoNFe evento)
        {
            evento.Id = infEventoEl.Attribute("Id").Value;
            infEventoEl.NFElementAsEnum<OrgaoIBGE>("cOrgao", value => evento.Orgao = value);
            infEventoEl.NFElementAsEnum<TipoAmbiente>("tpAmb", value => evento.Ambiente = value);
            infEventoEl.NFElementAsString("CNPJ", value => evento.CpfCnpjAutor = value);
            infEventoEl.NFElementAsString("CPF", value => evento.CpfCnpjAutor = value);
            infEventoEl.NFElementAsString("chNFe", value => evento.ChaveAcessoNFe = value);
            infEventoEl.NFElementAsDateTime("dhEvento", value => evento.Data = value);
            // tpEvento não é necessário por estar fixo na classe
            infEventoEl.NFElementAsInt32("nSeqEvento", value => evento.NumeroSequencial = value);
            infEventoEl.NFElementAsString("verEvento", value => evento.VersaoEvento = value);
        }
    }
}