using NotaFiscalNet.Core.Validacao;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace NotaFiscalNet.Core.Utils
{
    /// <summary>
    /// Classe de Suporte para Validação
    /// </summary>
    public static partial class ValidationUtil
    {
        ///// <summary>
        ///// Valida um valor double, sem permitir valores negativos
        ///// </summary>
        ///// <param name="value">Valor a ser validado</param>
        ///// <param name="casasInteiro">Número de casas na parte Inteira</param>
        //public static void Validate(double value, int casasInteiro)
        //{
        //    Validate(value, casasInteiro, false);
        //}

        ///// <summary>
        ///// Valida um valor double
        ///// </summary>
        ///// <param name="value">Valor a ser validado</param>
        ///// <param name="casasInteiro">Número de casas na parte Inteira</param>
        ///// <param name="permiteNegativo">Permite ou não valores negativos</param>
        //public static void Validate(double value, int casasInteiro, bool permiteNegativo)
        //{
        //    double maior = double.Parse("1" + string.Empty.PadLeft(casasInteiro, '0'));
        //    double menor = 0D;
        //    if (permiteNegativo)
        //        menor -= maior;

        // if (value <= menor) throw new ArgumentOutOfRangeException("O valor informado é menor que o permitido.");

        //    if (value >= maior)
        //        throw new ArgumentOutOfRangeException("O valor informado é maior que o permitido.");
        //}

        public static bool ValidateRegex(string value, string pattern)
        {
            return Regex.IsMatch(value, pattern);
        }

        public static bool ValidateRegex(int value, string pattern)
        {
            return Regex.IsMatch(value.ToString(), pattern);
        }

        public static TEnum ValidateEnum<TEnum>(TEnum value, string paramName)
        {
            if (typeof(TEnum).BaseType != typeof(Enum))
                throw new ArgumentException("TEnum deve ser um tipo enumerador.");

            if (!Enum.IsDefined(typeof(TEnum), value))
                throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.GenericArgumentException, paramName));
            return value;
        }

        public static TEnum ValidateEnumOptional<TEnum>(TEnum value, TEnum notSetValue, string paramName)
        {
            if (typeof(TEnum).BaseType != typeof(Enum))
                throw new ArgumentException("TEnum deve ser um tipo enumerador.");

            if (!Enum.IsDefined(typeof(TEnum), value))
                throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.GenericArgumentException, paramName));

            if (Equals(value, notSetValue))
                throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.EnumNaoEspecificado, paramName));
            return value;
        }

        public static int ValidateInt9(int value, string paramName)
        {
            return ValidateRange(value, 0, 999999999, paramName);
        }

        public static int ValidateInt7(int value, string paramName)
        {
            return ValidateRange(value, 0, 9999999, paramName);
        }

        public static long ValidateLong15(long value, string paramName)
        {
            return ValidateRange(value, 0L, 999999999999999L, paramName);
        }

        public static double ValidateTDec_0803(double value, string paramName)
        {
            return ValidateRange(value, 0.0, 99999999.999, paramName);
        }

        public static decimal ValidateTDec_0803(decimal value, string paramName)
        {
            return ValidateRange(value, 0.0m, 99999999.999m, paramName);
        }

        public static double ValidateTDec_1504(double value, string paramName)
        {
            // "0|0\.[0-9]{1,4}|[1-9]{1}[0-9]{0,14}|[1-9]{1}[0-9]{0,14}(\.[0-9]{1,4})?"

            return ValidateRange(value, 0.0, 999999999999999.9999, paramName);
        }

        public static decimal ValidateTDec_1504(decimal value, string paramName)
        {
            // "0|0\.[0-9]{1,4}|[1-9]{1}[0-9]{0,14}|[1-9]{1}[0-9]{0,14}(\.[0-9]{1,4})?"

            return ValidateRange(value, 0.0m, 999999999999999.9999m, paramName);
        }

        public static double ValidateTDec_1104v(double value, string paramName)
        {
            // 0|0\.[0-9]{1,4}|[1-9]{1}[0-9]{0,10}|[1-9]{1}[0-9]{0,10}(\.[0-9]{1,4})?

            return ValidateRange(value, 0.0, 99999999999.9999, paramName);
        }

        public static decimal ValidateTDec_1104v(decimal value, string paramName)
        {
            // 0|0\.[0-9]{1,4}|[1-9]{1}[0-9]{0,10}|[1-9]{1}[0-9]{0,10}(\.[0-9]{1,4})?

            return ValidateRange(value, 0.0m, 99999999999.9999m, paramName);
        }

        public static double ValidateTDec_0804(double value, string paramName)
        {
            // "0|0\.[0-9]{4}|[1-9]{1}[0-9]{0,7}(\.[0-9]{4})?"
            return ValidateRange(value, 0.0, 99999999.9999, paramName);
        }

        public static double ValidateTDec_1302Opc(double value, string paramName)
        {
            // "0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?"
            return ValidateTDec_1302(value, paramName);
        }

        public static decimal ValidateTDec_1302Opc(decimal value, string paramName)
        {
            // "0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?"
            return ValidateTDec_1302(value, paramName);
        }

        public static decimal? ValidateTDec_1302Opc(decimal? value, string paramName)
        {
            if (value.HasValue)
                return ValidateTDec_1302(value.Value, paramName);
            return null;
        }

        public static double ValidateTDec_1302(double value, string paramName)
        {
            // "0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?
            return ValidateRange(value, 0.0, 9999999999999.99, paramName);
        }

        public static decimal ValidateTDec_1302(decimal value, string paramName)
        {
            // "0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?
            return ValidateRange(value, 0.0m, 9999999999999.99m, paramName);
        }

        public static decimal? ValidateTDec_1302(decimal? value, string paramName)
        {
            // "0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?
            return value.HasValue
                ? ValidateRange(value.Value, 0.0m, 9999999999999.99m, paramName)
                : (decimal?)null;
        }

        public static decimal ValidateTDec_0204v(decimal value, string paramName)
        {
            // 0|0\.[0-9]{1,4}|[1-9]{1}[0-9]{0,1}(\.[0-9]{1,4})?
            return ValidateRange(value, 0.0m, 99.9999m, paramName);
        }

        public static decimal? ValidateTDec_0204v(decimal? value, string paramName)
        {
            // 0|0\.[0-9]{1,4}|[1-9]{1}[0-9]{0,1}(\.[0-9]{1,4})?

            if (value.HasValue)
                return ValidateRange(value.Value, 0.0m, 99.9999m, paramName);
            return null;
        }

        public static double ValidateTDec_0302(double value, string paramName)
        {
            return ValidateRange(value, 0.0, 999.99, paramName);
        }

        public static decimal ValidateTDec_0302(decimal value, string paramName)
        {
            return ValidateRange(value, 0.0m, 999.99m, paramName);
        }

        public static decimal ValidateTDec_0302Max100(decimal value, string paramName)
        {
            return ValidateRange(value, 0.0m, 100.00m, paramName);
        }

        public static decimal? ValidateTDec_0302Opc(decimal? value, string paramName)
        {
            if (!value.HasValue)
                return null;
            return ValidateRange(value.Value, 0.0m, 999.99m, paramName);
        }

        public static decimal? ValidateTDec_0302a04Opc(decimal? value, string paramName)
        {
            if (!value.HasValue)
                return null;
            return ValidateRange(value.Value, 0.0m, 999.9999m, paramName);
        }

        public static double ValidateTDec_0504(double value, string paramName)
        {
            return ValidateRange(value, 0.0, 99999.9999, paramName);
        }

        public static decimal ValidateTDec_0504(decimal value, string paramName)
        {
            return ValidateRange(value, 0.0m, 99999.9999m, paramName);
        }

        public static double ValidateTDec_1203(double value, string paramName)
        {
            //0|0\.[0-9]{3}|[1-9]{1}[0-9]{0,11}(\.[0-9]{3})?
            return ValidateRange(value, 0.0, 999999999999.999, paramName);
        }

        public static decimal ValidateTDec_1203(decimal value, string paramName)
        {
            //0|0\.[0-9]{3}|[1-9]{1}[0-9]{0,11}(\.[0-9]{3})?
            return ValidateRange(value, 0m, 999999999999.999m, paramName);
        }

        public static double ValidateTDec_1104(double value, string paramName)
        {
            return ValidateRange(value, 0.0, 99999999999.9999, paramName);
        }

        public static decimal ValidateTDec_1104(decimal value, string paramName)
        {
            return ValidateRange(value, 0.0m, 99999999999.9999m, paramName);
        }

        public static decimal ValidateTDec_1204(decimal value, string paramName)
        {
            return ValidateRange(value, 0.0m, 999999999999.9999m, paramName);
        }

        public static decimal ValidateTDec_1110(decimal value, string paramName)
        {
            //0|0\.[0-9]{1,10}|[1-9]{1}[0-9]{0,10}|[1-9]{1}[0-9]{0,10}(\.[0-9]{1,10})?
            return ValidateRange(value, 0.0m, 99999999999.9999999999m, paramName);
        }

        public static string ValidateGTIN(string value, string paramName)
        {
            if (!Regex.IsMatch(value, @"[0-9]{0}|[0-9]{8}|[0-9]{12,14}"))
                throw new ErroValidacaoNFeException(ChaveErroValidacao.GTINInvalido);
            return value;
        }

        public static string ValidateNCM(string value, string paramName)
        {
            if (!Regex.IsMatch(value, @"[0-9]{2}|[0][1-9][0-9]{6}|[1-9][0-9]{7}"))
                throw new ErroValidacaoNFeException(ChaveErroValidacao.NCMInvalido);
            return value;
        }

        public static string ValidateNVE(string value, string paramName)
        {
            if (!Regex.IsMatch(value, @"[A-Z]{2}[0-9]{4}"))
                throw new ErroValidacaoNFeException(ChaveErroValidacao.NVEInvalido);
            return value;
        }

        public static string ValidateExTIPI(string value, string paramName)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (!Regex.IsMatch(value, @"[0-9]{2,3}"))
                    throw new ErroValidacaoNFeException(ChaveErroValidacao.CodigoExTipiInvalido);
            }
            return value;
        }

        public static string ValidateRange(string value, int min, int max, string paramName)
        {
            int valueLength = value.Length;
            if (value.Length < min || value.Length > max)
                throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.ValueOutOfRange, min, max));
            return value;
        }

        public static string TruncateString(string value, int tamMax)
        {
            if (value == null) return string.Empty;
            value = value.Trim();
            return value.Length > tamMax ? value.Substring(0, tamMax) : value;
        }

        public static string ValidateTString(string value, int minLength, int maxLength)
        {
            if (value == null && minLength > 0)
                throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.ValueOutOfRange, minLength, maxLength));

            if (value.Length < minLength)
                throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.ValueOutOfRange, minLength, maxLength));

            return value.ToTString(maxLength);
        }

        //public static void CheckReadOnly(ISomenteLeitura entity)
        //{
        //    if (entity.SomenteLeitura)
        //        throw new NFeReadOnlyException();
        //}

        public static int ValidateTSerie(int value, string paramName)
        {
            // 0|[1-9]{1}[0-9]{0,2}
            return ValidateRange(value, 0, 999, paramName);
        }

        public static int ValidateTNF(int value, string paramName)
        {
            //[1-9]{1}[0-9]{0,8}
            return ValidateRange(value, 1, 999999999, paramName);
        }

        public static DateTime ValidateTData(DateTime value, string paramName)
        {
            if (value == DateTime.MinValue)
                throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.GenericArgumentException, paramName));
            return value;
        }

        public static DateTime ValidateTDateTimeUtc(DateTime value, string paramName)
        {
            if (value == DateTime.MinValue)
                throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.GenericArgumentException, paramName));
            return value;
        }

        public static int ValidateTCodMunIBGE(int value, string paramName)
        {
            return ValidateRange(value, 1000000, 9999999, paramName);
        }

        public static string ValidateCNPJ(string value, string paramName)
        {
            return ValidateCNPJ(value, paramName, false);
        }

        private static string GetOnlyNumbers(string str)
        {
            return Regex.Replace(str, @"[^\d]", string.Empty);
        }

        public static string ValidateCNPJ(string value, string paramName, bool opcional)
        {
            // TODO: Implementar Verificação de Cnpj (módulo 11)
            if (!opcional && string.IsNullOrEmpty(value))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.CNPJInvalido), paramName);
            value = GetOnlyNumbers(value);
            if (!ValidateRegex(value, "^[0-9]{14}$"))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.CNPJInvalido), paramName);
            return value;
        }

        public static string ValidateTelefone(string value, string paramName)
        {
            if (!ValidateRegex(value, "^[0-9]{6,14}$"))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.TelefoneInvalido), paramName);
            return value;
        }

        public static string ValidateCPF(string value, string paramName)
        {
            return ValidateCPF(value, paramName, false);
        }

        public static string ValidateCPF(string value, string paramName, bool opcional)
        {
            // TODO: Implementar Verificação de Cpf (módulo 11)
            if (!opcional && string.IsNullOrEmpty(value))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.CPFInvalido), paramName);
            value = GetOnlyNumbers(value);
            if (!ValidateRegex(value, "[0-9]{11}"))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.CPFInvalido), paramName);
            return value;
        }

        public static string ValidateCNAE(string value, string paramName)
        {
            return ValidateCNAE(value, paramName, false);
        }

        public static string ValidateCNAE(string value, string paramName, bool opcional)
        {
            // TODO: Implementar Verificação de CNAE
            if (!opcional && string.IsNullOrEmpty(value))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.CNAEInvalido), paramName);
            value = GetOnlyNumbers(value);
            if (!ValidateRegex(value, "[0-9]{7}"))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.CNAEInvalido), paramName);
            return value;
        }

        public static string ValidateCEP(string value, string paramName)
        {
            return ValidateCEP(value, paramName, false);
        }

        public static string ValidateCEP(string value, string paramName, bool opcional)
        {
            if (!opcional && string.IsNullOrEmpty(value))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.CEPInvalido), paramName);
            value = GetOnlyNumbers(value);
            if (!ValidateRegex(value, "[0-9]{8}"))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.CEPInvalido), paramName);
            return value;
        }

        public static string ValidateTIeDest(string value, string paramName)
        {
            if (!Regex.IsMatch(value, "ISENTO|[0-9]{0,14})"))
                throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.InscricaoEstadualDestinatario, paramName));
            return value;
        }

        public static void ValidateIncricaoEstadual(string value, string paramName)
        {
            return;
            // TODO: Implementar Validação de Inscrição Estadual
        }

        public static void ValidateIncricaoEstadualDestino(string value, string paramName)
        {
            return;
            // TODO: Implementar Validação de Inscrição Estadual de Destino
        }

        public static void ValidateIncricaoSUFRAMA(string value, string paramName)
        {
            return;
            //TODO: Implementar Inscrição SUFRAMA
        }

        public static int ValidateTCfop(int value, string paramName)
        {
            //[123567][0-9]([0-9][1-9]|[1-9][0-9])
            if (!ValidateRegex(value, "[123567][0-9]([0-9][1-9]|[1-9][0-9])"))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.CFOPInvalido), paramName);
            return value;
        }

        public static string ValidatePlaca(string value, string paramName)
        {
            //[A-Z0-9]+
            if (!ValidateRegex(value, "[A-Z0-9]+"))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.PlacaInvalida), paramName);
            return value;
        }

        /// <summary>
        /// Códigos da Lista de Serviços LC 116/2003
        /// </summary>
        private static readonly string[] CodigosListaServicos = { "01.01", "01.02", "01.03", "01.04", "01.05", "01.06", "01.07", "01.08", "02.01", "03.02", "03.03", "03.04", "03.05", "04.01", "04.02", "04.03", "04.04", "04.05", "04.06", "04.07", "04.08", "04.09", "04.10", "04.11", "04.12", "04.13", "04.14", "04.15", "04.16", "04.17", "04.18", "04.19", "04.20", "04.21", "04.22", "04.23", "05.01", "05.02", "05.03", "05.04", "05.05", "05.06", "05.07", "05.08", "05.09", "06.01", "06.02", "06.03", "06.04", "06.05", "07.01", "07.02", "07.03", "07.04", "07.05", "07.06", "07.07", "07.08", "07.09", "07.10", "07.11", "07.12", "07.13", "07.16", "07.17", "07.18", "07.19", "07.20", "07.21", "07.22", "08.01", "08.02", "09.01", "09.02", "09.03", "10.01", "10.02", "10.03", "10.04", "10.05", "10.06", "10.07", "10.08", "10.09", "10.10", "11.01", "11.02", "11.03", "11.04", "12.01", "12.02", "12.03", "12.04", "12.05", "12.06", "12.07", "12.08", "12.09", "12.10", "12.11", "12.12", "12.13", "12.14", "12.15", "12.16", "12.17", "13.02", "13.03", "13.04", "13.05", "14.01", "14.02", "14.03", "14.04", "14.05", "14.06", "14.07", "14.08", "14.09", "14.10", "14.11", "14.12", "14.13", "15.01", "15.02", "15.03", "15.04", "15.05", "15.06", "15.07", "15.08", "15.09", "15.10", "15.11", "15.12", "15.13", "15.14", "15.15", "15.16", "15.17", "15.18", "16.01", "17.01", "17.02", "17.03", "17.04", "17.05", "17.06", "17.08", "17.09", "17.10", "17.11", "17.12", "17.13", "17.14", "17.15", "17.16", "17.17", "17.18", "17.19", "17.20", "17.21", "17.22", "17.23", "17.24", "18.01", "19.01", "20.01", "20.02", "20.03", "21.01", "22.01", "23.01", "24.01", "25.01", "25.02", "25.03", "25.04", "26.01", "27.01", "28.01", "29.01", "30.01", "31.01", "32.01", "33.01", "34.01", "35.01", "36.01", "37.01", "38.01", "39.01", "40.01" };

        public static string ValidateTCListServ(string value, string paramName)
        {
            // Códigos da Lista de Serviços LC 116/2003

            if (!CodigosListaServicos.Contains(value, StringComparer.OrdinalIgnoreCase))
                throw new ApplicationException("O valor informado não é um código de serviço válido segundo a LC 116/2003.");
            return value;
        }

        private static readonly int[] CodigosTpais = { 132, 175, 230, 310, 370, 400, 418, 434, 477, 531, 590, 639, 647, 655, 698, 728, 736, 779, 809, 817, 833, 850, 876, 884, 906, 930, 973, 981, 1015, 1058, 1082, 1112, 1155, 1198, 1279, 1376, 1414, 1457, 1490, 1504, 1508, 1511, 1538, 1546, 1589, 1600, 1619, 1635, 1651, 1694, 1732, 1775, 1830, 1872, 1902, 1937, 1953, 1961, 1988, 1996, 2291, 2321, 2356, 2399, 2402, 2437, 2445, 2453, 2461, 2470, 2496, 2518, 2534, 2550, 2593, 2674, 2712, 2755, 2810, 2852, 2895, 2917, 2933, 2976, 3018, 3050, 3093, 3131, 3174, 3255, 3298, 3310, 3344, 3379, 3417, 3450, 3514, 3557, 3573, 3595, 3611, 3654, 3697, 3727, 3751, 3794, 3832, 3867, 3913, 3964, 3999, 4030, 4111, 4200, 4235, 4260, 4278, 4316, 4340, 4383, 4405, 4421, 4456, 4472, 4499, 4502, 4525, 4553, 4588, 4618, 4642, 4677, 4723, 4740, 4766, 4774, 4855, 4880, 4885, 4901, 4936, 4944, 4952, 4979, 4985, 4995, 5010, 5053, 5070, 5088, 5118, 5177, 5215, 5258, 5282, 5312, 5355, 5380, 5428, 5452, 5487, 5517, 5568, 5665, 5738, 5754, 5762, 5800, 5860, 5894, 5932, 5991, 6033, 6076, 6114, 6238, 6254, 6289, 6408, 6475, 6602, 6653, 6700, 6750, 6769, 6777, 6781, 6858, 6874, 6904, 6912, 6955, 6971, 7005, 7056, 7102, 7153, 7200, 7285, 7315, 7358, 7370, 7412, 7447, 7480, 7501, 7544, 7560, 7595, 7641, 7676, 7706, 7722, 7765, 7803, 7820, 7838, 7889, 7919, 7951, 8001, 8052, 8109, 8150, 8206, 8230, 8249, 8273, 8281, 8311, 8338, 8451, 8478, 8486, 8508, 8583, 8630, 8664, 8702, 8737, 8885, 8907, 8958, 9903, 9946, 9950, 9970 };

        public static int ValidateTpais(int value, string paramName)
        {
            if (!CodigosTpais.Contains(value))
                throw new ApplicationException("O valor informado não é um Código de País válido.");
            return value;
        }

        public static double ValidateRange(double value, double min, double max, string paramName)
        {
            if (value < min || value > max)
                throw new ArgumentOutOfRangeException(paramName, value, MsgUtil.GetString(ChaveErroValidacao.ValueOutOfRange, min, max));
            return value;
        }

        public static decimal ValidateRange(decimal value, decimal min, decimal max, string paramName)
        {
            if (value < min || value > max)
                throw new ArgumentOutOfRangeException(paramName, value, MsgUtil.GetString(ChaveErroValidacao.ValueOutOfRange, min, max));
            return value;
        }

        public static int ValidateRange(int value, int min, int max, string paramName)
        {
            if (value < min || value > max)
                throw new ArgumentOutOfRangeException(paramName, value, MsgUtil.GetString(ChaveErroValidacao.ValueOutOfRange, min, max));
            return value;
        }

        public static long ValidateRange(long value, long min, long max, string paramName)
        {
            if (value < min || value > max)
                throw new ArgumentOutOfRangeException(paramName, value, MsgUtil.GetString(ChaveErroValidacao.ValueOutOfRange, min, max));
            return value;
        }
    }
}