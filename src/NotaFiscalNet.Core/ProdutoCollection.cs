using NotaFiscalNet.Core.Interfaces;
using System;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Coleção de Produtos do Contribuinte e do Fisco. Informar no máximo 10 observações.
    /// </summary>
    public sealed class ProdutoCollection : BaseCollection<Produto>, ISerializavel, IModificavel
    {
        protected override void PostAdd(Produto item)
        {
            if (item.NumeroItem != 0)
                return;

            int max = GetMaxNumeroItem();
            item.NumeroItem = ++max;
        }

        protected override void PostRemove(Produto item)
        {
            /// Quando remover um ítem da coleção, todos os itens serão re-organizados.
            int count = 0;
            foreach (Produto p in this)
            {
                count++;
                p.NumeroItem = count;
            }
        }

        private int GetMaxNumeroItem()
        {
            int max = 0;
            foreach (Produto p in this)
            {
                if (p.NumeroItem > max)
                    max = p.NumeroItem;
            }
            return max;
        }

        /// <summary>
        /// Quantidade Máxima de Elementos
        /// </summary>
        private const int Capacidade = 990;

        /// <summary>
        /// Override para não permitir adicionar além da capacidade
        /// </summary>
        /// <param name="e">CancelEventArgs</param>
        /// <param name="item">Produto</param>
        protected override void PreAdd(System.ComponentModel.CancelEventArgs e, Produto item)
        {
            if (Count == Capacidade)
                throw new ApplicationException($"A capacidade máxima deste campo é de {Capacidade} produtos.");

            base.PreAdd(e, item);
        }

        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool Modificado
        {
            get
            {
                foreach (Produto item in this)
                {
                    if (item.Modificado)
                        return true;
                }
                return false;
            }
        }

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            foreach (Produto produto in this)
            {
                if (produto.Modificado)
                    produto.Serializar(writer, nfe);
            }
        }
    }
}