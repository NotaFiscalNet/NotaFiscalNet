using System;
using System.Collections.Generic;

namespace NotaFiscalNet.Core.Tests.Dados
{
    public class RepositorioReferenciaDocumentoFiscalNotaFiscal :
        Repositorio<ReferenciaDocumentoFiscalNotaFiscal>
    {
        public override List<ReferenciaDocumentoFiscalNotaFiscal> CriarElementos()
        {
            return new List<ReferenciaDocumentoFiscalNotaFiscal>()
            {
                CriarReferencia1(),
                CriarReferencia2()
            };
        }

        private static ReferenciaDocumentoFiscalNotaFiscal CriarReferencia1()
        {
            return new ReferenciaDocumentoFiscalNotaFiscal()
            {
                UnidadeFederativa = UfIBGE.AC,
                Cnpj = "010010010000101",
                CodigoModelo = "01",
                MesAnoEmissao = new DateTime(2020, 10, 4),
                Numero = 1,
                Serie = 0
            };
        }

        private static ReferenciaDocumentoFiscalNotaFiscal CriarReferencia2()
        {
            return new ReferenciaDocumentoFiscalNotaFiscal()
            {
                UnidadeFederativa = UfIBGE.MT,
                Cnpj = "020020020000202",
                CodigoModelo = "01",
                MesAnoEmissao = new DateTime(2016, 5, 1),
                Numero = 10,
                Serie = 10
            };
        }
    }
}