namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Indicador da Inscrição Estadual do Destinatário.
    /// </summary>
    /// <remarks>
    /// Nota 1: No caso de NFC-e informar indIEDest=9 (NaoContribuinte) e não informar a tag IE do destinatário; 
    /// Nota 2: No caso de operação com o Exterior informar indIEDest=9 (NaoContribuinte) e não informar a tag IE do destinatário;
    /// Nota 3: No caso de Contribuinte Isento de Inscrição (indIEDest=2) (ContribuinteIsentoIE), não informar a tag IE do destinatário
    /// </remarks>
    public enum IndicadorIEDestinatario
    {
        /// <summary>
        /// 1 - Contribuinte ICMS (informar a IE do destinatário).
        /// </summary>
        ContribuinteIcms = 1,

        /// <summary>
        /// 2 - Contribuinte isento de Inscrição no cadastro de Contribuintes do ICMS.
        /// </summary>
        ContribuinteIsentoIE = 2,

        /// <summary>
        /// 9 - Não Contribuinte, que pode ou não possuir Inscrição Estadual no Cadastro de Contribuintes do ICMS.
        /// </summary>
        NaoContribuinte = 9
    }
}
