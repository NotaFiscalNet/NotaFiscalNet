using System.Collections.Generic;
using System.Text;

namespace NotaFiscalNet.Core.Validacao
{
    public class ResultadoValidacao
    {
        public bool Sucesso { get; private set; }

        public IDictionary<string, ErroValidacao> Erros { get; private set; }

        public ResultadoValidacao(IDictionary<string, ErroValidacao> erros)
        {
            Sucesso = false;
            Erros = new Dictionary<string, ErroValidacao>(erros);
        }

        public ResultadoValidacao()
        {
            Sucesso = true;
            Erros = new Dictionary<string, ErroValidacao>();
        }

        public override string ToString()
        {
            if (Sucesso)
                return "Nenhum erro de validação foi encontrado.";
            else
            {
                var sb = new StringBuilder();
                foreach (var erro in Erros.Values)
                    sb.AppendFormat("{0} - {1}\r\n", erro.Descricao, erro.Local);
                return sb.ToString();
            }
        }
    }
}
