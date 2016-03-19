using System;
using System.Collections.Generic;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma lista de Notas Fiscais Eletrônicas.
    /// </summary>

    public sealed class NFeCollection : BaseCollection<NFe>
    {
        private List<NFe> _list;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="NFeCollection"/>.
        /// </summary>
        public NFeCollection()
        {
            _list = new List<NFe>();
        }

        /// <summary>
        /// Gera uma lista de Lotes contendo até no máximo 50 Notas Fiscais Eletrônicas em cada lote
        /// e respeitando o limite de 500KB (referente ao xml).
        /// </summary>
        /// <remarks>
        /// O primeiro LoteNFe gerado conterá o valor informado no parâmetro <paramref
        /// name="idLoteInicial"/>. Os lotes posteriormente gerados receberão <paramref
        /// name="idLoteInicial"/> + 1.
        /// </remarks>
        /// <param name="idLoteInicial">Código identificador inicial dos Lotes gerados.</param>
        /// <returns>Lista de Lotes de Nota Fiscal Eletrônica.</returns>
        public LoteNFeCollection GerarLotes(long idLoteInicial)
        {
            if (idLoteInicial < 1L || idLoteInicial > 999999999999999L)
                throw new ArgumentOutOfRangeException(nameof(idLoteInicial), idLoteInicial, "O código identificador do Lote deve estar compreendido entre 1 e 999999999999999.");

            long idLote = idLoteInicial;

            LoteNFe lote = null;
            LoteNFeCollection lotes = new LoteNFeCollection();

            foreach (NFe item in this)
            {
                if (lote == null)
                {
                    lote = new LoteNFe(idLoteInicial);
                    lotes.Add(lote);
                }

                if (lote.Count == Constants.MaxLoteItens)
                {
                    lotes.Add(lote);
                    idLote++;
                    lote = new LoteNFe(idLote);
                }

                lote.Add(item);
            }
            return lotes;
        }
    }
}