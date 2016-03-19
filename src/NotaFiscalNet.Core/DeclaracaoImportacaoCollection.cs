﻿using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Coleção Declarações de Importação do Produto
    /// </summary>
    public sealed class DeclaracaoImportacaoCollection : BaseCollection<DeclaracaoImportacao>, INFeSerializable
    {
        internal DeclaracaoImportacaoCollection(Produto produto)
        {
            Produto = produto;
        }

        /// <summary>
        /// Retorna a referência para o objeto Produto este objeto está contido.
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
            foreach (var declaracao in this)
            {
                if (declaracao.IsDirty)
                    ((INFeSerializable)declaracao).Serialize(writer, nfe);
            }
        }

        protected override void PostAdd(DeclaracaoImportacao item)
        {
            /// define a referência da declaração de Importação para o Objeto Produto no qual está inserido.
            item.Produto = Produto;
        }

        protected override void PostRemove(DeclaracaoImportacao item)
        {
            item.Produto = null;
        }
    }
}