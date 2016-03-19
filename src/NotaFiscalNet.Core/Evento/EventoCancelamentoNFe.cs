using System.Xml;

namespace NotaFiscalNet.Core.Evento
{
    /// <summary>
    /// Detalhamento de Evento de NFe para Cancelamento.
    /// </summary>
    public class EventoCancelamentoNFe : EventoNFe
    {
        /// <summary>
        /// [tpEvento] Retorna o tipo de evento referente ao Cancelamento de NF-e.
        /// </summary>
        public override TipoEventoNFe Tipo => TipoEventoNFe.Cancelamento;

        /// <summary>
        /// [descEvento ]Retorna a descrição para o evento de Cancelamento da NF-e.
        /// </summary>
        public const string Descricao = "Cancelamento";

        /// <summary>
        /// [nProt] Retorna ou define o Número do Protocolo de Status da NF-e.
        /// </summary>
        public string NumeroProtocolo { get; set; }

        /// <summary>
        /// [xJust] Retorna ou define a justificativa do cancelamento da NF-e.
        /// </summary>
        public string Justificativa { get; set; }

        protected override void SerializeDetalheEvento(XmlWriter writer)
        {
            writer.WriteStartElement("detEvento");
            writer.WriteAttributeString("versao", Versao);

            writer.WriteElementString("descEvento", Descricao);
            writer.WriteElementString("nProt", NumeroProtocolo);
            writer.WriteElementString("xJust", Justificativa);

            writer.WriteEndElement();
        }
    }
}