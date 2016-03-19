using System;

namespace NotaFiscalNet.Core.Validacao.Validators
{
    internal class DefaultValidator : BaseValidator
    {
        public override void Validate(ValidationContext context, FieldMember field)
        {
            object value = field.GetValue();
            TypeCode type = Type.GetTypeCode(value.GetType());
            object defaultValue;

            switch (type)
            {
                case TypeCode.DBNull:
                case TypeCode.Empty:
                    throw new NotSupportedException();
                
                case TypeCode.Boolean:
                case TypeCode.Object:
                    break;
                
                case TypeCode.String:
                    defaultValue = field.Attribute.DefaultValue ?? Convert.ChangeType(string.Empty, type);
                    if (Equals(value, defaultValue))
                        context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));
                    break;

                case TypeCode.Char:
                    defaultValue = field.Attribute.DefaultValue ?? Convert.ChangeType(' ', type);
                    if (Equals(value, defaultValue))
                        context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));
                    break;

                case TypeCode.DateTime:
                    defaultValue = field.Attribute.DefaultValue ?? Convert.ChangeType(DateTime.MinValue, type);
                    if ((DateTime)value <= (DateTime)defaultValue)
                        context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));
                    break;

                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.SByte:
                    defaultValue = field.Attribute.DefaultValue ?? Convert.ChangeType(0, type);
                    if (Equals(value, defaultValue))
                        context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));
                    break;
            }

            //return true;
        }

        public DefaultValidator()
        {
        }
    }
}
