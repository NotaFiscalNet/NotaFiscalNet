using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using System;
using System.Xml;
using NotaFiscalNet.Core.Interfaces;

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
                        "O valor informado não é um CPF ou CNPJ válido.");
            }
        }

        /// <summary>
        /// [CNPJ] Retorna o CNPJ autorizado a realizar o download do Xml.
        /// </summary>
        [NFeField(FieldName = "CNPJ", DataType = "TCnpj", ID = "G51")]
        [ValidateField(1, ChaveErroValidacao.CNPJInvalido)]
        public string CNPJ
        {
            get { return _cnpj; }
            private set
            {
                _cnpj = ValidationUtil.ValidateCPF(value, "CNPJ");
                ;
            }
        }

        /// <summary>
        /// [CPF] Retorna o CPF autorizado a realizar o download do Xml.
        /// </summary>
        [NFeField(FieldName = "CPF", DataType = "TCpf", ID = "G52")]
        [ValidateField(1, ChaveErroValidacao.CPFInvalido)]
        public string CPF
        {
            get { return _cpf; }
            private set
            {
                _cpf = ValidationUtil.ValidateCPF(value, "CPF");
                ;
            }
        }

        void ISerializavel.Serializar(XmlWriter writer, NFe nfe)
        {
            if (string.IsNullOrEmpty(CNPJ) && string.IsNullOrEmpty(CPF))
                throw new ApplicationException("");

            writer.WriteStartElement("autXML");

            if (!string.IsNullOrEmpty(CNPJ))
                writer.WriteElementString("CNPJ", CNPJ);
            else
                writer.WriteElementString("CPF", CPF);

            writer.WriteEndElement();
        }
    }
}