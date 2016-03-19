namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Especifica a Espécie do Veículo de acordo com tabela do RENAVAM.
    /// </summary>
    public enum EspecieVeiculoRENAVAM
    {
        /// <summary>
        /// Não Especificado
        /// </summary>
        NaoEspecificado = -1,

        /// <summary>
        /// 1 - Passageiro.
        /// </summary>
        Passageiro = 1,

        /// <summary>
        /// 2 - Carga.
        /// </summary>
        Carga = 2,

        /// <summary>
        /// 3 - Misto.
        /// </summary>
        Misto = 3,

        /// <summary>
        /// 4 - Corrida.
        /// </summary>
        Corrida = 4,

        /// <summary>
        /// 5 - Tração.
        /// </summary>
        Tração = 5,

        /// <summary>
        /// 6 - Especial.
        /// </summary>
        Especial = 6
    }
}