using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Validacao.Validators;

namespace NotaFiscalNet.Core.Validacao
{
    internal class FieldMember
    {
        static readonly Type IDirtyableType = typeof(IModificavel);
        static readonly Type IEnumerableType = typeof(IEnumerable);

        public FieldMember(ValidationContext context, object source, PropertyInfo property, ValidateFieldAttribute attribute)
        {
            Context = context;
            Source = source;
            Property = property;
            Attribute = attribute;
        }

        public ValidationContext Context { get; private set; }
        public object Source { get; private set; }
        public PropertyInfo Property { get; private set; }
        public ValidateFieldAttribute Attribute { get; private set; }

        public void Validate()
        {
            BaseValidator validator = null;

            object value = null;

            try
            {
                value = GetValue();
            }
            catch
            {
                Context.Path.Append(Property.Name);
                Context.Add(ErroValidacao.Create(ChaveErroValidacao.ExcecaoNaoTratada, Context.Path.ToString(), Property.Name));
                Context.Path.Remove();

                return;
            }

            if (value == null)
                throw new ApplicationException(string.Format("Valor do campo é nulo: {1}.{0}", Property.Name, Context.Path));

            Type typeValue = Property.PropertyType;

            // Obtém o validador do campo atual
            if ( Attribute.Validator == null )
            {
                DefaultValidatorAttribute[] defaultValidators = (DefaultValidatorAttribute[])typeValue.GetCustomAttributes(typeof(DefaultValidatorAttribute), true);
                if ( defaultValidators != null && defaultValidators.Length > 0 )
                    validator = ValidatorFactory.Create(defaultValidators[0].ValidatorType); // decorado na classe
                else
                    validator = ValidatorFactory.DefaultValidator; // padrão
            }
            else
                validator = ValidatorFactory.Create(Attribute.Validator);

            /// determina o caminho adicionando o nome do campo atual a pilha de caminhos
            Context.Path.Append(Property.Name);

            /// Realiza a validação baseada na regra de validação do campo atual
            validator.Validate(Context, this);
            
            /// Se o valor do campo for um tipo diferente de primitivo (string, int, double, etc), então,
            /// realiza a validação de todos os campos do objeto filho.
            if (Type.GetTypeCode(typeValue) == TypeCode.Object && 
                (validator.GetType() == typeof(DefaultValidator) ||
                 validator.GetType() == typeof(RangeCollectionValidator)))
            {
                foreach ( FieldMember field in GetValidatableProperties(Context, value, typeValue) )
                {
                    if (field.Attribute.Ignore)
                        continue;

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

                if (IEnumerableType.IsAssignableFrom(typeValue))
                {
                    int posicaoItem = 0;

                    foreach (object item in (IEnumerable)value)
                    {
                        Context.Path.Append("[" + posicaoItem.ToString() + "]");

                        Type typeValueItem = item.GetType();

                        foreach (FieldMember fieldItem in GetValidatableProperties(Context, item, typeValueItem))
                        {
                            if (!fieldItem.Attribute.Ignore)
                                fieldItem.Validate();
                        }

                        posicaoItem++;

                        Context.Path.Remove();
                    }
                }
            }

            Context.Path.Remove();
        }

        /// <summary>
        /// Retorna o valor contido no atributo.
        /// </summary>
        /// <returns></returns>
        public object GetValue()
        {
            return Property.GetValue(Source, null);
        }

        /// <summary>
        /// Retorna a lista de atributos NFeFieldAttribute decorado no campo.
        /// </summary>
        /// <returns></returns>
        public NFeFieldAttribute[] GetNFeFieldAttribute()
        {
            NFeFieldAttribute[] attributes = (NFeFieldAttribute[])Property.GetCustomAttributes(typeof(NFeFieldAttribute), false);
            return attributes;
        }

        /// <summary>
        /// Retorna uma lista de campos aptos a serem validados.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="source"></param>
        /// <param name="typeSource"></param>
        /// <returns></returns>
        public static List<FieldMember> GetValidatableProperties(ValidationContext context, object source, Type typeSource)
        {
            if ( source == null ) throw new ArgumentNullException(nameof(source));

            PropertyInfo[] properties = typeSource.GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance);

            // tipo que representa o atributo decorado nas propriedades
            Type attributeType = typeof(ValidateFieldAttribute);

            List<FieldMember> validProperties = new List<FieldMember>();

            /// Seleciona todas as propriedades que estejam decoradas com o atributo
            /// ValidateFieldAttribute.
            foreach ( PropertyInfo property in properties )
            {
                ValidateFieldAttribute[] attributes = (ValidateFieldAttribute[])property.GetCustomAttributes(attributeType, true);
                if ( attributes != null && attributes.Length > 0 )
                {
                    FieldMember field = new FieldMember(context, source, property, attributes[0]);
                    validProperties.Add(field);
                }
            }

            /// Ordena a lista de acordo com a sequência, do menor para o maior
            validProperties.Sort((x, y) => x.Attribute.Sequence.CompareTo(y.Attribute.Sequence));

            return validProperties;
        }
    }
}
