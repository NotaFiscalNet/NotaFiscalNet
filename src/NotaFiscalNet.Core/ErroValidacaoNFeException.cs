using NotaFiscalNet.Core.Validacao;
using System;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa um erro de validação ocorrido na Nota Fiscal Eletrônica.
    /// </summary>
    public class ErroValidacaoNFeException : Exception
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ErroValidacaoNFeException"/>.
        /// </summary>
        /// <param name="chave"></param>
        internal ErroValidacaoNFeException(ChaveErroValidacao chave)
        {
            Detalhes = ErroValidacao.Create(chave);
        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ErroValidacaoNFeException"/>.
        /// </summary>
        /// <param name="erro">Objeto contendo os detalhes do erro de validação.</param>
        internal ErroValidacaoNFeException(ErroValidacao erro)
        {
            Detalhes = erro;
        }

        /// <summary>
        /// Retorna o objeto contendo as informações de erro de validação.
        /// </summary>
        public ErroValidacao Detalhes { get; }

        public override string Message
        {
            get { return Detalhes.Descricao; }
        }
    }
}