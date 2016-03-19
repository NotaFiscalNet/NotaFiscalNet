using System;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa as informações de emissão da DANFE.
    /// </summary>
    public sealed class ConfiguracaoDANFE
    {
        /// <summary>
        /// Salva o relatório em formato PDF
        /// </summary>
        public bool SalvaPDF { get; set; } = true;

        /// <summary>
        /// Mostra o progresso
        /// </summary>
        public bool MostraProgresso { get; set; } = false;

        /// <summary>
        /// Caminho onde o PDF será salvo
        /// </summary>
        public string CaminhoPDF { get; set; } = string.Empty;

        /// <summary>
        /// Imprime o relatório
        /// </summary>
        public bool ImprimePDF { get; set; } = true;

        /// <summary>
        /// Nome da Impressora para imprimir
        /// </summary>
        public string NomeImpressora { get; set; } = string.Empty;

        /// <summary>
        /// Número de cópias para imprimir
        /// </summary>
        public short NumeroCopias { get; set; } = 1;

        /// <summary>
        /// Imagem da empresa impresso na DANFE
        /// </summary>
        public byte[] Imagem { get; set; } = null;

        /// <summary>
        /// Numero do Protocolo de Autorização da NFe
        /// </summary>
        public long? NumeroProtocolo { get; set; } = 0;

        /// <summary>
        /// Data da Autorização da NFe
        /// </summary>
        public DateTime? DataAutorizacao { get; set; } = null;
    }
}