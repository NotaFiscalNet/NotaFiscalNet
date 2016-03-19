using System.Runtime.InteropServices;
using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using NotaFiscalNet.Core.Validacao.Validators;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa as informações de Transporte da Nota Fiscal Eletrônica.
    /// </summary>
    
    
    
    public sealed class TransporteNFe : ISerializavel
    {
        #region Fields

        private TipoModalidadeFrete _modFrete = TipoModalidadeFrete.Destinatario;
        private Transportador _transporta = new Transportador();
        private RetencaoICMSTransporte _retencaoICMS = new RetencaoICMSTransporte();
        private VeiculoTransporte _veiculoTransporte = new VeiculoTransporte();
        private ReboqueCollection _reboques = new ReboqueCollection();
        private VolumeCargaCollection _volumes = new VolumeCargaCollection();
        private TipoMeioTransporte _meioTransporte = TipoMeioTransporte.Rodoviario;

        public TransporteNFe()
        {
            Vagao = string.Empty;
            Balsa = string.Empty;
        }

        #endregion Fields

        #region Properties

        /// <summary>
        ///  Retorna ou define o tipo do meio utilizado no transporte
        /// </summary>
        [ValidateField(0, false)]
        public TipoMeioTransporte MeioTransporte
        {
            get { return _meioTransporte; }
            set
            {
                ValidationUtil.ValidateEnum(value, "MeioTransporte");
                _meioTransporte = value;
            }
        }

        /// <summary>
        /// [modFrete] Retorna ou define a Modalidade de Frete.
        /// </summary>
        [NFeField(FieldName = "modFrete", DataType = "token", ID = "X02")]
        [ValidateField(1, true)]
        public TipoModalidadeFrete ModalidadeFrete
        {
            get { return _modFrete; }
            set {
                ValidationUtil.ValidateEnum(value, "ModalidadeFrete");
                _modFrete = value; 
            }
        }

        /// <summary>
        /// [transporta] Retorna as informações do Transportador.
        /// </summary>
        [NFeField(FieldName = "transporta", ID = "X03")]
        [ValidateField(2, true)]
        public Transportador Transportador
        {
            get { return _transporta; }
        }

        /// <summary>
        /// [retTransp] Retorna as informações de Retenção de ICMS do transporte. <strong>Opcional</strong>.
        /// </summary>
        [NFeField(FieldName = "retTransp", ID = "X11")]
        [ValidateField(3, true)]
        public RetencaoICMSTransporte RetencaoICMS
        {
            get { return _retencaoICMS; }
        }

        /// <summary>
        /// [veicTransp] Retorna as informações do Veículo de Transporte da Carga. <strong>Opcional</strong>.
        /// </summary>
        [NFeField(FieldName = "veicTransp", ID = "X18")]
        [ValidateField(4, true)]
        public VeiculoTransporte VeiculoTransporte
        {
            get { return _veiculoTransporte; }
        }

        /// <summary>
        /// [reboque] Retorna a lista de Reboques. <strong>Opcional</strong>.
        /// </summary>
        [NFeField(FieldName = "reboque", ID = "X22")]
        [ValidateField(5, ChaveErroValidacao.CollectionMinValue, Validator = typeof(RangeCollectionValidator), MaxLength = 5)]
        public ReboqueCollection Reboques
        {
            get { return _reboques; }
        }
        
        /// <summary>
        /// [vol] Retorna a lista de Volumes da Carga. <strong>Opcional</strong>.
        /// </summary>
        [NFeField(FieldName = "vol", ID = "X26")]
        [ValidateField(6, true)]
        public VolumeCargaCollection VolumesCarga
        {
            get { return _volumes; }
        }

        /// <summary>
        /// [vagao] Retorna ou define os dados do Vagão.
        /// </summary>
        [NFeField(FieldName = "vagao", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}", MinLength = 1, MaxLength = 20, Opcional = true), ValidateField(7, true)]
        public string Vagao { get; set; }

        /// <summary>
        /// [balsa] Retorna ou define os dados do Vagão.
        /// </summary>
        [NFeField(FieldName = "balsa", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}", MinLength = 1, MaxLength = 20, Opcional = true), ValidateField(8, true)]
        public string Balsa { get; set; }

        #endregion Properties

        #region ISerializavel Members

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            #region transp

            writer.WriteStartElement("transp");

            writer.WriteElementString("modFrete", SerializationUtil.GetEnumValue<TipoModalidadeFrete>(ModalidadeFrete));

            if (Transportador.IsDirty)
                ((ISerializavel)Transportador).Serializar(writer, nfe);

            if (RetencaoICMS.Modificado)
                ((ISerializavel)RetencaoICMS).Serializar(writer, nfe);

            switch (MeioTransporte)
            {
                case TipoMeioTransporte.Rodoviario:
                    if (VeiculoTransporte.Modificado)
                    {
                        writer.WriteStartElement("veicTransp");
                        ((ISerializavel)VeiculoTransporte).Serializar(writer, nfe);
                        writer.WriteEndElement();
                    }
                    if (Reboques.IsDirty)
                        ((ISerializavel)Reboques).Serializar(writer, nfe);
                    break;
                case TipoMeioTransporte.Ferroviario:
                    if (!string.IsNullOrEmpty(Vagao))
                        writer.WriteElementString("vagao", Vagao); 
                    break;
                case TipoMeioTransporte.Pluvial:
                    if (!string.IsNullOrEmpty(Balsa))
                        writer.WriteElementString("balsa", Balsa);
                    break;
            }

            if (VolumesCarga.IsDirty)
                ((ISerializavel)VolumesCarga).Serializar(writer, nfe);

            writer.WriteEndElement(); // fim do elemento 'transp'

            #endregion transp
        }

        #endregion
    }
}