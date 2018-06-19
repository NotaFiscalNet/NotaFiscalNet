using NotaFiscalNet.Core.Interfaces;
using System;
using System.ComponentModel;
using System.Linq;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Coleção de Armamentos.
    /// </summary>
    public sealed class FornecimentoDiarioCanaCollection : BaseCollection<FornecimentoDiarioCana>, ISerializavel, IModificavel
    {
        private const int Capacidade = 31;

        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool Modificado => this.Any(item => item.Modificado);

        public void Serializar(XmlWriter writer, INFe nfe)
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