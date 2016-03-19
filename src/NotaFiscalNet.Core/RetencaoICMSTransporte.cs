using System.Runtime.InteropServices;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa as informações de Retenção de ICMS do Transporte.
    /// </summary>
    
    
    
    public sealed class RetencaoICMSTransporte : INFeSerializable, IDirtyable
    {
        #region Fields

        private decimal _valorServico;
        private decimal _BaseCalculoRetencao;
        private decimal _aliquotaRetencao;
        private decimal _valorICMSRetido;
        private int _cfop;
        private int _codigoMunicipioFatorGeradorICMS;

        public RetencaoICMSTransporte()
        {
        }

        #endregion Fields

        #region Properties

        /// <summary>
        /// [vServ] Retorna ou define o Valor do Serviço de Transporte retido.
        /// </summary>
        [NFeField(FieldName = "vServ", DataType = "TDec_1302", ID = "X12", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal ValorServico
        {
            get { return _valorServico; }
            set {
                ValidationUtil.ValidateTDec_1302(value, "ValorServico"); 
                
                _valorServico = value; 
            }
        }

        /// <summary>
        /// [vBCRet] Retorna ou define o Valor da Base de Cálculo de Retenção do ICMS.
        /// </summary>
        [NFeField(FieldName = "vBCRet", DataType = "TDec_1302", ID = "X13", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal BaseCalculoRetencao
        {
            get { return _BaseCalculoRetencao; }
            set {
                ValidationUtil.ValidateTDec_1302(value, "BaseCalculoRetencao");
                
                _BaseCalculoRetencao = value; 
            }
        }

        /// <summary>
        /// [pICMSRet] Retorna ou define a Alíquota de Retenção do ICMS.
        /// </summary>
        [NFeField(FieldName = "pICMSRet", DataType = "TDec_0302", ID = "X14", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,2}(\.[0-9]{2})?")]
        [ValidateField(3, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal AliquotaRetencao
        {
            get { return _aliquotaRetencao; }
            set {
                ValidationUtil.ValidateTDec_0302(value, "AliquotaRetencao");
                
                _aliquotaRetencao = value; 
            }
        }

        /// <summary>
        /// [vICMSRet] Retorna ou define o Valor do ICMS retido.
        /// </summary>
        [NFeField(FieldName = "vICMSRet", DataType = "TDec_1302", ID = "X15", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [ValidateField(4, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal ValorICMSRetido
        {
            get { return _valorICMSRetido; }
            set {
                ValidationUtil.ValidateTDec_1302(value, "ValorICMSRetido"); 
                
                _valorICMSRetido = value; 
            }
        }

        /// <summary>
        /// [CFOP] Retorna ou define o CFOP (Código Fiscal de Operações e Prestações).
        /// </summary>
        [NFeField(FieldName = "CFOP", DataType = "TCfop", ID = "X16", Pattern = @"[123567][0-9]([0-9][1-9]|[1-9][0-9])")]
        [ValidateField(5, ChaveErroValidacao.CampoNaoPreenchido)]
        public int CFOP
        {
            get { return _cfop; }
            set {
                ValidationUtil.ValidateTCfop(value, "CFOP");
                
                _cfop = value; 
            }
        }

        /// <summary>
        /// Retorna ou define o Código do Município de Ocorrência do Fator Gerador do ICMS do transporte. Utilizar os códigos de municípios do IBGE.
        /// </summary>
        [NFeField(FieldName = "cMunFG", DataType = "TCodMunIBGE", ID = "X17", Pattern = @"[0-9]{7}")]
        [ValidateField(6, ChaveErroValidacao.CampoNaoPreenchido)]
        public int CodigoMunicipioFatorGerador
        {
            get { return _codigoMunicipioFatorGeradorICMS; }
            set {
                ValidationUtil.ValidateTCodMunIBGE(value, "CodigoMunicipioFatorGerador");
                
                _codigoMunicipioFatorGeradorICMS = value; 
            }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return
                    ValorServico != 0m ||
                    BaseCalculoRetencao != 0m ||
                    AliquotaRetencao != 0m ||
                    ValorICMSRetido != 0m ||
                    CFOP != 0 ||
                    CodigoMunicipioFatorGerador != 0;
            }
        }

        #endregion Properties

        #region INFeSerializable Members

        void INFeSerializable.Serialize(System.Xml.XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("retTransp"); // Elemento 'retTransp'

            writer.WriteElementString("vServ", SerializationUtil.ToTDec_1302(ValorServico));
            writer.WriteElementString("vBCRet", SerializationUtil.ToTDec_1302(BaseCalculoRetencao));
            writer.WriteElementString("pICMSRet", SerializationUtil.ToTDec_0302(AliquotaRetencao));
            writer.WriteElementString("vICMSRet", SerializationUtil.ToTDec_1302(ValorICMSRetido));
            writer.WriteElementString("CFOP", SerializationUtil.ToString4(CFOP));
            writer.WriteElementString("cMunFG", SerializationUtil.ToString7(CodigoMunicipioFatorGerador));

            writer.WriteEndElement(); // Elemento 'retTransp'
        }

        #endregion
    }
}