namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Interface que define a estrutura de um tipo que pode conter a propriedade Dirty.
    /// </summary>

    public interface IDirtyable
    {
        bool IsDirty { get; }
    }
}