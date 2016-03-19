using System;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Coleção de Armamentos.
    /// </summary>
    public sealed class FornecimentoDiarioCanaCollection : BaseCollection<FornecimentoDiarioCana>, ISerializavel,IModificavel
    {
        private const int Capacidade = 31;

        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool Modificado
        {
            get { return this.Any(x => x.Modificado); }
        }

        void ISerializavel.Serializar(XmlWriter writer, NFe nfe)
        {
            foreach (var item in this)
            {
                if (item.Modificado)
                    ((ISerializavel)item).Serializar(writer, nfe);
            }
        }

        protected override void PreAdd(CancelEventArgs e, FornecimentoDiarioCana item)
        {
            if (Count == Capacidade)
                throw new ApplicationException(
                    $"A capacidade máxima deste campo é de {Capacidade} Fornecimentos Diários de Cana.");
            base.PreAdd(e, item);
        }
    }
}