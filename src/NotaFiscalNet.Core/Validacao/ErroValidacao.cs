using NotaFiscalNet.Core.Utils;

namespace NotaFiscalNet.Core.Validacao
{
    /// <summary>
    /// Representa um erro de validação da Nota Fiscal Eletrônica.
    /// </summary>
    public class ErroValidacao
    {
        public ErroValidacao(string propriedade, string descricao)
        {
            Propriedade = propriedade;
            Descricao = descricao;
        }

        public ErroValidacao(string propriedade, string descricao, object valorInformado)
            : this(propriedade, descricao)
        {
            ValorInformado = valorInformado;
        }

        /// <summary>
        /// Retorna o valor informado que gerou o erro de validação.
        /// </summary>
        public object ValorInformado { get; }

        /// <summary>
        /// Retorna a descrição completa do erro de validação.
        /// </summary>
        public string Descricao { get; }

        /// <summary>
        /// Retorna o caminho completo do local onde o erro ocorreu.
        /// </summary>
        public string Propriedade { get; }

        public override string ToString()
        {
            return Descricao;
        }

        /// <summary>
        /// Retorna um Array de String de duas posições contendo o código e a descrição do Erro de
        /// Validação nas posições 0 e 1 (code-base 0) respectivamente.
        /// </summary>
        /// <param name="chave">Chave de acesso do erro.</param>
        /// <returns></returns>
        internal static ErroValidacao Create(ChaveErroValidacao chave)
        {
            var text = MsgUtil.GetString(chave);
            var indexOfPipe = text.IndexOf('|');

            var codigo = text.Substring(0, indexOfPipe);
            var descricao = text.Substring(indexOfPipe + 1);

            return Create(codigo, descricao, string.Empty);
        }

        internal static ErroValidacao Create(ChaveErroValidacao chave, params object[] args)
        {
            var text = MsgUtil.GetString(chave, args);
            var indexOfPipe = text.IndexOf('|');

            var codigo = text.Substring(0, indexOfPipe);
            var descricao = text.Substring(indexOfPipe + 1);
            descricao = string.Format(descricao, args);

            return Create(codigo, descricao, string.Empty);
        }

        internal static ErroValidacao Create(ChaveErroValidacao chave, string local)
        {
            var text = MsgUtil.GetString(chave);
            var indexOfPipe = text.IndexOf('|');

            var codigo = text.Substring(0, indexOfPipe);
            var descricao = text.Substring(indexOfPipe + 1);

            return Create(codigo, descricao, local);
        }

        internal static ErroValidacao Create(ChaveErroValidacao chave, string local, params object[] args)
        {
            var text = MsgUtil.GetString(chave, args);
            var indexOfPipe = text.IndexOf('|');

            var codigo = text.Substring(0, indexOfPipe);
            var descricao = text.Substring(indexOfPipe + 1);

            return Create(codigo, descricao, local);
        }

        private static ErroValidacao Create(string codigo, string descricao, string local)
        {
            return new ErroValidacao(codigo, descricao, local);
        }
    }
}