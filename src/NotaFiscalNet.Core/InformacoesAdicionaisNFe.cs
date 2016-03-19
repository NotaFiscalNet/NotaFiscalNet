using System.Runtime.InteropServices;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using NotaFiscalNet.Core.Validacao.Validators;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o Destinatário da Nota Fiscal Eletrônica.
    /// </summary>
    
    
    
    public sealed class InformacoesAdicionaisNFe : INFeSerializable, IDirtyable
    {
        #region Fields

        private string _informacoesComplementaresFisco = string.Empty;
        private string _informacoesComplementaresContribuinte = string.Empty;
        private ObservacaoContribuinteCollection _observacoesContribuinte = new ObservacaoContribuinteCollection();
        private ObservacaoFiscoCollection _observacoesFisco = new ObservacaoFiscoCollection();
        private ProcessoCollection _processos = new ProcessoCollection();

        public InformacoesAdicionaisNFe()
        {
        }

        #endregion Fields

        #region Properties

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
            set {
                _informacoesComplementaresContribuinte = ValidationUtil.TruncateString(value, 5000); 
            }
        }

        /// <summary>
        /// [obsCont] Retorna as Informações de uso Livre do Contribuinte. Opcional.
        /// </summary>
        [NFeField(ID = "Z04", FieldName = "obsCont", MinLength = 1, MaxLength = 10, Opcional = true)]
        [ValidateField(3, ChaveErroValidacao.CollectionMinValue, Validator = typeof(RangeCollectionValidator), MaxLength = 10)]
        public ObservacaoContribuinteCollection ObservacoesContribuinte
        {
            get { return _observacoesContribuinte; }
        }

        /// <summary>
        /// [obsFisco] Retorna as Informações de uso Livre do Fisco. Opcional.
        /// </summary>
        [NFeField(ID = "Z07", FieldName = "obsFisco", MinLength = 1, MaxLength = 10, Opcional = true)]
        [ValidateField(4, ChaveErroValidacao.CollectionMinValue, Validator = typeof(RangeCollectionValidator), MaxLength = 10)]
        public ObservacaoFiscoCollection ObservacoesFisco
        {
            get { return _observacoesFisco; }
        }

        /// <summary>
        /// [procRef] Retorna os Processos Referenciados. Opcional.
        /// </summary>
        [NFeField(ID = "Z10", FieldName = "procRef", Opcional = true)]
        [ValidateField(5, ChaveErroValidacao.CollectionMinValue, Validator = typeof(RangeCollectionValidator))]
        public ProcessoCollection Processos
        {
            get { return _processos; }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool IsDirty
        {
            get 
            { 
                return
                    !string.IsNullOrEmpty(InformacoesComplementaresContribuinte) ||
                    !string.IsNullOrEmpty(InformacoesComplementaresFisco) ||
                    ObservacoesContribuinte.IsDirty ||
                    ObservacoesFisco.IsDirty ||
                    Processos.IsDirty;
            }
        }

        #endregion Properties

        #region INFeSerializable Members

        void INFeSerializable.Serialize(System.Xml.XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("infAdic"); // Elemento 'infAdic'

            #region infAdic

            if (!string.IsNullOrEmpty(InformacoesComplementaresFisco))
                writer.WriteElementString("infAdFisco", SerializationUtil.ToTString(InformacoesComplementaresFisco, 2000));
            if (!string.IsNullOrEmpty(InformacoesComplementaresContribuinte))
                writer.WriteElementString("infCpl", SerializationUtil.ToTString(InformacoesComplementaresContribuinte, 5000));
            if (ObservacoesContribuinte.IsDirty)
                ((INFeSerializable)ObservacoesContribuinte).Serialize(writer, nfe);
            if (ObservacoesFisco.IsDirty)
                ((INFeSerializable)ObservacoesFisco).Serialize(writer, nfe);
            if (Processos.IsDirty)
                ((INFeSerializable)Processos).Serialize(writer, nfe);

            #endregion infAdic

            writer.WriteEndElement(); // Elemento 'infAdic'
        }

        #endregion
    }
}