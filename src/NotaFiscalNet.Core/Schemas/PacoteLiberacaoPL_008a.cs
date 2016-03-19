using System;
using System.IO;

namespace NotaFiscalNet.Core.Schemas
{
    public class PacoteLiberacaoPL_008a : IPacoteLiberacaoNFe
    {
        private const string BasePath = "Schemas\\PL_008a\\{0}";
        private const string BasePathEventoCancelamento = "Schemas\\Evento_Canc_PL\\{0}";

        public string VersaoLayout => "3.10";

        public string PathNFe => Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            string.Format(BasePath, "nfe_v3.10.xsd"));

        public string PathNFeNoSig => Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            string.Format(BasePath, "nfe_v3.10_NoSig.xsd"));

        public string PathEnviNFe => Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            string.Format(BasePath, "enviNFe_v3.10.xsd"));

        public string PathEnvEventoCancelamento => Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            string.Format(BasePathEventoCancelamento, "envEventoCancNFe_v1.00.xsd"));

        public string PathEnvEventoCancelamentoNoSig => Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            string.Format(BasePathEventoCancelamento, "envEventoCancNFe_v1.00_NoSig.xsd"));

        public string PathInutNFe => Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            string.Format(BasePath, "inutNFe_v3.10.xsd"));

        public string PathInutNFeNoSig => Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            string.Format(BasePath, "inutNFe_v3.10_NoSig.xsd"));
    }
}