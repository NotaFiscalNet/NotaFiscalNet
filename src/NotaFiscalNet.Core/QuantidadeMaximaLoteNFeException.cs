namespace NotaFiscalNet.Core
{
    public class QuantidadeMaximaLoteNFeException : LoteNFeException
    {
        internal QuantidadeMaximaLoteNFeException() : base("A quantidade m�xima de Notas Fiscais Eletr�nicas por lote foi excedida (50).")
        {
        }
    }
}