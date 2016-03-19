using System;
using System.Collections.Generic;

namespace NotaFiscalNet.Core.Tests.Dados
{
    public class RepositorioReferenciaDocumentoFiscalNotaFiscalProdutor :
        Repositorio<ReferenciaDocumentoFiscalNotaFiscalProdutor>
    {
        public override List<ReferenciaDocumentoFiscalNotaFiscalProdutor> CriarElementos()
        {
            return new List<ReferenciaDocumentoFiscalNotaFiscalProdutor>()
            {
                CriarReferencia1(),
                CriarReferencia2()
            };
        }

        private static ReferenciaDocumentoFiscalNotaFiscalProdutor CriarReferencia1()
        {
            return new ReferenciaDocumentoFiscalNotaFiscalProdutor()
            {
                UnidadeFederativa = UfIBGE.AC,
                CNPJ = "010010010000101",
                CodigoModeloDocumentoFiscal = "01",
                InscricaoEstadual = "20",
                MesAnoEmissao = new DateTime(2020, 10, 4),
                NumeroNf = 1,
                SerieNf = 0
            };
        }

        private static ReferenciaDocumentoFiscalNotaFiscalProdutor CriarReferencia2()
        {
            return new ReferenciaDocumentoFiscalNotaFiscalProdutor()
            {
                UnidadeFederativa = UfIBGE.MT,
                CPF = "00100100101",
                CodigoModeloDocumentoFiscal = "04",
                InscricaoEstadual = "10",
                MesAnoEmissao = new DateTime(2016, 5, 1),
                NumeroNf = 10,
                SerieNf = 10
            };
        }
    }
}