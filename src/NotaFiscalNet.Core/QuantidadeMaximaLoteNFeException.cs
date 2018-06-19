namespace NotaFiscalNet.Core
{
    public class QuantidadeMaximaLoteNFeException : LoteNFeException
    {
        internal QuantidadeMaximaLoteNFeException() : base("A quantidade máxima de Notas Fiscais Eletrônicas por lote foi excedida (50).")
        {
        }
    }
}