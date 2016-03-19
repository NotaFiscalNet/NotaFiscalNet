namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Especifica a Condição do Veículo.
    /// </summary>
    public enum CondicaoVeiculo
    {
        /// <summary>
        /// Não Especificado
        /// </summary>
        NaoEspecificado = -1,

        /// <summary>
        /// Veículo Acabado
        /// </summary>
        Acabado = 1,

        /// <summary>
        /// Veículo Inacabado
        /// </summary>
        Inacabado = 2,

        /// <summary>
        /// Veículo Semi-Acabado
        /// </summary>
        SemiAcabado = 3
    }
}