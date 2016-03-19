using System.ComponentModel;
using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Lista de Códigos de UF do IBGE.
    /// </summary>
    
    
    public enum UfIBGE
    {
        /// <summary>
        /// -1 - Não Especificado
        /// </summary>
        [Description("Não Especificado")]
        NaoEspecificado = -1,
        /// <summary>
        /// 11 - Rondônia.
        /// </summary>
        [Description("Rondônia")]
        RO = 11,
        /// <summary>
        /// 12 - Acre.
        /// </summary>
        [Description("Acre")]
        AC = 12,
        /// <summary>
        /// 13 - Amazonas.
        /// </summary>
        [Description("Amazonas")]
        AM = 13,
        /// <summary>
        /// 14 - Roraima.
        /// </summary>
        [Description("Roraima")]
        RR = 14,
        /// <summary>
        /// 15 - Pará.
        /// </summary>
        [Description("Pará")]
        PA = 15,
        /// <summary>
        /// 16 - Amapá.
        /// </summary>
        [Description("Amapá")]
        AP = 16,
        /// <summary>
        /// 17 - Tocantins.
        /// </summary>
        [Description("Tocantins")]
        TO = 17,
        /// <summary>
        /// 21 - Maranhão.
        /// </summary>
        [Description("Maranhão")]
        MA = 21,
        /// <summary>
        /// 22 - Piauí.
        /// </summary>
        [Description("Piauí")]
        PI = 22,
        /// <summary>
        /// 23 - Ceará.
        /// </summary>
        [Description("Ceará")]
        CE = 23,
        /// <summary>
        /// 24 - Rio Grande do Norte.
        /// </summary>
        [Description("Rio Grande do Norte")]
        RN = 24,
        /// <summary>
        /// 25 - Paraíba.
        /// </summary>
        [Description("Paraíba")]
        PB = 25,
        /// <summary>
        /// 26 - Pernambuco.
        /// </summary>
        [Description("Pernambuco")]
        PE = 26,
        /// <summary>
        /// 27 - Alagoas.
        /// </summary>
        [Description("Alagoas")]
        AL = 27,
        /// <summary>
        /// 28 - Sergipe.
        /// </summary>
        [Description("Sergipe")]
        SE = 28,
        /// <summary>
        /// 29 - Bahia.
        /// </summary>
        [Description("Bahia")]
        BA = 29,
        /// <summary>
        /// 31 - Minas Gerais.
        /// </summary>
        [Description("Minas Gerais")]
        MG = 31,
        /// <summary>
        /// 32 - Espirito Santo.
        /// </summary>
        [Description("Espirito Santo")]
        ES = 32,
        /// <summary>
        /// 33 - Rio de Janeiro.
        /// </summary>
        [Description("Rio de Janeiro")]
        RJ = 33,
        /// <summary>
        /// 35 - São Paulo.
        /// </summary>
        [Description("São Paulo")]
        SP = 35,
        /// <summary>
        /// 41 - Paraná.
        /// </summary>
        [Description("Paraná")]
        PR = 41,
        /// <summary>
        /// 42 - Santa Catarina.
        /// </summary>
        [Description("Santa Catarina")]
        SC = 42,
        /// <summary>
        /// 43 - Rio Grande do Sul.
        /// </summary>
        [Description("Rio Grande do Sul")]
        RS = 43,
        /// <summary>
        /// 50 - Mato Grosso do Sul.
        /// </summary>
        [Description("Mato Grosso do Sul")]
        MS = 50,
        /// <summary>
        /// 51 - Mato Grosso.
        /// </summary>
        [Description("Mato Grosso")]
        MT = 51,
        /// <summary>
        /// 52 - Goiás.
        /// </summary>
        [Description("Goiás")]
        GO = 52,
        /// <summary>
        /// 53 - Distrito Federal.
        /// </summary>
        [Description("Distrito Federal")]
        DF = 53
    }
}
