using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Validacao;
using NotaFiscalNet.Core.Validacao.Validators;
using System;

namespace NotaFiscalNet.Core
{
    public sealed class ReferenciaDocFiscalCTe : ISerializavel, IModificavel
    {
        private string _referenciaCTe = string.Empty;

        private bool _isReadOnly = false;

        /// <summary>
        /// Retorna ou define a referência de um CT-e emitido anteriormente vinculada com esta NFe.
        /// </summary>
        /// <remarks>
        /// Este campo deve ser preenchido apenas caso o Documento Fiscal referenciado seja um CTe.
        /// </remarks>
        [NFeField(FieldName = "refCTe", DataType = "TChNFe", ID = "B13")]
        [ValidateField(1, Validator = typeof(ReferenciaDocFiscalValidator))]
        public string ReferenciaCTe
        {
            get { return _referenciaCTe; }
            set { _referenciaCTe = value; }
        }

        /// <summary>
        /// Retorna o valor indicando se a Nota Fiscal Eletrônica está em modo somente-leitura.
        /// </summary>
        /// <remarks>
        /// A Nota Fiscal Eletrônica estará em modo somente-leitura quando for instanciada a partir
        /// de um arquivo assinado digitalmente.
        /// </remarks>
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado
        {
            get
            {
                return
                    !string.IsNullOrEmpty(ReferenciaCTe);
            }
        }

        /// <summary>
        /// Inicializa uma nova instância da classe ReferenciaDocFiscalCTe
        /// </summary>
        public ReferenciaDocFiscalCTe()
        {
        }

        public void Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            writer.WriteElementString("refCTe", ReferenciaCTe);
        }

        public void Deserialize(System.Xml.XmlReader reader)
        {
            throw new NotImplementedException();
        }
    }
}