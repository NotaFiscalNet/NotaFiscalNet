using System;
using System.ComponentModel;
using System.Xml;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Coleção de Produtos do Contribuinte e do Fisco. Informar no máximo 10 observações.
    /// </summary>
    public sealed class DeducaoCanaCollection : BaseCollection<DeducaoCana>, ISerializavel
    {
        private const int Capacidade = 10;

        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool IsDirty
        {
            get
            {
                foreach (var item in this)
                {
                    if (item.Modificado)
                        return true;
                }
                return false;
            }
        }

        void ISerializavel.Serializar(XmlWriter writer, NFe nfe)
        {
            foreach (var item in this)
            {
                if (item.Modificado)
                    item.Serializar(writer, nfe);
            }
        }

        /// <summary>
        /// Override para não permitir adicionar além da capacidade
        /// </summary>
        /// <param name="e">CancelEventArgs</param>
        /// <param name="item">Produto</param>
        protected override void PreAdd(CancelEventArgs e, DeducaoCana item)
        {
            if (Count == Capacidade)
                throw new ApplicationException(
                    string.Format("A capacidade máxima deste campo é de {0} dedução/deduções.", Capacidade));

            base.PreAdd(e, item);
        }
    }
}