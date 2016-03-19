namespace NotaFiscalNet.Core.Validacao
{
    internal abstract class BaseValidator
    {
        public virtual bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public abstract void Validate(ValidationContext context, FieldMember field);
    }
}