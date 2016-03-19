using NotaFiscalNet.Core.Utils;
using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace NotaFiscalNet.Core.Evento
{
    /// <summary>
    /// Representa o tipo complexo 'TEvento'.
    /// </summary>
    public abstract class EventoNFe
    {
        private string _id;
        private int _numeroSequencial;
        private string _cpfCnpj;
        private string _chaveAcessoNFe;

        protected EventoNFe()
        {
            Data = DateTime.Now;
            NumeroSequencial = 1;
            VersaoEvento = "1.00";
        }

        /// <summary>
        /// [@versao] Retorna a versão do evento.
        /// </summary>
        public const string Versao = "1.00";

        /// <summary>
        /// [@Id] Retorna o Identificador da TAG a ser assinada (composto por 'ID' + ChaveAcessoNFe + NumeroSequencial).
        /// </summary>
        public string Id
        {
            get
            {
                if (!Regex.IsMatch(ChaveAcessoNFe, "[0-9]{44}"))
                    throw new ArgumentException("O campo ChaveAcessoNFe não foi informado ou é inválido.");

                if (_id != null)
                    return _id;

                return _id = String.Format("ID{0}{1}{2}", (int)Tipo, ChaveAcessoNFe, NumeroSequencial.ToString("00"));
            }
            set
            {
                if (!Regex.IsMatch(value, "ID[0-9]{52}"))
                    throw new ArgumentException("Id do evento inválido.");
                _id = value;
            }
        }

        /// <summary>
        /// [cOrgao] Retorna ou define o Orgão de Recepção do Evento.
        /// </summary>
        /// <remarks>
        /// Utilizar a tabela do IBGE extendida. Utilizar 91 para identificar Ambiente Nacional.
        /// </remarks>
        public OrgaoIBGE Orgao { get; set; }

        /// <summary>
        /// [tpAmb] Retorna ou define o Ambiente (Produção ou Homologação).
        /// </summary>
        public TipoAmbiente Ambiente { get; set; }

        /// <summary>
        /// [CNPJ,CPF] Retorna ou define o Cpf ou Cnpj do Autor do Evento.
        /// </summary>
        public string CpfCnpjAutor
        {
            get { return _cpfCnpj; }
            set
            {
                if (!Regex.IsMatch(value, "^([0-9]{11}|[0-9]{14})$"))
                    throw new ApplicationException("O CPF ou CNPJ do Autor não é inválido.");

                _cpfCnpj = value;
            }
        }

        /// <summary>
        /// [chNFe] Retorna ou define a Chave de Acesso da NFe vinculada ao evento.
        /// </summary>
        public string ChaveAcessoNFe
        {
            get { return _chaveAcessoNFe; }
            set
            {
                if (!Regex.IsMatch(value, "^[0-9]{44}$"))
                    throw new ApplicationException("A valor informado para a Chave de Acesso da NF-e não é válido.");

                _chaveAcessoNFe = value;
            }
        }

        /// <summary>
        /// [dhEvento] Retorna ou define a Data do Evento.
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// [tpEvento] Retorna ou define o Tipo do Evento.
        /// </summary>
        public abstract TipoEventoNFe Tipo { get; }

        /// <summary>
        /// [nSeqEvento] Retorna ou define o número sequencial do evento para o mesmo tipo de evento.
        /// Para maioria dos eventos será 1, nos casos em que possa existir mais de um evento, como é
        /// o caso da carta de correção, o autor do evento deve numerar de forma seqüencial.
        /// </summary>
        public int NumeroSequencial
        {
            get { return _numeroSequencial; }
            set
            {
                if (value < 1 || value > 20)
                    throw new ArgumentOutOfRangeException("NumeroSequencial", value, "O número sequencial deve estar compreendido entre 1 e 20.");

                _numeroSequencial = value;
            }
        }

        /// <summary>
        /// [verEvento] Retorna ou define a Versão do Evento ('verEvento').
        /// </summary>
        public string VersaoEvento { get; set; }

        public void Serialize(XmlWriter writer)
        {
            writer.WriteStartElement("evento");
            writer.WriteAttributeString("versao", Versao);

            writer.WriteStartElement("infEvento");
            writer.WriteAttributeString("Id", Id);

            writer.WriteElementString("cOrgao", Orgao.GetEnumValue());
            writer.WriteElementString("tpAmb", Ambiente.GetEnumValue());
            writer.WriteElementString(CpfCnpjAutor.Length == 14 ? "CNPJ" : "CPF", CpfCnpjAutor);
            writer.WriteElementString("chNFe", ChaveAcessoNFe);
            writer.WriteElementString("dhEvento", Data.ToTDateTimeUtc());
            writer.WriteElementString("tpEvento", Tipo.GetEnumValue());
            writer.WriteElementString("nSeqEvento", NumeroSequencial.ToString());
            writer.WriteElementString("verEvento", VersaoEvento);

            // renderiza o detalhamento do evento de acordo com o tipo
            SerializeDetalheEvento(writer);

            writer.WriteEndElement(); // tag: infEvento
            writer.WriteEndElement(); // tag: evento
        }

        /// <summary>
        /// Checa se o preenchimento do evento está correto.
        /// </summary>
        /// <exception cref="System.ApplicationException">Caso algum erro exista no evento.</exception>
        public void Validar()
        {
            if (!Orgao.IsDefined())
                throw new ApplicationException("O campo 'Orgao' não foi preenchido ou é inválido.");

            if (!Ambiente.IsDefined())
                throw new ApplicationException("O campo 'Ambiente' não foi preenchido ou é inválido.");

            if (String.IsNullOrEmpty(CpfCnpjAutor))
                throw new ApplicationException("O campo 'CpfCnpj' não foi preenchido ou é inválido.");

            if (String.IsNullOrEmpty(ChaveAcessoNFe))
                throw new ApplicationException("O campo 'ChaveAcessoNFe' não foi preenchido ou é inválido.");

            if (Data == DateTime.MinValue)
                throw new ApplicationException("O campo 'Data' não foi preenchido.");

            if (!Tipo.IsDefined())
                throw new ApplicationException("O campo 'Tipo' não foi preenchido ou é inválido.");
        }

        protected abstract void SerializeDetalheEvento(XmlWriter writer);
    }
}