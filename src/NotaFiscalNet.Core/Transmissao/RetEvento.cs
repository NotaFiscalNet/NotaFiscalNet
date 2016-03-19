using NotaFiscalNet.Core.Evento;
using NotaFiscalNet.Core.Utils;
using System;
using System.Xml.Linq;

namespace NotaFiscalNet.Core.Transmissao
{
    /// <summary>
    /// Representa o tipo complexo 'TRetEvento'.
    /// </summary>
    public class RetEvento
    {
        internal RetEvento(XElement retEventoEl)
        {
            Versao = retEventoEl.Attribute("versao").Value;

            var infEventoEl = retEventoEl.Element(Constants.XNamespacePortalFiscalNFe + "infEvento");

            var idAtt = infEventoEl.Attribute("Id");
            if (idAtt != null)
                Id = infEventoEl.Attribute("Id").Value;
            infEventoEl.NFElementAsEnum<TipoAmbiente>("tpAmb", value => Ambiente = value);
            infEventoEl.NFElementAsString("verAplic", value => VersaoAplicativo = value);
            infEventoEl.NFElementAsEnum<OrgaoIBGE>("cOrgao", value => Orgao = value);
            infEventoEl.NFElementAsString("cStat", value => CodigoStatus = value);
            infEventoEl.NFElementAsString("xMotivo", value => Motivo = value);
            infEventoEl.NFElementAsString("chNFe", value => ChaveAcessoNFe = value);
            infEventoEl.NFElementAsEnum<TipoEventoNFe>("tpEvento", value => Tipo = value);
            infEventoEl.NFElementAsString("xEvento", value => Descricao = value);
            infEventoEl.NFElementAsInt32("nSeqEvento", value => NumeroSequencial = value);

            infEventoEl.NFElementAsString("CNPJDest", value => CpfCnpjDestinatario = value);
            infEventoEl.NFElementAsString("CPFDest", value => CpfCnpjDestinatario = value);

            infEventoEl.NFElementAsString("emailDest", value => EmailDestinatario = value);
            infEventoEl.NFElementAsDateTime("dhRegEvento", value => DataRegistro = value);
            infEventoEl.NFElementAsString("nProt", value => NumeroProtocolo = value);
        }

        /// <summary>
        /// [@versao] Retorna a versão do retorno de evento.
        /// </summary>
        public string Versao { get; private set; }

        /// <summary>
        /// [@Id] Retorna o Identificador do retorno de evento.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// [tpAmb] Retorna ou define o Ambiente (Produção ou Homologação).
        /// </summary>
        public TipoAmbiente Ambiente { get; private set; }

        /// <summary>
        /// [verAplic] Retorna ou define a versão do aplicativo que recebeu o Evento.
        /// </summary>
        public string VersaoAplicativo { get; private set; }

        /// <summary>
        /// [cOrgao] Retorna ou define o Orgão de Recepção do Evento.
        /// </summary>
        /// <remarks>
        /// Utilizar a tabela do IBGE extendida. Utilizar 91 para identificar Ambiente Nacional.
        /// </remarks>
        public OrgaoIBGE Orgao { get; private set; }

        /// <summary>
        /// [cStat] Retorna ou define o Código do Status do registro do Evento.
        /// </summary>
        public string CodigoStatus { get; private set; }

        /// <summary>
        /// [xMotivo] Retorna ou define a descrição literal do status do registro do evento.
        /// </summary>
        public string Motivo { get; private set; }

        /// <summary>
        /// [chNFe] Retorna ou define a Chave de Acesso da NFe vinculada ao evento.
        /// </summary>
        public string ChaveAcessoNFe { get; private set; }

        /// <summary>
        /// [tpEvento] Retorna ou define o Tipo do Evento.
        /// </summary>
        public TipoEventoNFe? Tipo { get; private set; }

        /// <summary>
        /// [xEvento] Retorna ou define a descrição do evento.
        /// </summary>
        public string Descricao { get; private set; }

        /// <summary>
        /// [nSeqEvento] Retorna ou define o número sequencial do evento para o mesmo tipo de evento.
        /// </summary>
        public int? NumeroSequencial { get; private set; }

        /// <summary>
        /// [Cnpj,Cpf] Retorna ou define o Cpf ou Cnpj do Destinatário do Evento.
        /// </summary>
        public string CpfCnpjDestinatario { get; private set; }

        /// <summary>
        /// [emailDest] Retorna ou define o e-mail do destinatário.
        /// </summary>
        public string EmailDestinatario { get; set; }

        /// <summary>
        /// [dhRegEvento] Retorna ou define a Data de registro do Evento.
        /// </summary>
        public DateTime DataRegistro { get; private set; }

        /// <summary>
        /// [nProt] Retorna ou define o Número do Protocolo de registro do evento.
        /// </summary>
        public string NumeroProtocolo { get; private set; }
    }
}