using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Adição na Declaração de Importação do Produto
    /// </summary>
    public sealed class DeclaracaoImportacaoAdicao : ISerializavel, IModificavel
    {
        void ISerializavel.Serializar(XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("adi");

            writer.WriteElementString("nAdicao", Numero.ToString());
            writer.WriteElementString("nSeqAdic", NumeroSequencial.ToString());
            writer.WriteElementString("cFabricante", SerializationUtil.ToToken(CodigoFabricante, 60));

            if (ValorDescontoItemDI > 0m)
                writer.WriteElementString("vDescDI", SerializationUtil.ToTDec_1302(ValorDescontoItemDI));

            writer.WriteEndElement();
        }

        /// <summary>
        /// [nAdicao] Retorna ou define o Número da Adição
        /// </summary>
        [NFeField(ID = "I26", FieldName = "nAdicao", DataType = "token", Pattern = "[1-9]{1}[0-9]{0,2}")]
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public int Numero { get; set; }

        /// <summary>
        /// [nSeqAdic] Retorna ou define o Número Sequencial do Item dentro da Adição
        /// </summary>
        [NFeField(ID = "I27", FieldName = "nSeqAdic", DataType = "token", Pattern = "[1-9]{1}[0-9]{0,2}")]
        [ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public int NumeroSequencial { get; set; }

        /// <summary>
        /// [cFabricante] Retorna ou define o Código do Fabricante Estrangeiro, usado nos sistemas
        /// internos de informação do emitente da NF-e
        /// </summary>
        [NFeField(ID = "I28", FieldName = "cFabricante", DataType = "token", MinLength = 1, MaxLength = 60)]
        [ValidateField(3, ChaveErroValidacao.CampoNaoPreenchido)]
        public string CodigoFabricante { get; set; } = string.Empty;

        /// <summary>
        /// [cDescDI] Retorna ou define o Valor do Desconto do item da Adição na Declaração de
        /// Importação. Opcional.
        /// </summary>
        [NFeField(ID = "I29", FieldName = "vDescDI", DataType = "TDec_1302Opc",
            Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?", Opcional = true)]
        [ValidateField(4, true)]
        public decimal ValorDescontoItemDI { get; set; }

        /// <summary>
        /// Retorna o valor indicando se a Nota Fiscal Eletrônica está em modo somente-leitura.
        /// </summary>
        /// <remarks>
        /// A Nota Fiscal Eletrônica estará em modo somente-leitura quando for instanciada a partir
        /// de um arquivo assinado digitalmente.
        /// </remarks>
        public bool IsReadOnly { get; } = false;

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => Numero != 0 ||
                                  NumeroSequencial != 0 ||
                                  !string.IsNullOrEmpty(CodigoFabricante) ||
                                  ValorDescontoItemDI != 0m;
    }
}