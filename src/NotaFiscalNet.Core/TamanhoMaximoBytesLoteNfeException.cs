namespace NotaFiscalNet.Core
{
    public class TamanhoMaximoBytesLoteNfeException : LoteNFeException
    {
        internal TamanhoMaximoBytesLoteNfeException() : base("A quantidade m�xima de bytes (500KB) para um lote NF-e foi excedida.")
        {
        }
    }
}