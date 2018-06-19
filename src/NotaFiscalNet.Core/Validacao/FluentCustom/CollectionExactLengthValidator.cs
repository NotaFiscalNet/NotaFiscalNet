namespace NotaFiscalNet.Core.Validacao.FluentCustom
{
    public class CollectionExactLengthValidator : CollectionLengthValidator
    {
        public CollectionExactLengthValidator(int length) : base(length, length, () => Mensagens.collectionexact_length_error)
        {
        }
    }
}