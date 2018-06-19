using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;

using System;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o Imposto Sobre Circulação de Mercadorias e Serviços
    /// </summary>
    public sealed class ImpostoICMS : ISerializavel, IModificavel
    {
        private OrigemMercadoria _origemMercadoria = OrigemMercadoria.NaoEspecificado;
        private SituacaoTributariaICMS _situacaoTributaria = SituacaoTributariaICMS.NaoEspecificado;
        private MotivoDesoneracaoCondicionalICMS _motivoDesoneracaoCondicional = MotivoDesoneracaoCondicionalICMS.NaoEspecificado;
        private ModalidadeBaseCalculoIcms _modalidadeBaseCalculo = ModalidadeBaseCalculoIcms.NaoEspecificado;
        private decimal _percentualReducaoBaseCalculo;
        private decimal _baseCalculo;
        private decimal _aliquota;
        private decimal _valor;
        private ModalidadeBaseCalculoIcmsST _modalidadeBaseCalculoST = ModalidadeBaseCalculoIcmsST.NaoEspecificado;
        private decimal _percentualMargemValorAdicionado;
        private decimal _percentualReducaoBaseCalculoST;
        private decimal _baseCalculoST;
        private decimal _aliquotaST;
        private decimal _aliquotaSN;
        private decimal _valorST;
        private decimal _valorSN;

	    internal ImpostoICMS(ImpostoProduto imposto)
        {
            Imposto = imposto;
        }

        /// <summary>
        /// Retorna a referência para o objeto ImpostoProduto no qual o Imposto se refere.
        /// </summary>
        internal ImpostoProduto Imposto { get; }

	    private void ValidarConflitoISSQN()
        {
            if (Imposto.ISSQN.Modificado)
                throw new ErroValidacaoNFeException(ChaveErroValidacao.ConflitoICMSISSQN);
        }

        /// <summary>
        /// [orig] Retorna ou define a Origem da Mercadoria. <br/> Obrigatório para todos os tipos de ICMS
        /// </summary>
        [NFeField(ID = "N11", FieldName = "orig", DataType = "Torig")]
        [CampoValidavel(1, ChaveErroValidacao.CampoNaoPreenchido, ValorNaoPreenchido = OrigemMercadoria.NaoEspecificado)]
        public OrigemMercadoria OrigemMercadoria
        {
            get => _origemMercadoria;
	        set
            {
                ValidarConflitoISSQN();
                ValidationUtil.ValidateEnum(value, "OrigemMercadoria");
                _origemMercadoria = value;
            }
        }

        /// <summary>
        /// Retorna ou define o Tipo da Tributação do ICMS. <br/> Obrigatório para todos os tipos de ICMS
        /// </summary>
        [NFeField(ID = "N12", FieldName = "CST")]
        [CampoValidavel(2, ChaveErroValidacao.CampoNaoPreenchido, ValorNaoPreenchido = SituacaoTributariaICMS.NaoEspecificado)]
        public SituacaoTributariaICMS SituacaoTributaria
        {
            get => _situacaoTributaria;
	        set
            {
                ValidarConflitoISSQN();
                ValidationUtil.ValidateEnum(value, "SituacaoTributaria");
                _situacaoTributaria = value;
            }
        }

        /// <summary>
        /// Retorna ou define a Modalidade da Base de Cálculo do ICMS <br/> Obrigatório nos ICMS: 00,
        /// 10, 20, 70, 90 <br/> Opcional nos ICMS: 51
        /// </summary>
        [NFeField(ID = "N13", FieldName = "modBC")]
        [CampoValidavel(3)]
        public ModalidadeBaseCalculoIcms ModalidadeBaseCalculo
        {
            get => _modalidadeBaseCalculo;
	        set
            {
                ValidarConflitoISSQN();
                if (value != ModalidadeBaseCalculoIcms.NaoEspecificado)
                {
                    switch (SituacaoTributaria)
                    {
                        case SituacaoTributariaICMS.Cst00:
                        case SituacaoTributariaICMS.Cst10:
                        case SituacaoTributariaICMS.Cst20:
                        case SituacaoTributariaICMS.Cst51:
                        case SituacaoTributariaICMS.Cst70:
                        case SituacaoTributariaICMS.Cst90:
                        case SituacaoTributariaICMS.CSOSN900:
                            ValidationUtil.ValidateEnum(value, "ModalidadeBaseCalculo");
                            break;

                        default:
                            throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeModalidadeBaseCalculo, SituacaoTributaria));
                    }
                }
                _modalidadeBaseCalculo = value;
            }
        }

        /// <summary>
        /// Retorna ou define o Percentual de Redução da Base de Cálculo do ICMS <br/> Obrigatório
        /// nos ICMS: 20, 70 <br/> Opcional nos ICMS: 51, 90
        /// </summary>
        [NFeField(ID = "N14", FieldName = "pRedBC", DataType = "TDec_0302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,2}(\.[0-9]{2})?")]
        public decimal PercentualReducaoBaseCalculo
        {
            get => _percentualReducaoBaseCalculo;
	        set
            {
                ValidarConflitoISSQN();
                if (value != 0m)
                {
                    switch (SituacaoTributaria)
                    {
                        case SituacaoTributariaICMS.Cst20:
                        case SituacaoTributariaICMS.Cst51:
                        case SituacaoTributariaICMS.Cst70:
                        case SituacaoTributariaICMS.Cst90:
                        case SituacaoTributariaICMS.CSOSN900:
                            ValidationUtil.ValidateTDec_0302(value, "PercentualReducaoBaseCalculo");
                            break;

                        default:
                            throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangePercentualReducaoBaseCalculo, SituacaoTributaria));
                    }
                }
                _percentualReducaoBaseCalculo = value;
            }
        }

        /// <summary>
        /// Retorna ou define a Base de Cálculo do ICMS <br/> Obrigatório nos ICMS: 00, 10, 20, 30,
        /// 70, 90 <br/> Opcional nos ICMS: 51
        /// </summary>
        [NFeField(ID = "N15", FieldName = "vBC", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        public decimal BaseCalculo
        {
            get => _baseCalculo;
	        set
            {
                ValidarConflitoISSQN();
                if (value != 0m)
                {
                    switch (SituacaoTributaria)
                    {
                        case SituacaoTributariaICMS.Cst00:
                        case SituacaoTributariaICMS.Cst10:
                        case SituacaoTributariaICMS.Cst20:
                        case SituacaoTributariaICMS.Cst30:
                        case SituacaoTributariaICMS.Cst51:
                        case SituacaoTributariaICMS.Cst70:
                        case SituacaoTributariaICMS.Cst90:
                        case SituacaoTributariaICMS.CSOSN900:
                            ValidationUtil.ValidateTDec_1302(value, "BaseCalculo");
                            break;

                        default:
                            throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeBaseCalculo, SituacaoTributaria));
                    }
                }
                _baseCalculo = value;
            }
        }

        /// <summary>
        /// Retorna ou define a Alíquota do ICMS <br/> Obrigatório nos ICMS: 00, 10, 20, 30, 70, 90,
        /// 101 <br/> Opcional nos ICMS: 51
        /// </summary>
        [NFeField(ID = "N16", FieldName = "pICMS", DataType = "TDec_0302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,2}(\.[0-9]{2})?")]
        public decimal Aliquota
        {
            get => _aliquota;
	        set
            {
                ValidarConflitoISSQN();
                if (value != 0m)
                {
                    switch (SituacaoTributaria)
                    {
                        case SituacaoTributariaICMS.Cst00:
                        case SituacaoTributariaICMS.Cst10:
                        case SituacaoTributariaICMS.Cst20:
                        case SituacaoTributariaICMS.Cst30:
                        case SituacaoTributariaICMS.Cst51:
                        case SituacaoTributariaICMS.Cst70:
                        case SituacaoTributariaICMS.Cst90:
                        case SituacaoTributariaICMS.CSOSN900:
                            ValidationUtil.ValidateTDec_0302(value, "Aliquota");
                            break;

                        default:
                            throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeAliquota, SituacaoTributaria));
                    }
                }
                _aliquota = value;
            }
        }

        /// <summary>
        /// Retorna ou define a Alíquota do ICMS Simples Nacional
        /// </summary>
        [NFeField(ID = "N16", FieldName = "pCredSN", DataType = "TDec_0302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,2}(\.[0-9]{2})?")]
        public decimal AliquotaSN
        {
            get => _aliquotaSN;
	        set
            {
                ValidarConflitoISSQN();
                if (value != 0m)
                {
                    switch (SituacaoTributaria)
                    {
                        case SituacaoTributariaICMS.CSOSN101:
                        case SituacaoTributariaICMS.CSOSN201:
                        case SituacaoTributariaICMS.CSOSN900:
                            ValidationUtil.ValidateTDec_0302(value, "AliquotaSN");
                            break;

                        default:
                            throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeAliquota, SituacaoTributaria));
                    }
                }
                _aliquotaSN = value;
            }
        }

        /// <summary>
        /// [vICMS, vCredICMSSN] Retorna ou define o Valor do ICMS <br/> Obrigatório nos ICMS: 00,
        /// 10, 20, 30, 70, 90, 101 <br/> Opcional nos ICMS: 40, 41, 50, 51
        /// </summary>
        [NFeField(ID = "N17", FieldName = "vICMS", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [NFeField(FieldName = "vCredICMSSN", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        public decimal Valor
        {
            get => _valor;
	        set
            {
                ValidarConflitoISSQN();
                if (value != 0m)
                {
                    switch (SituacaoTributaria)
                    {
                        case SituacaoTributariaICMS.Cst00:
                        case SituacaoTributariaICMS.Cst10:
                        case SituacaoTributariaICMS.Cst20:
                        case SituacaoTributariaICMS.Cst30:
                        case SituacaoTributariaICMS.Cst40:
                        case SituacaoTributariaICMS.Cst41:
                        case SituacaoTributariaICMS.Cst50:
                        case SituacaoTributariaICMS.Cst51:
                        case SituacaoTributariaICMS.Cst70:
                        case SituacaoTributariaICMS.Cst90:
                        case SituacaoTributariaICMS.CSOSN900:
                            ValidationUtil.ValidateTDec_1302(value, "Valor");
                            break;

                        default:
                            throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeValor, SituacaoTributaria));
                    }
                }
                _valor = value;
            }
        }

        /// <summary>
        /// Retorna ou define o Valor do ICMS Simples Nacional
        /// </summary>
        [NFeField(FieldName = "vCredICMSSN", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        public decimal ValorSN
        {
            get => _valorSN;
	        set
            {
                ValidarConflitoISSQN();
                if (value != 0m)
                {
                    switch (SituacaoTributaria)
                    {
                        case SituacaoTributariaICMS.CSOSN101:
                        case SituacaoTributariaICMS.CSOSN201:
                        case SituacaoTributariaICMS.CSOSN900:
                            ValidationUtil.ValidateTDec_1302(value, "ValorSN");
                            break;

                        default:
                            throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeValor, SituacaoTributaria));
                    }
                }
                _valorSN = value;
            }
        }

        /// <summary>
        /// O valor do ICMS será informado apenas nas operações com veículos beneficiados com a
        /// desoneração condicional do ICMS Obrigatório nos ICMS 40, 41, 50, quando informado o valor
        /// da desoneração.
        /// </summary>
        [NFeField(FieldName = "motDesICMS")]
        [CampoValidavel(5)]
        public MotivoDesoneracaoCondicionalICMS MotivoDesoneracaoCondicional
        {
            get => _motivoDesoneracaoCondicional;
	        set
            {
                ValidarConflitoISSQN();
                ValidationUtil.ValidateEnum(value, "MotivoDesoneracaoCondicional");
                _motivoDesoneracaoCondicional = value;
            }
        }

        /// <summary>
        /// Retorna ou define a Modalidade de Base de Cálculo do ICMS ST. <br/> Obrigatório nos ICMS:
        /// 10, 30, 70, 90
        /// </summary>
        [NFeField(ID = "N18", FieldName = "modBCST")]
        public ModalidadeBaseCalculoIcmsST ModalidadeBaseCalculoST
        {
            get => _modalidadeBaseCalculoST;
	        set
            {
                ValidarConflitoISSQN();
                if (value != ModalidadeBaseCalculoIcmsST.NaoEspecificado)
                {
                    switch (SituacaoTributaria)
                    {
                        case SituacaoTributariaICMS.Cst10:
                        case SituacaoTributariaICMS.Cst30:
                        case SituacaoTributariaICMS.Cst70:
                        case SituacaoTributariaICMS.Cst90:
                        case SituacaoTributariaICMS.CSOSN201:
                        case SituacaoTributariaICMS.CSOSN202:
                        case SituacaoTributariaICMS.CSOSN203:
                        case SituacaoTributariaICMS.CSOSN900:
                            ValidationUtil.ValidateEnum(value, "ModalidadeBaseCalculoST");
                            break;

                        default:
                            throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeModalidadeBaseCalculo, SituacaoTributaria));
                    }
                }
                _modalidadeBaseCalculoST = value;
            }
        }

        /// <summary>
        /// [pMVAST] Retorna ou define o Percentual da Margem de Valor Adicionado do ICMS ST. <br/>
        /// Obrigatório nos ICMS: 10, 30, 70 <br/> Opcional nos ICMS: 90
        /// </summary>
        [NFeField(ID = "N19", FieldName = "pMVAST", DataType = "TDec_0302Opc", Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,2}(\.[0-9]{2})?")]
        public decimal PercentualMargemValorAdicionadoST
        {
            get => _percentualMargemValorAdicionado;
	        set
            {
                ValidarConflitoISSQN();
                if (value != 0m)
                {
                    switch (SituacaoTributaria)
                    {
                        case SituacaoTributariaICMS.Cst10:
                        case SituacaoTributariaICMS.Cst30:
                        case SituacaoTributariaICMS.Cst70:
                        case SituacaoTributariaICMS.Cst90:
                        case SituacaoTributariaICMS.CSOSN201:
                        case SituacaoTributariaICMS.CSOSN202:
                        case SituacaoTributariaICMS.CSOSN203:
                        case SituacaoTributariaICMS.CSOSN900:
                            ValidationUtil.ValidateTDec_0302(value, "PercentualMargemValorAdicionado");
                            break;

                        default:
                            throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangePercentualMargemValorAdicionado, SituacaoTributaria));
                    }
                }
                _percentualMargemValorAdicionado = value;
            }
        }

        /// <summary>
        /// [pRedBCST] Retorna ou define o Percentual de Redução da Base de Cálculo do ICMS ST. <br/>
        /// Obrigatório nos ICMS: 10, 30, 70 <br/> Opcional nos ICMS: 90
        /// </summary>
        [NFeField(ID = "N20", FieldName = "pRedBCST", DataType = "TDec_0302Opc", Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,2}(\.[0-9]{2})?")]
        public decimal PercentualReducaoBaseCalculoST
        {
            get => _percentualReducaoBaseCalculoST;
	        set
            {
                ValidarConflitoISSQN();
                if (value != 0m)
                {
                    switch (SituacaoTributaria)
                    {
                        case SituacaoTributariaICMS.Cst10:
                        case SituacaoTributariaICMS.Cst30:
                        case SituacaoTributariaICMS.Cst70:
                        case SituacaoTributariaICMS.Cst90:
                        case SituacaoTributariaICMS.CSOSN201:
                        case SituacaoTributariaICMS.CSOSN202:
                        case SituacaoTributariaICMS.CSOSN203:
                        case SituacaoTributariaICMS.CSOSN900:
                            ValidationUtil.ValidateTDec_0302(value, "PercentualReducaoBaseCalculoST");
                            break;

                        default:
                            throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangePercentualReducaoBaseCalculo, SituacaoTributaria));
                    }
                }

                _percentualReducaoBaseCalculoST = value;
            }
        }

        /// <summary>
        /// Retorna ou define a Base de Cálculo do ICMS ST <br/> Obrigatório nos ICMS: 10, 30, 60,
        /// 70, 90
        /// </summary>
        [NFeField(ID = "N21", FieldName = "vBCST", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        public decimal BaseCalculoST
        {
            get => _baseCalculoST;
	        set
            {
                ValidarConflitoISSQN();

                if (value == 0m)
                    return;

                switch (SituacaoTributaria)
                {
                    case SituacaoTributariaICMS.Cst10:
                    case SituacaoTributariaICMS.Cst30:
                    case SituacaoTributariaICMS.Cst60:
                    case SituacaoTributariaICMS.Cst70:
                    case SituacaoTributariaICMS.Cst90:
                    case SituacaoTributariaICMS.CSOSN201:
                    case SituacaoTributariaICMS.CSOSN500:
                    case SituacaoTributariaICMS.CSOSN202:
                    case SituacaoTributariaICMS.CSOSN203:
                    case SituacaoTributariaICMS.CSOSN900:
                        ValidationUtil.ValidateTDec_1302(value, "BaseCalculoST");
                        break;

                    default:
                        throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeBaseCalculo, SituacaoTributaria));
                }
                _baseCalculoST = value;
            }
        }

        /// <summary>
        /// Retorna ou define a Alíquota do ICMS ST <br/> Obrigatório nos ICMS: 10, 30, 70, 90
        /// </summary>
        [NFeField(ID = "N22", FieldName = "pICMSST", DataType = "TDec_0302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,2}(\.[0-9]{2})?")]
        public decimal AliquotaST
        {
            get => _aliquotaST;
	        set
            {
                ValidarConflitoISSQN();
                if (value != 0m)
                {
                    switch (SituacaoTributaria)
                    {
                        case SituacaoTributariaICMS.Cst10:
                        case SituacaoTributariaICMS.Cst30:
                        case SituacaoTributariaICMS.Cst70:
                        case SituacaoTributariaICMS.Cst90:
                        case SituacaoTributariaICMS.CSOSN201:
                        case SituacaoTributariaICMS.CSOSN202:
                        case SituacaoTributariaICMS.CSOSN203:
                        case SituacaoTributariaICMS.CSOSN900:
                            ValidationUtil.ValidateTDec_0302(value, "AliquotaST");
                            break;

                        default:
                            throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeAliquota, SituacaoTributaria));
                    }
                }
                _aliquotaST = value;
            }
        }

        /// <summary>
        /// Retorna ou define o Valor do ICMS ST <br/> Obrigatório nos ICMS: 10, 30, 60, 70, 90
        /// </summary>
        [NFeField(ID = "N23", FieldName = "vICMSST", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        public decimal ValorST
        {
            get => _valorST;
	        set
            {
                ValidarConflitoISSQN();
                if (value != 0m)
                {
                    switch (SituacaoTributaria)
                    {
                        case SituacaoTributariaICMS.Cst10:
                        case SituacaoTributariaICMS.Cst30:
                        case SituacaoTributariaICMS.Cst60:
                        case SituacaoTributariaICMS.Cst70:
                        case SituacaoTributariaICMS.Cst90:
                        case SituacaoTributariaICMS.CSOSN201:
                        case SituacaoTributariaICMS.CSOSN500:
                        case SituacaoTributariaICMS.CSOSN202:
                        case SituacaoTributariaICMS.CSOSN203:
                        case SituacaoTributariaICMS.CSOSN900:
                            ValidationUtil.ValidateTDec_1302(value, "ValorST");
                            break;

                        default:
                            throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.NotCanChangeValor, SituacaoTributaria));
                    }
                }

                _valorST = value;
            }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => Aliquota != 0m ||
                                  AliquotaST != 0m ||
                                  AliquotaSN != 0m ||
                                  BaseCalculo != 0m ||
                                  BaseCalculoST != 0m ||
                                  ModalidadeBaseCalculo != ModalidadeBaseCalculoIcms.NaoEspecificado ||
                                  ModalidadeBaseCalculoST != ModalidadeBaseCalculoIcmsST.NaoEspecificado ||
                                  OrigemMercadoria != OrigemMercadoria.NaoEspecificado ||
                                  PercentualMargemValorAdicionadoST != 0m ||
                                  PercentualReducaoBaseCalculo != 0m ||
                                  PercentualReducaoBaseCalculoST != 0m ||
                                  SituacaoTributaria != SituacaoTributariaICMS.NaoEspecificado ||
                                  Valor != 0m ||
                                  ValorST != 0m ||
                                  ValorSN != 0m;

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMS"); // Elemento 'ICMS'

            switch (SituacaoTributaria)
            {
                case SituacaoTributariaICMS.Cst00:
                    SerializeICMS00(writer, nfe);
                    break;

                case SituacaoTributariaICMS.Cst10:
                    SerializeICMS10(writer, nfe);
                    break;

                case SituacaoTributariaICMS.Cst20:
                    SerializeICMS20(writer, nfe);
                    break;

                case SituacaoTributariaICMS.Cst30:
                    SerializeICMS30(writer, nfe);
                    break;

                case SituacaoTributariaICMS.Cst40:
                case SituacaoTributariaICMS.Cst41:
                case SituacaoTributariaICMS.Cst50:
                    SerializeICMS40(writer, nfe);
                    break;

                case SituacaoTributariaICMS.Cst51:
                    SerializeICMS51(writer, nfe);
                    break;

                case SituacaoTributariaICMS.Cst60:
                    SerializeICMS60(writer, nfe);
                    break;

                case SituacaoTributariaICMS.Cst70:
                    SerializeICMS70(writer, nfe);
                    break;

                case SituacaoTributariaICMS.Cst90:
                    SerializeICMS90(writer, nfe);
                    break;

                case SituacaoTributariaICMS.CSOSN101:
                    SerializeICMSSN101(writer, nfe);
                    break;

                case SituacaoTributariaICMS.CSOSN102:
                case SituacaoTributariaICMS.CSOSN103:
                case SituacaoTributariaICMS.CSOSN300:
                case SituacaoTributariaICMS.CSOSN400:
                    SerializeICMSSN102(writer, nfe);
                    break;

                case SituacaoTributariaICMS.CSOSN201:
                    SerializeICMSSN201(writer, nfe);
                    break;

                case SituacaoTributariaICMS.CSOSN202:
                case SituacaoTributariaICMS.CSOSN203:
                    SerializeICMSSN202(writer, nfe);
                    break;

                case SituacaoTributariaICMS.CSOSN500:
                    SerializeICMSSN500(writer, nfe);
                    break;

                case SituacaoTributariaICMS.CSOSN900:
                    SerializeICMSSN900(writer, nfe);
                    break;
            }

            writer.WriteEndElement(); // Elemento 'ICMS'
        }

        private void SerializeICMSSN900(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMSSN900"); // Elemento 'ICMSSN900'
            writer.WriteElementString("orig", SerializationUtil.GetEnumValue(OrigemMercadoria));
            writer.WriteElementString("CSOSN", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue(SituacaoTributaria))));
            writer.WriteElementString("modBC", SerializationUtil.GetEnumValue(ModalidadeBaseCalculo));
            writer.WriteElementString("vBC", SerializationUtil.ToTDec_1302(BaseCalculo));
            if (PercentualReducaoBaseCalculo > 0)
                writer.WriteElementString("pRedBC", SerializationUtil.ToTDec_0302(PercentualReducaoBaseCalculo));
            writer.WriteElementString("pICMS", SerializationUtil.ToTDec_0302(Aliquota));
            writer.WriteElementString("vICMS", SerializationUtil.ToTDec_1302(Valor));

            if (ModalidadeBaseCalculoST != ModalidadeBaseCalculoIcmsST.NaoEspecificado)
            {
                writer.WriteElementString("modBCST", SerializationUtil.GetEnumValue(ModalidadeBaseCalculoST));
                if (PercentualMargemValorAdicionadoST > 0)
                    writer.WriteElementString("pMVAST", SerializationUtil.ToTDec_0302(PercentualMargemValorAdicionadoST));
                if (PercentualReducaoBaseCalculoST > 0)
                    writer.WriteElementString("pRedBCST", SerializationUtil.ToTDec_0302(PercentualReducaoBaseCalculoST));
                writer.WriteElementString("vBCST", SerializationUtil.ToTDec_1302(BaseCalculoST));
                writer.WriteElementString("pICMSST", SerializationUtil.ToTDec_0302(AliquotaST));
                writer.WriteElementString("vICMSST", SerializationUtil.ToTDec_1302(ValorST));
            }

            if (AliquotaSN > 0 && ValorSN > 0)
            {
                writer.WriteElementString("pCredSN", SerializationUtil.ToTDec_0302(AliquotaSN));
                writer.WriteElementString("vCredICMSSN", SerializationUtil.ToTDec_1302(ValorSN));
            }

            writer.WriteEndElement();
        }

        private void SerializeICMSSN500(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMSSN500"); // Elemento 'ICMSSN500'
            writer.WriteElementString("orig", SerializationUtil.GetEnumValue(OrigemMercadoria));
            writer.WriteElementString("CSOSN", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue(SituacaoTributaria))));
            writer.WriteElementString("vBCSTRet", SerializationUtil.ToTDec_1302(BaseCalculoST));
            writer.WriteElementString("vICMSSTRest", SerializationUtil.ToTDec_1302(ValorST));
            writer.WriteEndElement();
        }

        private void SerializeICMSSN202(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMSSN202"); // Elemento 'ICMSSN202'
            writer.WriteElementString("orig", SerializationUtil.GetEnumValue(OrigemMercadoria));
            writer.WriteElementString("CSOSN", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue(SituacaoTributaria))));
            writer.WriteElementString("modBCST", SerializationUtil.GetEnumValue(ModalidadeBaseCalculoST));
            if (PercentualMargemValorAdicionadoST > 0)
                writer.WriteElementString("pMVAST", SerializationUtil.ToTDec_0302(PercentualMargemValorAdicionadoST));
            if (PercentualReducaoBaseCalculoST > 0)
                writer.WriteElementString("pRedBCST", SerializationUtil.ToTDec_0302(PercentualReducaoBaseCalculoST));
            writer.WriteElementString("vBCST", SerializationUtil.ToTDec_1302(BaseCalculoST));
            writer.WriteElementString("pICMSST", SerializationUtil.ToTDec_0302(AliquotaST));
            writer.WriteElementString("vICMSST", SerializationUtil.ToTDec_1302(ValorST));
            writer.WriteEndElement();
        }

        private void SerializeICMSSN201(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMSSN201"); // Elemento 'ICMSSN201'
            writer.WriteElementString("orig", SerializationUtil.GetEnumValue(OrigemMercadoria));
            writer.WriteElementString("CSOSN", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue(SituacaoTributaria))));
            writer.WriteElementString("modBCST", SerializationUtil.GetEnumValue(ModalidadeBaseCalculoST));
            if (PercentualMargemValorAdicionadoST > 0)
                writer.WriteElementString("pMVAST", SerializationUtil.ToTDec_0302(PercentualMargemValorAdicionadoST));
            if (PercentualReducaoBaseCalculoST > 0)
                writer.WriteElementString("pRedBCST", SerializationUtil.ToTDec_0302(PercentualReducaoBaseCalculoST));
            writer.WriteElementString("vBCST", SerializationUtil.ToTDec_1302(BaseCalculoST));
            writer.WriteElementString("pICMSST", SerializationUtil.ToTDec_0302(AliquotaST));
            writer.WriteElementString("vICMSST", SerializationUtil.ToTDec_1302(ValorST));
            writer.WriteElementString("pCredSN", SerializationUtil.ToTDec_0302(AliquotaSN));
            writer.WriteElementString("vCredICMSSN", SerializationUtil.ToTDec_1302(ValorSN));
            writer.WriteEndElement();
        }

        private void SerializeICMSSN102(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMSSN102"); // Elemento 'ICMSSN102'
            writer.WriteElementString("orig", SerializationUtil.GetEnumValue(OrigemMercadoria));
            writer.WriteElementString("CSOSN", SerializationUtil.ToString3(Convert.ToInt32(SerializationUtil.GetEnumValue(SituacaoTributaria))));
            writer.WriteEndElement();
        }

        /// <summary>
        /// Serializa o ICMS Simples Nacional 101
        /// </summary>
        private void SerializeICMSSN101(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMSSN101"); // Elemento 'ICMSSN101'
            writer.WriteElementString("orig", SerializationUtil.GetEnumValue(OrigemMercadoria));
            writer.WriteElementString("CSOSN", SerializationUtil.ToString3(Convert.ToInt32(SerializationUtil.GetEnumValue(SituacaoTributaria))));
            writer.WriteElementString("pCredSN", SerializationUtil.ToTDec_0302(AliquotaSN));
            writer.WriteElementString("vCredICMSSN", SerializationUtil.ToTDec_1302(ValorSN));
            writer.WriteEndElement();
        }

        /// <summary>
        /// Serializa o ICMS 00
        /// </summary>
        private void SerializeICMS00(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMS00"); // Elemento 'ICMS00'
            writer.WriteElementString("orig", SerializationUtil.GetEnumValue(OrigemMercadoria));
            writer.WriteElementString("CST", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue(SituacaoTributaria))));
            writer.WriteElementString("modBC", SerializationUtil.GetEnumValue(ModalidadeBaseCalculo));
            writer.WriteElementString("vBC", SerializationUtil.ToTDec_1302(BaseCalculo));
            writer.WriteElementString("pICMS", SerializationUtil.ToTDec_0302(Aliquota));
            writer.WriteElementString("vICMS", SerializationUtil.ToTDec_1302(Valor));
            writer.WriteEndElement();
        }

        /// <summary>
        /// Serializa o ICMS 10
        /// </summary>
        private void SerializeICMS10(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMS10"); // Elemento 'ICMS10'
            writer.WriteElementString("orig", SerializationUtil.GetEnumValue(OrigemMercadoria));
            writer.WriteElementString("CST", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue(SituacaoTributaria))));
            writer.WriteElementString("modBC", SerializationUtil.GetEnumValue(ModalidadeBaseCalculo));
            writer.WriteElementString("vBC", SerializationUtil.ToTDec_1302(BaseCalculo));
            writer.WriteElementString("pICMS", SerializationUtil.ToTDec_0302(Aliquota));
            writer.WriteElementString("vICMS", SerializationUtil.ToTDec_1302(Valor));
            writer.WriteElementString("modBCST", SerializationUtil.GetEnumValue(ModalidadeBaseCalculoST));
            if (PercentualMargemValorAdicionadoST > 0)
                writer.WriteElementString("pMVAST", SerializationUtil.ToTDec_0302(PercentualMargemValorAdicionadoST));
            if (PercentualReducaoBaseCalculoST > 0)
                writer.WriteElementString("pRedBCST", SerializationUtil.ToTDec_0302(PercentualReducaoBaseCalculoST));
            writer.WriteElementString("vBCST", SerializationUtil.ToTDec_1302(BaseCalculoST));
            writer.WriteElementString("pICMSST", SerializationUtil.ToTDec_0302(AliquotaST));
            writer.WriteElementString("vICMSST", SerializationUtil.ToTDec_1302(ValorST));
            writer.WriteEndElement();
        }

        /// <summary>
        /// Serializa o ICMS 20
        /// </summary>
        private void SerializeICMS20(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMS20"); // Elemento 'ICMS20'
            writer.WriteElementString("orig", SerializationUtil.GetEnumValue(OrigemMercadoria));
            writer.WriteElementString("CST", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue(SituacaoTributaria))));
            writer.WriteElementString("modBC", SerializationUtil.GetEnumValue(ModalidadeBaseCalculo));
            writer.WriteElementString("pRedBC", SerializationUtil.ToTDec_0302(PercentualReducaoBaseCalculo));
            writer.WriteElementString("vBC", SerializationUtil.ToTDec_1302(BaseCalculo));
            writer.WriteElementString("pICMS", SerializationUtil.ToTDec_0302(Aliquota));
            writer.WriteElementString("vICMS", SerializationUtil.ToTDec_1302(Valor));
            writer.WriteEndElement();
        }

        /// <summary>
        /// Serializa o ICMS 30
        /// </summary>
        private void SerializeICMS30(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMS30"); // Elemento 'ICMS30'
            writer.WriteElementString("orig", SerializationUtil.GetEnumValue(OrigemMercadoria));
            writer.WriteElementString("CST", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue(SituacaoTributaria))));
            writer.WriteElementString("modBCST", SerializationUtil.GetEnumValue(ModalidadeBaseCalculoST));
            if (PercentualMargemValorAdicionadoST > 0)
                writer.WriteElementString("pMVAST", SerializationUtil.ToTDec_0302(PercentualMargemValorAdicionadoST));
            if (PercentualReducaoBaseCalculoST > 0)
                writer.WriteElementString("pRedBCST", SerializationUtil.ToTDec_0302(PercentualReducaoBaseCalculoST));
            writer.WriteElementString("vBCST", SerializationUtil.ToTDec_1302(BaseCalculoST));
            writer.WriteElementString("pICMSST", SerializationUtil.ToTDec_0302(AliquotaST));
            writer.WriteElementString("vICMSST", SerializationUtil.ToTDec_1302(ValorST));
            writer.WriteEndElement();
        }

        /// <summary>
        /// Serializa o ICMS 40, 41 e 50
        /// </summary>
        private void SerializeICMS40(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMS40"); // Elemento 'ICMS40'
            writer.WriteElementString("orig", SerializationUtil.GetEnumValue(OrigemMercadoria));
            writer.WriteElementString("CST", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue(SituacaoTributaria))));
            if (Valor > 0m)
            {
                writer.WriteElementString("vICMS", SerializationUtil.ToTDec_1302(Valor));
                writer.WriteElementString("motDesICMS", SerializationUtil.GetEnumValue(MotivoDesoneracaoCondicional));
            }

            writer.WriteEndElement();
        }

        /// <summary>
        /// Serializa o ICMS 51
        /// </summary>
        private void SerializeICMS51(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMS51"); // Elemento 'ICMS51'
            writer.WriteElementString("orig", SerializationUtil.GetEnumValue(OrigemMercadoria));
            writer.WriteElementString("CST", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue(SituacaoTributaria))));
            if (ModalidadeBaseCalculo != ModalidadeBaseCalculoIcms.NaoEspecificado)
                writer.WriteElementString("modBC", SerializationUtil.GetEnumValue(ModalidadeBaseCalculo));
            if (PercentualReducaoBaseCalculo > 0)
                writer.WriteElementString("pRedBC", SerializationUtil.ToTDec_0302(PercentualReducaoBaseCalculo));
            if (BaseCalculo > 0)
                writer.WriteElementString("vBC", SerializationUtil.ToTDec_1302(BaseCalculo));
            if (Aliquota > 0)
                writer.WriteElementString("pICMS", SerializationUtil.ToTDec_0302(Aliquota));
            if (Valor > 0)
                writer.WriteElementString("vICMS", SerializationUtil.ToTDec_1302(Valor));
            writer.WriteEndElement();
        }

        /// <summary>
        /// Serializa o ICMS 60
        /// </summary>
        private void SerializeICMS60(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMS60"); // Elemento 'ICMS60'
            writer.WriteElementString("orig", SerializationUtil.GetEnumValue(OrigemMercadoria));
            writer.WriteElementString("CST", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue(SituacaoTributaria))));
            writer.WriteElementString("vBCSTRet", SerializationUtil.ToTDec_1302(BaseCalculoST));
            writer.WriteElementString("vICMSSTRet", SerializationUtil.ToTDec_1302(ValorST));
            writer.WriteEndElement();
        }

        /// <summary>
        /// Serializa o ICMS 70
        /// </summary>
        private void SerializeICMS70(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMS70"); // Elemento 'ICMS70'
            writer.WriteElementString("orig", SerializationUtil.GetEnumValue(OrigemMercadoria));
            writer.WriteElementString("CST", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue(SituacaoTributaria))));
            writer.WriteElementString("modBC", SerializationUtil.GetEnumValue(ModalidadeBaseCalculo));
            writer.WriteElementString("pRedBC", SerializationUtil.ToTDec_0302(PercentualReducaoBaseCalculo));
            writer.WriteElementString("vBC", SerializationUtil.ToTDec_1302(BaseCalculo));
            writer.WriteElementString("pICMS", SerializationUtil.ToTDec_0302(Aliquota));
            writer.WriteElementString("vICMS", SerializationUtil.ToTDec_1302(Valor));
            writer.WriteElementString("modBCST", SerializationUtil.GetEnumValue(ModalidadeBaseCalculoST));
            if (PercentualMargemValorAdicionadoST > 0)
                writer.WriteElementString("pMVAST", SerializationUtil.ToTDec_0302(PercentualMargemValorAdicionadoST));
            if (PercentualReducaoBaseCalculoST > 0)
                writer.WriteElementString("pRedBCST", SerializationUtil.ToTDec_0302(PercentualReducaoBaseCalculoST));
            writer.WriteElementString("vBCST", SerializationUtil.ToTDec_1302(BaseCalculoST));
            writer.WriteElementString("pICMSST", SerializationUtil.ToTDec_0302(AliquotaST));
            writer.WriteElementString("vICMSST", SerializationUtil.ToTDec_1302(ValorST));
            writer.WriteEndElement();
        }

        /// <summary>
        /// Serializa o ICMS 90
        /// </summary>
        private void SerializeICMS90(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMS90"); // Elemento 'ICMS90'
            writer.WriteElementString("orig", SerializationUtil.GetEnumValue(OrigemMercadoria));
            writer.WriteElementString("CST", SerializationUtil.ToString2(Convert.ToInt32(SerializationUtil.GetEnumValue(SituacaoTributaria))));

            if (ModalidadeBaseCalculo != ModalidadeBaseCalculoIcms.NaoEspecificado)
            {
                writer.WriteElementString("modBC", SerializationUtil.GetEnumValue(ModalidadeBaseCalculo));
                writer.WriteElementString("vBC", SerializationUtil.ToTDec_1302(BaseCalculo));
                if (PercentualReducaoBaseCalculo > 0)
                    writer.WriteElementString("pRedBC", SerializationUtil.ToTDec_0302(PercentualReducaoBaseCalculo));
                writer.WriteElementString("pICMS", SerializationUtil.ToTDec_0302(Aliquota));
                writer.WriteElementString("vICMS", SerializationUtil.ToTDec_1302(Valor));
            }

            if (ModalidadeBaseCalculoST != ModalidadeBaseCalculoIcmsST.NaoEspecificado)
            {
                writer.WriteElementString("modBCST", SerializationUtil.GetEnumValue(ModalidadeBaseCalculoST));
                if (PercentualMargemValorAdicionadoST > 0)
                    writer.WriteElementString("pMVAST", SerializationUtil.ToTDec_0302(PercentualMargemValorAdicionadoST));
                if (PercentualReducaoBaseCalculoST > 0)
                    writer.WriteElementString("pRedBCST", SerializationUtil.ToTDec_0302(PercentualReducaoBaseCalculoST));
                writer.WriteElementString("vBCST", SerializationUtil.ToTDec_1302(BaseCalculoST));
                writer.WriteElementString("pICMSST", SerializationUtil.ToTDec_0302(AliquotaST));
                writer.WriteElementString("vICMSST", SerializationUtil.ToTDec_1302(ValorST));
            }

            writer.WriteEndElement();
        }
    }
}