using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Indica a modalidade de venda do Veículo Novo.
    /// </summary>
    
    
    public enum TipoOperacaoVendaVeiculo
    {
        /// <summary>
        /// Não Especificado
        /// </summary>
        NaoEspecificado = -1,
        /// <summary>
        /// Venda Concessionária.
        /// </summary>
        Concessionaria = 0,
        /// <summary>
        /// Faturamento Direto.
        /// </summary>
        FaturamentoDireto = 1,
        /// <summary>
        /// Venda Direta.
        /// </summary>
        VendaDireta = 2,
        /// <summary>
        /// Outras modalidades.
        /// </summary>
        Outros = 0
    }
}
