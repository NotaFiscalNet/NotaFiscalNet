using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using NotaFiscalNet.Core.Validacao.Validators;
using System;
using System.Xml;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Informações de registro aquisições de cana.
    /// </summary>
    public sealed class AquisicaoCana : ISerializavel, IModificavel
    {
        private decimal _quantidadeTotalAnterior;
        private decimal _quantidadeTotalGeral;
        private decimal _quantidadeTotalMes;
        private DateTime _referencia;
        private string _safra;
        private decimal _valorFornecimentos;
        private decimal _valorLiquidoFornecimentos;
        private decimal _valorTotalDeducoes;

        /// <summary>
        /// [safra] Retorna ou define o identificador da Safra. Informar AAAA ou AAAA/AAAA.
        /// </summary>
        [NFeField(ID = "ZC02", FieldName = "safra", DataType = "TString")]
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public string Safra
        {
            get { return _safra; }
            set { _safra = ValidationUtil.ValidateRange(value, 4, 9, "Safra"); }
        }

        /// <summary>
        /// [ref] Retorna ou define o Mês e Ano de referência.
        /// </summary>
        [NFeField(ID = "ZC03", FieldName = "ref", DataType = "xs:string")]
        [ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public DateTime Referencia
        {
            get { return _referencia; }
            set { _referencia = new DateTime(value.Year, value.Month, 1); }
        }

        /// <summary>
        /// [forDia] Retorna a lista de fornecimentos diários de Cana.
        /// </summary>
        [NFeField(ID = "ZC04", FieldName = "forDia")]
        [ValidateField(3, ChaveErroValidacao.CampoNaoPreenchido, Validator = typeof(RangeCollectionValidator),
            MinLength = 1, MaxLength = 31)]
        public FornecimentoDiarioCanaCollection FornecimentosDiarios { get; } = new FornecimentoDiarioCanaCollection();

        /// <summary>
        /// [qTotMes] Retorna ou define a quantidade total do mês.
        /// </summary>
        [NFeField(ID = "ZC07", FieldName = "qTotMes", DataType = "TDec_1110")]
        [ValidateField(4, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal QuantidadeTotalMes
        {
            get { return _quantidadeTotalMes; }
            set { _quantidadeTotalMes = ValidationUtil.ValidateTDec_1110(value, "QuantidadeTotalMes"); }
        }

        /// <summary>
        /// [qTotAnt] Retorna ou define a quantidade total anterior.
        /// </summary>
        [NFeField(ID = "ZC08", FieldName = "qTotAnt", DataType = "TDec_1110")]
        [ValidateField(5, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal QuantidadeTotalAnterior
        {
            get { return _quantidadeTotalAnterior; }
            set { _quantidadeTotalAnterior = ValidationUtil.ValidateTDec_1110(value, "QuantidadeTotalAnterior"); }
        }

        /// <summary>
        /// [qTotGer] Retorna ou define a quantidade total geral.
        /// </summary>
        [NFeField(ID = "ZC09", FieldName = "qTotGer", DataType = "TDec_1110")]
        [ValidateField(6, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal QuantidadeTotalGeral
        {
            get { return _quantidadeTotalGeral; }
            set { _quantidadeTotalGeral = ValidationUtil.ValidateTDec_1110(value, "QuantidadeTotalGeral"); }
        }

        /// <summary>
        /// [deduc] Retorna a lista de deduções (Taxas e Contribuições).
        /// </summary>
        [NFeField(ID = "ZC10", FieldName = "deduc")]
        [ValidateField(7, ChaveErroValidacao.CampoNaoPreenchido, Validator = typeof(RangeCollectionValidator),
            MinLength = 0, MaxLength = 10)]
        public DeducaoCanaCollection Deducoes { get; } = new DeducaoCanaCollection();

        /// <summary>
        /// [vFor] Retorna ou define o Valor dos Fornecimentos.
        /// </summary>
        [NFeField(ID = "ZC13", FieldName = "vFor", DataType = "TDec_1302")]
        [ValidateField(8, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal ValorFornecimentos
        {
            get { return _valorFornecimentos; }
            set { _valorFornecimentos = ValidationUtil.ValidateTDec_1302(value, "ValorFornecimentos"); }
        }

        /// <summary>
        /// [vTotDed] Retorna ou define o Valor Total das Deduções.
        /// </summary>
        [NFeField(ID = "ZC14", FieldName = "vTotDed", DataType = "TDec_1302")]
        [ValidateField(9, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal ValorTotalDeducoes
        {
            get { return _valorTotalDeducoes; }
            set { _valorTotalDeducoes = ValidationUtil.ValidateTDec_1302(value, "ValorTotalDeducoes"); }
        }

        /// <summary>
        /// [vLiqFor] Retorna ou define o Valor Líquido dos fornecimentos.
        /// </summary>
        [NFeField(ID = "ZC15", FieldName = "vLiqFor", DataType = "TDec_1302")]
        [ValidateField(10, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal ValorLiquidoFornecimentos
        {
            get { return _valorLiquidoFornecimentos; }
            set { _valorLiquidoFornecimentos = ValidationUtil.ValidateTDec_1302(value, "ValorLiquidoFornecimentos"); }
        }

        public bool Modificado
        {
            get
            {
                return !string.IsNullOrEmpty(Safra) || Referencia != DateTime.MinValue ||
                       FornecimentosDiarios.Count > 0 || QuantidadeTotalMes > 0m
                       || QuantidadeTotalAnterior > 0m || QuantidadeTotalGeral > 0m || Deducoes.Count > 0 ||
                       ValorFornecimentos > 0m || ValorTotalDeducoes > 0m
                       || ValorLiquidoFornecimentos > 0m;
            }
        }

        public void Serializar(XmlWriter writer, NFe nfe)
        {
            if (!Modificado) return;

            writer.WriteStartElement("cana"); // <cana>

            writer.WriteElementString("safra", SerializationUtil.ToTString(Safra, 9));
            writer.WriteElementString("ref", Referencia.ToString("MM/yyyy"));

            foreach (var item in FornecimentosDiarios)
            {
                writer.WriteStartElement("forDia"); // <forDia>
                writer.WriteAttributeString("dia", item.Dia.ToString());
                writer.WriteElementString("qtde", item.Quantidade.ToString());
                writer.WriteEndElement(); // </forDia>
            }

            writer.WriteElementString("qTotMes", SerializationUtil.ToTDec_1110(QuantidadeTotalMes));
            writer.WriteElementString("qTotAnt", SerializationUtil.ToTDec_1110(QuantidadeTotalAnterior));
            writer.WriteElementString("qTotGer", SerializationUtil.ToTDec_1110(QuantidadeTotalGeral));

            foreach (var item in Deducoes)
            {
                writer.WriteStartElement("deduc"); // <deduc>
                writer.WriteAttributeString("xDed", item.Descricao.ToTString(60));
                writer.WriteElementString("vDed", item.Valor.ToTDec_1302());
                writer.WriteEndElement(); // </deduc>
            }

            writer.WriteElementString("vFor", ValorFornecimentos.ToTDec_1302());
            writer.WriteElementString("vTotDed", ValorTotalDeducoes.ToTDec_1302());
            writer.WriteElementString("vLiqFor", ValorLiquidoFornecimentos.ToTDec_1302());

            writer.WriteEndElement(); // </cana>
        }
    }
}