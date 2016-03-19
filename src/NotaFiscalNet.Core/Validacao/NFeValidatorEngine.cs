using System;
using System.Collections.Generic;
using System.Diagnostics;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core.Validacao
{
    internal class NFeValidatorEngine
    {
        static readonly Type IDirtyableType = typeof(IModificavel);

        [DebuggerStepThrough]
        public NFeValidatorEngine(NFe nfe)
        {
            if ( nfe == null ) throw new ArgumentNullException("nfe");
            NFe = nfe;
            Context = new ValidationContext();
        }

        private NFe NFe { get; set; }
        internal ValidationContext Context { get; set; }

        public bool ValidateNFe()
        {
            Type typeNFe = NFe.GetType();
            Context.Path.Append(typeNFe.Name);

            List<FieldMember> fields = FieldMember.GetValidatableProperties(Context, NFe, typeNFe);
            foreach (FieldMember field in fields)
            {
                if (field.Attribute.Ignore)
                    continue;

                object value = null;

                try
                {
                    value = field.GetValue();
                }
                catch (Exception ex)
                {
                    Context.Path.Append(field.Property.Name);
                    Context.Add(ErroValidacao.Create(ChaveErroValidacao.ExcecaoNaoTratada, Context.Path.ToString() + " ### Erro: " + ex.Message));
                    Context.Path.Remove();

                    continue;
                }

                Type typeOfValue = field.Property.PropertyType;
                bool implementsIDirtyable = IDirtyableType.IsAssignableFrom(typeOfValue);
                if (!implementsIDirtyable || (implementsIDirtyable && value != null && ((IModificavel)value).Modificado))
                    field.Validate();
            }

            Context.Path.Remove();

            return Context.Errors.Count == 0;
        }
    }
}
