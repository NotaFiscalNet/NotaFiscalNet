using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using System;
using System.Xml;

namespace NotaFiscalNet.Core
{
    public class ExportacaoIndireta : ISerializavel, IModificavel
    {
        private string _chaveAcessoNFe;
        private string _numeroRegistro;

        /// <summary>
        /// [nRE] Retorna ou define o Número do Registro de Exportação.
        /// </summary>
        [NFeField(ID = "I53", FieldName = "nRE", DataType = "xs:string", Pattern = "[0-9]{0,12}")]
        public string NumeroRegistro
        {
            get { return _numeroRegistro; }
            set
            {
                if (!ValidationUtil.ValidateRegex(value, "^[0-9]{0,12}$"))
                    throw new ArgumentException("O valor informado não é válido.");
                _numeroRegistro = value;
            }
        }

        /// <summary>
        /// [chNFe] Retorna ou define a Chave de Acesso da NF-e recebida para exportação.
        /// </summary>
        [NFeField(ID = "I54", FieldName = "chNFe", DataType = "xs:string", Pattern = "^[0-9]{44}$")]
        public string ChaveAcessoNFe
        {
            get { return _chaveAcessoNFe; }
            set
            {
                if (!ValidationUtil.ValidateRegex(value, "^[0-9]{44}$"))
                    throw new ArgumentException("A chave de acesso informada não é válida (Exportação Indireta).");

                _chaveAcessoNFe = value;
            }
        }

        /// <summary>
        /// [chNFe] Retorna ou define a Chave de Acesso da NF-e recebida para exportação.
        /// </summary>
        [NFeField(ID = "I55", FieldName = "qExport", DataType = "TDec_1104v")]
        public decimal QuantidadeItemExportado { get; set; }

        public bool Modificado => !string.IsNullOrEmpty(NumeroRegistro) ||
                                  !string.IsNullOrEmpty(ChaveAcessoNFe) ||
                                  QuantidadeItemExportado != 0;

        public void Serializar(XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("exportInd");

            writer.WriteStartElement("nRE", NumeroRegistro);
            writer.WriteElementString("chNFe", ChaveAcessoNFe);
            writer.WriteElementString("qExport", QuantidadeItemExportado.ToTDec_1104v());

            writer.WriteEndElement();
        }
    }
}