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
        /// Retorna ou define o Cnpj da credenciadora de cartão de crédito/débito.
        /// </summary>
        [NFeField(ID = "YA05", FieldName = "Cnpj")]
        [CampoValidavel(1, ChaveErroValidacao.CNPJInvalido)]
        public string CNPJ
        {
            get => _cnpj;
	        set
            {
                ValidationUtil.ValidateCNPJ(value, "Cnpj");
                _cnpj = value;
            }
        }

        /// <summary>
        /// Retorna ou define a Bandeira da operadora de cartão de crédito/débito.
        /// </summary>
        [NFeField(ID = "YA06", FieldName = "tBand")]
        [CampoValidavel(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public TipoBandeiraCartao TipoBandeira
        {
            get => _tipoBandeiraCartao;
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
        [CampoValidavel(3, ChaveErroValidacao.CampoNaoPreenchido)]
        public string CodigoAutorizacao
        {
            get => _codigoAutorizacao;
	        set
            {
                ValidationUtil.ValidateRange(value, 1, 20, "CodigoAutorizacao");
                _codigoAutorizacao = value;
            }
        }

        public bool Modificado => string.IsNullOrEmpty(CNPJ) && (int)TipoBandeira != 0 && string.IsNullOrEmpty(CodigoAutorizacao);
    }
}