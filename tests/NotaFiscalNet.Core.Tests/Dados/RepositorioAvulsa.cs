using System;
using System.Collections.Generic;

namespace NotaFiscalNet.Core.Tests.Dados
{
    public class RepositorioAvulsa :
        Repositorio<Avulsa>
    {
        public override List<Avulsa> CriarElementos()
        {
            return new List<Avulsa>()
            {
                CriarElemento1(),
                CriarElemento2(),
                CriarElemento3()
            };
        }

        private static Avulsa CriarElemento1()
        {
            return new Avulsa()
            {
                Cnpj = "01001001000101",
                OrgaoEmitente = "Orgao Avulsa 1",
                MatriculaAgente = "Matricula Agente 1",
                NomeAgente = "Agente 1",
                Telefone = "01930010001",
                UnidadeFederativa = UfIBGE.SP,
                NumeroDocumento = "1540",
                DataEmissao = new DateTime(2015, 10, 30),
                Valor = 1234567890123.45m,
                ReparticaoFiscalEmitente = "Reparticao Emitente 1",
                DataPagamento = new DateTime(2015, 10, 31)
            };
        }

        private static Avulsa CriarElemento2()
        {
            return new Avulsa()
            {
                Cnpj = "01001001000102",
                OrgaoEmitente = "Orgao Avulsa 2",
                MatriculaAgente = "Matricula Agente 2",
                NomeAgente = "Agente 2",
                UnidadeFederativa = UfIBGE.SP,
                Valor = 0m,
                ReparticaoFiscalEmitente = "Reparticao Emitente 2"
            };
        }

        private static Avulsa CriarElemento3()
        {
            return new Avulsa()
            {
                Cnpj = "01001001000103",
                OrgaoEmitente = "Orgao Avulsa 3",
                MatriculaAgente = "Matricula Agente 3",
                NomeAgente = "Agente 3",
                UnidadeFederativa = UfIBGE.SP,
                Valor = 0.45m,
                ReparticaoFiscalEmitente = "Reparticao Emitente 3"
            };
        }
    }
}