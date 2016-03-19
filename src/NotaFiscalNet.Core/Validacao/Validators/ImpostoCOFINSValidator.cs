namespace NotaFiscalNet.Core.Validacao.Validators
{
    internal class ImpostoCOFINSValidator : BaseValidator
    {
        public override void Validate(ValidationContext context, FieldMember field)
        {
            //ImpostoCOFINS imposto = (ImpostoCOFINS)field.Source;
            //object value = field.GetValue();

            //if (field.Property.Name == "BaseCalculo")
            //    if (!(imposto.SituacaoTributaria == SituacaoTributariaCOFINS.TributavelMonofasicaAliquotaZero ||
            //        imposto.SituacaoTributaria == SituacaoTributariaCOFINS.TributavelAliquotaZero ||
            //        imposto.SituacaoTributaria == SituacaoTributariaCOFINS.IsentaContribuicao ||
            //        imposto.SituacaoTributaria == SituacaoTributariaCOFINS.SemIncidenciaContribuicao ||
            //        imposto.SituacaoTributaria == SituacaoTributariaCOFINS.ComSubstituicaoContribuicao))
            //        if ((double)value == 0D)
            //            context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));

            //if (field.Property.Name == "Aliquota")
            //    if (!(imposto.SituacaoTributaria == SituacaoTributariaCOFINS.TributavelMonofasicaAliquotaZero ||
            //        imposto.SituacaoTributaria == SituacaoTributariaCOFINS.TributavelAliquotaZero ||
            //        imposto.SituacaoTributaria == SituacaoTributariaCOFINS.IsentaContribuicao ||
            //        imposto.SituacaoTributaria == SituacaoTributariaCOFINS.SemIncidenciaContribuicao ||
            //        imposto.SituacaoTributaria == SituacaoTributariaCOFINS.ComSubstituicaoContribuicao))
            //        if ((double)value == 0D)
            //            context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));

            //if (field.Property.Name == "Valor")
            //    if (!(imposto.SituacaoTributaria == SituacaoTributariaCOFINS.TributavelMonofasicaAliquotaZero ||
            //        imposto.SituacaoTributaria == SituacaoTributariaCOFINS.TributavelAliquotaZero ||
            //        imposto.SituacaoTributaria == SituacaoTributariaCOFINS.IsentaContribuicao ||
            //        imposto.SituacaoTributaria == SituacaoTributariaCOFINS.SemIncidenciaContribuicao ||
            //        imposto.SituacaoTributaria == SituacaoTributariaCOFINS.ComSubstituicaoContribuicao))
            //        if ((double)value == 0D)
            //            context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));
        }
    }
}