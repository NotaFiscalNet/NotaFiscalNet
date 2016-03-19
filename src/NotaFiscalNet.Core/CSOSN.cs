namespace NotaFiscalNet.Core
{
    /// <summary>
    /// CSOSN (Código de Situação da Operação do Simples Nacional).
    /// </summary>
    public enum CSOSN
    {
        /// <summary>
        /// Não Especificado
        /// </summary>
        NaoEspecificado = -1,

        /// <summary>
        /// Tributada pelo Simples Nacional com permissão de crédito.
        /// </summary>
        SN101 = 101,

        /// <summary>
        /// Tributada pelo Simples Nacional sem permissão de crédito.
        /// </summary>
        SN102 = 102,

        /// <summary>
        /// Isenção do ICMS no Simples Nacional para faixa de receita bruta.
        /// </summary>
        SN103 = 103,

        /// <summary>
        /// Tributada pelo Simples Nacional com permissão de crédito e com cobrança do ICMS por
        /// substituição tributária.
        /// </summary>
        SN201 = 201,

        /// <summary>
        /// Tributada pelo Simples Nacional sem permissão de crédito e com cobrança do ICMS por
        /// substituição tributária.
        /// </summary>
        SN202 = 202,

        /// <summary>
        /// Isenção do ICMS no Simples Nacional para faixa de receita bruta e com cobrança do ICMS
        /// por substituição tributária.
        /// </summary>
        SN203 = 203,

        /// <summary>
        /// Imune.
        /// </summary>
        SN300 = 300,

        /// <summary>
        /// Não tributada pelo Simples Nacional.
        /// </summary>
        SN400 = 400,

        /// <summary>
        /// ICMS cobrado anteriormente por substituição tributária (substituído) ou por antecipação.
        /// </summary>
        SN500 = 500,

        /// <summary>
        /// Outros.
        /// </summary>
        /// <remarks>
        /// Classificam-se neste código as demais operações que não se enquadrem nos códigos 101,
        /// 102, 103, 201, 202, 203, 300, 400 e 500.
        /// </remarks>
        SN900 = 900
    }
}