using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;

namespace NotaFiscalNet.Core
{
    public class DetalhesOperacaoCartao : IModificavel
    {
        private string _cnpj;
        private string _codigoAutorizacao;
        private TipoBandeiraCartao _tipoBandeiraCartao;

        /// <summary>
        /// Retorna ou define o CNPJ da credenciadora de cartão de crédito/débito.
        /// </summary>
        [NFeField(ID = "YA05", FieldName = "CNPJ")]
        [ValidateField(1, ChaveErroValidacao.CNPJInvalido)]
        public string CNPJ
        {
            get { return _cnpj; }
            set
            {
                ValidationUtil.ValidateCNPJ(value, "CNPJ");
                _cnpj = value;
            }
        }

        /// <summary>
        /// Retorna ou define a Bandeira da operadora de cartão de crédito/débito.
        /// </summary>
        [NFeField(ID = "YA06", FieldName = "tBand")]
        [ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public TipoBandeiraCartao TipoBandeira
        {
            get { return _tipoBandeiraCartao; }
            set
            {
                ValidationUtil.ValidateEnum(value, "TipoBandeira");
                _tipoBandeiraCartao = value;
            }
        }

        /// <summary>
        /// Retorna ou define o número de autorização da operação de cartão de crédito/débito.
        /// </summary>
        [NFeField(ID = "YA07", FieldName = "cAut")]
        [ValidateField(3, ChaveErroValidacao.CampoNaoPreenchido)]
        public string CodigoAutorizacao
        {
            get { return _codigoAutorizacao; }
            set
            {
                ValidationUtil.ValidateRange(value, 1, 20, "CodigoAutorizacao");
                _codigoAutorizacao = value;
            }
        }

        public bool Modificado
        {
            get
            {
                return string.IsNullOrEmpty(CNPJ) && (int)TipoBandeira != 0 && string.IsNullOrEmpty(CodigoAutorizacao);
            }
        }
    }
}