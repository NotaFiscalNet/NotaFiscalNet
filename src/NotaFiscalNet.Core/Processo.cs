using System.Runtime.InteropServices;
using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa um Processo Referenciado
    /// </summary>
    public sealed class Processo : ISerializavel, IModificavel
    {
        #region Fields

        private string _identificador = string.Empty;
        private OrigemProcesso _origemProcesso = OrigemProcesso.NaoEspecificado;

        #endregion Fields

        #region Properties

        /// <summary>
        /// [nProc] Retorna ou define o Identificador do Processo ou Ato Concessório
        /// </summary>
        [NFeField(ID = "Z11", FieldName = "nProc", DataType = "token", MinLength = 1, MaxLength = 60)]
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public string Identificador
        {
            get { return _identificador; }
            set {
                _identificador = ValidationUtil.TruncateString(value, 60);
            }
        }

        /// <summary>
        /// [indProc] Retorna ou define a Origem do Processo
        /// </summary>
        [NFeField(ID = "Z12", FieldName = "indProc")]
        [ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido, DefaultValue=OrigemProcesso.NaoEspecificado)]
        public OrigemProcesso OrigemProcesso
        {
            get { return _origemProcesso; }
            set {
                _origemProcesso = ValidationUtil.ValidateEnum<OrigemProcesso>(value, "OrigemProcesso");
            }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado
        {
            get
            {
                return
                    !string.IsNullOrEmpty(Identificador) ||
                    OrigemProcesso != OrigemProcesso.NaoEspecificado;
            }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Inicializa uma nova instância da classe Processo
        /// </summary>
        public Processo()
        {
        }

        #endregion Constructor

        #region ISerializavel Members

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("procRef"); // Elemento 'procRef'
            writer.WriteElementString("nProc", SerializationUtil.ToToken(Identificador, 60));
            writer.WriteElementString("indProc", SerializationUtil.GetEnumValue<OrigemProcesso>(OrigemProcesso));
            writer.WriteEndElement(); // Elemento 'procRef'
        }

        #endregion
    }
}
