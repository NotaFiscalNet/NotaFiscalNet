namespace NotaFiscalNet.Core
{
    public class TamanhoMaximoBytesLoteNfeException : LoteNFeException
    {
        internal TamanhoMaximoBytesLoteNfeException() : base("A quantidade máxima de bytes (500KB) para um lote NF-e foi excedida.")
        {
        }
    }
}