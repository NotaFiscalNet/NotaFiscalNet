namespace NotaFiscalNet.Core.Validacao
{
    public class Mensagens
    {
        // ReSharper disable InconsistentNaming
        public static string email_error => "'{PropertyName}' é um endereço de email inválido.";

        public static string greaterthanorequal_error => "'{PropertyName}' deve ser superior ou igual a '{ComparisonValue}'.";

        public static string greaterthan_error => "'{PropertyName}' deve ser superior a '{ComparisonValue}'.";

        public static string length_error => "'{PropertyName}' deve ter {MinLength} a {MaxLength} caracteres.";

        public static string collectionlength_error => "'{PropertyName}' deve ter {MinLength} a {MaxLength} itens.";

        public static string lessthanorequal_error => "'{PropertyName}' deve ser inferior ou igual a '{ComparisonValue}'.";

        public static string lessthan_error => "'{PropertyName}' deve ser inferior a '{ComparisonValue}'.";

        public static string notempty_error => "'{PropertyName}' deve ser definido.";

        public static string notequal_error => "'{PropertyName}' deve ser diferente de '{ComparisonValue}'.";

        public static string notnull_error => "'{PropertyName}' não pode ser nulo.";

        public static string predicate_error => "'{PropertyName}' não verifica a condição definida.";

        public static string regex_error => "'{PropertyName}' não se encontra no formato correcto.";

        public static string equal_error => "'{PropertyName}' deve ser igual a '{ComparisonValue}'.";

        public static string exact_length_error => "'{PropertyName}' deve ter o comprimento de {MaxLength} caracteres.";

        public static string collectionexact_length_error => "'{PropertyName}' deve possuir {MaxLength} itens.";

        public static string exclusivebetween_error => "'{PropertyName}' deve estar entre {From} e {To} (exclusivo).";

        public static string inclusivebetween_error => "'{PropertyName}' deve estar entre {From} e {To}.";

        public static string empty_error => "'{PropertyName}' deve ser vazio.";

        public static string null_error => "'{PropertyName}' deve ser nulo.";

        public static string CreditCardError => "'{PropertyName}' não é um número de cartão de crédito válido.";

        public static string scale_precision_error => "'{PropertyName}' não pode ter mais que {expectedPrecision} digitos no total, com {expectedScale} casas decimais.";

        public static string enum_error => "'{PropertyName}' deve ser definido.";
        // ReSharper restore InconsistentNaming
    }
}