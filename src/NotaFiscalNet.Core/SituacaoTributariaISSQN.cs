namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Lista dos Códigos de Situação Tributária (CST) do ISSQN.
    /// </summary>

    public enum SituacaoTributariaISSQN
    {
        /// <summary>
        /// Não Informado
        /// </summary>
        NaoEspecificado = -1,

        /// <summary>
        /// N - Normal
        /// </summary>
        Normal = 1,

        /// <summary>
        /// R - Retida
        /// </summary>
        Retida = 2,

        /// <summary>
        /// S - Substituta
        /// </summary>
        Substituta = 3,

        /// <summary>
        /// I - Isenta
        /// </summary>
        Isenta = 4
    }
}