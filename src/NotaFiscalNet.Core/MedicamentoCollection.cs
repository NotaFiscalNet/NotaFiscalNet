using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma lista de Medicamentos.
    /// </summary>
    
    
    
    public sealed class MedicamentoCollection : BaseCollection<Medicamento>, ISerializavel
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
        internal Produto Produto
        {
            get { return _produto; }
        }

        protected override void PreAdd(CancelEventArgs e, Medicamento item)
        {
            if (Count == Capacidade)
                throw new ApplicationException(string.Format("A capacidade máxima deste campo é de {0} medicamento(s).", Capacidade));

            base.PreAdd(e, item);
        }

        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool IsDirty
        {
            get
            {
                foreach (Medicamento item in this)
                {
                    if (item.IsDirty)
                        return true;
                }
                return false;
            }
        }

        
        #region ISerializavel Members

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            foreach (Medicamento medicamento in this)
            {
                if (medicamento.IsDirty)
                    ((ISerializavel)medicamento).Serializar(writer, nfe);
            }
        }

        #endregion
    }
}
