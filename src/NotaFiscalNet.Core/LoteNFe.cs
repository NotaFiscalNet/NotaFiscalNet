using System;
using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa um Lote de Notas Fiscais Eletrônicas.
    /// </summary>
    
    
    
    public sealed class LoteNFe : BaseCollection<NFe>
    {
        private long _idLote;

        /// <summary>
        /// Quantidade máxima de Notas Fiscais Eletrônicas por lote.
        /// </summary>
        public const int MaxQuantidade = 50;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="LoteNFe"/>.
        /// </summary>
        /// <param name="idLote">Código identificador do Lote.</param>
        internal LoteNFe(long idLote)
        {
            if ( idLote < 1L || idLote > 999999999999999L )
                throw new ArgumentOutOfRangeException(nameof(idLote), idLote, "O código identificador do lote está fora da faixa permitida (de 1 até 15 posições).");

            IDLote = idLote;
        }

        /// <summary>
        /// Retorna ou define o Código Identificador do Lote de Notas Fiscais Eletrônicas.
        /// </summary>
        /// <remarks>O valor deve estar compreendido entre 1 e 999999999999999.</remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">Ocorre quando o valor informado está fora da faixa permitida (de 1 até 999999999999999).</exception>
        public long IDLote 
        {
            get { return _idLote; }
            set
            {
                if ( value < 1 || value > 999999999999999L )
                    throw new ArgumentOutOfRangeException("IDLote", value, "O valor informado para o Código Identificador do Lote não está dentro da faixa permitida (de 1 ate 999999999999999).");
                _idLote = value; 
            }
        }

        

        protected override void PreAdd(System.ComponentModel.CancelEventArgs e, NFe item)
        {
            if ( base.Count == Constants.MaxLoteItens )
                throw new ArgumentOutOfRangeException("NFe", "A quantidade máxima de Notas Fiscais Eletrônicas em um lote foi excedida (máx. 50).");

            base.PreAdd(e, item);
        }

        
    }

    
}
