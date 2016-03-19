using System.Runtime.InteropServices;
using NotaFiscalNet.Core.Utils;

namespace NotaFiscalNet.Core.Validacao
{
    /// <summary>
    /// Representa um erro de validação da Nota Fiscal Eletrônica.
    /// </summary>
    
    
    public sealed class ErroValidacao
    {
        #region Fields
        
        private string _codigo = string.Empty;
        private string _descricao = string.Empty;
        private string _local = string.Empty;

        #endregion Fields

        #region Constructors

        private ErroValidacao(string codigo, string descricao)
        {
            _codigo = codigo ?? string.Empty;
            _descricao = descricao ?? string.Empty;
        }

        private ErroValidacao(string codigo, string descricao, string local)
            : this(codigo, descricao)
        {
            _local = local ?? string.Empty;
        } 

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Retorna o código referente ao erro de validação.
        /// </summary>
        public string Codigo { get { return _codigo; } }

        /// <summary>
        /// Retorna o caminho completo do local onde o erro ocorreu.
        /// </summary>
        public string Local { get { return _local; } }
        
        /// <summary>
        /// Retorna a descrição completa do erro de validação.
        /// </summary>
        public string Descricao { get { return _descricao; } } 

        #endregion Properties

        /// <summary>
        /// Retorna um Array de String de duas posições contendo o código e a descrição do Erro de Validação nas posições 0 e 1 (code-base 0) respectivamente.
        /// </summary>
        /// <param name="chave">Chave de acesso do erro.</param>
        /// <returns></returns>
        internal static ErroValidacao Create(ChaveErroValidacao chave)
        {
            string text = MsgUtil.GetString(chave);
            int indexOfPipe = text.IndexOf('|');

            string codigo = text.Substring(0, indexOfPipe);
            string descricao = text.Substring(indexOfPipe + 1);

            return Create(codigo, descricao, string.Empty);
        }

        internal static ErroValidacao Create(ChaveErroValidacao chave, params object[] args)
        {
            string text = MsgUtil.GetString(chave, args);
            int indexOfPipe = text.IndexOf('|');

            string codigo = text.Substring(0, indexOfPipe);
            string descricao = text.Substring(indexOfPipe + 1);
            descricao = string.Format(descricao, args);

            return Create(codigo, descricao, string.Empty);
        }

        internal static ErroValidacao Create(ChaveErroValidacao chave, string local)
        {
            string text = MsgUtil.GetString(chave);
            int indexOfPipe = text.IndexOf('|');

            string codigo = text.Substring(0, indexOfPipe);
            string descricao = text.Substring(indexOfPipe + 1);

            return Create(codigo, descricao, local);
        }

        internal static ErroValidacao Create(ChaveErroValidacao chave, string local, params object[] args)
        {
            string text = MsgUtil.GetString(chave, args);
            int indexOfPipe = text.IndexOf('|');

            string codigo = text.Substring(0, indexOfPipe);
            string descricao = text.Substring(indexOfPipe + 1);

            return Create(codigo, descricao, local);
        }

        private static ErroValidacao Create(string codigo, string descricao, string local)
        {
            return new ErroValidacao(codigo, descricao, local);
        }
    }
}
