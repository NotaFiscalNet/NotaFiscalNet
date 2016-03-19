namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Lista dos Códigos de Situação Tributária (CST) do ICMS.
    /// </summary>

    public enum SituacaoTributariaICMS
    {
        /// <summary>
        /// Não Especificado
        /// </summary>
        NaoEspecificado = -1,

        /// <summary>
        /// 00 - Tributada integralmente
        /// </summary>
        Cst00 = 0,

        /// <summary>
        /// 10 - Tributada e com cobrança do ICMS por substituição tributária
        /// </summary>
        Cst10 = 10,

        /// <summary>
        /// 20 - Com redução de base de cálculo
        /// </summary>
        Cst20 = 20,

        /// <summary>
        /// 30 - Isenta ou não tributada e com cobrança do ICMS por substituição tributária
        /// </summary>
        Cst30 = 30,

        /// <summary>
        /// 40 - Isenta
        /// </summary>
        Cst40 = 40,

        /// <summary>
        /// 41 - Não tributada
        /// </summary>
        Cst41 = 41,

        /// <summary>
        /// 50 - Suspensão
        /// </summary>
        Cst50 = 50,

        /// <summary>
        /// 51 - Diferimento. A exigência do preenchimento das informações do ICMS diferido fica à
        /// critério de cada UF.
        /// </summary>
        Cst51 = 51,

        /// <summary>
        /// 60 - ICMS cobrado anteriormente por substituição tributária
        /// </summary>
        Cst60 = 60,

        /// <summary>
        /// 70 - Com redução de base de cálculo e cobrança do ICMS por substituição tributária
        /// </summary>
        Cst70 = 70,

        /// <summary>
        /// 90 - Outros
        /// </summary>
        Cst90 = 90,

        /// <summary>
        /// 101- Tributada pelo Simples Nacional com permissão de crédito
        /// </summary>
        CSOSN101 = 101,

        /// <summary>
        /// 102 - Tributada pelo Simples Nacional sem permissão de crédito.
        /// </summary>
        CSOSN102 = 102,

        /// <summary>
        /// 103 – Isenção do ICMS no Simples Nacional para faixa de receita bruta.
        /// </summary>
        CSOSN103 = 103,

        /// <summary>
        /// 300 – Imune (Simples Nacional).
        /// </summary>
        CSOSN300 = 300,

        /// <summary>
        /// 400 – Não tributada pelo Simples Nacional
        /// </summary>
        CSOSN400 = 400,

        /// <summary>
        /// 201 - Tributada pelo Simples Nacional com permissão de crédito e com cobrança do ICMS por
        /// Substituição Tributária
        /// </summary>
        CSOSN201 = 201,

        /// <summary>
        /// 202 - Tributada pelo Simples Nacional sem permissão de crédito e com cobrança do ICMS por
        /// Substituição Tributária
        /// </summary>
        CSOSN202 = 202,

        /// <summary>
        /// 203 - Isenção do ICMS nos Simples Nacional para faixa de receita bruta e com cobrança do
        /// ICMS por Substituição Tributária
        /// </summary>
        CSOSN203 = 203,

        /// <summary>
        /// 500 – ICMS cobrado anterirmente por substituição tributária (substituído) ou por antecipação
        /// </summary>
        CSOSN500 = 500,

        /// <summary>
        /// 900 - Tributação pelo ICMS 900 - Outros
        /// </summary>
        CSOSN900 = 900
    }
}