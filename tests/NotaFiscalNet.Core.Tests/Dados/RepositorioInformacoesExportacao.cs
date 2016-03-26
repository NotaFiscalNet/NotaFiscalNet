using System;
using System.Collections.Generic;

namespace NotaFiscalNet.Core.Tests.Dados
{
    public class RepositorioInformacoesExportacao :
        Repositorio<InformacoesExportacao>
    {
        public override List<InformacoesExportacao> CriarElementos()
        {
            return new List<InformacoesExportacao>()
            {
                CriarInformacoesExportacao1(),
                CriarInformacoesExportacao2()
            };
        }

        private static InformacoesExportacao CriarInformacoesExportacao1()
        {
            return new InformacoesExportacao()
            {
                UnidadeFederativa = UfIBGE.SP,
                LocalEmbarque = "Local de Embarque 1"
            };
        }

        private static InformacoesExportacao CriarInformacoesExportacao2()
        {
            return new InformacoesExportacao()
            {
                UnidadeFederativa = UfIBGE.RJ,
                LocalEmbarque = "Local de Embarque 2",
                LocalDespacho = "Local de Despacho 2"
            };
        }
    }
}