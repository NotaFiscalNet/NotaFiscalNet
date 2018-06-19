using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;


namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o Destinatário da Nota Fiscal Eletrônica.
    /// </summary>

    public sealed class InformacoesAdicionaisNFe : ISerializavel, IModificavel
    {
        private string _informacoesComplementaresFisco = string.Empty;
        private string _informacoesComplementaresContribuinte = string.Empty;

	    /// <summary>
        /// [infAdFisco] Retorna ou define Informações Complementares de Interesse do Fisco. Opcional.
        /// </summary>
        [NFeField(ID = "Z02", FieldName = "infAdFisco", DataType = "TString", MinLength = 1, MaxLength = 2000, Pattern = @"[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}", Opcional = true)]
        [CampoValidavel(1, Opcional = true)]
        public string InformacoesComplementaresFisco
        {
            get => _informacoesComplementaresFisco;
	        set => _informacoesComplementaresFisco = ValidationUtil.TruncateString(value, 2000);
        }

        /// <summary>
        /// [infCpl] Retorna ou define Informações Complementares de Interesse do Contribuinte. Opcional.
        /// </summary>
        [NFeField(ID = "Z03", FieldName = "infCpl", DataType = "TString", MinLength = 1, MaxLength = 5000, Pattern = @"[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}", Opcional = true)]
        [CampoValidavel(2, Opcional = true)]
        public string InformacoesComplementaresContribuinte
        {
            get => _informacoesComplementaresContribuinte;
	        set => _informacoesComplementaresContribuinte = ValidationUtil.TruncateString(value, 5000);
        }

        /// <summary>
        /// [obsCont] Retorna as Informações de uso Livre do Contribuinte. Opcional.
        /// </summary>
        [NFeField(ID = "Z04", FieldName = "obsCont", MinLength = 1, MaxLength = 10, Opcional = true)]
        [CampoValidavel(3, ChaveErroValidacao.CollectionMinValue)]
        public ObservacaoContribuinteCollection ObservacoesContribuinte { get; } = new ObservacaoContribuinteCollection();

	    /// <summary>
        /// [obsFisco] Retorna as Informações de uso Livre do Fisco. Opcional.
        /// </summary>
        [NFeField(ID = "Z07", FieldName = "obsFisco", MinLength = 1, MaxLength = 10, Opcional = true)]
        [CampoValidavel(4, ChaveErroValidacao.CollectionMinValue)]
        public ObservacaoFiscoCollection ObservacoesFisco { get; } = new ObservacaoFiscoCollection();

	    /// <summary>
        /// [procRef] Retorna os Processos Referenciados. Opcional.
        /// </summary>
        [NFeField(ID = "Z10", FieldName = "procRef", Opcional = true)]
        [CampoValidavel(5, ChaveErroValidacao.CollectionMinValue)]
        public ProcessoCollection Processos { get; } = new ProcessoCollection();

	    /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => !string.IsNullOrEmpty(InformacoesComplementaresContribuinte) ||
                                  !string.IsNullOrEmpty(InformacoesComplementaresFisco) ||
                                  ObservacoesContribuinte.Modificado ||
                                  ObservacoesFisco.Modificado ||
                                  Processos.Modificado;

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("infAdic"); // Elemento 'infAdic'

            if (!string.IsNullOrEmpty(InformacoesComplementaresFisco))
                writer.WriteElementString("infAdFisco", SerializationUtil.ToTString(InformacoesComplementaresFisco, 2000));
            if (!string.IsNullOrEmpty(InformacoesComplementaresContribuinte))
                writer.WriteElementString("infCpl", SerializationUtil.ToTString(InformacoesComplementaresContribuinte, 5000));
            if (ObservacoesContribuinte.Modificado)
                ((ISerializavel)ObservacoesContribuinte).Serializar(writer, nfe);
            if (ObservacoesFisco.Modificado)
                ((ISerializavel)ObservacoesFisco).Serializar(writer, nfe);
            if (Processos.Modificado)
                ((ISerializavel)Processos).Serializar(writer, nfe);

            writer.WriteEndElement(); // Elemento 'infAdic'
        }
    }
}