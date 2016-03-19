using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using System;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa um determinado tipo de Medicamento. Utilizado na NF-e como detalhamento nos casos
    /// de venda de medicamentos.
    /// </summary>
    public sealed class Medicamento : ISerializavel, IModificavel
    {
        private string _numeroLote = string.Empty;
        private decimal _quantidadeProdLote;
        private DateTime _dataFabricacao;
        private DateTime _dataValidade;
        private decimal _precoMaximoConsumidor;

        /// <summary>
        /// Retorna ou define o Número do Lote do Medicamento. Até 20 caracteres.
        /// </summary>
        [NFeField(FieldName = "nLote", DataType = "token", ID = "K02", MinLength = 1, MaxLength = 20)]
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public string NumeroLote
        {
            get { return _numeroLote; }
            set
            {
                _numeroLote = ValidationUtil.TruncateString(value, 20);
            }
        }

        /// <summary>
        /// Retorna ou define a Quantidade de Produtos no Lote do Medicamento.
        /// </summary>
        [NFeField(FieldName = "qLote", DataType = "TDec_0803", ID = "K03", Pattern = @"0|0\.[0-9]{3}|[1-9]{1}[0-9]{0,7}(\.[0-9]{3})?")]
        [ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal QuantidadeProdutoLote
        {
            get { return _quantidadeProdLote; }
            set
            {
                ValidationUtil.ValidateTDec_0803(value, "QuantidadeProdutoLote");
                _quantidadeProdLote = value;
            }
        }

        /// <summary>
        /// Retorna ou define a Data de Fabricação do produto.
        /// </summary>
        [NFeField(FieldName = "dFab", DataType = "TData", ID = "K04", Pattern = @"\d{4}-\d{2}-\d{2}")]
        [ValidateField(3, ChaveErroValidacao.CampoNaoPreenchido)]
        public DateTime DataFabricacao
        {
            get { return _dataFabricacao; }
            set
            {
                ValidationUtil.ValidateTData(value, "DataFabricacao");
                _dataFabricacao = value;
            }
        }

        /// <summary>
        /// Retorna ou define a Data de Validade do Produto.
        /// </summary>
        [NFeField(FieldName = "dVal", DataType = "TData", ID = "K05", Pattern = @"\d{4}-\d{2}-\d{2}")]
        [ValidateField(4, ChaveErroValidacao.CampoNaoPreenchido)]
        public DateTime DataValidade
        {
            get { return _dataValidade; }
            set
            {
                ValidationUtil.ValidateTData(value, "DataValidade");
                _dataValidade = value;
            }
        }

        /// <summary>
        /// Retorna ou define o Preço Máximo do Medicamento para o consumidor.
        /// </summary>
        [NFeField(FieldName = "vPMC", DataType = "TDec_1302", ID = "K06", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [ValidateField(5, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal PrecoMaximoConsumidor
        {
            get { return _precoMaximoConsumidor; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "PrecoMaximoConsumidor");
                _precoMaximoConsumidor = value;
            }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => !string.IsNullOrEmpty(NumeroLote) ||
                                  QuantidadeProdutoLote != 0m ||
                                  DataFabricacao != DateTime.MinValue ||
                                  DataValidade != DateTime.MinValue ||
                                  PrecoMaximoConsumidor != 0m;

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("med");

            writer.WriteElementString("nLote", SerializationUtil.ToToken(NumeroLote, 20));
            writer.WriteElementString("qLote", SerializationUtil.ToTDec_0803(QuantidadeProdutoLote));
            writer.WriteElementString("dFab", SerializationUtil.ToTData(DataFabricacao));
            writer.WriteElementString("dVal", SerializationUtil.ToTData(DataValidade));
            writer.WriteElementString("vPMC", SerializationUtil.ToTDec_1302(PrecoMaximoConsumidor));

            writer.WriteEndElement();
        }
    }
}