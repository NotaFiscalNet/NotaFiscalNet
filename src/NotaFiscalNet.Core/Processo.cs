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
        private string _identificador = string.Empty;
        private OrigemProcesso _origemProcesso = OrigemProcesso.NaoEspecificado;

        /// <summary>
        /// [nProc] Retorna ou define o Identificador do Processo ou Ato Concessório
        /// </summary>
        [NFeField(ID = "Z11", FieldName = "nProc", DataType = "token", MinLength = 1, MaxLength = 60)]
        [CampoValidavel(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public string Identificador
        {
            get => _identificador;
	        set => _identificador = ValidationUtil.TruncateString(value, 60);
        }

        /// <summary>
        /// [indProc] Retorna ou define a Origem do Processo
        /// </summary>
        [NFeField(ID = "Z12", FieldName = "indProc")]
        [CampoValidavel(2, ChaveErroValidacao.CampoNaoPreenchido, ValorNaoPreenchido = OrigemProcesso.NaoEspecificado)]
        public OrigemProcesso OrigemProcesso
        {
            get => _origemProcesso;
	        set => _origemProcesso = ValidationUtil.ValidateEnum(value, "OrigemProcesso");
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => !string.IsNullOrEmpty(Identificador) ||
                                  OrigemProcesso != OrigemProcesso.NaoEspecificado;

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("procRef"); // Elemento 'procRef'
            writer.WriteElementString("nProc", SerializationUtil.ToToken(Identificador, 60));
            writer.WriteElementString("indProc", SerializationUtil.GetEnumValue(OrigemProcesso));
            writer.WriteEndElement(); // Elemento 'procRef'
        }
    }
}