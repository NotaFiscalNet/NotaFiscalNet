using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o Destinatário da Nota Fiscal Eletrônica.
    /// </summary>
    public sealed class DestinatarioNFe : ISerializavel, IPossuiDocumentoIdentificador
    {
        public DestinatarioNFe(string cnpjOuCpfOuIdEstrangeiro)
        {
            IndicadorIEDestinatario = IndicadorIEDestinatario.NaoContribuinte;
            if (Regex.IsMatch(cnpjOuCpfOuIdEstrangeiro, "[0-9]{14}"))
                _cnpj = cnpjOuCpfOuIdEstrangeiro;
            else if (Regex.IsMatch(cnpjOuCpfOuIdEstrangeiro, "[0-9]{11}"))
                _cpf = cnpjOuCpfOuIdEstrangeiro;
            else if (Regex.IsMatch(cnpjOuCpfOuIdEstrangeiro, "[!-ÿ]{0}|[!-ÿ]{5,14}"))
                _idEstrangeiro = cnpjOuCpfOuIdEstrangeiro;
            else
                throw new ApplicationException("O valor informado não é válido como Cpf/Cnpj/ID ESTRANGEIRO.");
        }

        private string _cnpj;
        private string _cpf;
        private string _idEstrangeiro;
        private string _nome;
        private string _inscricaoEstadual;
        private string _inscricaoSUFRAMA;
        private string _inscricaoMunicipal;
        private string _email;

        /// <summary>
        /// [Cnpj] Retorna ou define o Cnpj do Destinatário da Nota Fiscal
        /// </summary>
        /// <remarks>O Cnpj e o Cpf do Destinatário são mutuamente exclusivos.</remarks>
        [NFeField(ID = "E02", FieldName = "Cnpj", DataType = "TCnpjOpc", Pattern = "[0-9]{0}|[0-9]{14}", Opcional = true
            )]
        [CampoValidavel(10, Opcional = true)]
        public string CNPJ
        {
            get { return _cnpj; }
            set
            {
                long cnpj;
                long.TryParse(value, out cnpj);
                if (cnpj == 0)
                    return;

                ValidationUtil.ValidateCNPJ(value, "Cnpj", true);

                _cnpj = value;
                _cpf = string.Empty;
            }
        }

        /// <summary>
        /// [Cpf] Retorna ou define o Cpf do Destinatário da Nota Fiscal
        /// </summary>
        /// <remarks>O Cnpj e o Cpf do Destinatário são mutuamente exclusivos.</remarks>
        [NFeField(ID = "E03", FieldName = "Cpf", DataType = "TCpf", Pattern = "[0-9]{11}", Opcional = true)]
        [CampoValidavel(20, Opcional = true)]
        public string CPF
        {
            get { return _cpf; }
            set
            {
                ValidationUtil.ValidateCPF(value, "Cpf", true);

                _cpf = value;
                _cnpj = string.Empty;
            }
        }

        /// <summary>
        /// [idEstrangeiro] Retorna ou define o Identificador do destinatário, em caso de comprador
        /// estrangeiro. Opcional, número do passaporte ou outro documento legal para identificar
        /// pessoa estrangeira. Informar este campo no caso de operação com exterior.
        /// </summary>
        [NFeField(ID = "E03a", FieldName = "idEstrangeiro", DataType = "string", Pattern = "[!-ÿ]{0}|[!-ÿ]{5,14}")]
        [CampoValidavel(30, Opcional = true)]
        public string IdEstrangeiro
        {
            get { return _idEstrangeiro; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    ValidationUtil.ValidateRange(value, 5, 14, "IdEstrangeiro");
                _idEstrangeiro = value;
            }
        }

        /// <summary>
        /// [xNome] Retorna ou define a Razão Social ou Nome do Destinatário da Nota Fiscal
        /// </summary>
        [NFeField(ID = "E04", FieldName = "xNome", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}",
            MinLength = 2, MaxLength = 60, Opcional = true)]
        [CampoValidavel(40, ChaveErroValidacao.CampoNaoPreenchido)]
        public string Nome
        {
            get { return _nome; }
            set { _nome = ValidationUtil.TruncateString(value, 60); }
        }

        /// <summary>
        /// [enderEmit] Retorna o Endereço do Emitente da Nota Fiscal
        /// </summary>
        [NFeField(ID = "E05", FieldName = "enderEmit", DataType = "TEndereco")]
        [CampoValidavel(50, ChaveErroValidacao.CampoNaoPreenchido)]
        public Endereco Endereco { get; } = new Endereco();

        /// <summary>
        /// [indIEDest] Retorna ou define o Indicador da IE do Destinatário.
        /// </summary>
        [NFeField(ID = "E16a", FieldName = "indIEDest"), CampoValidavel(51, ChaveErroValidacao.CampoNaoPreenchido)]
        public IndicadorIEDestinatario IndicadorIEDestinatario { get; set; }

        /// <summary>
        /// [IE] Retorna ou define a Inscrição Estadual. Obrigatório apenas quando o Destinatário for
        /// contribuinte do ICMS. Informar 'ISENTO' quando o destinatário for contribuinte do ICMS
        /// mas não estiver obrigado à inscrição no cadastro de contribuintes do ICMS. NÃO informar
        /// se o destinatário não for contribuinte do ICMS.
        /// </summary>
        /// <remarks>
        /// No caso de informação da IE, informar somente os algarismos, sem os caracteres de
        /// formatação (ponto, barra, hífen, etc). Nota: Não informar este campo no caso de NFC-e.
        /// </remarks>
        [NFeField(ID = "E17", FieldName = "IE", DataType = "TIeDest", MinLength = 0, MaxLength = 14,
            Pattern = "ISENTO|[0-9]{0,14}", Opcional = true)]
        [CampoValidavel(60, ChaveErroValidacao.CampoNaoPreenchido)]
        public string InscricaoEstadual
        {
            get { return _inscricaoEstadual; }
            set { _inscricaoEstadual = ValidationUtil.ValidateTIeDest(value, "InscricaoEstadual"); }
        }

        /// <summary>
        /// [ISUF] Retorna ou define a Inscrição Estadual na SUFRAMA. Obrigatório apenas nas
        /// operações que se beneficiam de incentivos fiscais existentes nas áreas sob controle da SUFRAMA.
        /// </summary>
        /// <remarks>
        /// A omissão da Inscrição SUFRAMA impede o processamento da operação pelo Sistema de
        /// Mercadoria Nacional da SUFRAMA e a liberação da Declaração de Ingresso, prejudicando a
        /// comprovação do ingresso/internamento da mercadoria nas áreas sob controle da SUFRAMA.
        /// </remarks>
        [NFeField(ID = "E18", FieldName = "ISUF", DataType = "token", MinLength = 8, MaxLength = 9, Opcional = true)]
        [CampoValidavel(70, Opcional = true)]
        public string InscricaoSUFRAMA
        {
            get { return _inscricaoSUFRAMA; }
            set
            {
                ValidationUtil.ValidateIncricaoSUFRAMA(value, "InscricaoSUFRAMA");
                _inscricaoSUFRAMA = ValidationUtil.TruncateString(value, 9);
            }
        }

        [NFeField(ID = "E18a", FieldName = "IM", DataType = "token", MinLength = 1, MaxLength = 15, Opcional = true)]
        [CampoValidavel(70, Opcional = true)]
        public string InscricaoMunicipal
        {
            get { return _inscricaoMunicipal; }
            set
            {
                ValidationUtil.ValidateIncricaoSUFRAMA(value, "InscricaoMunicipal");
                _inscricaoMunicipal = ValidationUtil.TruncateString(value, 15);
            }
        }

        /// <summary>
        /// [email] Retorna ou define o Email do destinatário
        /// </summary>
        [NFeField(ID = "E19", FieldName = "email", DataType = "string", MinLength = 1, MaxLength = 60, Opcional = true)]
        [CampoValidavel(80, Opcional = true)]
        public string Email
        {
            get { return _email; }
            set { _email = ValidationUtil.TruncateString(value, 60); }
        }

        public void Serializar(XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("dest"); // Elemento 'dest'

            SerializeDocumentoDestinario(writer, nfe);

            writer.WriteElementString("xNome", Nome.ToToken(60));

            if (Endereco.Modificado)
                SerializeEnderecoDestinatario(writer, nfe);

            writer.WriteElementString("indIEDest", IndicadorIEDestinatario.GetEnumValue());
            if (IndicadorIEDestinatario == IndicadorIEDestinatario.ContribuinteIcms)
                writer.WriteElementString("IE", SerializationUtil.ToToken(InscricaoEstadual, 14));

            if (!string.IsNullOrEmpty(InscricaoSUFRAMA))
                writer.WriteElementString("ISUF", SerializationUtil.ToToken(InscricaoSUFRAMA, 9));

            if (!string.IsNullOrEmpty(InscricaoMunicipal))
                writer.WriteElementString("IM", InscricaoMunicipal);

            if (!string.IsNullOrEmpty(Email))
                writer.WriteElementString("email", SerializationUtil.ToToken(Email, 60));

            writer.WriteEndElement(); // Elemento 'dest'
        }

        /// <summary>
        /// Serializa o Endereço do Destinatário
        /// </summary>
        private void SerializeEnderecoDestinatario(XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("enderDest"); // Elemento 'enderDest'

            ((ISerializavel)Endereco).Serializar(writer, nfe);

            writer.WriteEndElement(); // Elemento 'enderDest'
        }

        /// <summary>
        /// Serializa o Choice de Cpf ou Cnpj
        /// </summary>
        private void SerializeDocumentoDestinario(XmlWriter writer, INFe nfe)
        {
            if (!string.IsNullOrEmpty(CNPJ))
                writer.WriteElementString("Cnpj", SerializationUtil.ToCNPJ(CNPJ));
            else if (!string.IsNullOrEmpty(CPF))
                writer.WriteElementString("Cpf", SerializationUtil.ToCPF(CPF));
            else if (!string.IsNullOrEmpty(IdEstrangeiro))
                writer.WriteElementString("idEstrangeiro", IdEstrangeiro);
            else
                throw new InvalidOperationException("Nenhum dos campos (Cnpj, Cpf, ID Estrangeiro) foram preenchidos.");
        }
    }
}