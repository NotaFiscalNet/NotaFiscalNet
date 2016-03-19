using System.Runtime.InteropServices;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa a referência a um Documento Fiscal (Nota Fiscal Eletrônica ou NF modelo 1 ou 1A).
    /// </summary>
    /// <remarks>
    /// Caso o Documento Fiscal seja uma Nota Fiscal Eletrônica, os seguintes campos deverão ser preenchidos:
    /// <list type="bullet">
    ///     <item>CodigoModeloDocFiscal = 55</item>
    ///     <item>ChaveAcessoNFe</item>
    /// </list>
    /// Caso o Documento Fiscal seja um Nota Fiscal modelo 1 ou 1A, os outros campos deverão ser preenchidos com
    /// excessão do campo ChaveAcessoNFe.
    /// </remarks>
    
    
    
    public sealed class ReferenciaDocFiscal :  INFeSerializable
    {
        #region Fields

        private TipoReferenciaDocFiscal _tipoReferencia = TipoReferenciaDocFiscal.NFe;
        private readonly ReferenciaDocFiscalNFe _referenciaNFe = new ReferenciaDocFiscalNFe();
        private readonly ReferenciaDocFiscalNF _referenciaNF = new ReferenciaDocFiscalNF();
        private readonly ReferenciaDocFiscalNFProdutor _referenciaNFP = new ReferenciaDocFiscalNFProdutor();
        private readonly ReferenciaDocFiscalCTe _referenciaCTe = new ReferenciaDocFiscalCTe();
        private readonly ReferenciaDocFiscalEcf _referenciaECF = new ReferenciaDocFiscalEcf();

        #endregion Fields

        #region Properties

        /// <summary>
        /// Indica o tipo da referência utilizada. Padrão NF-e.
        /// </summary>
        [ValidateField(1, false)]
        public TipoReferenciaDocFiscal TipoReferencia
        {
            get { return _tipoReferencia; }
            set 
            {
                ValidationUtil.ValidateEnum(value, "TipoReferencia");
                
                _tipoReferencia = value; 
            }
        }

        /// <summary>
        /// Representa os tipos de dados da referência NF-e.
        /// </summary>
        [ValidateField(2, false)]
        public ReferenciaDocFiscalNFe ReferenciaNFe
        {
            get { return _referenciaNFe; }
        }

        /// <summary>
        /// Representa os tipos de dados da referência NF.
        /// </summary>
        [ValidateField(3, false)]
        public ReferenciaDocFiscalNF ReferenciaNF
        {
            get { return _referenciaNF; }
        }

        /// <summary>
        /// Representa os tipos de dados da referência NF de Produtor.
        /// </summary>
        [ValidateField(3, false)]
        public ReferenciaDocFiscalNFProdutor ReferenciaNFProdutor
        {
            get { return _referenciaNFP; }
        }

        /// <summary>
        /// Representa os tipos de dados da referência CTe.
        /// </summary>
        [ValidateField(4, false)]
        public ReferenciaDocFiscalCTe ReferenciaCTe
        {
            get { return _referenciaCTe; }
        }

        /// <summary>
        /// Representa os tipos de dados da referência de ECF.
        /// </summary>
        [ValidateField(4, false)]
        public ReferenciaDocFiscalEcf ReferenciaECF
        {
            get { return _referenciaECF; }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return
                    ReferenciaNFe.IsDirty ||
                    ReferenciaNF.IsDirty ||
                    ReferenciaNFProdutor.IsDirty ||
                    ReferenciaCTe.IsDirty;
            }
        }


        #endregion Properties

        #region Constructor

        /// <summary>
        /// Inicializa uma nova instância da classe ReferenciaDocFiscal
        /// </summary>
        public ReferenciaDocFiscal()
        {
        }

        #endregion Constructor

        #region INFeSerializable Members

        public void Serialize(System.Xml.XmlWriter writer, NFe nfe)
        {
            if (!IsDirty)
                return;

            writer.WriteStartElement("NFref");

            switch (TipoReferencia)
            {
                case TipoReferenciaDocFiscal.NFe:
                    if (ReferenciaNFe.IsDirty)
                        ((INFeSerializable)ReferenciaNFe).Serialize(writer, nfe);
                    break;
                case TipoReferenciaDocFiscal.NF:
                    if (ReferenciaNF.IsDirty)
                        ((INFeSerializable)ReferenciaNF).Serialize(writer, nfe);
                    break;
                case TipoReferenciaDocFiscal.NFProdutor:
                    if (ReferenciaNFProdutor.IsDirty)
                        ((INFeSerializable)ReferenciaNFProdutor).Serialize(writer, nfe);
                    break;
                case TipoReferenciaDocFiscal.CTe:
                    if (ReferenciaCTe.IsDirty)
                        ((INFeSerializable)ReferenciaCTe).Serialize(writer, nfe);
                    break;
                case TipoReferenciaDocFiscal.ECF:
                    break;
            }

            writer.WriteEndElement(); // fecha NFref

        }

        #endregion
    }
}