using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Validacao;
using System;
using System.Text.RegularExpressions;

namespace NotaFiscalNet.Core.Utils
{
    /// <summary>
    /// Classe de Suporte para Validação
    /// </summary>
    public static class ValidationExtensions
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

        public static bool ValidateRegex(this string value, string pattern)
        {
            return Regex.IsMatch(value, pattern);
        }

        public static bool ValidateRegex(this int value, string pattern)
        {
            return Regex.IsMatch(value.ToString(), pattern);
        }

        public static TEnum ValidateEnum<TEnum>(this TEnum value, string paramName)
        {
            if (typeof(TEnum).BaseType != typeof(Enum))
                throw new ArgumentException("TEnum deve ser um tipo enumerador.");

            if (!Enum.IsDefined(typeof(TEnum), value))
                throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.GenericArgumentException, paramName));
            return value;
        }

        public static TEnum ValidateEnumOptional<TEnum>(this TEnum value, TEnum notSetValue, string paramName)
        {
            if (typeof(TEnum).BaseType != typeof(Enum))
                throw new ArgumentException("TEnum deve ser um tipo enumerador.");

            if (!Enum.IsDefined(typeof(TEnum), value))
                throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.GenericArgumentException, paramName));

            if (Equals(value, notSetValue))
                throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.EnumNaoEspecificado, paramName));
            return value;
        }

        public static int ValidateInt9(this int value, string paramName)
        {
            return ValidateRange(value, 0, 999999999, paramName);
        }

        public static int ValidateInt7(this int value, string paramName)
        {
            return ValidateRange(value, 0, 9999999, paramName);
        }

        public static long ValidateLong15(this long value, string paramName)
        {
            return ValidateRange(value, 0L, 999999999999999L, paramName);
        }

        public static double ValidateTDec_0803(this double value, string paramName)
        {
            return ValidateRange(value, 0.0, 99999999.999, paramName);
        }

        public static double ValidateTDec_1504(this double value, string paramName)
        {
            // "0|0\.[0-9]{1,4}|[1-9]{1}[0-9]{0,14}|[1-9]{1}[0-9]{0,14}(\.[0-9]{1,4})?"

            return ValidateRange(value, 0.0, 999999999999999.9999, paramName);
        }

        public static double ValidateTDec_0804(this double value, string paramName)
        {
            // "0|0\.[0-9]{4}|[1-9]{1}[0-9]{0,7}(\.[0-9]{4})?"
            return ValidateRange(value, 0.0, 99999999.9999, paramName);
        }

        public static double ValidateTDec_1302Opc(this double value, string paramName)
        {
            // "0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?"
            return ValidateTDec_1302(value, paramName);
        }

        public static double ValidateTDec_1302(this double value, string paramName)
        {
            // "0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?
            return ValidateRange(value, 0.0, 9999999999999.99, paramName);
        }

        public static decimal ValidateTDec_1302(this decimal value, string paramName)
        {
            // "0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?
            return ValidateRange(value, 0.0m, 9999999999999.99m, paramName);
        }

        public static double ValidateTDec_0302(this double value, string paramName)
        {
            return ValidateRange(value, 0.0, 999.99, paramName);
        }

        public static double ValidateTDec_0504(this double value, string paramName)
        {
            return ValidateRange(value, 0.0, 99999.9999, paramName);
        }

        public static double ValidateTDec_1203(this double value, string paramName)
        {
            //0|0\.[0-9]{3}|[1-9]{1}[0-9]{0,11}(\.[0-9]{3})?
            return ValidateRange(value, 0.0, 999999999999.999, paramName);
        }

        public static double ValidateTDec_1104(this double value, string paramName)
        {
            return ValidateRange(value, 0.0, 99999999999.9999, paramName);
        }

        public static decimal ValidateTDec_1204(this decimal value, string paramName)
        {
            return ValidateRange(value, 0.0m, 999999999999.9999m, paramName);
        }

        public static decimal ValidateTDec_1110(this decimal value, string paramName)
        {
            //0|0\.[0-9]{1,10}|[1-9]{1}[0-9]{0,10}|[1-9]{1}[0-9]{0,10}(\.[0-9]{1,10})?
            return ValidateRange(value, 0.0m, 99999999999.9999999999m, paramName);
        }

        public static string ValidateGTIN(this string value, string paramName)
        {
            if (!Regex.IsMatch(value, @"[0-9]{0}|[0-9]{8}|[0-9]{12,14}"))
                throw new ErroValidacaoNFeException(ChaveErroValidacao.GTINInvalido);
            return value;
        }

        public static string ValidateNCM(this string value, string paramName)
        {
            if (!Regex.IsMatch(value, @"[0-9]{2}|[0][1-9][0-9]{6}|[1-9][0-9]{7}"))
                throw new ErroValidacaoNFeException(ChaveErroValidacao.NCMInvalido);
            return value;
        }

        public static string ValidateExTIPI(this string value, string paramName)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (!Regex.IsMatch(value, @"[0-9]{2,3}"))
                    throw new ErroValidacaoNFeException(ChaveErroValidacao.CodigoExTipiInvalido);
            }
            return value;
        }

        public static string ValidateRange(this string value, int min, int max, string paramName)
        {
            int valueLength = value.Length;
            if (value.Length < min || value.Length > max)
                throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.ValueOutOfRange, min, max));
            return value;
        }

        public static string TruncateString(this string value, int tamMax)
        {
            if (value == null) return string.Empty;
            value = value.Trim();
            return value.Length > tamMax ? value.Substring(0, tamMax) : value;
        }

        public static string ValidateTString(this string value, int minLength, int maxLength)
        {
            if (value == null && minLength > 0)
                throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.ValueOutOfRange, minLength, maxLength));

            if (value.Length < minLength)
                throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.ValueOutOfRange, minLength, maxLength));

            return value.ToTString(maxLength);
        }

        public static void CheckReadOnly(this ISomenteLeitura entity)
        {
            if (entity.SomenteLeitura)
                throw new NFeReadOnlyException();
        }

        public static int ValidateTSerie(this int value, string paramName)
        {
            // 0|[1-9]{1}[0-9]{0,2}
            return ValidateRange(value, 0, 999, paramName);
        }

        public static int ValidateTNF(this int value, string paramName)
        {
            //[1-9]{1}[0-9]{0,8}
            return ValidateRange(value, 1, 999999999, paramName);
        }

        public static DateTime ValidateTData(this DateTime value, string paramName)
        {
            if (value == DateTime.MinValue)
                throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.GenericArgumentException, paramName));
            return value;
        }

        public static DateTime ValidateTDateTimeUtc(this DateTime value, string paramName)
        {
            if (value == DateTime.MinValue)
                throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.GenericArgumentException, paramName));
            return value;
        }

        public static int ValidateTCodMunIBGE(this int value, string paramName)
        {
            //throw new NotImplementedException();
            // TODO: Fazer
            return value;
        }

        public static string ValidateCNPJ(this string value, string paramName)
        {
            return ValidateCNPJ(value, paramName, false);
        }

        private static string GetOnlyNumbers(this string str)
        {
            return Regex.Replace(str, @"[^\d]", string.Empty);
        }

        public static string ValidateCNPJ(this string value, string paramName, bool opcional)
        {
            // TODO: Implementar Verificação de CNPJ (módulo 11)
            if (!opcional && string.IsNullOrEmpty(value))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.CNPJInvalido), paramName);
            value = GetOnlyNumbers(value);
            if (!ValidateRegex(value, "[0-9]{14}"))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.CNPJInvalido), paramName);
            return value;
        }

        public static string ValidateTelefone(this string value, string paramName)
        {
            if (!ValidateRegex(value, "[0-9]{1,10}"))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.TelefoneInvalido), paramName);
            return value;
        }

        public static string ValidateCPF(this string value, string paramName)
        {
            return ValidateCPF(value, paramName, false);
        }

        public static string ValidateCPF(this string value, string paramName, bool opcional)
        {
            // TODO: Implementar Verificação de CPF (módulo 11)
            if (!opcional && string.IsNullOrEmpty(value))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.CPFInvalido), paramName);
            value = GetOnlyNumbers(value);
            if (!ValidateRegex(value, "[0-9]{11}"))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.CPFInvalido), paramName);
            return value;
        }

        public static string ValidateCNAE(this string value, string paramName)
        {
            return ValidateCNAE(value, paramName, false);
        }

        public static string ValidateCNAE(this string value, string paramName, bool opcional)
        {
            // TODO: Implementar Verificação de CNAE
            if (!opcional && string.IsNullOrEmpty(value))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.CNAEInvalido), paramName);
            value = GetOnlyNumbers(value);
            if (!ValidateRegex(value, "[0-9]{7}"))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.CNAEInvalido), paramName);
            return value;
        }

        public static string ValidateCEP(this string value, string paramName)
        {
            return ValidateCEP(value, paramName, false);
        }

        public static string ValidateCEP(this string value, string paramName, bool opcional)
        {
            if (!opcional && string.IsNullOrEmpty(value))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.CEPInvalido), paramName);
            value = GetOnlyNumbers(value);
            if (!ValidateRegex(value, "[0-9]{8}"))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.CEPInvalido), paramName);
            return value;
        }

        public static void ValidateIncricaoEstadual(this string value, string paramName)
        {
            return;
            // TODO: Implementar Validação de Inscrição Estadual
        }

        public static void ValidateIncricaoEstadualDestino(this string value, string paramName)
        {
            return;
            // TODO: Implementar Validação de Inscrição Estadual de Destino
        }

        public static void ValidateIncricaoSUFRAMA(this string value, string paramName)
        {
            return;
            //TODO: Implementar Inscrição SUFRAMA
        }

        public static int ValidateTCfop(this int value, string paramName)
        {
            //[123567][0-9]([0-9][1-9]|[1-9][0-9])
            if (!ValidateRegex(value, "[123567][0-9]([0-9][1-9]|[1-9][0-9])"))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.CFOPInvalido), paramName);
            return value;
        }

        public static string ValidatePlaca(this string value, string paramName)
        {
            //[A-Z0-9]+
            if (!ValidateRegex(value, "[A-Z0-9]+"))
                throw new ArgumentException(MsgUtil.GetString(ChaveErroValidacao.PlacaInvalida), paramName);
            return value;
        }

        public static int ValidateTCListServ(this int value, string paramName)
        {
            //TODO: Implementar Lista de Servicos
            return ValidateRange(value, 101, 4001, paramName);
        }

        public static double ValidateRange(this double value, double min, double max, string paramName)
        {
            if (value < min || value > max)
                throw new ArgumentOutOfRangeException(paramName, value, MsgUtil.GetString(ChaveErroValidacao.ValueOutOfRange, min, max));
            return value;
        }

        public static decimal ValidateRange(this decimal value, decimal min, decimal max, string paramName)
        {
            if (value < min || value > max)
                throw new ArgumentOutOfRangeException(paramName, value, MsgUtil.GetString(ChaveErroValidacao.ValueOutOfRange, min, max));
            return value;
        }

        public static int ValidateRange(this int value, int min, int max, string paramName)
        {
            if (value < min || value > max)
                throw new ArgumentOutOfRangeException(paramName, value, MsgUtil.GetString(ChaveErroValidacao.ValueOutOfRange, min, max));
            return value;
        }

        public static long ValidateRange(this long value, long min, long max, string paramName)
        {
            if (value < min || value > max)
                throw new ArgumentOutOfRangeException(paramName, value, MsgUtil.GetString(ChaveErroValidacao.ValueOutOfRange, min, max));
            return value;
        }
    }
}