using System;
using System.Collections.Generic;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core.Tests.Dados
{
    public class RepositorioIdentificacaoDocumentoFiscal :
        Repositorio<IdentificacaoDocumentoFiscal>
    {
        public override List<IdentificacaoDocumentoFiscal> CriarElementos()
        {
            return new List<IdentificacaoDocumentoFiscal>()
            {
                CriarIdentificacaoDocumentoFiscal1(),
                CriarIdentificacaoDocumentoFiscal2(),
                CriarIdentificacaoDocumentoFiscal3(),
                CriarIdentificacaoDocumentoFiscal4(),
                CriarIdentificacaoDocumentoFiscal5()
            };
        }

        private static IdentificacaoDocumentoFiscal CriarIdentificacaoDocumentoFiscal1()
        {
            var identificacao = new IdentificacaoDocumentoFiscal()
            {
                CodigoNumerico = 10000001,
                Ambiente = TipoAmbiente.Homologacao,
                CodigoMunicipioFatoGerador = 1234567,
                DataEmissao = new DateTime(2016, 01, 02),
                DataEntradaSaida = new DateTime(2016, 01, 02),
                Finalidade = TipoFinalidade.Complementar,
                FormaPagamento = IndicadorFormaPagamento.APrazo,
                IdentificadorLocalDestinoOperacao = TipoIdentificadorLocalDestinoOperacao.Exterior,
                ModalidadeDocumentoFiscal = TipoModalidadeDocumentoFiscal.Nfe,
                NaturezaOperacao = "Compra e venda",
                OperacaoConsumidorFinal = true,
                TipoEmissao = TipoEmissaoNFe.Normal,
                Serie = 2,
                TipoNotaFiscal = TipoNotaFiscal.Saida,
                VersaoAplicativoEmissao = "2.0",
                UnidadeFederativaEmitente = UfIBGE.AM,
                TipoProcessoEmissao = TipoProcessoEmissaoNFe.AplicativoFisco,
                TipoImpressao = TipoFormatoImpressaoDanfe.Paisagem,
                Numero = 12345,
                IndicadorPresencaComprador = TipoIndicadorPresencaComprador.Outros,
            };
            identificacao.ReferenciasDocumentoFiscais.Add(new ReferenciaDocumentoFiscalNfe()
            {
                ChaveAcessoNFe = "42100484684182000157550010000000020108042108"
            });
            return identificacao;
        }

        private static IdentificacaoDocumentoFiscal CriarIdentificacaoDocumentoFiscal2()
        {
            var identificacao = new IdentificacaoDocumentoFiscal()
            {
                CodigoNumerico = 10000002,
                Ambiente = TipoAmbiente.Producao,
                CodigoMunicipioFatoGerador = 9999999,
                DataEmissao = new DateTime(2016, 01, 02),
                DataEntradaSaida = new DateTime(2016, 10, 3),
                Finalidade = TipoFinalidade.Ajuste,
                FormaPagamento = IndicadorFormaPagamento.Outro,
                IdentificadorLocalDestinoOperacao = TipoIdentificadorLocalDestinoOperacao.Interestadual,
                ModalidadeDocumentoFiscal = TipoModalidadeDocumentoFiscal.Nfce,
                NaturezaOperacao = "Venda a prazo",
                OperacaoConsumidorFinal = false,
                TipoEmissao = TipoEmissaoNFe.ContingenciaOffLineNfce,
                Serie = 3,
                TipoNotaFiscal = TipoNotaFiscal.Entrada,
                VersaoAplicativoEmissao = "3.0",
                UnidadeFederativaEmitente = UfIBGE.MT,
                TipoProcessoEmissao = TipoProcessoEmissaoNFe.AplicativoContribuinte,
                TipoImpressao = TipoFormatoImpressaoDanfe.NfceMensagemEletronica,
                Numero = 67889,
                IndicadorPresencaComprador = TipoIndicadorPresencaComprador.NfceEntregaEmDomicilio,
            };
            return identificacao;
        }

        private static IdentificacaoDocumentoFiscal CriarIdentificacaoDocumentoFiscal3()
        {
            var identificacao = new IdentificacaoDocumentoFiscal()
            {
                CodigoNumerico = 10000003,
                Ambiente = TipoAmbiente.Producao,
                CodigoMunicipioFatoGerador = 9999999,
                DataEmissao = new DateTime(2016, 01, 02),
                DataEntradaSaida = new DateTime(2016, 10, 3),
                Finalidade = TipoFinalidade.Normal,
                FormaPagamento = IndicadorFormaPagamento.AVista,
                IdentificadorLocalDestinoOperacao = TipoIdentificadorLocalDestinoOperacao.Interestadual,
                ModalidadeDocumentoFiscal = TipoModalidadeDocumentoFiscal.Nfce,
                NaturezaOperacao = "Venda a prazo",
                OperacaoConsumidorFinal = false,
                TipoEmissao = TipoEmissaoNFe.DeclaracaoPrevia,
                Serie = 3,
                TipoNotaFiscal = TipoNotaFiscal.Entrada,
                VersaoAplicativoEmissao = "3.0",
                UnidadeFederativaEmitente = UfIBGE.SP,
                TipoProcessoEmissao = TipoProcessoEmissaoNFe.AvulsaContribuinteSiteFisco,
                TipoImpressao = TipoFormatoImpressaoDanfe.SemImpressao,
                Numero = 67889,
                IndicadorPresencaComprador = TipoIndicadorPresencaComprador.TeleAtendimento,
            };
            return identificacao;
        }

        private static IdentificacaoDocumentoFiscal CriarIdentificacaoDocumentoFiscal4()
        {
            var identificacao = new IdentificacaoDocumentoFiscal()
            {
                CodigoNumerico = 10000004,
                Ambiente = TipoAmbiente.Producao,
                CodigoMunicipioFatoGerador = 9999999,
                DataEmissao = new DateTime(2016, 01, 02),
                DataEntradaSaida = new DateTime(2016, 10, 3),
                Finalidade = TipoFinalidade.Devolucao,
                FormaPagamento = IndicadorFormaPagamento.AVista,
                IdentificadorLocalDestinoOperacao = TipoIdentificadorLocalDestinoOperacao.Interestadual,
                ModalidadeDocumentoFiscal = TipoModalidadeDocumentoFiscal.Nfce,
                NaturezaOperacao = "Venda a prazo",
                OperacaoConsumidorFinal = false,
                TipoEmissao = TipoEmissaoNFe.FormularioSeguranca,
                Serie = 3,
                TipoNotaFiscal = TipoNotaFiscal.Entrada,
                VersaoAplicativoEmissao = "3.0",
                UnidadeFederativaEmitente = UfIBGE.RJ,
                TipoProcessoEmissao = TipoProcessoEmissaoNFe.AvulsaFisco,
                TipoImpressao = TipoFormatoImpressaoDanfe.Paisagem,
                Numero = 67889,
                IndicadorPresencaComprador = TipoIndicadorPresencaComprador.Internet,
            };
            return identificacao;
        }

        private static IdentificacaoDocumentoFiscal CriarIdentificacaoDocumentoFiscal5()
        {
            var identificacao = new IdentificacaoDocumentoFiscal()
            {
                CodigoNumerico = 10000005,
                Ambiente = TipoAmbiente.Producao,
                CodigoMunicipioFatoGerador = 9999999,
                DataEmissao = new DateTime(2016, 01, 02),
                DataEntradaSaida = new DateTime(2016, 10, 3),
                Finalidade = TipoFinalidade.Devolucao,
                FormaPagamento = IndicadorFormaPagamento.AVista,
                IdentificadorLocalDestinoOperacao = TipoIdentificadorLocalDestinoOperacao.Interestadual,
                ModalidadeDocumentoFiscal = TipoModalidadeDocumentoFiscal.Nfce,
                NaturezaOperacao = "Venda a prazo",
                OperacaoConsumidorFinal = false,
                TipoEmissao = TipoEmissaoNFe.SefazVirtualAmbienteNacional,
                Serie = 3,
                TipoNotaFiscal = TipoNotaFiscal.Entrada,
                VersaoAplicativoEmissao = "3.0",
                UnidadeFederativaEmitente = UfIBGE.RJ,
                TipoProcessoEmissao = TipoProcessoEmissaoNFe.AvulsaFisco,
                TipoImpressao = TipoFormatoImpressaoDanfe.Nfce,
                Numero = 67889,
                IndicadorPresencaComprador = TipoIndicadorPresencaComprador.Outros,
            };
            return identificacao;
        }
    }
}