using System.ComponentModel;
using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Formas de Pagamento
    /// </summary>
    
    
    public enum TipoPagamento
    {
        /// <summary>
        /// 01 - Dinheiro.
        /// </summary>
        [Description("Dinheiro")]
        Dinheiro = 1,
        /// <summary>
        /// 02 - Cheque.
        /// </summary>
        [Description("Cheque")]
        Cheque = 2,
        /// <summary>
        /// 03 - Cartão de Crédito.
        /// </summary>
        [Description("Cartão de Crédito")]
        CartaoCredito = 3,
        /// <summary>
        /// 04 - Cartão de Débito.
        /// </summary>
        [Description("Cartão de Débito")]
        CartaoDebito = 4,
        /// <summary>
        /// 05 - Crédito Loja.
        /// </summary>
        [Description("Crédito Loja")]
        CreditoLoja = 5,
        /// <summary>
        /// 10 - Vale Alimentação
        /// </summary>
        [Description("Vale Alientação")]
        ValeAlimentacao = 10,
        /// <summary>
        /// 11 - Vale Refeição.
        /// </summary>
        [Description("Vale Refeição")]
        ValeRefeicao = 11,
        /// <summary>
        /// 12 - Vale Presente.
        /// </summary>
        [Description("Vale Presente")]
        ValePresente = 12,
        /// <summary>
        /// 13 - Vale Combustível.
        /// </summary>
        [Description("Vale Combustível")]
        ValeCombustivel = 13,
        /// <summary>
        /// 99 - Outros.
        /// </summary>
        [Description("Outros")]
        Outros = 99
    }
}