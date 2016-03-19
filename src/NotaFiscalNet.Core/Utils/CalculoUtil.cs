using System;

namespace NotaFiscalNet.Core.Utils
{
    internal static class CalculoUtil
    {
        /// <summary>
        /// Realiza o cálculo de Módulo 11.
        /// </summary>
        /// <param name="valor">valor a ser calculado em módulo 11.</param>
        /// <returns>Dígito verificador.</returns>
        public static int Modulo11(string valor)
        {
            /// Realiza o preenchimento dos multiplicadores
            int multiplicador = 2; // valor inicial

            int ponderacao = 0;

            for (int i = valor.Length - 1; i >= 0; i--)
            {
                ponderacao += Convert.ToInt32(valor[i].ToString()) * multiplicador++; // valor da posição * multiplicador
                if (multiplicador > 9)
                    multiplicador = 2;
            }

            int resto = (ponderacao % 11);

            int dv = 0;
            if (resto > 1)
                dv = 11 - resto;

            return dv;
        }
    }
}