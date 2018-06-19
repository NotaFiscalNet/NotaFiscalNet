using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa a entidade responsável por fazer o transporte das mercadorias.
    /// </summary>
    public sealed class Transportador : ISerializavel, IModificavel
    {
        private string _cpf = string.Empty;
        private string _cnpj = string.Empty;
        private string _xNome = string.Empty;
        private string _inscricaoEstadual = string.Empty;
        private string _xEnder = string.Empty;
        private string _xMun = string.Empty;
        private SiglaUF _UF = SiglaUF.NaoEspecificado;

        /// <summary>
        /// Retorna ou define o Número do Cpf (11 caracteres, apenas números) do Transportador.
        /// </summary>
        [NFeField(FieldName = "Cpf", DataType = "TCpf", ID = "X05")]
        public string CPF
        {
            get => _cpf;
	        set
            {
                ValidationUtil.ValidateCPF(value, "Cpf", true);

                _cpf = value;
                _cnpj = null;
            }
        }

        /// <summary>
        /// Retorna ou define o número do Cnpj (14 caracteres, apenas números) do Transportador.
        /// </summary>
        [NFeField(FieldName = "Cnpj", DataType = "TCnpj", ID = "X04")]
        public string CNPJ
        {
            get => _cnpj;
	        set
            {
                ValidationUtil.ValidateCNPJ(value, "Cnpj", true);

                _cnpj = value;
                _cpf = string.Empty;
            }
        }

        /// <summary>
        /// Retorna ou define o Nome (se informado Cpf) ou a Razão Social (se informado Cnpj) do
        /// Transportador. Opcional.
        /// </summary>
        [NFeField(FieldName = "xNome", DataType = "TString", ID = "X06", MinLength = 2, MaxLength = 60, Pattern = @"[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}")]
        public string Nome
        {
            get => _xNome;
	        set => _xNome = ValidationUtil.TruncateString(value, 60);
        }

        /// <summary>
        /// Retorna ou define o Número da Inscrição Estadual do Transportador. Opcional.
        /// </summary>
        [NFeField(FieldName = "IE", DataType = "TIeDest", ID = "X07", MinLength = 2, MaxLength = 14)]
        public string InscricaoEstadual
        {
            get => _inscricaoEstadual;
	        set
            {
                ValidationUtil.ValidateIncricaoEstadualDestino(value, "InscricaoEstadual");

                _inscricaoEstadual = value;
            }
        }

        /// <summary>
        /// Retorna ou define o valor representando o Endereço completo do Transportador. Opcional.
        /// </summary>
        [NFeField(FieldName = "xEnder", DataType = "TString", ID = "X08", MinLength = 1, MaxLength = 60, Pattern = @"[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}")]
        public string EnderecoCompleto
        {
            get => _xEnder;
	        set => _xEnder = ValidationUtil.TruncateString(value, 60);
        }

        /// <summary>
        /// Retorna ou define o Nome do Município do Transportador. Opcional.
        /// </summary>
        [NFeField(FieldName = "xMun", DataType = "TString", ID = "X09", MinLength = 1, MaxLength = 60, Pattern = @"[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}")]
        public string Municipio
        {
            get => _xMun;
	        set => _xMun = ValidationUtil.TruncateString(value, 60);
        }

        /// <summary>
        /// Retorna ou define a Sigla UF do Transportador. Opcional.
        /// </summary>
        [NFeField(FieldName = "UF", DataType = "TUf", ID = "X10")]
        public SiglaUF UF
        {
            get => _UF;
	        set
            {
                ValidationUtil.ValidateEnum<SiglaUF>(value, "UF");
                _UF = value;
            }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => !string.IsNullOrEmpty(CNPJ) ||
                                  !string.IsNullOrEmpty(CPF) ||
                                  !string.IsNullOrEmpty(Nome) ||
                                  !string.IsNullOrEmpty(InscricaoEstadual) ||
                                  !string.IsNullOrEmpty(EnderecoCompleto) ||
                                  !string.IsNullOrEmpty(Municipio) ||
                                  UF != SiglaUF.NaoEspecificado;

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("transporta");

            if (!string.IsNullOrEmpty(CNPJ))
                writer.WriteElementString("Cnpj", SerializationUtil.ToCNPJ(CNPJ));
            else if (!string.IsNullOrEmpty(CPF))
                writer.WriteElementString("Cpf", SerializationUtil.ToCPF(CPF));

            if (!string.IsNullOrEmpty(Nome))
                writer.WriteElementString("xNome", SerializationUtil.ToTString(Nome, 60));

            if (!string.IsNullOrEmpty(InscricaoEstadual))
                writer.WriteElementString("IE", SerializationUtil.ToTString(InscricaoEstadual, 14));

            if (!string.IsNullOrEmpty(EnderecoCompleto))
                writer.WriteElementString("xEnder", SerializationUtil.ToTString(EnderecoCompleto, 60));

            if (!string.IsNullOrEmpty(Municipio))
                writer.WriteElementString("xMun", SerializationUtil.ToTString(Municipio, 60));

            if (UF != SiglaUF.NaoEspecificado)
                writer.WriteElementString("UF", UF.ToString());

            writer.WriteEndElement(); // fim do elemento 'transporta'
        }
    }
}