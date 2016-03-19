using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Motivo da Desoneração Condicional do ICMS
    /// </summary>
    
    
    public enum MotivoDesoneracaoCondicionalICMS
    {
        NaoEspecificado = -1,
        /// <summary>
        /// 1 - Táxi.
        /// </summary>
        Taxi = 1,
        /// <summary>
        /// 2 - Deficiente Físico.
        /// </summary>
        DeficienteFisico = 2, 
        /// <summary>
        /// 3 - Produtor Agropecuário.
        /// </summary>
        ProdutorAgropecuario = 3,
        /// <summary>
        /// 4 - Frotista/Locadora
        /// </summary>
        FrotistaLocadora = 4,
        /// <summary>
        /// 5 - Diplomático/Consular.
        /// </summary>
        DiplomaticoConsular = 5,
        /// <summary>
        /// 6 - Utilitários e Motocicletas da Amazônia Ocidental e Áreas de Livre Comércio (Resolução 714/88 e 790/94 – CONTRAN e suas alterações)
        /// </summary>
        AmazoniaOcidentalAreaLivreComercio = 6,
        /// <summary>
        /// 7 - SUFRAMA.
        /// </summary>
        SUFRAMA = 7,
        /// <summary>
        /// 8 - Venda a órgão Público
        /// </summary>
        VendaOrgaoPublico = 8,
        /// <summary>
        /// 9 - Outros. (v2.0)
        /// </summary>
        Outros = 9,
        /// <summary>
        /// 10 - Decifiente Condutor.
        /// </summary>
        DeficienteCondutor = 10,
        /// <summary>
        /// 11 - Deficiente não condutor.
        /// </summary>
        DeficienteNaoCondutor = 11,
        /// <summary>
        /// 12 - Fomento agropecuário
        /// </summary>
        FomentoAgropecuario = 12
    }
}
