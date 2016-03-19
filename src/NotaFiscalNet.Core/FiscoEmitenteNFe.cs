using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using System;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa as informações do Fisco do Emitente da Nota Fiscal Eletrônica.
    /// </summary>
    public sealed class FiscoEmitenteNFe : INFeSerializable, IDirtyable
    {
        void INFeSerializable.Serialize(XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("avulsa");

            writer.WriteElementString("CNPJ", SerializationUtil.ToCNPJ(CNPJ));
            writer.WriteElementString("xOrgao", SerializationUtil.ToToken(OrgaoEmitente, 60));
            writer.WriteElementString("matr", SerializationUtil.ToToken(MatriculaAgente, 60));
            writer.WriteElementString("xAgente", SerializationUtil.ToToken(NomeAgente, 60));
            writer.WriteElementString("fone", SerializationUtil.ToToken(Telefone, 10));
            writer.WriteElementString("UF", UF.ToString());
            writer.WriteElementString("nDAR", SerializationUtil.ToToken(NumeroDAR, 60));
            writer.WriteElementString("dEmi", SerializationUtil.ToTData(DataEmissaoDAR));
            writer.WriteElementString("vDAR", SerializationUtil.ToTDec_1204(ValorDAR));
            writer.WriteElementString("repEmi", SerializationUtil.ToToken(ReparticaoFiscalEmitente, 60));

            if (DataPagamentoDAR != DateTime.MinValue)
                writer.WriteElementString("dPag", SerializationUtil.ToTData(DataPagamentoDAR));

            writer.WriteEndElement(); // fim do elemento 'avulsa'
        }

        private string _CNPJ = string.Empty;
        private string _orgaoEmitente = string.Empty;
        private string _matriculaAgente = string.Empty;
        private string _nomeAgente = string.Empty;
        private string _telefone = string.Empty;
        private SiglaUF _UF = SiglaUF.NaoEspecificado;
        private string _numeroDAR = string.Empty;
        private DateTime _dataEmissaoDAR;
        private decimal _valorDAR;
        private string _reparticaoFiscalEmitente = string.Empty;
        private DateTime _dataPagamentoDAR;

        /// <summary>
        /// Retorna ou define o CNPJ do Órgão Emitente
        /// </summary>
        [NFeField(ID = "D02", FieldName = "CNPJ", DataType = "TCnpj", Pattern = "[0-9]{14}")]
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public string CNPJ
        {
            get { return _CNPJ; }
            set { _CNPJ = ValidationUtil.ValidateCNPJ(value, "CNPJ", true); }
        }

        /// <summary>
        /// Retorna ou define o nome do Órgão Emitente
        /// </summary>
        [NFeField(ID = "D03", FieldName = "xOrgao", DataType = "token", MinLength = 1, MaxLength = 60)]
        [ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public string OrgaoEmitente
        {
            get { return _orgaoEmitente; }
            set { _orgaoEmitente = ValidationUtil.TruncateString(value, 60); }
        }

        /// <summary>
        /// Retorna ou define a Matrícula do Agente
        /// </summary>
        [NFeField(ID = "D04", FieldName = "matr", DataType = "token", MinLength = 1, MaxLength = 60)]
        [ValidateField(3, ChaveErroValidacao.CampoNaoPreenchido)]
        public string MatriculaAgente
        {
            get { return _matriculaAgente; }
            set { _matriculaAgente = ValidationUtil.TruncateString(value, 60); }
        }

        /// <summary>
        /// Retorna ou define o nome do Agente.
        /// </summary>
        [NFeField(ID = "D05", FieldName = "xAgente", DataType = "token", MinLength = 1, MaxLength = 60)]
        [ValidateField(4, ChaveErroValidacao.CampoNaoPreenchido)]
        public string NomeAgente
        {
            get { return _nomeAgente; }
            set { _nomeAgente = ValidationUtil.TruncateString(value, 60); }
        }

        /// <summary>
        /// Retorna ou define o Telefone. Preencher com o DDD + número do telefone.
        /// </summary>
        [NFeField(ID = "D06", FieldName = "fone", DataType = "token", Pattern = "[0-9]{1,10}")]
        [ValidateField(5, ChaveErroValidacao.CampoNaoPreenchido)]
        public string Telefone
        {
            get { return _telefone; }
            set
            {
                ValidationUtil.ValidateTelefone(value, "Telefone");
                _telefone = value;
            }
        }

        /// <summary>
        /// Retorna ou define a Sigla da UF. Informar 'EX' para operações com o Exterior.
        /// </summary>
        [NFeField(ID = "D07", FieldName = "UF", DataType = "TUf")]
        [ValidateField(6, ChaveErroValidacao.CampoNaoPreenchido)]
        public SiglaUF UF
        {
            get { return _UF; }
            set { _UF = ValidationUtil.ValidateEnum(value, "UF"); }
        }

        /// <summary>
        /// Retorna ou define o número do Documento de Arrecadação da Receita.
        /// </summary>
        [NFeField(ID = "D08", FieldName = "nDAR", DataType = "token", MinLength = 1, MaxLength = 60)]
        [ValidateField(7, ChaveErroValidacao.CampoNaoPreenchido)]
        public string NumeroDAR
        {
            get { return _numeroDAR; }
            set { _numeroDAR = ValidationUtil.TruncateString(value, 60); }
        }

        /// <summary>
        /// Retorna ou define a Data de emissão do Documento de Arrecadação da Receita.
        /// </summary>
        /// <remarks>Formato AAAA-MM-DD</remarks>
        [NFeField(ID = "D09", FieldName = "dEmi", DataType = "TData", Pattern = @"\d{4}-\d{2}-\d{2}")]
        [ValidateField(8, ChaveErroValidacao.CampoNaoPreenchido)]
        public DateTime DataEmissaoDAR
        {
            get { return _dataEmissaoDAR; }
            set
            {
                ValidationUtil.ValidateTData(value, "DataEmissaoDAR");
                _dataEmissaoDAR = value;
            }
        }

        /// <summary>
        /// Retorna ou define o valor total constante no Documento de Arrecadação da Receita
        /// </summary>
        /// <remarks>Formato com 15 caracteres, sendo 13 no corpo e 2 decimais</remarks>
        [NFeField(ID = "D10", FieldName = "vDAR", DataType = "TDec_1302",
            Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [ValidateField(9, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal ValorDAR
        {
            get { return _valorDAR; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorDAR");
                _valorDAR = value;
            }
        }

        /// <summary>
        /// Retorna ou define a Repartição Fiscal Emitente.
        /// </summary>
        [NFeField(ID = "D11", FieldName = "repEmi", DataType = "token", MinLength = 1, MaxLength = 60)]
        [ValidateField(10, ChaveErroValidacao.CampoNaoPreenchido)]
        public string ReparticaoFiscalEmitente
        {
            get { return _reparticaoFiscalEmitente; }
            set { _reparticaoFiscalEmitente = ValidationUtil.TruncateString(value, 60); }
        }

        /// <summary>
        /// Retorna ou define a Data de pagamento do Documento de Arrecadação da Receita. Opcional.
        /// </summary>
        /// <remarks>Formato AAAA-MM-DD</remarks>
        [NFeField(ID = "D12", FieldName = "dPag", DataType = "TData", Pattern = @"\d{4}-\d{2}-\d{2}")]
        [ValidateField(11, true)]
        public DateTime DataPagamentoDAR
        {
            get { return _dataPagamentoDAR; }
            set
            {
                ValidationUtil.ValidateTData(value, "DataPagamentoDAR");
                _dataPagamentoDAR = value;
            }
        }

        /// <summary>
        /// Retorna se a classe foi modificada.
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return
                    !string.IsNullOrEmpty(CNPJ) ||
                    !string.IsNullOrEmpty(OrgaoEmitente) ||
                    !string.IsNullOrEmpty(MatriculaAgente) ||
                    !string.IsNullOrEmpty(NomeAgente) ||
                    !string.IsNullOrEmpty(Telefone) ||
                    UF != SiglaUF.NaoEspecificado ||
                    !string.IsNullOrEmpty(NumeroDAR) ||
                    DataEmissaoDAR != DateTime.MinValue ||
                    ValorDAR != 0m ||
                    !string.IsNullOrEmpty(ReparticaoFiscalEmitente) ||
                    DataPagamentoDAR != DateTime.MinValue;
            }
        }
    }
}