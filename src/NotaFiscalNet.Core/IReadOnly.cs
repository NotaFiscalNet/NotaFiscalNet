using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Interface que define a estrutura de um tipo que pode conter a situação de Apenas-Leitura.
    /// </summary>
    
    
    public interface IReadOnly
    {
        bool IsReadOnly { get; }
    }
}
