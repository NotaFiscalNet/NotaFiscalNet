using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Validacao;
using NotaFiscalNet.Core.Validacao.Validators;
using System;

namespace NotaFiscalNet.Core
{
    public sealed class ReferenciaDocFiscalNFe : ISerializavel, IModificavel
    {
        private string _chaveAcessoNFe = string.Empty;

        private bool _isReadOnly = false;

        /// <summary>
        /// Retorna ou define a Chave de Acesso da Nota Fiscal Eletrônica referenciada (emitida
        /// anteriormente, vinculada a atual NF-e).
        /// </summary>
        /// <remarks>
        /// Este campo deve ser preenchido apenas caso o Documento Fiscal referenciado seja uma Nota
        /// Fiscal Eletrônica. Esta informação será utilizada nas hipóteses previstas na legislação
        /// (ex.: Devolução de Mercadorias, Substituição de NF cancelada, Complementação de NF, etc).
        /// </remarks>
        [NFeField(FieldName = "refNFe", DataType = "TChNFe", ID = "B13")]
        [ValidateField(1, Validator = typeof(ReferenciaDocFiscalValidator))]
        public string ChaveAcessoNFe
        {
            get { return _chaveAcessoNFe; }
            set { _chaveAcessoNFe = value; }
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
                    !string.IsNullOrEmpty(ChaveAcessoNFe);
            }
        }

        /// <summary>
        /// Inicializa uma nova instância da classe ReferenciaDocFiscalNFe
        /// </summary>
        public ReferenciaDocFiscalNFe()
        {
        }

        public void Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            /// Campos para Nota Fiscal modelo NFe (55)
            writer.WriteElementString("refNFe", ChaveAcessoNFe);
        }

        public void Deserialize(System.Xml.XmlReader reader)
        {
            throw new NotImplementedException();
        }
    }
}