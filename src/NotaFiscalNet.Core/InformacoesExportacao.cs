using System;
using System.Runtime.InteropServices;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representam as informações de Exportação da Nota Fiscal Eletrônica.
    /// </summary>
    
    
    
    public sealed class InformacoesExportacao : INFeSerializable, IDirtyable
    {
        #region Fields

        private SiglaUF _ufSaidaPais = SiglaUF.NaoEspecificado;
        private string _localEmbarque = string.Empty;
        private string _localDespacho = string.Empty;

        public InformacoesExportacao()
        {
        }

        #endregion Fields
        
        #region Properties

        /// <summary>
        /// [UFEmbarq] Retorna ou define a Sigla da UF de Embarque o de transposição de fronteira.
        /// </summary>
        [NFeField(ID = "ZA02", FieldName = "UFSaidaPais", DataType = "TUfEmi")]
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido, DefaultValue = UfIBGE.NaoEspecificado)]
        public SiglaUF UFSaidaPais
        {
            get { return _ufSaidaPais; }
            set
            {
                if (value == SiglaUF.EX)
                    throw new InvalidOperationException("EX não é um valor válido para o campo.");

                _ufSaidaPais = ValidationUtil.ValidateEnum(value, "UFEmbarque");
            }
        }

        /// <summary>
        /// [xLocExporta] Retorna ou define a Descrição do Local de Embarque ou de transposição de fronteira.
        /// </summary>
        [NFeField(ID = "ZA03", FieldName = "xLocExporta", DataType = "TString", MinLength = 1, MaxLength = 60, Pattern = @"[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}")]
        [ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public string LocalEmbarque
        {
            get { return _localEmbarque; }
            set {
                _localEmbarque = ValidationUtil.TruncateString(value, 60);
            }
        }

        /// <summary>
        /// [xLocDespacho] Retorna ou define a Descrição do Local de Despacho.
        /// </summary>
        [NFeField(ID = "ZA04", FieldName = "xLocDespacho", DataType = "TString", MinLength = 1, MaxLength = 60, Pattern = @"[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}", Opcional = true)]
        [ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public string LocalDespacho
        {
            get { return _localDespacho; }
            set
            {
                _localDespacho = ValidationUtil.TruncateString(value, 60);
            }
        }

        /// <summary>
        /// Retorna se a classe foi modificada.
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return
                    UFSaidaPais != SiglaUF.NaoEspecificado ||
                    !string.IsNullOrEmpty(LocalEmbarque) ||
                    !string.IsNullOrEmpty(LocalDespacho);

            }
        }

        #endregion Properties

        #region INFeSerializable Members

        void INFeSerializable.Serialize(System.Xml.XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("exporta"); // Elemento 'exporta'

            #region exporta

            writer.WriteElementString("UFSaidaPais", UFSaidaPais.ToString());
            writer.WriteElementString("xLocExporta", SerializationUtil.ToToken(LocalEmbarque, 60));

            if (!String.IsNullOrEmpty(LocalDespacho))
                writer.WriteElementString("xLocDespacho", SerializationUtil.ToToken(LocalDespacho, 60));

            #endregion exporta

            writer.WriteEndElement(); // Elemento 'exporta'
        }

        #endregion
    }
}
