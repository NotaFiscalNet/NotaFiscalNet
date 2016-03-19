using System;
using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Coleção de Observações do Contribuinte. Informar no máximo 10 observações.
    /// </summary>
    
    
    
    public sealed class ObservacaoContribuinteCollection : BaseCollection<ObservacaoContribuinte>, INFeSerializable
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
        public bool IsDirty
        {
            get
            {
                foreach (ObservacaoContribuinte item in this)
                {
                    if (item.IsDirty)
                        return true;
                }
                return false;
            }
        }

        #region INFeSerializable Members

        void INFeSerializable.Serialize(System.Xml.XmlWriter writer, NFe nfe)
        {
            foreach (ObservacaoContribuinte observacao in this)
            {
                if (observacao.IsDirty)
                    ((INFeSerializable)observacao).Serialize(writer, nfe);
            }
        }

        #endregion

    }
}
