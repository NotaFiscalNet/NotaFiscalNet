using System;
using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma lista de Reboques (parte sendo rebocada por um veículo com motor).
    /// </summary>
    /// <remarks>Permite até 5 itens na lista.</remarks>
    /// <exception cref="ApplicationException">Caso tente adicionar mais de 5 itens na lista.</exception>
    
    
    
    public sealed class ReboqueCollection : BaseCollection<VeiculoTransporte>, INFeSerializable
    {
        /// <summary>
        /// Quantidade Máxima de Elementos
        /// </summary>
        private int capacidade = 5;

        /// <summary>
        /// Override para não permitir adicionar além da capacidade
        /// </summary>
        /// <param name="e">CancelEventArgs</param>
        /// <param name="item"></param>
        protected override void PreAdd(System.ComponentModel.CancelEventArgs e, VeiculoTransporte item)
        {
            if (Count == capacidade)
                throw new ApplicationException("Não é permitido adicionar mais de " + capacidade + " itens na lista de Veículo-Reboque.");
        }

        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool IsDirty
        {
            get
            {
                foreach (VeiculoTransporte item in this)
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
            foreach (VeiculoTransporte reboque in this)
            {
                if (reboque.IsDirty)
                {
                    writer.WriteStartElement("reboque"); // Elemento 'reboque'
                    ((INFeSerializable)reboque).Serialize(writer, nfe);
                    writer.WriteEndElement(); // Elemento 'reboque'
                }
            }
        }

        #endregion

    }
}
