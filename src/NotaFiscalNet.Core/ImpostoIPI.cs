using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using System;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Armazena as informações do imposto IPI (Imposto sobre Produtos Industrializados) de um
    /// determinado produto na Nota Fiscal Eletrônica.
    /// </summary>

    public sealed class ImpostoIPI : ISerializavel, IModificavel
    {
        private SituacaoTributariaIPI _situacaoTributaria = SituacaoTributariaIPI.NaoEspecificado;
        private string _classeEnquadramentoIpiCigarrosBebidas = string.Empty;
        private string _cnpjProdutor = string.Empty;
        private string _codigoSeloControle = string.Empty;
        private int _quantidadeSeloControleIPI;
        private string _codigoEnquadramentoLegalIPI = string.Empty;
        private TipoCalculoIPI _tipoCalculo = TipoCalculoIPI.Percentual;
        private decimal _baseCalculo;
        private decimal _aliquota;
        private decimal _quantidade;
        private decimal _valorUnidade;
        private decimal _valor;

        private readonly ImpostoProduto _imposto;

        internal ImpostoIPI(ImpostoProduto imposto)
        {
            _imposto = imposto;
        }

        /// <summary>
        /// Retorna a referência para o objeto ImpostoProduto no qual o Imposto se refere.
        /// </summary>
        internal ImpostoProduto Imposto => _imposto;

        private void ValidarConflitoISSQN()
        {
            if (Imposto.ISSQN.Modificado)
                throw new ErroValidacaoNFeException(ChaveErroValidacao.ConflitoIPIISSQN);
        }

        /// <summary>
        /// [CST] Retorna ou define a Situação Tributária no cálculo do IPI.
        /// </summary>
        /// <remarks>
        /// Os campos abaixo citados deverão ser preenchidos apenas caso a Situação Tributária seja
        /// diferente de <br/> 00 (EntradaComRecuperacaoCredito), 49 (OutrasEntradas), 50
        /// (SaidaTributada) e 99 (OutrasSaidas):
        /// <list type="bullet">
        /// <item>
        /// <description>TipoCalculo</description>
        /// </item>
        /// <item>
        /// <description>ValorBaseCalculo</description>
        /// </item>
        /// <item>
        /// <description>Aliquota</description>
        /// </item>
        /// <item>
        /// <description>QuantidadeTotalUnidPadrao</description>
        /// </item>
        /// <item>
        /// <description>ValorUnidade</description>
        /// </item>
        /// <item>
        /// <description>ValorIPI</description>
        /// </item>
        /// </list>
        /// </remarks>
        [NFeField(FieldName = "CST", DataType = "token", ID = "O09")]
        [CampoValidavel(1, ChaveErroValidacao.CampoNaoPreenchido, ValorNaoPreenchido = SituacaoTributariaIPI.NaoEspecificado)]
        public SituacaoTributariaIPI SituacaoTributaria
        {
            get { return _situacaoTributaria; }
            set
            {
                ValidarConflitoISSQN();
                ValidationUtil.ValidateEnum(value, "SituacaoTributaria");

                switch (_situacaoTributaria)
                {
                    case SituacaoTributariaIPI.EntradaTributadaAliqZero:
                    case SituacaoTributariaIPI.EntradaIsenta:
                    case SituacaoTributariaIPI.EntradaNaoTributada:
                    case SituacaoTributariaIPI.EntradaImune:
                    case SituacaoTributariaIPI.EntradaComSuspensao:
                    case SituacaoTributariaIPI.SaidaTributadaAliqZero:
                    case SituacaoTributariaIPI.SaidaIsenta:
                    case SituacaoTributariaIPI.SaidaNaoTributada:
                    case SituacaoTributariaIPI.SaidaImune:
                    case SituacaoTributariaIPI.SaidaComSuspensao:
                        // limpa os valores dos campos que não são obrigatórios.
                        _classeEnquadramentoIpiCigarrosBebidas = string.Empty;
                        _cnpjProdutor = string.Empty;
                        _codigoSeloControle = string.Empty;
                        _quantidadeSeloControleIPI = 0;
                        _codigoEnquadramentoLegalIPI = string.Empty;
                        _tipoCalculo = TipoCalculoIPI.Percentual;
                        _baseCalculo = 0;
                        _aliquota = 0;
                        _quantidade = 0;
                        _valorUnidade = 0;
                        _valor = 0;
                        break;
                }
                _situacaoTributaria = value;
            }
        }

        /// <summary>
        /// [clEnq] Retorna ou define a Classe de Enquadramento do IPI para Cigarros e Bebidas. Até 5
        /// caracteres. Opcional.
        /// </summary>
        /// <remarks>
        /// A informação da Classe de enquadramento do IPI para Cigarros e Bebidas, quando aplicável,
        /// deve ser informada utilizando a codificação prevista nos Atos Normativos editados pela
        /// Receita Federal.
        /// </remarks>
        [NFeField(FieldName = "clEnq", DataType = "token", ID = "O02", MinLength = 1, MaxLength = 5, Opcional = true)]
        [CampoValidavel(2, Opcional = true)]
        public string ClasseIPICigarroBebida
        {
            get { return _classeEnquadramentoIpiCigarrosBebidas; }
            set
            {
                ValidarConflitoISSQN();
                _classeEnquadramentoIpiCigarrosBebidas = ValidationUtil.TruncateString(value, 5);
            }
        }

        /// <summary>
        /// [CNPJProd] Retorna ou define o Cnpj do Produtor da Mercadoria, quando diferente do
        /// emitente. <br/> Somente para os casos de exportação direta ou indireta. Opcional.
        /// </summary>
        [NFeField(FieldName = "CNPJProd", DataType = "TCnpj", ID = "O03", Opcional = true, Pattern = @"[0-9]{14}")]
        [CampoValidavel(3, Opcional = true)]
        public string CNPJProdutor
        {
            get { return _cnpjProdutor; }
            set
            {
                ValidarConflitoISSQN();
                ValidationUtil.ValidateCNPJ(value, "CNPJProdutor", true);
                _cnpjProdutor = value;
            }
        }

        /// <summary>
        /// [cSelo] Retorna ou define o Código do Selo de Controle do IPI. <br/> Preenchimento
        /// conforme Atos Normativos editados pela Receita Federal. <br/> Opcional.
        /// </summary>
        /// <remarks>
        /// A informação do código de selo, quando aplicável, deve ser informada utilizando a
        /// codificação prevista nos Atos Normativos editados pela Receita Federal.
        /// </remarks>
        [NFeField(FieldName = "cSelo", DataType = "token", ID = "O04", MinLength = 1, MaxLength = 60, Opcional = true)]
        [CampoValidavel(4, Opcional = true)]
        public string CodigoSeloControle
        {
            get { return _codigoSeloControle; }
            set
            {
                ValidarConflitoISSQN();
                _codigoSeloControle = ValidationUtil.TruncateString(value, 60);
            }
        }

        /// <summary>
        /// [qSelo] Retorna ou define a Quantidade de Selo de Controle do IPI. Opcional.
        /// </summary>
        [NFeField(FieldName = "qSelo", DataType = "token", ID = "O05", Pattern = @"[0-9]{1,12}", Opcional = true)]
        [CampoValidavel(5, Opcional = true)]
        public int QuantidadeSeloControle
        {
            get { return _quantidadeSeloControleIPI; }
            set
            {
                ValidarConflitoISSQN();
                ValidationUtil.ValidateRange(value, 0, 999999999999, "QuantidadeSeloControle");
                _quantidadeSeloControleIPI = value;
            }
        }

        /// <summary>
        /// [cEnq] Retorna ou define o Código de Enquadramento Legal do IPI (tabela criada pela RFB).
        /// </summary>
        [NFeField(FieldName = "cEnq", DataType = "token", ID = "O06", MinLength = 1, MaxLength = 3)]
        [CampoValidavel(6, ChaveErroValidacao.CampoNaoPreenchido)]
        public string CodigoEnquadramentoLegal
        {
            get { return _codigoEnquadramentoLegalIPI; }
            set
            {
                ValidarConflitoISSQN();
                _codigoEnquadramentoLegalIPI = ValidationUtil.TruncateString(value, 3);
            }
        }

        /// <summary>
        /// Retorna ou define o Tipo de Cálculo do IPI.
        /// </summary>
        /// <remarks>
        /// Este campo deverá ser preenchido caso a Situação Tributária indique que deverá informar
        /// os dados de tributação. <br/> Para maiores informações, seja a documentação presente no
        /// campo SituacaoTributaria.
        /// <para>
        /// Caso o Tipo de Cálculo seja por Percentual, os campos ValorBaseCalculo e Aliquota deverão
        /// ser preenchidos.
        /// </para>
        /// <para>
        /// Caso o Tipo de Cálculo seja por Valor, os campos QuantidadeTotalUnidPadrao e ValorUnidade
        /// deverão ser preenchidos.
        /// </para>
        /// </remarks>
        public TipoCalculoIPI TipoCalculo
        {
            get { return _tipoCalculo; }
            set
            {
                ValidarConflitoISSQN();
                switch (SituacaoTributaria)
                {
                    case SituacaoTributariaIPI.EntradaComRecuperacaoCredito:
                    case SituacaoTributariaIPI.OutrasEntradas:
                    case SituacaoTributariaIPI.SaidaTributada:
                    case SituacaoTributariaIPI.OutrasSaidas:
                        ValidationUtil.ValidateEnum<TipoCalculoIPI>(value, "TipoCalculo");
                        break;

                    case SituacaoTributariaIPI.EntradaTributadaAliqZero:
                    case SituacaoTributariaIPI.EntradaIsenta:
                    case SituacaoTributariaIPI.EntradaNaoTributada:
                    case SituacaoTributariaIPI.EntradaImune:
                    case SituacaoTributariaIPI.EntradaComSuspensao:
                    case SituacaoTributariaIPI.SaidaTributadaAliqZero:
                    case SituacaoTributariaIPI.SaidaIsenta:
                    case SituacaoTributariaIPI.SaidaNaoTributada:
                    case SituacaoTributariaIPI.SaidaImune:
                    case SituacaoTributariaIPI.SaidaComSuspensao:
                        throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeTipoCalculo, SituacaoTributaria));
                }

                _tipoCalculo = value;
                switch (value)
                {
                    case TipoCalculoIPI.Percentual:
                        _baseCalculo = 0;
                        _aliquota = 0;
                        break;

                    case TipoCalculoIPI.Valor:
                        _quantidade = 0m;
                        _valorUnidade = 0;
                        break;
                }
            }
        }

        /// <summary>
        /// [vBC] Retorna ou define o Valor da Base de Cálculo do IPI.
        /// </summary>
        [NFeField(FieldName = "vBC", DataType = "TDec_1302", ID = "O10", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?", Opcional = true)]
        //[ValidateField(7)]
        public decimal BaseCalculo
        {
            get { return _baseCalculo; }
            set
            {
                ValidarConflitoISSQN();
                switch (TipoCalculo)
                {
                    case TipoCalculoIPI.Percentual:
                        ValidationUtil.ValidateTDec_1302(value, "BaseCalculo");
                        break;

                    case TipoCalculoIPI.Valor:
                        throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeBaseCalculoTipoCalculo, TipoCalculo));
                }
                _baseCalculo = value;
            }
        }

        /// <summary>
        /// [pIPI] Retorna ou define o Alíquota (percentual) do IPI. Opcional.
        /// </summary>
        [NFeField(FieldName = "pIPI", DataType = "TDec_0302", ID = "O13", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,2}(\.[0-9]{2})?", Opcional = true)]
        //[ValidateField(8)]
        public decimal Aliquota
        {
            get { return _aliquota; }
            set
            {
                ValidarConflitoISSQN();
                switch (TipoCalculo)
                {
                    case TipoCalculoIPI.Percentual:
                        ValidationUtil.ValidateTDec_0302(value, "Aliquota");
                        break;

                    case TipoCalculoIPI.Valor:
                        throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeAliquotaTipoCalculo, TipoCalculo));
                }
                _aliquota = value;
            }
        }

        /// <summary>
        /// [qUnid] Retorna ou define a Quantidade Total da Unidade Padrão para Tributação. <br/>
        /// Obs. Somente para os produtos tributados por Unidade.
        /// </summary>
        [NFeField(FieldName = "qUnid", DataType = "TDec_1204", ID = "O11", Pattern = @"0|0\.[0-9]{4}|[1-9]{1}[0-9]{0,11}(\.[0-9]{4})", Opcional = true)]
        //[ValidateField(9)]
        public decimal Quantidade
        {
            get { return _quantidade; }
            set
            {
                ValidarConflitoISSQN();
                switch (TipoCalculo)
                {
                    case TipoCalculoIPI.Percentual:
                        throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeQuantidadeTipoCalculo, TipoCalculo));
                    case TipoCalculoIPI.Valor:
                        ValidationUtil.ValidateTDec_1204(value, "Quantidade");
                        break;
                }
                _quantidade = value;
            }
        }

        /// <summary>
        /// [vUnid] Retorna ou define o Valor por Unidade Tributável.
        /// </summary>
        [NFeField(FieldName = "vUnid", DataType = "TDec_1104", ID = "O12", Pattern = @"0|0\.[0-9]{4}|[1-9]{1}[0-9]{0,10}(\.[0-9]{4})?")]
        //[ValidateField(10)]
        public decimal ValorUnidade
        {
            get { return _valorUnidade; }
            set
            {
                ValidarConflitoISSQN();
                switch (TipoCalculo)
                {
                    case TipoCalculoIPI.Percentual:
                        throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeValorUnidadeTipoCalculo, TipoCalculo));
                    case TipoCalculoIPI.Valor:
                        ValidationUtil.ValidateTDec_1104(value, "ValorUnidade");
                        break;
                }
                _valorUnidade = value;
            }
        }

        /// <summary>
        /// [vIPI] Retorna ou define o Valor do IPI. Opcional caso não haja tributação do IPI segundo
        /// Situação Tributária.
        /// </summary>
        [NFeField(FieldName = "vIPI", DataType = "TDec_1302", ID = "O14", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?", Opcional = true)]
        //[ValidateField(11)]
        public decimal Valor
        {
            get { return _valor; }
            set
            {
                ValidarConflitoISSQN();
                ValidationUtil.ValidateTDec_1302(value, "Valor");

                _valor = value;
            }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => Aliquota != 0 ||
                                  BaseCalculo != 0 ||
                                  !string.IsNullOrEmpty(ClasseIPICigarroBebida) ||
                                  !string.IsNullOrEmpty(CNPJProdutor) ||
                                  !string.IsNullOrEmpty(CodigoEnquadramentoLegal) ||
                                  !string.IsNullOrEmpty(CodigoSeloControle) ||
                                  Quantidade != 0m ||
                                  QuantidadeSeloControle != 0 ||
                                  SituacaoTributaria != SituacaoTributariaIPI.NaoEspecificado ||
                                  Valor != 0 ||
                                  ValorUnidade != 0;

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("IPI"); // Elemento 'IPI'

            if (!string.IsNullOrEmpty(ClasseIPICigarroBebida))
                writer.WriteElementString("clEnq", SerializationUtil.ToToken(ClasseIPICigarroBebida, 5));
            if (!string.IsNullOrEmpty(CNPJProdutor))
                writer.WriteElementString("CNPJProd", SerializationUtil.ToCNPJ(CNPJProdutor));
            if (!string.IsNullOrEmpty(CodigoSeloControle))
                writer.WriteElementString("cSelo", SerializationUtil.ToToken(CodigoSeloControle, 60));
            if (QuantidadeSeloControle > 0)
                writer.WriteElementString("qSelo", SerializationUtil.ToToken(QuantidadeSeloControle));
            writer.WriteElementString("cEnq", SerializationUtil.ToToken(CodigoEnquadramentoLegal, 3));

            switch (SituacaoTributaria)
            {
                case SituacaoTributariaIPI.EntradaComRecuperacaoCredito:
                case SituacaoTributariaIPI.OutrasEntradas:
                case SituacaoTributariaIPI.SaidaTributada:
                case SituacaoTributariaIPI.OutrasSaidas:
                    SerializeIPITrib(writer, nfe);
                    break;

                case SituacaoTributariaIPI.EntradaTributadaAliqZero:
                case SituacaoTributariaIPI.EntradaIsenta:
                case SituacaoTributariaIPI.EntradaNaoTributada:
                case SituacaoTributariaIPI.EntradaImune:
                case SituacaoTributariaIPI.EntradaComSuspensao:
                case SituacaoTributariaIPI.SaidaTributadaAliqZero:
                case SituacaoTributariaIPI.SaidaIsenta:
                case SituacaoTributariaIPI.SaidaNaoTributada:
                case SituacaoTributariaIPI.SaidaImune:
                case SituacaoTributariaIPI.SaidaComSuspensao:
                    SerializeIPINT(writer, nfe);
                    break;
            }

            writer.WriteEndElement(); // Elemento 'IPI'
        }

        /// <summary>
        /// Serializa o Elemento IPITrib
        /// </summary>
        private void SerializeIPITrib(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("IPITrib"); // Elemento 'IPITrib'
            writer.WriteElementString("CST", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue<SituacaoTributariaIPI>(SituacaoTributaria))));
            switch (TipoCalculo)
            {
                case TipoCalculoIPI.Percentual:
                    writer.WriteElementString("vBC", SerializationUtil.ToTDec_1302(BaseCalculo));
                    writer.WriteElementString("pIPI", SerializationUtil.ToTDec_0302(Aliquota));
                    break;

                case TipoCalculoIPI.Valor:
                    writer.WriteElementString("qUnid", SerializationUtil.ToTDec_1204(Quantidade));
                    writer.WriteElementString("vUnid", SerializationUtil.ToTDec_1104(ValorUnidade));
                    break;
            }
            writer.WriteElementString("vIPI", SerializationUtil.ToTDec_1302(Valor));
            writer.WriteEndElement(); // Elemento 'IPITrib'
        }

        /// <summary>
        /// Serializa o Elemento IPINT
        /// </summary>
        private void SerializeIPINT(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("IPINT"); // Elemento 'IPINT'
            writer.WriteElementString("CST", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue<SituacaoTributariaIPI>(SituacaoTributaria))));
            writer.WriteEndElement(); // Elemento 'IPINT'
        }
    }
}