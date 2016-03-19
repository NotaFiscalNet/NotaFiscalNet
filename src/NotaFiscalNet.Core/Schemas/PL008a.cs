using System;
using System.IO;

namespace NotaFiscalNet.Core.Schemas
{
    public class PL008a : IPacoteLiberacaoNFe
    {

        private const string BASE_PATH = "Schemas\\PL_008a\\{0}";
        private const string BASE_PATH_EVENTO_CANCELAMENTO = "Schemas\\Evento_Canc_PL\\{0}";

        public string VersaoLayout { get { return "3.10"; } }

        public string PathNFe
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    String.Format(BASE_PATH, "nfe_v3.10.xsd"));
            }
        }

        public string PathNFeNoSig
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    String.Format(BASE_PATH, "nfe_v3.10_NoSig.xsd"));
            }
        }

        public string PathEnviNFe
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    String.Format(BASE_PATH, "enviNFe_v3.10.xsd"));
            }
        }


        public string PathEnvEventoCancelamento
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    String.Format(BASE_PATH_EVENTO_CANCELAMENTO, "envEventoCancNFe_v1.00.xsd"));
            }

        }

        public string PathEnvEventoCancelamentoNoSig
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    String.Format(BASE_PATH_EVENTO_CANCELAMENTO, "envEventoCancNFe_v1.00_NoSig.xsd"));

            }
        }


        public string PathInutNFe
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                  String.Format(BASE_PATH, "inutNFe_v3.10.xsd"));
            }
        }


        public string PathInutNFeNoSig
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                  String.Format(BASE_PATH, "inutNFe_v3.10_NoSig.xsd"));
            }
        }
    }
}