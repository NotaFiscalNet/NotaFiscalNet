namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Especifica o Tipo do Veículo de acordo com tabela do RENAVAM.
    /// </summary>

    public enum TipoVeiculoRENAVAM
    {
        /// <summary>
        /// Não Especificado
        /// </summary>
        NaoEspecificado = -1,

        /// <summary>
        /// 02 - Ciclomoto.
        /// </summary>
        Ciclomoto = 2,

        /// <summary>
        /// 03 - Motoneta.
        /// </summary>
        Motoneta = 3,

        /// <summary>
        /// 04 - Motociclo.
        /// </summary>
        Motociclo = 4,

        /// <summary>
        /// 05 - Triciclo.
        /// </summary>
        Triciclo = 5,

        /// <summary>
        /// 06 - Automovel.
        /// </summary>
        Automovel = 6,

        /// <summary>
        /// 07 - Microônibus.
        /// </summary>
        Microonibus = 7,

        /// <summary>
        /// 08 - Ônibus.
        /// </summary>
        Onibus = 8,

        /// <summary>
        /// 10 - Reboque.
        /// </summary>
        Reboque = 10,

        /// <summary>
        /// 13 - Caminhoneta.
        /// </summary>
        Caminhoneta = 13,

        /// <summary>
        /// 14 - Caminhão.
        /// </summary>
        Caminhao = 14,

        /// <summary>
        /// 17 - Caminhão trator (C. TRATOR).
        /// </summary>
        CaminhaoTrator = 17,

        /// <summary>
        /// 22 - Esp./Ônibus.
        /// </summary>
        EspOnibus = 22,

        /// <summary>
        /// 23 - MISTO / CAM.
        /// </summary>
        MistoCam = 23,

        /// <summary>
        /// 24 - CARGA / CAM.
        /// </summary>
        CargaCam = 24
    }
}