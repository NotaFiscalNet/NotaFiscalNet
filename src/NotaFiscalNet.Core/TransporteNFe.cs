using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;


namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa as informações de Transporte da Nota Fiscal Eletrônica.
    /// </summary>

    public sealed class TransporteNFe : ISerializavel
    {
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

        /// <summary>
        /// Retorna ou define o tipo do meio utilizado no transporte
        /// </summary>
        [CampoValidavel(0, Opcional = false)]
        public TipoMeioTransporte MeioTransporte
        {
            get => _meioTransporte;
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
        [CampoValidavel(1, Opcional = true)]
        public TipoModalidadeFrete ModalidadeFrete
        {
            get => _modFrete;
	        set
            {
                ValidationUtil.ValidateEnum(value, "ModalidadeFrete");
                _modFrete = value;
            }
        }

        /// <summary>
        /// [transporta] Retorna as informações do Transportador.
        /// </summary>
        [NFeField(FieldName = "transporta", ID = "X03")]
        [CampoValidavel(2, Opcional = true)]
        public Transportador Transportador => _transporta;

        /// <summary>
        /// [retTransp] Retorna as informações de Retenção de ICMS do transporte. <strong>Opcional</strong>.
        /// </summary>
        [NFeField(FieldName = "retTransp", ID = "X11")]
        [CampoValidavel(3, Opcional = true)]
        public RetencaoICMSTransporte RetencaoICMS => _retencaoICMS;

        /// <summary>
        /// [veicTransp] Retorna as informações do Veículo de Transporte da Carga. <strong>Opcional</strong>.
        /// </summary>
        [NFeField(FieldName = "veicTransp", ID = "X18")]
        [CampoValidavel(4, Opcional = true)]
        public VeiculoTransporte VeiculoTransporte => _veiculoTransporte;

        /// <summary>
        /// [reboque] Retorna a lista de Reboques. <strong>Opcional</strong>.
        /// </summary>
        [NFeField(FieldName = "reboque", ID = "X22")]
        [CampoValidavel(5, ChaveErroValidacao.CollectionMinValue)]
        public ReboqueCollection Reboques => _reboques;

        /// <summary>
        /// [vol] Retorna a lista de Volumes da Carga. <strong>Opcional</strong>.
        /// </summary>
        [NFeField(FieldName = "vol", ID = "X26")]
        [CampoValidavel(6, Opcional = true)]
        public VolumeCargaCollection VolumesCarga => _volumes;

        /// <summary>
        /// [vagao] Retorna ou define os dados do Vagão.
        /// </summary>
        [NFeField(FieldName = "vagao", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}", MinLength = 1, MaxLength = 20, Opcional = true), CampoValidavel(7, Opcional = true)]
        public string Vagao { get; set; }

        /// <summary>
        /// [balsa] Retorna ou define os dados do Vagão.
        /// </summary>
        [NFeField(FieldName = "balsa", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}", MinLength = 1, MaxLength = 20, Opcional = true), CampoValidavel(8, Opcional = true)]
        public string Balsa { get; set; }

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("transp");

            writer.WriteElementString("modFrete", SerializationUtil.GetEnumValue<TipoModalidadeFrete>(ModalidadeFrete));

            if (Transportador.Modificado)
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
                    if (Reboques.Modificado)
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

            if (VolumesCarga.Modificado)
                VolumesCarga.Serializar(writer, nfe);

            writer.WriteEndElement(); // fim do elemento 'transp'
        }
    }
}