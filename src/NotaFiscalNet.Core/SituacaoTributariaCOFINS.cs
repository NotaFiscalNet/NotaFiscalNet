namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Lista dos Códigos de Situação Tributária (CST) do COFINS.
    /// </summary>

    public enum SituacaoTributariaCOFINS
    {
        /// <summary>
        /// Não Especificado
        /// </summary>
        NaoEspecificado = -1,

        /// <summary>
        /// 01 - Operação Tributável - Base de Cálculo igual ao Valor da Operação com Alíquota Normal
        /// (Cumulativo / Não Cumulativo).
        /// </summary>
        Cst01 = 1,

        /// <summary>
        /// 02 - Operação Tributável - Base de Cálculo igual ao Valor da Operação com Alíquota Diferenciada.
        /// </summary>
        Cst02 = 2,

        /// <summary>
        /// 03 - Operação Tributável - Base de Cálculo igual a Quantidade Vendida x Alíquota por
        /// Unidade de Produto.
        /// </summary>
        Cst03 = 3,

        /// <summary>
        /// 04 - Operação Tributável - Tributação Monofásica com Alíquota Zero.
        /// </summary>
        Cst04 = 4,

        /// <summary>
        /// 06 - Operação Tributável - Alíquota Zero.
        /// </summary>
        Cst06 = 6,

        /// <summary>
        /// 07 - Operação Isenta de Contribuição.
        /// </summary>
        Cst07 = 7,

        /// <summary>
        /// 08 - Operação Sem Incidência da Contribuição.
        /// </summary>
        Cst08 = 8,

        /// <summary>
        /// 09 - Operação com Suspensão da Contribuição.
        /// </summary>
        Cst09 = 9,

        /// <summary>
        /// 49 - Outras Operações de Saída
        /// </summary>
        Cst49 = 49,

        /// <summary>
        /// 50 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Tributada no
        /// Mercado Interno
        /// </summary>
        Cst50 = 50,

        /// <summary>
        /// 51 - Operação com Direito a Crédito – Vinculada Exclusivamente a Receita Não Tributada no
        /// Mercado Interno
        /// </summary>
        Cst51 = 51,

        /// <summary>
        /// 52 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita de Exportação
        /// </summary>
        Cst52 = 52,

        /// <summary>
        /// 53 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no
        /// Mercado Interno
        /// </summary>
        Cst53 = 53,

        /// <summary>
        /// 54 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas no Mercado Interno
        /// e de Exportação
        /// </summary>
        Cst54 = 54,

        /// <summary>
        /// 55 - Operação com Direito a Crédito - Vinculada a Receitas Não-Tributadas no Mercado
        /// Interno e de Exportação
        /// </summary>
        Cst55 = 55,

        /// <summary>
        /// 56 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no
        /// Mercado Interno, e de Exportação
        /// </summary>
        Cst56 = 56,

        /// <summary>
        /// 60 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita
        /// Tributada no Mercado Interno
        /// </summary>
        Cst60 = 60,

        /// <summary>
        /// 61 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita
        /// Não-Tributada no Mercado Interno
        /// </summary>
        Cst61 = 61,

        /// <summary>
        /// 62 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita de Exportação
        /// </summary>
        Cst62 = 62,

        /// <summary>
        /// 63 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e
        /// Não-Tributadas no Mercado Interno
        /// </summary>
        Cst63 = 63,

        /// <summary>
        /// 64 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas no Mercado
        /// Interno e de Exportação
        /// </summary>
        Cst64 = 64,

        /// <summary>
        /// 65 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Não-Tributadas no
        /// Mercado Interno e de Exportação
        /// </summary>
        Cst65 = 65,

        /// <summary>
        /// 66 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e
        /// Não-Tributadas no Mercado Interno, e de Exportação
        /// </summary>
        Cst66 = 66,

        /// <summary>
        /// 67 - Crédito Presumido - Outras Operações
        /// </summary>
        Cst67 = 67,

        /// <summary>
        /// 70 - Operação de Aquisição sem Direito a Crédito
        /// </summary>
        Cst70 = 70,

        /// <summary>
        /// 71 - Operação de Aquisição com Isenção
        /// </summary>
        Cst71 = 71,

        /// <summary>
        /// 72 - Operação de Aquisição com Suspensão
        /// </summary>
        Cst72 = 72,

        /// <summary>
        /// 73 - Operação de Aquisição a Alíquota Zero
        /// </summary>
        Cst73 = 73,

        /// <summary>
        /// 74 - Operação de Aquisição sem Incidência da Contribuição
        /// </summary>
        Cst74 = 74,

        /// <summary>
        /// 75 - Operação de Aquisição por Substituição Tributária
        /// </summary>
        Cst75 = 75,

        /// <summary>
        /// 98 - Outras Operações de Entrada
        /// </summary>
        Cst98 = 98,

        /// <summary>
        /// 99 - Outras Operações
        /// </summary>
        Cst99 = 99,
    }
}