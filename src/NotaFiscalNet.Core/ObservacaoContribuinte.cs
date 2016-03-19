﻿using System.Runtime.InteropServices;
using System.Xml;
using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa as Observações de Uso Livre do Contribuinte
    /// </summary>
    
    
    
    public sealed class ObservacaoContribuinte :  ISerializavel
    {
        #region Fields

        private string _campo = string.Empty;
        private string _texto = string.Empty;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Retorna ou define o Nome do Campo Livre
        /// </summary>
        [NFeField(ID = "Z05", FieldName = "xCampo", DataType = "token", MinLength = 1, MaxLength = 20, NodeType = XmlNodeType.Attribute)]
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public string Campo
        {
            get { return _campo; }
            set {
                _campo = ValidationUtil.TruncateString(value, 20);
           }
        }

        /// <summary>
        /// Retorna ou define o Texto do Campo Livre
        /// </summary>
        [NFeField(ID = "Z06", FieldName = "xTexto", DataType = "token", MinLength = 1, MaxLength = 60)]
        [ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public string Texto
        {
            get { return _texto; }
            set {
                _texto = ValidationUtil.TruncateString(value, 60);
            }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return
                    !string.IsNullOrEmpty(Campo) ||
                    !string.IsNullOrEmpty(Texto);
            }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Inicializa uma nova instância da classe ObservacaoContribuinte
        /// </summary>
        public ObservacaoContribuinte()
        {
        }

        #endregion Constructor

        #region ISerializavel Members

        void ISerializavel.Serializar(XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("obsCont"); // Elemento 'obsCont'
            writer.WriteAttributeString("xCampo", SerializationUtil.ToToken(Campo, 20));
            writer.WriteElementString("xTexto", SerializationUtil.ToToken(Texto, 60));
            writer.WriteEndElement(); // Elemento 'obsCont'
        }

        #endregion
    }
}
