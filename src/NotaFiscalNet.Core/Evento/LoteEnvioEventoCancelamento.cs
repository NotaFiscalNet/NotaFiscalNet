using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace NotaFiscalNet.Core.Evento
{
    /// <summary>
    /// Representa um lote de envio de eventos de cancelamento de NF-e's.
    /// </summary>
    public class LoteEnvioEventoCancelamento
    {
        public const string Versao = "1.00";

        /// <summary>
        /// Inicializa uma nova instância do lote de envio de eventos de cancelamento.
        /// </summary>
        public LoteEnvioEventoCancelamento()
        {
            Eventos = new EventoNFeCollection<EventoCancelamentoNFe>();
            IdLote = DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        /// <summary>
        /// Retorna ou define o Identificador do Lote.
        /// </summary>
        /// <remarks>
        /// Identificador de controle do Lote de envio do Evento. Número sequencial autoincremental
        /// único para identificação do Lote. A responsabilidade de gerar e controlar é exclusiva do
        /// autor do evento. O Web Service não faz qualquer uso deste identificador.
        /// </remarks>
        public string IdLote { get; set; }

        /// <summary>
        /// Retorna a lista de eventos de cancelamento.
        /// </summary>
        public ICollection<EventoCancelamentoNFe> Eventos { get; private set; }

        /// <summary>
        /// Gera o xml não assinado do lote de envio de eventos de cancelemtndo de NF-e.
        /// </summary>
        /// <returns></returns>
        public string GerarXmlNaoAssinado()
        {
            ValidarEvento();

            var settings = new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(),
                OmitXmlDeclaration = true
            };

            string result;

            using (var ms = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(ms, settings))
                    GerarXmlNaoAssinado(writer);

                result = Encoding.UTF8.GetString(ms.ToArray());
            }

            return result;
        }

        private void GerarXmlNaoAssinado(XmlWriter writer)
        {
            writer.WriteStartElement("envEvento", Constants.NamespacePortalFiscalNFe);
            writer.WriteAttributeString("versao", Versao);

            writer.WriteElementString("idLote", IdLote);

            foreach (var evento in Eventos)
                evento.Serialize(writer);

            writer.WriteEndElement(); // tag: envEvento
        }

        private void ValidarEvento()
        {
            if (Eventos.Count == 0)
                throw new ApplicationException("O Lote de Eventos de Cancelamento está vazio.");

            foreach (var evento in Eventos)
                evento.Validar();
        }
    }
}