namespace NotaFiscalNet.Core.Transmissao
{
    /// <summary>
    /// Recibo de Entrega do Lote de NF-e's.
    /// </summary>
    public class ReciboLoteNFe
    {
        /// <summary>
        /// Retorna ou define o número do recibo de entrega da lote de NF-e's.
        /// </summary>
        public string NumeroRecibo { get; set; }

        /// <summary>
        /// Retorna ou define o tempo médio (em segundos) de resposta do serviço (nos últimos 5 minutos).
        /// </summary>
        public int TempoMedio { get; set; }
    }
}