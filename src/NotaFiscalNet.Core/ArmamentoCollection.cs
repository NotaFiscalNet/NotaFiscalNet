﻿using System;
using System.ComponentModel;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Coleção de Armamentos.
    /// </summary>
    public sealed class ArmamentoCollection : BaseCollection<Armamento>, INFeSerializable
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
        public bool IsDirty
        {
            get
            {
                foreach (var item in this)
                {
                    if (item.IsDirty)
                        return true;
                }
                return false;
            }
        }

        void INFeSerializable.Serialize(XmlWriter writer, NFe nfe)
        {
            foreach (var arma in this)
            {
                if (arma.IsDirty)
                    ((INFeSerializable)arma).Serialize(writer, nfe);
            }
        }

        protected override void PreAdd(CancelEventArgs e, Armamento item)
        {
            if (Count == Capacidade)
                throw new ApplicationException(string.Format("A capacidade máxima deste campo é de {0} item(ns).",
                    Capacidade));

            base.PreAdd(e, item);
        }

        private void Add(Armamento item)
        {
            Add(item);
        }

        private void Clear()
        {
            Clear();
        }

        private bool Contains(Armamento item)
        {
            return Contains(item);
        }

        private void Remove(Armamento item)
        {
            Remove(item);
        }

        private int Count
        {
            get { return Count; }
        }
    }
}