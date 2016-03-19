namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Lista dos Códigos de Situação Tributária (CST) do PIS.
    /// </summary>

    public enum SituacaoTributariaPIS
    {
        /// <summary>
        /// Não Informado
        /// </summary>
        NaoEspecificado = -1,

        /// <summary>
        /// CST 01 - Operação Tributável - Base de Cálculo igual ao Valor da Operação com Alíquota
        /// Normal (Cumulativo / Não Cumulativo).
        /// </summary>
        Cst01 = 1,

        /// <summary>
        /// CST 02 - Operação Tributável - Base de Cálculo igual ao Valor da Operação com Alíquota Diferenciada.
        /// </summary>
        Cst02 = 2,

        /// <summary>
        /// CST 03 - Operação Tributável - Base de Cálculo igual a Quantidade Vendida x Alíquota por
        /// Unidade de Produto.
        /// </summary>
        Cst03 = 3,

        /// <summary>
        /// CST 04 - Operação Tributável - Tributação Monofásica com Alíquota Zero.
        /// </summary>
        Cst04 = 4,

        /// <summary>
        /// CST 05 - Operação Tributável por Substituição Tributária.
        /// </summary>
        Cst05 = 5,

        /// <summary>
        /// CST 06 - Operação Tributável - Alíquota Zero.
        /// </summary>
        Cst06 = 6,

        /// <summary>
        /// CST 07 - Operação Isenta de Contribuição.
        /// </summary>
        Cst07 = 7,

        /// <summary>
        /// CST 08 - Operação Sem Incidência da Contribuição.
        /// </summary>
        Cst08 = 8,

        /// <summary>
        /// CST 09 - Operação com Suspensão da Contribuição.
        /// </summary>
        Cst09 = 9,

        /// <summary>
        /// CST 49 - Outras Operações de Saída.
        /// </summary>
        Cst49 = 49,

        /// <summary>
        /// CST 50 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Tributada no
        /// Mercado Interno.
        /// </summary>
        Cst50 = 50,

        /// <summary>
        /// CST 51 - Operação com Direito a Crédito – Vinculada Exclusivamente a Receita Não
        /// Tributada no Mercado Interno.
        /// </summary>
        Cst51 = 51,

        /// <summary>
        /// CST 52 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita de Exportação.
        /// </summary>
        Cst52 = 52,

        /// <summary>
        /// CST 53 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas e
        /// Não-Tributadas no Mercado Interno.
        /// </summary>
        Cst53 = 53,

        /// <summary>
        /// CST 54 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas no Mercado
        /// Interno e de Exportação.
        /// </summary>
        Cst54 = 54,

        /// <summary>
        /// CST 55 - Operação com Direito a Crédito - Vinculada a Receitas Não-Tributadas no Mercado
        /// Interno e de Exportação.
        /// </summary>
        Cst55 = 55,

        /// <summary>
        /// CST 56 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas e
        /// Não-Tributadas no Mercado Interno, e de Exportação.
        /// </summary>
        Cst56 = 56,

        /// <summary>
        /// CST 60 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita
        /// Tributada no Mercado Interno.
        /// </summary>
        Cst60 = 60,

        /// <summary>
        /// CST 61 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita
        /// Não-Tributada no Mercado Interno.
        /// </summary>
        Cst61 = 61,

        /// <summary>
        /// CST 62 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita de Exportação.
        /// </summary>
        Cst62 = 62,

        /// <summary>
        /// CST 63 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e
        /// Não-Tributadas no Mercado Interno.
        /// </summary>
        Cst63 = 63,

        /// <summary>
        /// CST 64 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas no
        /// Mercado Interno e de Exportação.
        /// </summary>
        Cst64 = 64,

        /// <summary>
        /// CST 65 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Não-Tributadas no
        /// Mercado Interno e de Exportação.
        /// </summary>
        Cst65 = 65,

        /// <summary>
        /// CST 66 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e
        /// Não-Tributadas no Mercado Interno, e de Exportação.
        /// </summary>
        Cst66 = 66,

        /// <summary>
        /// CST 67 - Crédito Presumido - Outras Operações.
        /// </summary>
        Cst67 = 67,

        /// <summary>
        /// CST 70 - Operação de Aquisição sem Direito a Crédito.
        /// </summary>
        Cst70 = 70,

        /// <summary>
        /// CST 71 - Operação de Aquisição com Isenção.
        /// </summary>
        Cst71 = 71,

        /// <summary>
        /// CST 72 - Operação de Aquisição com Suspensão.
        /// </summary>
        Cst72 = 72,

        /// <summary>
        /// CST 73 - Operação de Aquisição a Alíquota Zero.
        /// </summary>
        Cst73 = 73,

        /// <summary>
        /// CST 74 - Operação de Aquisição sem Incidência da Contribuição.
        /// </summary>
        Cst74 = 74,

        /// <summary>
        /// CST 75 - Operação de Aquisição por Substituição Tributária.
        /// </summary>
        Cst75 = 75,

        /// <summary>
        /// CST 98 - Outras Operações de Entrada.
        /// </summary>
        Cst98 = 98,

        /// <summary>
        /// CST 99 - Outras Operações
        /// </summary>
        Cst99 = 99
    }
}