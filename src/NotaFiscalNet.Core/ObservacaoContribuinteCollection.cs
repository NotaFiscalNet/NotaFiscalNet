﻿using System;
using System.Runtime.InteropServices;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Coleção de Observações do Contribuinte. Informar no máximo 10 observações.
    /// </summary>
    public sealed class ObservacaoContribuinteCollection : BaseCollection<ObservacaoContribuinte>, ISerializavel, IModificavel
    {
        /// <summary>
        /// Quantidade Máxima de Elementos
        /// </summary>
        private int capacidade = 10;

        /// <summary>
        /// Override para não permitir adicionar além da capacidade
        /// </summary>
        /// <param name="e">CancelEventArgs</param>
        /// <param name="item">Item sendo</param>
        protected override void PreAdd(System.ComponentModel.CancelEventArgs e, ObservacaoContribuinte item)
        {
            if (Count == capacidade)
                throw new ApplicationException(string.Format("A capacidade máxima deste campo é de {0} observações.", capacidade.ToString()));

            base.PreAdd(e, item);
        }

        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool Modificado
        {
            get
            {
                foreach (ObservacaoContribuinte item in this)
                {
                    if (item.Modificado)
                        return true;
                }
                return false;
            }
        }

        #region ISerializavel Members

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            foreach (ObservacaoContribuinte observacao in this)
            {
                if (observacao.Modificado)
                    ((ISerializavel)observacao).Serializar(writer, nfe);
            }
        }

        #endregion

    }
}
