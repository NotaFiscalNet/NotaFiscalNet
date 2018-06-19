using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using System;

namespace NotaFiscalNet.Core
{
    public sealed class ReferenciaDocumentoFiscalNotaFiscal : ISerializavel, IReferenciaDocumentoFiscal
    {
        /// <summary>
        /// [cUF] Retorna ou define o código da UF do emitente da Nota Fiscal (1 ou 1A)
        /// </summary>
        public UfIBGE UnidadeFederativa { get; set; } = UfIBGE.NaoEspecificado;

        /// <summary>
        /// [AAMM] Retorna ou define o Mês e Ano de Emissão da Nota Fiscal (1 ou 1A).
        /// </summary>
        /// <remarks>
        /// A informação de dia não será considerada para este campo, podendo ser informado qualquer
        /// dia do mês/ano informado.
        /// </remarks>
        public DateTime MesAnoEmissao { get; set; } = DateTime.MinValue;

        /// <summary>
        /// [CNPJ] Retorna ou define o CNPJ do emitente da Nota Fiscal (1 ou 1A) referenciada.
        /// </summary>
        public string Cnpj { get; set; }

        /// <summary>
        /// [mod] Retorna o Código do Modelo do Documento Fiscal Referênciado. Utilizar 01 para NF
        /// modelo 1/1A
        /// </summary>
        public string CodigoModelo { get; set; }

        /// <summary>
        /// [serie] Retorna ou define a Série do Documento Fiscal. Informar zero (0) se inexistente.
        /// Valor padrão 0.
        /// </summary>
        public int Serie { get; set; }

        /// <summary>
        /// [nNF] Retorna ou define o Número do Documento Fiscal
        /// </summary>
        public int Numero { get; set; }

        public void Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("refNF");
            writer.WriteElementString("cUF", SerializationUtil.GetEnumValue(UnidadeFederativa));
            writer.WriteElementString("AAMM", MesAnoEmissao.ToString("yyMM"));
            writer.WriteElementString("CNPJ", SerializationUtil.ToCNPJ(Cnpj));
            writer.WriteElementString("mod", CodigoModelo);
            writer.WriteElementString("serie", Serie.ToString());
            writer.WriteElementString("nNF", Numero.ToString());
            writer.WriteEndElement(); 
        }
    }
}