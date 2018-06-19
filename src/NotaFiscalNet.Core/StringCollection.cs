using System.Linq;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma lista de strings.
    /// </summary>
    public sealed class StringCollection : BaseCollection<string>, IModificavel
    {
	    /// <summary>
	    /// Retorna se existe alguma instancia da classe modificada na coleção
	    /// </summary>
	    public bool Modificado => this.Any(item => !string.IsNullOrEmpty(item));
    }
}