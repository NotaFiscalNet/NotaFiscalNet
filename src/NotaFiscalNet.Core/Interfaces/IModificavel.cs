namespace NotaFiscalNet.Core.Interfaces
{
    /// <summary>
    /// Interface que define a estrutura de um tipo que pode conter a propriedade Dirty.
    /// </summary>
    public interface IModificavel
    {
        bool Modificado { get; }
    }
}