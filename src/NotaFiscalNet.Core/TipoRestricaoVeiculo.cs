using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Especifica o Tipo de Restrição do Veículo.
    /// </summary>
    
    
    public enum TipoRestricaoVeiculo
    {
        NaoEspecificado = -1,
        /// <summary>
        /// 0 - Não há restrições.
        /// </summary>
        SemRestricoes = 0,
        /// <summary>
        /// 1 - Alienação Fiduciária.
        /// </summary>
        AlienacaoFiduciaria = 1,
        /// <summary>
        /// 2 - Arrendamento Mercantil.
        /// </summary>
        ArrendamentoMercantil = 2,
        /// <summary>
        /// 3 - Reserva de Domínio.
        /// </summary>
        ReservaDominio = 3,
        /// <summary>
        /// 4 - Penhor de Veículos.
        /// </summary>
        PenhorVeiculo = 4,
        /// <summary>
        /// 9 - Outras.
        /// </summary>
        Outras = 9
    }
}