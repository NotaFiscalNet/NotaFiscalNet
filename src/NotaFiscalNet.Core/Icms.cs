﻿using NotaFiscalNet.Core.Utils;
using System.Xml;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Classe base para implementação do detalhamento do ICMS.
    /// </summary>
    public abstract class Icms : ISerializavel
    {
        private OrigemMercadoria _origem;

        /// <summary>
        /// [orig] Retorna ou define a Origem da Mercadoria.
        /// </summary>
        public OrigemMercadoria Origem
        {
            get { return _origem; }
            set
            {
                ValidationUtil.ValidateEnum(value, "Origem");
                _origem = value;
            }
        }

        /// <summary>
        /// Retorna ou define o Códido da Situação Tributária do ICMS.
        /// </summary>
        public virtual SituacaoTributariaICMS CST { get; protected set; }

        void ISerializavel.Serializar(XmlWriter writer, NFe nfe)
        {
            SerializeInternal(writer, nfe);
        }

        protected abstract void SerializeInternal(XmlWriter writer, NFe nfe);
    }
}