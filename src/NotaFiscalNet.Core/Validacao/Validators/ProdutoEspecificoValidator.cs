using System;
using System.Collections;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core.Validacao.Validators
{
    internal class ProdutoEspecificoValidator : BaseValidator
    {
        public override void Validate(ValidationContext context, FieldMember field)
        {
            Produto produto = (Produto)field.Source;

            if ((field.Property.Name == "DetalhamentoVeiculo" && produto.TipoProdutoEspecifico == TipoProdutoEspecifico.VeiculoNovo) ||
                (field.Property.Name == "DetalhamentoCombustivel" && produto.TipoProdutoEspecifico == TipoProdutoEspecifico.Combustivel))
            {
                object value = field.GetValue();
                Type typeValue = value.GetType();

                foreach (FieldMember fieldInternal in FieldMember.GetValidatableProperties(context, value, typeValue))
                {
                    if (!fieldInternal.Attribute.Ignore)
                        fieldInternal.Validate();
                    else if (fieldInternal.GetValue() != null && fieldInternal.GetValue().GetType().GetInterface("IModificavel") != null && ((IModificavel)fieldInternal.GetValue()).Modificado)
                        fieldInternal.Validate();
                }
            }
            if ((field.Property.Name == "DetalhamentoMedicamento" && produto.TipoProdutoEspecifico == TipoProdutoEspecifico.Medicamento) ||
                (field.Property.Name == "DetalhamentoArmamento" && produto.TipoProdutoEspecifico == TipoProdutoEspecifico.Armamento))
            {
                object value = field.GetValue();
                int count = 0;
                IEnumerable enumerable = (IEnumerable)value;
                foreach (object item in enumerable)
                    count++;

                if (count < 1)
                    context.Add(ErroValidacao.Create(ChaveErroValidacao.CollectionMinValue, 1));
                field.Validate();
            }
        }
    }
}
