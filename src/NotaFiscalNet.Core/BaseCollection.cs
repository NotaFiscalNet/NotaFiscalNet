using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Coleção genérica usada como base para outras coleções.
    /// </summary>
    /// <typeparam name="T">Tipo</typeparam>
    public abstract class BaseCollection<T> : ICollection<T>
    {
        private readonly IList<T> _items;

        protected BaseCollection()
        {
            _items = new List<T>();
        }

        internal BaseCollection(IEnumerable<T> collectionReadOnly)
            : this()
        {
            foreach (var item in collectionReadOnly)
                _items.Add(item);
            IsReadOnly = true;
        }

        /// <summary>
        /// Retorna um determinado item com base na posição ordinal.
        /// </summary>
        /// <param name="index">Índice do item a ser retornado.</param>
        /// <returns>Retorna o item no qual o índice se refere.</returns>
        public T this[int index] => _items[index];

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        protected virtual void PreAdd(CancelEventArgs e, T item)
        {
        }

        protected virtual void PostAdd(T item)
        {
        }

        protected virtual void PreRemove(CancelEventArgs e, T item)
        {
        }

        protected virtual void PostRemove(T item)
        {
        }

        /// <summary>
        /// Adiciona um novo item no final da coleção.
        /// </summary>
        /// <param name="item">Item a ser adicionado.</param>
        public void Add(T item)
        {
            var e = new CancelEventArgs();
            PreAdd(e, item);
            if (e.Cancel)
                return;

            if (_items.IsReadOnly)
                throw new NotSupportedException("A coleção foi marcada como Apenas-Leitura.");

            _items.Add(item);

            PostAdd(item);
        }

        /// <summary>
        /// Remove todos os itens da lista.
        /// </summary>
        public void Clear()
        {
            if (_items.IsReadOnly)
                throw new NotSupportedException("A coleção foi marcada como Apenas-Leitura.");

            _items.Clear();
        }

        /// <summary>
        /// Checa se um determinado item já consta na lista.
        /// </summary>
        /// <param name="item">Item a ser verificado.</param>
        /// <returns>True caso o item conste na lista. Caso contrário retorna False.</returns>
        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        /// <summary>
        /// Realiza uma cópia dos itens na lista para um Array do mesmo tipo do item.
        /// </summary>
        /// <param name="array">Referência para o array que será preenchido com os itens da lista.</param>
        /// <param name="arrayIndex">Posição do array onde os itens começarão a ser inseridos.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Retora a quantidade de Itens na lista.
        /// </summary>
        public int Count => _items.Count;

        /// <summary>
        /// Retorna o valor indicando se a lista é apenas-leitura ou não.
        /// </summary>
        public bool IsReadOnly { get; }

        /// <summary>
        /// Remove um determinado item da lista.
        /// </summary>
        /// <param name="item">Item a ser removido da lista.</param>
        /// <returns>Retorna True caso o Item tenha sido removido com sucesso da lista.</returns>
        public bool Remove(T item)
        {
            if (_items.IsReadOnly)
                throw new NotSupportedException("A coleção foi marcada como Apenas-Leitura.");
            var index = _items.IndexOf(item);
            if (index < 0)
                return false;

            var e = new CancelEventArgs();
            PreRemove(e, item);
            if (e.Cancel)
                return false;

            _items.RemoveAt(index);

            PostRemove(item);
            return true;
        }
    }
}