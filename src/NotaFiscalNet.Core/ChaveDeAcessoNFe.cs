using NotaFiscalNet.Core.Utils;
using System;
using System.Globalization;

namespace NotaFiscalNet.Core
{
    public struct ChaveDeAcessoNFe
    {
        private static readonly CultureInfo PtBr = new CultureInfo("pt-br");
        private readonly string _valor;

        /// <summary>
        /// Retorna o código da UF de emissão.
        /// </summary>
        public UfIBGE UF { get; private set; }

        /// <summary>
        /// Retorna o Ano e Mês da emissão.
        /// </summary>
        public DateTime AnoMes { get; private set; }

        /// <summary>
        /// Retorna o CNPJ do Emitente.
        /// </summary>
        public string Cnpj { get; private set; }

        /// <summary>
        /// Retorna o Tipo da Modalidade do Documento Fiscal (modalidade).
        /// </summary>
        public TipoModalidadeDocumentoFiscal Modalidade { get; private set; }

        /// <summary>
        /// Retorna a Série da Numeração do Documento Fiscal.
        /// </summary>
        public int Serie { get; private set; }

        /// <summary>
        /// Retorna o Número do Documento Fiscal.
        /// </summary>
        public int Numero { get; private set; }

        /// <summary>
        /// Retorna o Tipo da Emissão do Documento Fiscal.
        /// </summary>
        public TipoEmissaoNFe TipoEmissao { get; private set; }

        /// <summary>
        /// Retorna o código numérico do Documento Fiscal.
        /// </summary>
        public int CodigoNF { get; private set; }

        /// <summary>
        /// Retorna o Dígito Verificador da Chave de Acesso.
        /// </summary>
        public int DV { get; private set; }

        /// <summary>
        /// Inicializa uma nova instância com base em um valor existente.
        /// </summary>
        /// <param name="valor"></param>
        public ChaveDeAcessoNFe(string valor) : this()
        {
            _valor = valor;
            PreencherCampos(valor);
        }

        /// <summary>
        /// Cria uma nova chave de acesso.
        /// </summary>
        /// <param name="uf"></param>
        /// <param name="anoMes"></param>
        /// <param name="cnpj"></param>
        /// <param name="modalidade"></param>
        /// <param name="serie"></param>
        /// <param name="numero"></param>
        /// <param name="tipoEmissao"></param>
        /// <param name="codigoNF"></param>
        public ChaveDeAcessoNFe(UfIBGE uf, DateTime anoMes, string cnpj, TipoModalidadeDocumentoFiscal modalidade,
            int serie, int numero, TipoEmissaoNFe tipoEmissao, int codigoNF) : this()
        {
            anoMes = new DateTime(anoMes.Year, anoMes.Month, 1);
            UF = uf;
            AnoMes = anoMes;
            Cnpj = cnpj;
            Modalidade = modalidade;
            Serie = serie;
            Numero = numero;
            TipoEmissao = tipoEmissao;
            CodigoNF = codigoNF;

            var chave = string.Concat(
                (int)UF,
                anoMes.ToString("yyMM", PtBr),
                Cnpj,
                (int)Modalidade,
                Serie.ToString("000"),
                Numero.ToString("000000000"),
                (int)TipoEmissao,
                codigoNF.ToString("00000000"));

            _valor = string.Concat(chave, CalculoUtil.Modulo11(chave));
        }

        private void PreencherCampos(string chaveDeAcesso)
        {
            // 0 1 2 3 4 01 23 45 67890123456789 01 234 567890123 4 56789012 3 51 15 02
            // 21164887000123 65 000 000015954 9 16089101 5

            UF = UfIBGEEx.Parse(chaveDeAcesso.Substring(0, 2));
            AnoMes = DateTime.ParseExact(chaveDeAcesso.Substring(2, 4), "yyMM", PtBr);
            Cnpj = chaveDeAcesso.Substring(6, 14);
            Modalidade = TipoModalidadeDocumentoFiscalEx.Parse(chaveDeAcesso.Substring(20, 2));
            Serie = int.Parse(chaveDeAcesso.Substring(22, 3));
            Numero = int.Parse(chaveDeAcesso.Substring(25, 9));
            TipoEmissao = TipoEmissaoNFeEx.Parse(chaveDeAcesso.Substring(34, 1));
            CodigoNF = int.Parse(chaveDeAcesso.Substring(35, 8));
            DV = int.Parse(chaveDeAcesso.Substring(43, 0));
        }

        public override string ToString()
        {
            return _valor;
        }
    }
}