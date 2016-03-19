using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Especifica a Modalidade de Determinação da Base de Cálculo do ICMS
    /// </summary>
    
    
    public enum ModalidadeBaseCalculoIcms
    {
        /// <summary>
        /// Não Informado (-1).
        /// </summary>
        NaoEspecificado = -1,
        /// <summary>
        /// 0 - Margem do Valor Agregado em Porcentagem
        /// </summary>
        MargemAgregado = 0,
        /// <summary>
        /// 1 - Pauta em Valor
        /// </summary>
        Pauta = 1,
        /// <summary>
        /// 2 - Preço Tabelado Máximo em Valor
        /// </summary>
        PrecoTabelado = 2,
        /// <summary>
        /// 3 - Valor da Operação
        /// </summary>
        ValorOperacao = 3
    }
}
