using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotaFiscalNet.Core.Validacao
{
    public class ResultadoValidacao
    {
        public ResultadoValidacao()
        {
            Erros = new List<ErroValidacao>();
        }

        public ResultadoValidacao(IEnumerable<ErroValidacao> erros)
        {
            Erros = erros.ToList();
        }

        public List<ErroValidacao> Erros { get; }

        public bool Valido => !Erros.Any();

        public override string ToString()
        {
            if (Valido)
                return "Nenhum erro de validação foi encontrado.";

            var sb = new StringBuilder();
            foreach (var erro in Erros)
                sb.AppendFormat("{0} - {1}\r\n", erro.Descricao, erro.Propriedade);
            return sb.ToString();
        }
    }
}