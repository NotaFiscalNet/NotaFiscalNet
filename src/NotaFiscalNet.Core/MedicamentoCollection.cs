using NotaFiscalNet.Core.Interfaces;
using System;
using System.ComponentModel;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma lista de Medicamentos.
    /// </summary>
    public sealed class MedicamentoCollection : BaseCollection<Medicamento>, ISerializavel, IModificavel
    {
        private const int Capacidade = 500;

        private Produto _produto;

        internal MedicamentoCollection(Produto produto)
        {
            _produto = produto;
        }

        /// <summary>
        /// Retorna a referência para o Produto no qual a coleção está contida.
        /// </summary>
        internal Produto Produto => _produto;

        protected override void PreAdd(CancelEventArgs e, Medicamento item)
        {
            if (Count == Capacidade)
                throw new ApplicationException($"A capacidade máxima deste campo é de {Capacidade} medicamento(s).");

            base.PreAdd(e, item);
        }

        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool Modificado
        {
            get
            {
                foreach (Medicamento item in this)
                {
                    if (item.Modificado)
                        return true;
                }
                return false;
            }
        }

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            foreach (Medicamento medicamento in this)
            {
                if (medicamento.Modificado)
                    ((ISerializavel)medicamento).Serializar(writer, nfe);
            }
        }
    }
}