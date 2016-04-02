using FluentValidation;
using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Validacao.FluentCustom;
using System;

namespace NotaFiscalNet.Core.Validacao.Validadores
{
    internal class IdentificacaoDocumentoFiscalValidador : AbstractValidator<IdentificacaoDocumentoFiscal>
    {
        public IdentificacaoDocumentoFiscalValidador()
        {
            RuleFor(t => t.UnidadeFederativaEmitente)
                .IsInEnum()
                .NotEqual(UfIBGE.NaoEspecificado);

            RuleFor(t => t.CodigoNumerico)
                .InclusiveBetween(10000000, 99999999);

            RuleFor(t => t.NaturezaOperacao)
                .NotEmpty()
                .Length(1, 60);

            RuleFor(t => t.FormaPagamento)
                .IsInEnum();
            
            RuleFor(t => t.ModalidadeDocumentoFiscal)
                .IsInEnum();

            RuleFor(t => t.Serie)
                .InclusiveBetween(0, 999);

            RuleFor(t => t.Numero)
                .InclusiveBetween(1, 999999999);

            RuleFor(t => t.DataEmissao)
                .NotEmpty();

            RuleFor(t => t.DataEntradaSaida)
                .NotEqual(DateTime.MinValue);

            RuleFor(t => t.TipoNotaFiscal)
                .IsInEnum();

            RuleFor(t => t.IdentificadorLocalDestinoOperacao)
                .IsInEnum();

            RuleFor(t => t.CodigoMunicipioFatoGerador)
                .InclusiveBetween(1000000, 9999999);

            RuleFor(t => t.TipoImpressao)
                .IsInEnum();

            RuleFor(t => t.TipoEmissao)
                .IsInEnum();

            RuleFor(t => t.Ambiente)
                .IsInEnum();

            RuleFor(t => t.Finalidade)
                .IsInEnum();

            RuleFor(t => t.IndicadorPresencaComprador)
                .IsInEnum();

            RuleFor(t => t.TipoProcessoEmissao)
                .IsInEnum();

            RuleFor(t => t.VersaoAplicativoEmissao)
                .NotEmpty()
                .Length(1, 20);

            RuleFor(t => t.DataHoraEntradaContingencia)
                .NotEmpty()
                .NotEqual(DateTime.MinValue)
                .When(t => t.TipoEmissao != TipoEmissaoNFe.Normal);

            RuleFor(t => t.DataHoraEntradaContingencia)
                .Null()
                .When(t => t.TipoEmissao == TipoEmissaoNFe.Normal);

            RuleFor(t => t.JustificativaEntradaContingencia)
                .NotEmpty()
                .Length(15, 256)
                .When(t => t.TipoEmissao != TipoEmissaoNFe.Normal);

            RuleFor(t => t.JustificativaEntradaContingencia)
                .Null()
                .When(t => t.TipoEmissao == TipoEmissaoNFe.Normal);

            RuleFor(t => t.ReferenciasDocumentoFiscais)
                .CollectionLength(0, 500)
                .SetValidator(
                    new ValidadorPolimorfico<IReferenciaDocumentoFiscal>()
                        .Add(new ReferenciaDocumentoFiscalNfeValidador())
                        .Add(new ReferenciaDocumentoFiscalCteValidador())
                        .Add(new ReferenciaDocumentoFiscalEcfValidador())
                        .Add(new ReferenciaDocumentoFiscalNotaFiscalValidador())
                        .Add(new ReferenciaDocumentoFiscalNotaFiscalProdutorValidador())
                );
        }
    }
}