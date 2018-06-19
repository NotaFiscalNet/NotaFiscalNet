using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using System;
using System.Xml;

namespace NotaFiscalNet.Core
{
    public class AutorizacaoDownloadXml : ISerializavel
    {
        private string _cnpj;
        private string _cpf;

        public AutorizacaoDownloadXml(string cpfOuCnpj)
        {
            if (string.IsNullOrEmpty(cpfOuCnpj))
                throw new ArgumentNullException(nameof(cpfOuCnpj));

            switch (cpfOuCnpj.Length)
            {
                case 14:
                    CNPJ = cpfOuCnpj;
                    break;

                case 11:
                    CPF = cpfOuCnpj;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(cpfOuCnpj), cpfOuCnpj,
                        "O valor informado não é um Cpf ou Cnpj válido.");
            }
        }

        /// <summary>
        /// [Cnpj] Retorna o Cnpj autorizado a realizar o download do Xml.
        /// </summary>
        [NFeField(FieldName = "Cnpj", DataType = "TCnpj", ID = "G51")]
        [CampoValidavel(1, ChaveErroValidacao.CNPJInvalido)]
        public string CNPJ
        {
            get => _cnpj;
	        private set => _cnpj = ValidationUtil.ValidateCPF(value, "Cnpj");
        }

        /// <summary>
        /// [Cpf] Retorna o Cpf autorizado a realizar o download do Xml.
        /// </summary>
        [NFeField(FieldName = "Cpf", DataType = "TCpf", ID = "G52")]
        [CampoValidavel(1, ChaveErroValidacao.CPFInvalido)]
        public string CPF
        {
            get => _cpf;
	        private set => _cpf = ValidationUtil.ValidateCPF(value, "Cpf");
        }

        public void Serializar(XmlWriter writer, INFe nfe)
        {
            if (string.IsNullOrEmpty(CNPJ) && string.IsNullOrEmpty(CPF))
                throw new ApplicationException("");

            writer.WriteStartElement("autXML");

            if (!string.IsNullOrEmpty(CNPJ))
                writer.WriteElementString("Cnpj", CNPJ);
            else
                writer.WriteElementString("Cpf", CPF);

            writer.WriteEndElement();
        }
    }
}