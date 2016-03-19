using System;
using System.Text;
using System.Xml.Schema;

namespace NotaFiscalNet.Core.Validacao
{
    public class XmlInvalidoException : Exception
    {
        public ValidationEventArgs[] Errors { get; private set; }

        public XmlInvalidoException(ValidationEventArgs[] errors)
            : base(CreateMessage(errors))
        {
            if (errors == null || errors.Length == 0)
                throw new ArgumentException("Nenhum parâmetro foi informado para o objeto.", nameof(errors));

            Errors = errors;
        }

        private static string CreateMessage(ValidationEventArgs[] errors)
        {
            if (errors == null)
                return null;

            var text = new StringBuilder("Ocorreram os seguintes erros de validação do xml:\r\n");

            foreach (var e in errors)
                text.AppendFormat("\t{0} -{1}\r\n", errors.Length, e.Message);

            return text.ToString();
        }
    }
}