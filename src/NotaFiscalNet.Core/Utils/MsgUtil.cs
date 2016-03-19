using NotaFiscalNet.Core.Validacao;
using System;
using System.ComponentModel;

namespace NotaFiscalNet.Core.Utils
{
    /// <summary>
    /// Utilitário de gerenciamento de mensagens armazenadas em arquivo de recurso.
    /// </summary>
    internal static class MsgUtil
    {
        //private static ResourceManager _resource;

        ///// <summary>
        ///// Retorna a referência para o arquivo de recurso contendo a lista de mensagens.
        ///// </summary>
        //private static ResourceManager Resource
        //{
        //    get
        //    {
        //        if (_resource == null)
        //            _resource = new ResourceManager("Mensagens", Assembly.GetExecutingAssembly());
        //        return _resource;
        //    }
        //}

        /// <summary>
        /// Retorna o texto de uma determinada mensagem de validação.
        /// </summary>
        /// <param name="chave">Chave identificadora da mensagem.</param>
        /// <param name="args">Argumentos de formatação da mensagem.</param>
        /// <returns></returns>
        public static string GetString(ChaveErroValidacao chave, params object[] args)
        {
            return string.Format(GetString(chave), args);
        }

        /// <summary>
        /// Retorna o texto de uma determinada mensagem de validação.
        /// </summary>
        /// <param name="chave">Chave identificadora da mensagem.</param>
        /// <returns></returns>
        public static string GetString(ChaveErroValidacao chave)
        {
            string text = ((DescriptionAttribute)chave.GetType().GetField(chave.ToString()).GetCustomAttributes(true)[0]).Description;
            //if ( Regex.IsMatch(text, @"^EV\d{4}|") ) // se começa com 'EV0000|'
            //{
            //    int indexOfPipe = text.IndexOf('|');
            //    return text.Substring(indexOfPipe + 1);
            //}

            return text ?? string.Empty;
        }

        /// <summary>
        /// Retorna o texto de uma determinada mensagem com base na chave de identificação.
        /// </summary>
        /// <param name="chave">Chave identificadora da mensagem.</param>
        /// <param name="args">Argumentos de formatação da mensagem.</param>
        /// <returns></returns>
        public static string GetString(string chave, params object[] args)
        {
            return string.Format(GetString(chave), args);
        }

        /// <summary>
        /// Retorna o texto de uma determinada mensagem com base na chave de identificação.
        /// </summary>
        /// <param name="chave">Chave identificadora da mensagem.</param>
        /// <returns></returns>
        public static string GetString(string chave)
        {
            throw new NotImplementedException();
        }
    }
}