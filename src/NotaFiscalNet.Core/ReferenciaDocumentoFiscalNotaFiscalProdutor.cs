using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using System;

namespace NotaFiscalNet.Core
{
    public sealed class ReferenciaDocumentoFiscalNotaFiscalProdutor : ISerializavel, IReferenciaDocumentoFiscal, IPossuiDocumentoIdentificador
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
        public string CNPJ { get; set; }

        /// <summary>
        /// [CPF] Retorna ou define o CPF do emitente da Nota Fiscal de Produtor (1 ou 1A) referenciada.
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// [CPF] Retorna ou define a Inscrição Estadual emitente da Nota Fiscal de Produtor (1 ou
        /// 1A) referenciada.
        /// </summary>
        public string InscricaoEstadual { get; set; }

        /// <summary>
        /// [mod] Retorna o Código do Modelo do Documento Fiscal Referênciado. Se o Documento Fiscal
        /// referenciado por uma Nota Fiscal Eletrônica, o valor deverá ser '55'. Caso contrário, se
        /// o Documento Fiscal for uma Nota Fiscal modelo 1 ou 1A, deverá ser informado '01'.
        /// </summary>
        public string CodigoModelo { get; set; }

        /// <summary>
        /// [serie] Retorna ou define a Série da Nota Fiscal (modelo 1 ou 1A) referenciada. Informar
        /// zero (0) se inexistente. Valor padrão 0.
        /// </summary>
        public int Serie { get; set; }

        /// <summary>
        /// [nNF] Retorna ou define o Número da Nota Fical (modelo 1 ou 1A) referenciada.
        /// </summary>
        public int Numero { get; set; }

        public void Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("refNFP");
            writer.WriteElementString("cUF", SerializationUtil.GetEnumValue(UnidadeFederativa));
            writer.WriteElementString("AAMM", MesAnoEmissao.ToString("yyMM"));

            if (!string.IsNullOrEmpty(CNPJ))
                writer.WriteElementString("CNPJ", SerializationUtil.ToCNPJ(CNPJ));
            else
                writer.WriteElementString("CPF", SerializationUtil.ToCPF(CPF));

            writer.WriteElementString("IE", InscricaoEstadual);
            writer.WriteElementString("mod", CodigoModelo);
            writer.WriteElementString("serie", Serie.ToString());
            writer.WriteElementString("nNF", Numero.ToString());
            writer.WriteEndElement();
        }
    }
}