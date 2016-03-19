using System;
using System.Collections.Generic;

namespace NotaFiscalNet.Core.Tests.Dados
{
    public class RepositorioReferenciaDocumentoFiscalNotaFiscal
    {
        public Dictionary<string, ReferenciaDocumentoFiscalNotaFiscal> Referencias { get; } = new Dictionary<string, ReferenciaDocumentoFiscalNotaFiscal>()
        {
            {"Referencia1", CriarReferencia1() },
            {"Referencia2", CriarReferencia2() }
        };

        private static ReferenciaDocumentoFiscalNotaFiscal CriarReferencia1()
        {
            return new ReferenciaDocumentoFiscalNotaFiscal()
            {
                UnidadeFederativa = UfIBGE.AC,
                CNPJ = "010010010000101",
                CodigoModeloDocumentoFiscal = "01",
                MesAnoEmissao = new DateTime(2020, 10, 4),
                NumeroNf = 1,
                SerieNf = 0
            };
        }

        private static ReferenciaDocumentoFiscalNotaFiscal CriarReferencia2()
        {
            return new ReferenciaDocumentoFiscalNotaFiscal()
            {
                UnidadeFederativa = UfIBGE.MT,
                CNPJ = "020020020000202",
                CodigoModeloDocumentoFiscal = "01",
                MesAnoEmissao = new DateTime(2016, 5, 1),
                NumeroNf = 10,
                SerieNf = 10
            };
        }
    }
}