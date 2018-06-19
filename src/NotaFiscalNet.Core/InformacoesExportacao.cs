using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representam as informações de Exportação da Nota Fiscal Eletrônica.
    /// </summary>
    public sealed class InformacoesExportacao : ISerializavel, IModificavel
    {
        private UfIBGE _unidadeFederativa = UfIBGE.NaoEspecificado;
        private string _localEmbarque;
        private string _localDespacho;

        /// <summary>
        /// [UFSaidaPais] Retorna ou define a Sigla da UF de Embarque ou de transposição de fronteira.
        /// </summary>
        public UfIBGE UnidadeFederativa
        {
            get => _unidadeFederativa;
	        set => _unidadeFederativa = ValidationUtil.ValidateEnum(value, "UnidadeFederativa");
        }

        /// <summary>
        /// [xLocExporta] Retorna ou define a Descrição do Local de Embarque ou de transposição de fronteira.
        /// </summary>
        public string LocalEmbarque
        {
            get => _localEmbarque;
	        set => _localEmbarque = ValidationUtil.TruncateString(value, 60);
        }

        /// <summary>
        /// [xLocDespacho] Retorna ou define a Descrição do Local de Despacho.
        /// </summary>
        public string LocalDespacho
        {
            get => _localDespacho;
	        set => _localDespacho = ValidationUtil.TruncateString(value, 60);
        }

        /// <summary>
        /// Retorna se a classe foi modificada.
        /// </summary>
        public bool Modificado => UnidadeFederativa != UfIBGE.NaoEspecificado ||
                                  !string.IsNullOrEmpty(LocalEmbarque) ||
                                  !string.IsNullOrEmpty(LocalDespacho);

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("exporta");

            writer.WriteElementString("UFSaidaPais", UnidadeFederativa.ToString());
            writer.WriteElementString("xLocExporta", SerializationUtil.ToToken(LocalEmbarque, 60));
            if (!string.IsNullOrEmpty(LocalDespacho))
                writer.WriteElementString("xLocDespacho", SerializationUtil.ToToken(LocalDespacho, 60));

            writer.WriteEndElement();
        }
    }
}