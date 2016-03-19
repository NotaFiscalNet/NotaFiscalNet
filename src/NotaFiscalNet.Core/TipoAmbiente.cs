using System.ComponentModel;
using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Tipo do ambiente no qual a NF-e está rodando.
    /// </summary>
    
    
    public enum TipoAmbiente
    {
        /// <summary>
        /// 1 - Ambiente de produção.
        /// </summary>
        [Description("Produção")]
        Producao = 1,
        /// <summary>
        /// 2 - Ambiente de homologação.
        /// </summary>
        [Description("Homologação")]
        Homologacao = 2
    }
}
