﻿using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using NotaFiscalNet.Core.Validacao.Validators;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o Emitente da Nota Fiscal Eletrônica.
    /// </summary>
    public sealed class EmitenteNFe : ISerializavel, IPossuiDocumentoIdentificador
    {
        private string _CNPJ = string.Empty;
        private string _CPF = string.Empty;
        private string _nome = string.Empty;
        private string _nomeFantasia = string.Empty;
        private string _inscricaoEstadual = string.Empty;
        private string _inscricaoEstadualSubstitutoTributario = string.Empty;
        private string _inscricaoMunicipal = string.Empty;
        private string _CNAEFiscal = string.Empty;

        private CodigoRegimeTributario _codigoRegimeTributario = CodigoRegimeTributario.NaoInformado;

        /// <summary>
        /// [CNPJ] Retorna ou define o CNPJ do Emitente da Nota Fiscal (informar apenas números).
        /// </summary>
        /// <remarks>O CNPJ e o CPF do Emitente são mutuamente exclusivos.</remarks>
        [NFeField(ID = "C02", FieldName = "CNPJ", DataType = "TCnpj", Pattern = "[0-9]{14}", Opcional = true)]
        [ValidateField(1, Validator = typeof(CNPJouCPFValidator))]
        public string CNPJ
        {
            get { return _CNPJ; }
            set
            {
                _CNPJ = ValidationUtil.ValidateCNPJ(value, "CNPJ", true);
                _CPF = string.Empty;
            }
        }

        /// <summary>
        /// [CPF] Retorna ou define o CPF do Emitente da Nota Fiscal (informar apenas números).
        /// </summary>
        /// <remarks>O CNPJ e o CPF do Emitente são mutuamente exclusivos.</remarks>
        [NFeField(ID = "C02a", FieldName = "CPF", DataType = "TCpf", Pattern = "[0-9]{11}", Opcional = true)]
        [ValidateField(2, true)]
        public string CPF
        {
            get { return _CPF; }
            set
            {
                _CPF = ValidationUtil.ValidateCPF(value, "CPF", true);
                _CNPJ = string.Empty;
            }
        }

        /// <summary>
        /// [xNome] Retorna ou define a Razão Social ou Nome do Emitente da Nota Fiscal
        /// </summary>
        [NFeField(ID = "C03", FieldName = "xNome", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}",
            MinLength = 1, MaxLength = 60)]
        [ValidateField(3, ChaveErroValidacao.CampoNaoPreenchido)]
        public string Nome
        {
            get { return _nome; }
            set { _nome = ValidationUtil.TruncateString(value, 60); }
        }

        /// <summary>
        /// [xFant] Retorna ou define o Nome Fantasia do Emitente da Nota Fiscal
        /// </summary>
        [NFeField(ID = "C04", FieldName = "xFant", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}",
            MinLength = 1, MaxLength = 60)]
        [ValidateField(4, true)]
        public string NomeFantasia
        {
            get { return _nomeFantasia; }
            set { _nomeFantasia = ValidationUtil.TruncateString(value, 60); }
        }

        /// <summary>
        /// [enderEmit] Retorna o Endereço do Emitente da Nota Fiscal
        /// </summary>
        [NFeField(ID = "C05", FieldName = "enderEmit", DataType = "TEndEmi")]
        [ValidateField(5, ChaveErroValidacao.CampoNaoPreenchido)]
        public Endereco Endereco { get; } = new Endereco();

        /// <summary>
        /// [IE] Retorna ou define a Inscrição Estadual
        /// </summary>
        [NFeField(ID = "C17", FieldName = "IE", DataType = "TIe", MinLength = 0, MaxLength = 14)]
        [ValidateField(6, ChaveErroValidacao.CampoNaoPreenchido)]
        public string InscricaoEstadual
        {
            get { return _inscricaoEstadual; }
            set
            {
                ValidationUtil.ValidateIncricaoEstadual(value, "InscricaoEstadual");
                _inscricaoEstadual = ValidationUtil.TruncateString(value, 14);
            }
        }

        /// <summary>
        /// [IEST] Retorna ou define a Inscrição Estadual do Substituto Tributário. Obrigatório
        /// apenas quando houver a retenção do ICMS Substituto Tributário para a UF de destino.
        /// </summary>
        [NFeField(ID = "C18", FieldName = "IEST", DataType = "TIeST", MinLength = 2, MaxLength = 14, Opcional = true)]
        [ValidateField(7, true)]
        public string InscricaoEstadualSubstitutoTributario
        {
            get { return _inscricaoEstadualSubstitutoTributario; }
            set
            {
                ValidationUtil.ValidateIncricaoEstadual(value, "InscricaoEstadualSubstitutoTributario");
                _inscricaoEstadualSubstitutoTributario = ValidationUtil.TruncateString(value, 14);
            }
        }

        /// <summary>
        /// [IM] Retorna ou define a Inscrição Municipal. Obrigatório apenas quando ocorrer a emissão
        /// de NF-e conjugada, com prestação de serviços sujeitos ao ISSQN e fornecimento de peças
        /// sujeitos ao ICMS.
        /// </summary>
        [NFeField(ID = "C19", FieldName = "IM", DataType = "TString", MinLength = 1, MaxLength = 15, Opcional = true)]
        [ValidateField(8, Validator = typeof(EmitenteNFeValidator))]
        public string InscricaoMunicipal
        {
            get { return _inscricaoMunicipal; }
            set { _inscricaoMunicipal = ValidationUtil.TruncateString(value, 15); }
        }

        /// <summary>
        /// [CNAE] Retorna ou define o CNAE Fiscal. Obrigatório apenas quando a Inscrição Municipal
        /// for informada.
        /// </summary>
        [NFeField(ID = "C20", FieldName = "CNAE", DataType = "string", Pattern = "[0-9]{7}", Opcional = true)]
        [ValidateField(9, Validator = typeof(EmitenteNFeValidator))]
        public string CNAEFiscal
        {
            get { return _CNAEFiscal; }
            set
            {
                ValidationUtil.ValidateCNAE(value, "CNAEFiscal", true);
                _CNAEFiscal = value;
            }
        }

        /// <summary>
        /// [CRT] Retorna ou define o Código de Regime Tributário. Este campo será obrigatoriamente
        /// preenchido com: 1 – Simples Nacional; 2 – Simples Nacional – excesso de sublimite de
        /// receita bruta; 3 – Regime Normal.
        /// </summary>
        [NFeField(ID = "C21", FieldName = "CRT", DataType = "string")]
        [ValidateField(10, ChaveErroValidacao.CampoNaoPreenchido, DefaultValue = CodigoRegimeTributario.NaoInformado)]
        public CodigoRegimeTributario CodigoRegimeTributario
        {
            get { return _codigoRegimeTributario; }
            set { _codigoRegimeTributario = ValidationUtil.ValidateEnum(value, "CodigoRegimeTributario"); }
        }

        void ISerializavel.Serializar(XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("emit"); // Elemento 'emit'

            SerializeDocumentoEmitente(writer, nfe);
            writer.WriteElementString("xNome", SerializationUtil.ToToken(Nome, 60));
            if (!string.IsNullOrEmpty(NomeFantasia))
                writer.WriteElementString("xFant", SerializationUtil.ToToken(NomeFantasia, 60));
            SerializeEnderecoEmitente(writer, nfe);
            writer.WriteElementString("IE", SerializationUtil.ToToken(InscricaoEstadual, 14));
            if (!string.IsNullOrEmpty(InscricaoEstadualSubstitutoTributario))
                writer.WriteElementString("IEST", SerializationUtil.ToToken(InscricaoEstadualSubstitutoTributario, 14));
            if (!string.IsNullOrEmpty(InscricaoMunicipal) && !string.IsNullOrEmpty(CNAEFiscal))
            {
                writer.WriteElementString("IM", SerializationUtil.ToToken(InscricaoMunicipal, 15));

                if (!string.IsNullOrEmpty(CNAEFiscal))
                    writer.WriteElementString("CNAE", SerializationUtil.ToToken(CNAEFiscal, 7));
            }

            writer.WriteElementString("CRT", SerializationUtil.GetEnumValue(CodigoRegimeTributario));

            writer.WriteEndElement(); // Elemento 'emit'
        }

        /// <summary>
        /// Serializa o Endereço do Emitente
        /// </summary>
        private void SerializeEnderecoEmitente(XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("enderEmit"); // Elemento 'enderEmit'

            ((ISerializavel)Endereco).Serializar(writer, nfe);

            writer.WriteEndElement(); // Elemento 'enderEmit'
        }

        /// <summary>
        /// Serializa o Choice de CPF ou CNPJ
        /// </summary>
        private void SerializeDocumentoEmitente(XmlWriter writer, NFe nfe)
        {
            if (!string.IsNullOrEmpty(CNPJ))
                writer.WriteElementString("CNPJ", SerializationUtil.ToCNPJ(CNPJ));
            else if (!string.IsNullOrEmpty(CPF))
                writer.WriteElementString("CPF", SerializationUtil.ToCPF(CPF));
        }
    }
}