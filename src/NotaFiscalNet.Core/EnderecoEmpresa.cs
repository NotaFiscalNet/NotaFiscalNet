using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using NotaFiscalNet.Core.Validacao.Validators;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o Endereço de uma Empresa para Retirada e Entrega
    /// </summary>
    public sealed class EnderecoEmpresa : EnderecoSimples, ISerializavel, IPossuiDocumentoIdentificador
    {
        public void Serializar(XmlWriter writer, INFe nfe)
        {
            if (!string.IsNullOrEmpty(CNPJ))
                writer.WriteElementString("Cnpj", SerializationUtil.ToCNPJ(CNPJ));
            else if (!string.IsNullOrEmpty(CPF))
                writer.WriteElementString("Cpf", SerializationUtil.ToCPF(CPF));

            SerializeEnderecoSimples(writer, nfe);
        }

        private string _CNPJ = string.Empty;
        private string _CPF = string.Empty;

        /// <summary>
        /// Retorna ou define o Cnpj da Empresa.
        /// </summary>
        [NFeField(ID = "G02", FieldName = "Cnpj", DataType = "TCnpjOpc", Pattern = "[0-9]{0}|[0-9]{14}")]
        [NFeField(ID = "F02", FieldName = "Cnpj", DataType = "TCnpjOpc", Pattern = "[0-9]{0}|[0-9]{14}")]
        [ValidateField(0, Validator = typeof(CNPJouCPFValidator))]
        public string CNPJ
        {
            get { return _CNPJ; }
            set
            {
                _CPF = string.Empty;
                _CNPJ = ValidationUtil.ValidateCNPJ(value, "Cnpj", true);
            }
        }

        /// <summary>
        /// Retorna ou define o Cpf da Empresa.
        /// </summary>
        [NFeField(ID = "G03", FieldName = "Cpf", DataType = "TCpf", Pattern = "[0-9]{11}", Opcional = true)]
        [NFeField(ID = "F03", FieldName = "Cpf", DataType = "TCpf", Pattern = "[0-9]{11}", Opcional = true)]
        [ValidateField(1, true)]
        public string CPF
        {
            get { return _CPF; }
            set
            {
                _CNPJ = string.Empty;
                _CPF = ValidationUtil.ValidateCPF(value, "Cpf", true);
            }
        }

        /// <summary>
        /// Retorna se a classe foi modificada.
        /// </summary>
        public override bool Modificado => !string.IsNullOrEmpty(CNPJ) ||
                                           !string.IsNullOrEmpty(CPF) ||
                                           !string.IsNullOrEmpty(Logradouro) ||
                                           !string.IsNullOrEmpty(Numero) ||
                                           !string.IsNullOrEmpty(Complemento) ||
                                           !string.IsNullOrEmpty(Bairro) ||
                                           CodigoMunicipioIBGE != 0 ||
                                           !string.IsNullOrEmpty(NomeMunicipio) ||
                                           UF != SiglaUF.NaoEspecificado;
    }
}