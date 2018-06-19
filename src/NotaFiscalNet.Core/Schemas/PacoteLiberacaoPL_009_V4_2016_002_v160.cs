using System;
using System.IO;

namespace NotaFiscalNet.Core.Schemas
{
    public class PacoteLiberacaoPL_009_V4_2016_002_v160 : IPacoteLiberacaoNFe
    {
        private const string BasePath = @"Schemas\PacoteLiberacaoPL_009_V4_2016_002_v160\{0}";
        private const string BasePathEventoCancelamento = @"Schemas\Evento_Canc_PL\{0}";

        public string VersaoLayout => "4.00";

        public string PathNFe => Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            string.Format(BasePath, "nfe_v4.00.xsd"));

        public string PathNFeNoSig => Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            string.Format(BasePath, "nfe_v4.00_NoSig.xsd"));

        public string PathEnviNFe => Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            string.Format(BasePath, "enviNFe_v4.00.xsd"));

        public string PathEnvEventoCancelamento => Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            string.Format(BasePathEventoCancelamento, "envEventoCancNFe_v1.00.xsd"));

        public string PathEnvEventoCancelamentoNoSig => Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            string.Format(BasePathEventoCancelamento, "envEventoCancNFe_v1.00_NoSig.xsd"));

        public string PathInutNFe => Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            string.Format(BasePath, "inutNFe_v4.00.xsd"));

        public string PathInutNFeNoSig => Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            string.Format(BasePath, "inutNFe_v4.00_NoSig.xsd"));
    }
}