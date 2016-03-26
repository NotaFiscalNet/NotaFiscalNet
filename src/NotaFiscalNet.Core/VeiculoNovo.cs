using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o Veículo Novo utilizado nos Produtos Específicos
    /// </summary>
    public sealed class VeiculoNovo : ISerializavel, IModificavel
    {
        private TipoOperacaoVendaVeiculo _tipoOperacaoVenda = TipoOperacaoVendaVeiculo.NaoEspecificado;
        private string _chassi = string.Empty;
        private string _codigoCor = string.Empty;
        private string _descricaoCor = string.Empty;
        private string _potencia = string.Empty;
        private string _cilindradas = string.Empty;
        private string _pesoLiquido = string.Empty;
        private string _pesoBruto = string.Empty;
        private string _serie = string.Empty;
        private string _combustivel = string.Empty;
        private string _numeroMotor = string.Empty;
        private decimal _capacidadeMaximaTracao;
        private int _distanciaEntreEixo;
        private int _anoModelo;
        private int _anoFabricacao;
        private TipoVeiculoRENAVAM _tipoVeiculo = TipoVeiculoRENAVAM.NaoEspecificado;
        private EspecieVeiculoRENAVAM _especieVeiculo = EspecieVeiculoRENAVAM.NaoEspecificado;
        private CondicaoVeiculo _condicaoVeiculo = CondicaoVeiculo.NaoEspecificado;
        private int _codigoMarcaModelo;
        private TipoCorDenatran _corDenatran = TipoCorDenatran.NaoEspecificado;
        private int _lotacaoMaximaPassageirosSentados;
        private TipoRestricaoVeiculo _tipoRestricao = TipoRestricaoVeiculo.NaoEspecificado;

        private readonly Produto _produto;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="VeiculoNovo"/>.
        /// </summary>
        public VeiculoNovo()
        {
            ChassiRemarcado = false;
            TipoPintura = string.Empty;
        }

        internal VeiculoNovo(Produto produto)
        {
            ChassiRemarcado = false;
            TipoPintura = string.Empty;
            _produto = produto;
        }

        /// <summary>
        /// [tpOp] Retorna ou define o Tipo da Operação de Venda.
        /// </summary>
        [NFeField(ID = "J02", FieldName = "tpOp", DataType = "token")]
        [CampoValidavel(1, ChaveErroValidacao.CampoNaoPreenchido, ValorNaoPreenchido = TipoOperacaoVendaVeiculo.NaoEspecificado)]
        public TipoOperacaoVendaVeiculo TipoOperacaoVenda
        {
            get { return _tipoOperacaoVenda; }
            set
            {
                _tipoOperacaoVenda = ValidationUtil.ValidateEnum<TipoOperacaoVendaVeiculo>(value, "TipoOperacaoVenda");
            }
        }

        /// <summary>
        /// [chassi] Retorna ou define o Código de Chassi do Veículo.
        /// </summary>
        [NFeField(ID = "J03", FieldName = "chassi", DataType = "token", MinLength = 17, MaxLength = 17)]
        [CampoValidavel(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public string Chassi
        {
            get { return _chassi; }
            set
            {
                _chassi = ValidationUtil.ValidateRange(SerializationUtil.ToToken(value), 17, 17, "Chassi");
            }
        }

        /// <summary>
        /// [cCor] Retorna ou define o Código da Cor do Veículo, específico de cada montadora.
        /// </summary>
        [NFeField(ID = "J04", FieldName = "cCor", DataType = "token", MinLength = 1, MaxLength = 4)]
        [CampoValidavel(3, ChaveErroValidacao.CampoNaoPreenchido)]
        public string CodigoCor
        {
            get { return _codigoCor; }
            set
            {
                _codigoCor = ValidationUtil.ValidateRange(SerializationUtil.ToToken(value), 1, 4, "CodigoCor");
            }
        }

        /// <summary>
        /// [xCor] Retorna ou define a Descrição da Cor do Veículo, específico de cada montadora.
        /// </summary>
        [NFeField(ID = "J05", FieldName = "xCor", DataType = "token", MinLength = 1, MaxLength = 40)]
        [CampoValidavel(4, ChaveErroValidacao.CampoNaoPreenchido)]
        public string DescricaoCor
        {
            get { return _descricaoCor; }
            set
            {
                _descricaoCor = ValidationUtil.ValidateRange(SerializationUtil.ToToken(value), 1, 40, "DescricaoCor");
            }
        }

        /// <summary>
        /// [pot] Retorna ou define a Potencia do Motor em cavalo vapor (CV)
        /// </summary>
        [NFeField(ID = "J06", FieldName = "pot", DataType = "token", MinLength = 1, MaxLength = 4)]
        [CampoValidavel(5, ChaveErroValidacao.CampoNaoPreenchido)]
        public string PotenciaMotor
        {
            get { return _potencia; }
            set
            {
                _potencia = ValidationUtil.ValidateRange(SerializationUtil.ToToken(value), 1, 4, "PotenciaMotor");
            }
        }

        /// <summary>
        /// [cilin] Retorna ou define a Capacidade voluntária do motor expressa em centímetros
        /// cúbicos (CC).
        /// </summary>
        /// <remarks>De 1 até 4 caracteres.</remarks>
        [NFeField(ID = "J07", FieldName = "cilin", DataType = "token", MinLength = 1, MaxLength = 4)]
        [CampoValidavel(6, ChaveErroValidacao.CampoNaoPreenchido)]
        public string Cilindradas
        {
            get
            {
                return _cilindradas;
            }
            set
            {
                _cilindradas = ValidationUtil.ValidateRange(SerializationUtil.ToToken(value), 1, 4, "CM3");
            }
        }

        /// <summary>
        /// [pesoL] Retorna ou define o Peso Líquido do Veículo.
        /// </summary>
        /// <remarks>De 1 até 9 caracteres</remarks>
        [NFeField(ID = "J08", FieldName = "pesoL", DataType = "token", MinLength = 1, MaxLength = 9)]
        [CampoValidavel(7, ChaveErroValidacao.CampoNaoPreenchido)]
        public string PesoLiquido
        {
            get
            {
                return _pesoLiquido;
            }
            set
            {
                _pesoLiquido = ValidationUtil.ValidateRange(SerializationUtil.ToToken(value), 1, 9, "PesoLiquido");
            }
        }

        /// <summary>
        /// [pesoB] Retorna ou define o Peso Bruto do Veículo.
        /// </summary>
        /// <remarks>De 1 até 9 caracteres</remarks>
        [NFeField(ID = "J09", FieldName = "pesoB", DataType = "token", MinLength = 1, MaxLength = 9)]
        [CampoValidavel(8, ChaveErroValidacao.CampoNaoPreenchido)]
        public string PesoBruto
        {
            get
            {
                return _pesoBruto;
            }
            set
            {
                _pesoBruto = ValidationUtil.ValidateRange(SerializationUtil.ToToken(value), 1, 9, "PesoBruto");
            }
        }

        /// <summary>
        /// [nSerie] Retorna ou define o Número de Série do Veículo.
        /// </summary>
        [NFeField(ID = "J10", FieldName = "nSerie", DataType = "token", MinLength = 1, MaxLength = 9)]
        [CampoValidavel(9, ChaveErroValidacao.CampoNaoPreenchido)]
        public string NumeroSerie
        {
            get { return _serie; }
            set
            {
                _serie = ValidationUtil.ValidateRange(SerializationUtil.ToToken(value), 1, 9, "NumeroSerie");
            }
        }

        /// <summary>
        /// [tpComb] Retorna ou define o Tipo do Combustível do Veículo. 01 ALCOOL 02 GASOLINA 03
        /// DIESEL 04 GASOGENIO 05 GAS METANO 06 ELETRICO/FONTE INTERNA 07 ELETRICO/FONTE EXTERNA 08
        /// GASOL/GAS NATURAL COMBUSTIVEL 09 ALCOOL/GAS NATURAL COMBUSTIVEL 10 DIESEL/GAS NATURAL
        /// COMBUSTIVEL 11 VIDE/CAMPO/OBSERVACAO 12 ALCOOL/GAS NATURAL VEICULAR 13 GASOLINA/GAS
        /// NATURAL VEICULAR 14 DIESEL/GAS NATURAL VEICULAR 15 GAS NATURAL VEICULAR 16
        /// ALCOOL/GASOLINA 17 GASOLINA/ALCOOL/GAS NATURAL 18 GASOLINA/ELETRICO
        /// </summary>
        /// <remarks>De 1 até 2 caracteres.</remarks>
        [NFeField(ID = "J11", FieldName = "tpComb", DataType = "token", MinLength = 1, MaxLength = 2)]
        [CampoValidavel(10, ChaveErroValidacao.CampoNaoPreenchido)]
        public string Combustivel
        {
            get { return _combustivel; }
            set
            {
                _combustivel = ValidationUtil.ValidateRange(SerializationUtil.ToToken(value), 1, 2, "Combustivel");
            }
        }

        /// <summary>
        /// [nMotor] Retorna ou define o Número do Motor.
        /// </summary>
        /// <remarks>De 1 até 21 caracteres.</remarks>
        [NFeField(ID = "J12", FieldName = "nMotor", DataType = "token", MinLength = 1, MaxLength = 21)]
        [CampoValidavel(11, ChaveErroValidacao.CampoNaoPreenchido)]
        public string NumeroMotor
        {
            get { return _numeroMotor; }
            set
            {
                _numeroMotor = ValidationUtil.ValidateRange(SerializationUtil.ToToken(value), 1, 21, "NumeroMotor");
            }
        }

        /// <summary>
        /// [CMT] Retorna ou define a capacidade máxima de tração do Veículo, em toneladas, 4 casas decimais
        /// </summary>
        [NFeField(FieldName = "CMT")]
        [CampoValidavel(12, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal CapacidadeMaximaTracao
        {
            get { return _capacidadeMaximaTracao; }
            set
            {
                _capacidadeMaximaTracao = ValidationUtil.ValidateTDec_0504(value, "CapacidadeMaximaTracao");
            }
        }

        /// <summary>
        /// [dist] Retorna ou define a Distância entre os Eixos
        /// </summary>
        [NFeField(ID = "J14", FieldName = "dist", DataType = "token")]
        [CampoValidavel(13, ChaveErroValidacao.CampoNaoPreenchido)]
        public int DistanciaEixos
        {
            get { return _distanciaEntreEixo; }
            set
            {
                _distanciaEntreEixo = ValidationUtil.ValidateRange(value, 0, 9999, "DistanciaEixos");
            }
        }

        /// <summary>
        /// [anoMod] Retorna ou define o Ano Modelo do Veículo.
        /// </summary>
        /// <remarks>O valor deve compreender de 0 até 9999.</remarks>
        [NFeField(ID = "J16", FieldName = "anoMod", DataType = "token", MinLength = 1, MaxLength = 4, Pattern = "[0-9]{4}")]
        [CampoValidavel(15, ChaveErroValidacao.CampoNaoPreenchido)]
        public int AnoModelo
        {
            get { return _anoModelo; }
            set
            {
                _anoModelo = ValidationUtil.ValidateRange(value, 0, 9999, "AnoModelo");
            }
        }

        /// <summary>
        /// [anoFab] Retorna ou define o Ano de Fabricação do Veículo
        /// </summary>
        /// <remarks>O valor deve compreender de 0 até 9999.</remarks>
        [NFeField(ID = "J17", FieldName = "anoFab", DataType = "token", MinLength = 1, MaxLength = 4, Pattern = "[0-9]{4}")]
        [CampoValidavel(16, ChaveErroValidacao.CampoNaoPreenchido)]
        public int AnoFabricacao
        {
            get { return _anoFabricacao; }
            set
            {
                _anoFabricacao = ValidationUtil.ValidateRange(value, 0, 9999, "AnoFabricacao");
            }
        }

        /// <summary>
        /// [tpPint] Retorna ou define o Tipo de Pintura.
        /// </summary>
        [NFeField(ID = "J18", FieldName = "tpPint", DataType = "token", MinLength = 1, MaxLength = 1), CampoValidavel(17, ChaveErroValidacao.CampoNaoPreenchido)]
        public string TipoPintura { get; set; }

        /// <summary>
        /// [tpVeic] Retorna ou define Tipo do Veículo de acordo com a tabela do RENAVAM.
        /// </summary>
        [NFeField(ID = "J19", FieldName = "tpVeic", DataType = "token", Pattern = "[0-9]{1,2}")]
        [CampoValidavel(18, ChaveErroValidacao.CampoNaoPreenchido, ValorNaoPreenchido = TipoVeiculoRENAVAM.NaoEspecificado)]
        public TipoVeiculoRENAVAM TipoVeiculo
        {
            get { return _tipoVeiculo; }
            set
            {
                _tipoVeiculo = ValidationUtil.ValidateEnum(value, "TipoVeiculo");
            }
        }

        /// <summary>
        /// [espVeic] Retorna ou define a Espécie do Veículo de acordo com a tabela do RENAVAM.
        /// </summary>
        [NFeField(ID = "J20", FieldName = "espVeic", DataType = "token", Pattern = "[0-9]{1}")]
        [CampoValidavel(19, ChaveErroValidacao.CampoNaoPreenchido, ValorNaoPreenchido = EspecieVeiculoRENAVAM.NaoEspecificado)]
        public EspecieVeiculoRENAVAM EspecieVeiculo
        {
            get { return _especieVeiculo; }
            set
            {
                _especieVeiculo = ValidationUtil.ValidateEnum<EspecieVeiculoRENAVAM>(value, "EspecieVeiculo");
            }
        }

        /// <summary>
        /// [VIN] Retorna ou define se o chassi foi remarcado.
        /// </summary>
        [NFeField(ID = "J21", FieldName = "VIN"), CampoValidavel(20, ChaveErroValidacao.CampoNaoPreenchido)]
        public bool ChassiRemarcado { get; set; }

        /// <summary>
        /// [condVeic] Retorna ou define a Condição do Veículo
        /// </summary>
        [NFeField(ID = "J22", FieldName = "condVeic", DataType = "token")]
        [CampoValidavel(21, ChaveErroValidacao.CampoNaoPreenchido, ValorNaoPreenchido = CondicaoVeiculo.NaoEspecificado)]
        public CondicaoVeiculo CondicaoVeiculo
        {
            get { return _condicaoVeiculo; }
            set
            {
                _condicaoVeiculo = ValidationUtil.ValidateEnum<CondicaoVeiculo>(value, "CondicaoVeiculo");
            }
        }

        /// <summary>
        /// [cMod] Retorna ou define o Código Marca Modelo de acordo com a tabela do RENAVAM
        /// </summary>
        [NFeField(ID = "J23", FieldName = "cMod", DataType = "token", Pattern = "[0-9]{1,6}")]
        [CampoValidavel(22, ChaveErroValidacao.CampoNaoPreenchido)]
        public int CodigoMarcaModelo
        {
            get { return _codigoMarcaModelo; }
            set
            {
                _codigoMarcaModelo = ValidationUtil.ValidateRange(value, 0, 999999, "CodigoMarcaModelo");
            }
        }

        /// <summary>
        /// [cCorDENATRAN] Retorna ou define o Código da Cor Segundo as regras de pré-cadastro do DENATRAN.
        /// </summary>
        [NFeField(ID = "J24", FieldName = "cCorDENATRAN")]
        [CampoValidavel(22, ChaveErroValidacao.CampoNaoPreenchido)]
        public TipoCorDenatran CorDenatran
        {
            get { return _corDenatran; }
            set
            {
                _corDenatran = ValidationUtil.ValidateEnum(value, "CorDenatran");
            }
        }

        /// <summary>
        /// [lota] Retorna ou define a Quantidade máxima permitida de passageiros sentados, inclusive
        /// o motorista.
        /// </summary>
        [NFeField(ID = "J25", FieldName = "lota")]
        [CampoValidavel(22, ChaveErroValidacao.CampoNaoPreenchido)]
        public int LotacaoMaximaPassageirosSentados
        {
            get { return _lotacaoMaximaPassageirosSentados; }
            set
            {
                _lotacaoMaximaPassageirosSentados = ValidationUtil.ValidateRange(value, 0, 999, "LotacaoMaximaPassageirosSentados");
            }
        }

        /// <summary>
        /// [tpRest] Retorna ou define o Tipo de Restrição do Veículo.
        /// </summary>
        [NFeField(ID = "J26", FieldName = "tpRest")]
        [CampoValidavel(22, ChaveErroValidacao.CampoNaoPreenchido)]
        public TipoRestricaoVeiculo TipoRestricao
        {
            get { return _tipoRestricao; }
            set
            {
                _tipoRestricao = ValidationUtil.ValidateEnum(value, "TipoRestricao");
            }
        }

        /// <summary>
        /// Retorna a referência para o objeto Produto no qual o VeiculoNovo se refere.
        /// </summary>
        internal Produto Produto => _produto;

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => TipoOperacaoVenda != TipoOperacaoVendaVeiculo.NaoEspecificado ||
                                  !string.IsNullOrEmpty(Chassi) ||
                                  !string.IsNullOrEmpty(CodigoCor) ||
                                  !string.IsNullOrEmpty(DescricaoCor) ||
                                  !string.IsNullOrEmpty(PotenciaMotor) ||
                                  !string.IsNullOrEmpty(Cilindradas) ||
                                  !string.IsNullOrEmpty(PesoLiquido) ||
                                  !string.IsNullOrEmpty(PesoBruto) ||
                                  !string.IsNullOrEmpty(NumeroSerie) ||
                                  !string.IsNullOrEmpty(Combustivel) ||
                                  !string.IsNullOrEmpty(NumeroMotor) ||
                                  CapacidadeMaximaTracao != 0 ||
                                  DistanciaEixos != 0 ||
                                  AnoModelo != 0 ||
                                  AnoFabricacao != 0 ||
                                  !char.Equals(TipoPintura, ' ') ||
                                  TipoVeiculo != TipoVeiculoRENAVAM.NaoEspecificado ||
                                  EspecieVeiculo != EspecieVeiculoRENAVAM.NaoEspecificado ||
                                  CondicaoVeiculo != CondicaoVeiculo.NaoEspecificado ||
                                  CodigoMarcaModelo != 0 ||
                                  CorDenatran != TipoCorDenatran.NaoEspecificado ||
                                  LotacaoMaximaPassageirosSentados > 0 ||
                                  TipoRestricao != TipoRestricaoVeiculo.NaoEspecificado;

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("veicProd");

            writer.WriteElementString("tpOp", SerializationUtil.GetEnumValue<TipoOperacaoVendaVeiculo>(TipoOperacaoVenda));
            writer.WriteElementString("chassi", SerializationUtil.ToToken(Chassi, 17));
            writer.WriteElementString("cCor", SerializationUtil.ToToken(CodigoCor, 4));
            writer.WriteElementString("xCor", SerializationUtil.ToToken(DescricaoCor, 40));
            writer.WriteElementString("pot", SerializationUtil.ToToken(PotenciaMotor, 4));
            writer.WriteElementString("cilin", SerializationUtil.ToToken(Cilindradas, 4));
            writer.WriteElementString("pesoL", PesoLiquido.ToString());
            writer.WriteElementString("pesoB", PesoBruto.ToString());
            writer.WriteElementString("nSerie", SerializationUtil.ToToken(NumeroSerie, 9));
            writer.WriteElementString("tpComb", SerializationUtil.ToToken(Combustivel, 8));
            writer.WriteElementString("nMotor", SerializationUtil.ToToken(NumeroMotor, 21));
            writer.WriteElementString("CMT", SerializationUtil.ToToken(SerializationUtil.ToTDec_0504(CapacidadeMaximaTracao).Replace(".", ""), 9));
            writer.WriteElementString("dist", DistanciaEixos.ToString());
            writer.WriteElementString("anoMod", AnoModelo.ToString());
            writer.WriteElementString("anoFab", AnoFabricacao.ToString());
            writer.WriteElementString("tpPint", TipoPintura);
            writer.WriteElementString("tpVeic", SerializationUtil.GetEnumValue<TipoVeiculoRENAVAM>(TipoVeiculo));
            writer.WriteElementString("espVeic", SerializationUtil.GetEnumValue<EspecieVeiculoRENAVAM>(EspecieVeiculo));
            writer.WriteElementString("VIN", ChassiRemarcado ? "R" : "N");
            writer.WriteElementString("condVeic", SerializationUtil.GetEnumValue<CondicaoVeiculo>(CondicaoVeiculo));
            writer.WriteElementString("cMod", CodigoMarcaModelo.ToString());
            writer.WriteElementString("cCorDENATRAN", ((int)CorDenatran).ToString("00"));
            writer.WriteElementString("lota", LotacaoMaximaPassageirosSentados.ToString());
            writer.WriteElementString("tpRest", TipoRestricao.GetEnumValue());

            writer.WriteEndElement(); // fim do elemento 'veicProd'
        }
    }
}