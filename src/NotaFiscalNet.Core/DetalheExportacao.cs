using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using System;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// [detExport] Detalhes da Exportação.
    /// </summary>
    public class DetalheExportacao : ISerializavel, IModificavel
    {
        private string _numeroDrawback;

        /// <summary>
        /// [nDraw] Retorna ou define o Número do ato concessório de Drawback.
        /// </summary>
        [NFeField(ID = "I51", FieldName = "nDraw", DataType = "xs:string", Pattern = "[0-9]{0,11}")]
        public string NumeroDrawback
        {
            get => _numeroDrawback;
	        set
            {
                if (!ValidationUtil.ValidateRegex(value, "^[0-9]{0,11}$"))
                    throw new ArgumentException("O número do Ato Concessório de Drawback informado não é válido.");
                _numeroDrawback = value;
            }
        }

        /// <summary>
        /// [exportInd] Retorna a referência para as informações de exportação indireta.
        /// </summary>
        [NFeField(ID = "I52", FieldName = "exportInd")]
        public ExportacaoIndireta Detalhamento { get; } = new ExportacaoIndireta();

        public bool Modificado => !string.IsNullOrEmpty(NumeroDrawback) && Detalhamento.Modificado;

        public void Serializar(XmlWriter writer, INFe nfe)
        {
            if (!Modificado)
                return;

            writer.WriteStartElement("detExport");

            if (!string.IsNullOrEmpty(NumeroDrawback))
                writer.WriteElementString("nDraw", NumeroDrawback);

            if (Detalhamento.Modificado)
                ((ISerializavel)Detalhamento).Serializar(writer, nfe);

            writer.WriteEndElement();
        }
    }
}