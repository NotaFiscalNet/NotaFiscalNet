using NotaFiscalNet.Core.Validacao;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa um erro de validação que ocorre quando se tenta modificar o valor da Nota Fiscal
    /// Eletrônica em modo Somente-Leitura.
    /// </summary>

    public sealed class NFeReadOnlyException : ErroValidacaoNFeException
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="NFeReadOnlyException"/>.
        /// </summary>
        internal NFeReadOnlyException():base(ChaveErroValidacao.ReadOnlyClass)
        {
        }
    }
}