using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using NotaFiscalNet.Core.Validacao.Validators;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o Destinatário da Nota Fiscal Eletrônica.
    /// </summary>

    public sealed class InformacoesAdicionaisNFe : ISerializavel, IModificavel
    {
        private string _informacoesComplementaresFisco = string.Empty;
        private string _informacoesComplementaresContribuinte = string.Empty;
        private ObservacaoContribuinteCollection _observacoesContribuinte = new ObservacaoContribuinteCollection();
        private ObservacaoFiscoCollection _observacoesFisco = new ObservacaoFiscoCollection();
        private ProcessoCollection _processos = new ProcessoCollection();

        public InformacoesAdicionaisNFe()
        {
        }

        /// <summary>
        /// [infAdFisco] Retorna ou define Informações Complementares de Interesse do Fisco. Opcional.
        /// </summary>
        [NFeField(ID = "Z02", FieldName = "infAdFisco", DataType = "TString", MinLength = 1, MaxLength = 2000, Pattern = @"[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}", Opcional = true)]
        [ValidateField(1, true)]
        public string InformacoesComplementaresFisco
        {
            get { return _informacoesComplementaresFisco; }
            set
            {
                _informacoesComplementaresFisco = ValidationUtil.TruncateString(value, 2000);
            }
        }

        /// <summary>
        /// [infCpl] Retorna ou define Informações Complementares de Interesse do Contribuinte. Opcional.
        /// </summary>
        [NFeField(ID = "Z03", FieldName = "infCpl", DataType = "TString", MinLength = 1, MaxLength = 5000, Pattern = @"[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}", Opcional = true)]
        [ValidateField(2, true)]
        public string InformacoesComplementaresContribuinte
        {
            get { return _informacoesComplementaresContribuinte; }
            set
            {
                _informacoesComplementaresContribuinte = ValidationUtil.TruncateString(value, 5000);
            }
        }

        /// <summary>
        /// [obsCont] Retorna as Informações de uso Livre do Contribuinte. Opcional.
        /// </summary>
        [NFeField(ID = "Z04", FieldName = "obsCont", MinLength = 1, MaxLength = 10, Opcional = true)]
        [ValidateField(3, ChaveErroValidacao.CollectionMinValue, Validator = typeof(RangeCollectionValidator), MaxLength = 10)]
        public ObservacaoContribuinteCollection ObservacoesContribuinte => _observacoesContribuinte;

        /// <summary>
        /// [obsFisco] Retorna as Informações de uso Livre do Fisco. Opcional.
        /// </summary>
        [NFeField(ID = "Z07", FieldName = "obsFisco", MinLength = 1, MaxLength = 10, Opcional = true)]
        [ValidateField(4, ChaveErroValidacao.CollectionMinValue, Validator = typeof(RangeCollectionValidator), MaxLength = 10)]
        public ObservacaoFiscoCollection ObservacoesFisco => _observacoesFisco;

        /// <summary>
        /// [procRef] Retorna os Processos Referenciados. Opcional.
        /// </summary>
        [NFeField(ID = "Z10", FieldName = "procRef", Opcional = true)]
        [ValidateField(5, ChaveErroValidacao.CollectionMinValue, Validator = typeof(RangeCollectionValidator))]
        public ProcessoCollection Processos => _processos;

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => !string.IsNullOrEmpty(InformacoesComplementaresContribuinte) ||
                                  !string.IsNullOrEmpty(InformacoesComplementaresFisco) ||
                                  ObservacoesContribuinte.Modificado ||
                                  ObservacoesFisco.Modificado ||
                                  Processos.Modificado;

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, NFe nfe)
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