using NotaFiscalNet.Core.Interfaces;
using System;
using System.ComponentModel;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Coleção de Armamentos.
    /// </summary>
    public sealed class ArmamentoCollection : BaseCollection<Armamento>, ISerializavel, IModificavel
    {
        private const int Capacidade = 500;

        internal ArmamentoCollection(Produto produto)
        {
            Produto = produto;
        }

        /// <summary>
        /// Retorna a referência para o Produto no qual a coleção está contida.
        /// </summary>
        internal Produto Produto { get; }

        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool Modificado
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

        public void Serializar(XmlWriter writer, INFe nfe)
        {
            foreach (var arma in this)
            {
                if (arma.Modificado)
                    ((ISerializavel)arma).Serializar(writer, nfe);
            }
        }

        protected override void PreAdd(CancelEventArgs e, Armamento item)
        {
            if (Count == Capacidade)
                throw new ApplicationException($"A capacidade máxima deste campo é de {Capacidade} item(ns).");

            base.PreAdd(e, item);
        }
    }
}