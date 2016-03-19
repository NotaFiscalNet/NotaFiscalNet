namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Especifica a Modalidade de Determinação da Base de Cálculo do ICMS ST
    /// </summary>

    public enum ModalidadeBaseCalculoIcmsST
    {
        /// <summary>
        /// Não informado.
        /// </summary>
        NaoEspecificado = -1,

        /// <summary>
        /// 0 - Preço Tabelado ou Máximo Sugerido em Valor.
        /// </summary>
        PrecoTabelado = 0,

        /// <summary>
        /// 1 - Lista Negativa em Valor.
        /// </summary>
        ListaNegativa = 1,

        /// <summary>
        /// 2 - Lista Positiva em Valor.
        /// </summary>
        ListaPositiva = 2,

        /// <summary>
        /// 3 - Lista Neutra em Valor.
        /// </summary>
        ListaNeutra = 3,

        /// <summary>
        /// 4 - Margem do Valor Agregado em Porcentagem.
        /// </summary>
        MargemAgregado = 4,

        /// <summary>
        /// 5 - Pauta em Valor.
        /// </summary>
        Pauta = 5
    }
}