using System;

namespace NotaFiscalNet.Core.Validacao.Validators
{
    internal class EntradaContingenciaValidator : BaseValidator
    {
        public override void Validate(ValidationContext context, FieldMember field)
        {
            IdentificacaoNFe identificacao = (IdentificacaoNFe)field.Source;

            if (identificacao.TipoEmissao != TipoEmissaoNFe.Normal)
            {
                if (field.Property.Name == "DataHoraEntradaContingencia")
                {
                    DateTime dataEntrada = (DateTime)field.GetValue();
                    if (dataEntrada == DateTime.MinValue)
                        context.Add(ErroValidacao.Create(field.Attribute.ErrorKey, context.Path.ToString(), field.Property.Name));
                }
                if (field.Property.Name == "JustificativaEntradaContingencia")
                {
                    string justificativa = field.GetValue().ToString();
                    if (string.IsNullOrEmpty(justificativa))
                        context.Add(ErroValidacao.Create(field.Attribute.ErrorKey, context.Path.ToString(), field.Property.Name));
                }
            }
        }
    }
}