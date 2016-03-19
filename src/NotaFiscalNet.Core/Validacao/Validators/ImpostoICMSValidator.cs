namespace NotaFiscalNet.Core.Validacao.Validators
{
    internal class ImpostoICMSValidator : BaseValidator
    {
        public override void Validate(ValidationContext context, FieldMember field)
        {
            ImpostoICMS imposto = (ImpostoICMS)field.Source;
            object value = field.GetValue();

            if (field.Property.Name == "Aliquota")
                if (imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst00 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst10 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst20 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst30 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst51 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst70 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst90 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.CSOSN900)
                    if ((decimal)value == 0m)
                        context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));

            if (field.Property.Name == "AliquotaSN")
                if (imposto.SituacaoTributaria == SituacaoTributariaICMS.CSOSN101 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.CSOSN201)
                    if ((decimal)value == 0m)
                        context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));

            if (field.Property.Name == "ModalidadeBaseCalculo")
                if (imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst00 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst10 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst20 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst70 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst90)
                    if ((ModalidadeBaseCalculoIcms)value == ModalidadeBaseCalculoIcms.NaoEspecificado)
                        context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));

            if (field.Property.Name == "PercentualReducaoBaseCalculo")
                if (imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst20 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst70)
                    if ((decimal)value == 0m)
                        context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));

            if (field.Property.Name == "BaseCalculo")
                if (imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst00 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst10 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst20 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst30 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst70 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst90)
                    if ((decimal)value == 0m)
                        context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));

            if (field.Property.Name == "Valor")
                if (imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst00 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst10 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst20 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst30 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst70 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst40 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst41 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst50 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst90)
                    if ((decimal)value == 0m)
                        context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));

            if (field.Property.Name == "MotivoDesoneracaoCondicional")
                if (imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst40 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst41 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst50)
                    if (imposto.Valor > 0m)
                        if ((MotivoDesoneracaoCondicionalICMS)value == MotivoDesoneracaoCondicionalICMS.NaoEspecificado)
                            context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));

            if (field.Property.Name == "ModalidadeBaseCalculoST")
                if (imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst10 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst30 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst70 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst90)
                    if ((ModalidadeBaseCalculoIcmsST)value == ModalidadeBaseCalculoIcmsST.NaoEspecificado)
                        context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));

            if (field.Property.Name == "PercentualMargemValorAdicionado")
                if (imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst10 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst30 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst70)
                    if ((decimal)value == 0m)
                        context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));

            if (field.Property.Name == "PercentualReducaoBaseCalculoST")
                if (imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst10 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst30 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst70)
                    if ((decimal)value == 0m)
                        context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));

            if (field.Property.Name == "BaseCalculoST")
                if (imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst10 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst30 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst60 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst70 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst90)
                    if ((decimal)value == 0m)
                        context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));

            if (field.Property.Name == "AliquotaST")
                if (imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst10 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst30 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst70 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst90)
                    if ((decimal)value == 0m)
                        context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));

            if (field.Property.Name == "ValorST")
                if (imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst10 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst30 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst60 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst70 ||
                    imposto.SituacaoTributaria == SituacaoTributariaICMS.Cst90)
                    if ((decimal)value == 0m)
                        context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));
        }
    }
}