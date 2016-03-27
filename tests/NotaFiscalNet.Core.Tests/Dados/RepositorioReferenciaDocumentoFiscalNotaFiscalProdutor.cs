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
                CodigoModelo = "01",
                InscricaoEstadual = "20",
                MesAnoEmissao = new DateTime(2020, 10, 4),
                Numero = 1,
                Serie = 0
            };
        }

        private static ReferenciaDocumentoFiscalNotaFiscalProdutor CriarReferencia2()
        {
            return new ReferenciaDocumentoFiscalNotaFiscalProdutor()
            {
                UnidadeFederativa = UfIBGE.MT,
                CPF = "00100100101",
                CodigoModelo = "04",
                InscricaoEstadual = "10",
                MesAnoEmissao = new DateTime(2016, 5, 1),
                Numero = 10,
                Serie = 10
            };
        }
    }
}