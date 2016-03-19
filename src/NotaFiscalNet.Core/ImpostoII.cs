using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o Imposto de Importação do Produto
    /// </summary>

    public sealed class ImpostoII : ISerializavel, IModificavel
    {
        private decimal _baseCalculo;
        private decimal _valorDespesasAduaneiras;
        private decimal _valorII;
        private decimal _valorIOF;

        private readonly ImpostoProduto _imposto;

        internal ImpostoII(ImpostoProduto imposto)
        {
            _imposto = imposto;
        }

        /// <summary>
        /// Retorna a referência para o objeto ImpostoProduto no qual o Imposto se refere.
        /// </summary>
        internal ImpostoProduto Imposto { get { return _imposto; } }

        private void ValidarConflitoISSQN()
        {
            if (Imposto.ISSQN.Modificado)
                throw new ErroValidacaoNFeException(ChaveErroValidacao.ConflitoIIISSQN);
        }

        /// <summary>
        /// [vBC] Retorna ou define a Base de Cálculo do II
        /// </summary>
        [NFeField(ID = "P02", FieldName = "vBC", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal BaseCalculo
        {
            get { return _baseCalculo; }
            set
            {
                ValidarConflitoISSQN();
                ValidationUtil.ValidateTDec_1302(value, "BaseCalculo");
                _baseCalculo = value;
            }
        }

        /// <summary>
        /// [vDespAdu] Retorna ou define o Valor das Despesas Aduaneiras
        /// </summary>
        [NFeField(ID = "P03", FieldName = "vDespAdu", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal ValorDespesasAduaneiras
        {
            get { return _valorDespesasAduaneiras; }
            set
            {
                ValidarConflitoISSQN();
                ValidationUtil.ValidateTDec_1302(value, "ValorDespesasAduaneiras");
                _valorDespesasAduaneiras = value;
            }
        }

        /// <summary>
        /// [vII] Retorna ou define o Valor do Imposto de Importação
        /// </summary>
        [NFeField(ID = "P04", FieldName = "vII", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [ValidateField(3, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal ValorII
        {
            get { return _valorII; }
            set
            {
                ValidarConflitoISSQN();
                ValidationUtil.ValidateTDec_1302(value, "ValorII");
                _valorII = value;
            }
        }

        /// <summary>
        /// [vIOF] Retorna ou define o Valor do Imposto sobre Operações Financeiras
        /// </summary>
        [NFeField(ID = "P05", FieldName = "vIOF", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [ValidateField(4, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal ValorIOF
        {
            get { return _valorIOF; }
            set
            {
                ValidarConflitoISSQN();
                ValidationUtil.ValidateTDec_1302(value, "ValorIOF");
                _valorIOF = value;
            }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado
        {
            get
            {
                return
                    BaseCalculo != 0m ||
                    ValorDespesasAduaneiras != 0m ||
                    ValorII != 0m ||
                    ValorIOF != 0m;
            }
        }

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("II"); // Elemento 'II'
            writer.WriteElementString("vBC", SerializationUtil.ToTDec_1302(BaseCalculo));
            writer.WriteElementString("vDespAdu", SerializationUtil.ToTDec_1302(ValorDespesasAduaneiras));
            writer.WriteElementString("vII", SerializationUtil.ToTDec_1302(ValorII));
            writer.WriteElementString("vIOF", SerializationUtil.ToTDec_1302(ValorIOF));
            writer.WriteEndElement();
        }
    }
}