using NotaFiscalNet.Core.Interfaces;
using System;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma lista de Reboques (parte sendo rebocada por um veículo com motor).
    /// </summary>
    /// <remarks>Permite até 5 itens na lista.</remarks>
    /// <exception cref="ApplicationException">Caso tente adicionar mais de 5 itens na lista.</exception>
    public sealed class ReboqueCollection : BaseCollection<VeiculoTransporte>, ISerializavel, IModificavel
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
        public bool Modificado
        {
            get
            {
                foreach (VeiculoTransporte item in this)
                {
                    if (item.Modificado)
                        return true;
                }
                return false;
            }
        }

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            foreach (VeiculoTransporte reboque in this)
            {
                if (reboque.Modificado)
                {
                    writer.WriteStartElement("reboque"); // Elemento 'reboque'
                    ((ISerializavel)reboque).Serializar(writer, nfe);
                    writer.WriteEndElement(); // Elemento 'reboque'
                }
            }
        }
    }
}