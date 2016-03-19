using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    
    
    public enum IndicadorExigibilidadeIss
    {
        /// <summary>
        /// 1 - Exig�vel.
        /// </summary>
        Exigivel = 1,
        /// <summary>
        /// 2 - N�o Incidente.
        /// </summary>
        NaoIncidente = 2,
        /// <summary>
        /// 3 - Isen��o.
        /// </summary>
        Isencao = 3,
        /// <summary>
        /// 4 - Exporta��o;
        /// </summary>
        Exportacao = 4,
        /// <summary>
        /// 5 - Imunidade
        /// </summary>
        Imunidade = 5,
        /// <summary>
        /// 6 - Exigibilidade Suspensa por Decis�o Judicial.
        /// </summary>
        SuspensaoJudicial = 6,
        /// <summary>
        /// 7 - Exigibilidade Suspensa por Processo Administrativo.
        /// </summary>
        SuspensaoAdministrativa = 7
    }
}