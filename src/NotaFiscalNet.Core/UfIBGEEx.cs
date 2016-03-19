using System;

namespace NotaFiscalNet.Core
{
    public static class UfIBGEEx
    {
        public static UfIBGE FromSiglaUF(string uf)
        {
            if (String.IsNullOrEmpty(uf))
                throw new ArgumentNullException("uf");

            uf = uf.ToUpper();

            switch (uf)
            {
                case "AC": return UfIBGE.AC;
                case "AL": return UfIBGE.AL;
                case "AP": return UfIBGE.AP;
                case "AM": return UfIBGE.AM;
                case "BA": return UfIBGE.BA;
                case "CE": return UfIBGE.CE;
                case "DF": return UfIBGE.DF;
                case "ES": return UfIBGE.ES;
                case "GO": return UfIBGE.GO;
                case "MA": return UfIBGE.MA;
                case "MT": return UfIBGE.MT;
                case "MS": return UfIBGE.MS;
                case "MG": return UfIBGE.MG;
                case "PR": return UfIBGE.PR;
                case "PB": return UfIBGE.PB;
                case "PA": return UfIBGE.PA;
                case "PE": return UfIBGE.PE;
                case "PI": return UfIBGE.PI;
                case "RJ": return UfIBGE.RJ;
                case "RN": return UfIBGE.RN;
                case "RS": return UfIBGE.RS;
                case "RO": return UfIBGE.RO;
                case "RR": return UfIBGE.RR;
                case "SC": return UfIBGE.SC;
                case "SE": return UfIBGE.SE;
                case "SP": return UfIBGE.SP;
                case "TO": return UfIBGE.TO;
                default:
                    throw new ApplicationException("A sigla '" + uf + "' não é uma UF válida.");
            }
        }

        public static string ToCustomValue(this UfIBGE value)
        {
            return ((int)value).ToString("00");
        }

        public static UfIBGE Parse(string value)
        {
            return (UfIBGE)Enum.Parse(typeof(UfIBGE), value);
        }

        public static UfIBGE Parse(string value, bool ignoreCase)
        {
            return (UfIBGE)Enum.Parse(typeof(UfIBGE), value, ignoreCase);
        }
    }
}