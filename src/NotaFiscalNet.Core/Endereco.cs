using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using System;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o Endereço do Emitente e do Destinatário da Nota Fiscal Eletrônica.
    /// </summary>
    /// <remarks>Equivalente ao tipo TEndereco no xml.</remarks>
    public sealed class Endereco : EnderecoSimples, ISerializavel
    {
        void ISerializavel.Serializar(XmlWriter writer, NFe nfe)
        {
            SerializeEnderecoSimples(writer, nfe);

            if (!string.IsNullOrEmpty(CEP))
                writer.WriteElementString("CEP", CEP);
            if (CodigoPaisBACEN > 0)
                writer.WriteElementString("cPais", CodigoPaisBACEN.ToString());
            if (!string.IsNullOrEmpty(NomePais))
                writer.WriteElementString("xPais", SerializationUtil.ToToken(NomePais, 60));
            if (!string.IsNullOrEmpty(Telefone))
                writer.WriteElementString("fone", SerializationUtil.ToToken(Telefone, 10));
        }

        private string _CEP = string.Empty;
        private int _codigoPaisBACEN;
        private string _telefone = string.Empty;

        /// <summary>
        /// Retorna ou define o Código do CEP (apenas números). Informar zeros não significativos. Opcional
        /// </summary>
        [NFeField(ID = "C13", FieldName = "CEP", DataType = "token", Pattern = "[0-9]{8}", Opcional = true)]
        [ValidateField(8, true)]
        public string CEP
        {
            get { return _CEP; }
            set { _CEP = ValidationUtil.ValidateCEP(value, "CEP"); }
        }

        /// <summary>
        /// [cPais] Retorna ou define o Código do País de acordo com a Tabela do BACEN (Brasil-1058).
        /// </summary>
        [NFeField(ID = "C14", FieldName = "cPais", DataType = "token", Pattern = "[0-9]{1,4}", Opcional = true)]
        [ValidateField(9, true)]
        public int CodigoPaisBACEN
        {
            get { return _codigoPaisBACEN; }
            set
            {
                if (value > 0 && !ValidationUtil.ValidateRegex(value, "[0-9]{1,4}"))
                    throw new ArgumentException("O valor informado para o Código do País não é válido.");

                _codigoPaisBACEN = value;
            }
        }

        /// <summary>
        /// [xPais] Retorna ou define o Nome do País.
        /// </summary>
        [NFeField(ID = "C15", FieldName = "xPais", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}",
            MinLength = 1, MaxLength = 60, Opcional = true)]
        [ValidateField(10, true)]
        public string NomePais { get; set; } = string.Empty;

        /// <summary>
        /// [fone] Retorna ou define o Telefone. Preencher com o DDD + número do telefone (informar
        /// apenas número, sem espaços). Opcional.
        /// </summary>
        [NFeField(ID = "C16", FieldName = "fone", DataType = "token", Pattern = "[0-9]{1,10}", Opcional = true)]
        [ValidateField(11, true)]
        public string Telefone
        {
            get { return _telefone; }
            set
            {
                if (!string.IsNullOrEmpty(value) && !ValidationUtil.ValidateRegex(value, "^[0-9]{1,10}$"))
                    throw new ArgumentException(
                        "O valor informado para o Telefone não é válido. Informe apenas números, sem espaços.");

                _telefone = value;
            }
        }

        public override bool Modificado => base.Modificado ||
                                           !string.IsNullOrEmpty(CEP) ||
                                           CodigoPaisBACEN != 0 ||
                                           !string.IsNullOrEmpty(NomePais) ||
                                           !string.IsNullOrEmpty(Telefone);
    }
}