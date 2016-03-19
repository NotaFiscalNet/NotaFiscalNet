namespace NotaFiscalNet.Core.Validacao.Validators
{
    internal class CNPJouCPFValidator : BaseValidator
    {
        public override void Validate(ValidationContext context, FieldMember field)
        {
            ICPFouCNPJ cpfoucnpj = (ICPFouCNPJ)field.Source;
            object value = field.GetValue();

            if (field.Property.Name == "CNPJ")
                if (string.IsNullOrEmpty(cpfoucnpj.CPF) && string.IsNullOrEmpty(cpfoucnpj.CNPJ))
                    context.Add(ErroValidacao.Create(ChaveErroValidacao.CPFouCNPJObrigatorio, context.Path.ToString(), field.Property.Name));

        }
    }
}
