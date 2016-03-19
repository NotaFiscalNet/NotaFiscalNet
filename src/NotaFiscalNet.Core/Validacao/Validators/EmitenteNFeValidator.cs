namespace NotaFiscalNet.Core.Validacao.Validators
{
    internal class EmitenteNFeValidator : BaseValidator
    {
        public override void Validate(ValidationContext context, FieldMember field)
        {
            EmitenteNFe emitente = (EmitenteNFe)field.Source;
            object value = field.GetValue();

            if (field.Property.Name == "InscricaoMunicipal")
                if (string.IsNullOrEmpty(value.ToString()) && !string.IsNullOrEmpty(emitente.CNAEFiscal))
                    context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));

            if (field.Property.Name == "CNAEFiscal")
                if (string.IsNullOrEmpty(value.ToString()) && !string.IsNullOrEmpty(emitente.InscricaoMunicipal))
                    context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));

        }
    }
}
