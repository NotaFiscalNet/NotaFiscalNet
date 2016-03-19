using System.Runtime.InteropServices;
using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Informações de Pagamento. Obrigatório apena para (NFC-e) NT 2012/004.
    /// </summary>
    
    
    
    public class Pagamento :  ISerializavel, IModificavel
    {
        private TipoPagamento _tipoPagamento;
        private decimal _valorPagamento;

        #region Properties

        /// <summary>
        /// Retorna ou define a forma de pagamento.
        /// </summary>
        [NFeField(ID = "YA02", FieldName = "tPag")]
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public TipoPagamento TipoPagamento
        {
            get
            {
                return _tipoPagamento;
            }
            set
            {
                ValidationUtil.ValidateEnum<TipoPagamento>(value, "TipoPagamento");
                _tipoPagamento = value;
            }
        }

        /// <summary>
        /// Retorna ou define o valor do pagamento.
        /// </summary>
        [NFeField(ID="YA30", FieldName = "vPag", DataType = "TDec_1302")]
        [ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal ValorPagamento
        {
            get
            {
                return _valorPagamento;
            }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorPagamento");
                _valorPagamento = value;
            }
        }

        /// <summary>
        /// Retorna ou define os detalhes referente a operação com cartão de crédito/débito.
        /// </summary>
        [NFeField(ID="YA04", FieldName = "card"), ValidateField(3, true)]
        public DetalhesOperacaoCartao DetalhesOperacaoCartao { get; set; }

        #endregion Properties

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("pag"); // <pag>

            writer.WriteElementString("tPag", SerializationUtil.ToString2((int)TipoPagamento));
            writer.WriteElementString("vPag", SerializationUtil.ToTDec_1302(ValorPagamento));

            if (DetalhesOperacaoCartao != null)
            {
                writer.WriteStartElement("card"); // <card>

                writer.WriteElementString("CNPJ", SerializationUtil.ToCNPJ(DetalhesOperacaoCartao.CNPJ));
                writer.WriteElementString("tBand", SerializationUtil.ToString2((int)DetalhesOperacaoCartao.TipoBandeira));
                writer.WriteElementString("cAut", SerializationUtil.ToTString(DetalhesOperacaoCartao.CodigoAutorizacao, 20));

                writer.WriteEndElement(); // </card>
            }

            writer.WriteEndElement(); // </pag>
        }

        public bool Modificado
        {
            get
            {
                return (int) TipoPagamento != 0 || ValorPagamento != 0.0m || DetalhesOperacaoCartao != null;
            }
        }
    }
}
