namespace NotaFiscalNet.Core.Validacao
{
    internal delegate bool ValidationCondiction<TObject, TValue>(TObject obj, TValue value, string fieldName);
}