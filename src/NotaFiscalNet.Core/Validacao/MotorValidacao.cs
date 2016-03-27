using FluentValidation;
using System;
using System.Linq;
using System.Reflection;

namespace NotaFiscalNet.Core.Validacao
{
    public class MotorValidacao
    {
        public MotorValidacao()
        {
            ValidatorOptions.DisplayNameResolver = (type, member, expr) => member.Name;
            ValidatorOptions.ResourceProviderType = typeof(Mensagens);
        }

        public ResultadoValidacao Validar(object validavel)
        {
            var root = validavel.GetType().Name;
            var validador = ObtemValidador(validavel);

            if (validador == null)
                return new ResultadoValidacao(new [] { new ErroValidacao(root, "Não foi implementado validador para " + root) });

            var resultado = validador.Validate(validavel);
            var erros = from erro in resultado.Errors
                        let caminhoPropriedade = $"{root}.{erro.PropertyName}"
                        let propriedade = erro.FormattedMessagePlaceholderValues["PropertyName"].ToString()
                        let mensagemErro = erro.ErrorMessage.Replace(propriedade, caminhoPropriedade)
                        let valorInformado = erro.AttemptedValue
                        select new ErroValidacao(caminhoPropriedade, mensagemErro, valorInformado);

            return new ResultadoValidacao(erros);
        }

        private IValidator ObtemValidador(object validavel)
        {
            var tipoValidavel = validavel.GetType();

            var validador = 
                (from resultado in AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly())
                let argumentos = resultado.InterfaceType.GetGenericArguments()
                from argumento in argumentos
                where argumento == tipoValidavel
                select resultado.ValidatorType).FirstOrDefault();
            
            var constructorInfo = validador?.GetConstructor(Type.EmptyTypes);
            return (IValidator) constructorInfo?.Invoke(new object[] { });
        }
    }
}