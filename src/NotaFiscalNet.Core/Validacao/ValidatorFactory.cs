using NotaFiscalNet.Core.Validacao.Validators;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace NotaFiscalNet.Core.Validacao
{
    internal static class ValidatorFactory
    {
        public static readonly DefaultValidator DefaultValidator = new DefaultValidator();

        private static Dictionary<string, BaseValidator> ReusableRules;

        static ValidatorFactory()
        {
            ReusableRules = new Dictionary<string, BaseValidator>();
        }

        public static BaseValidator Create(Type ruleType)
        {
            if (ReusableRules.ContainsKey(ruleType.FullName))
                return ReusableRules[ruleType.FullName];

            BaseValidator rule = (BaseValidator)Assembly.GetAssembly(ruleType).CreateInstance(ruleType.FullName);
            if (rule.IsReusable)
                ReusableRules.Add(ruleType.FullName, rule);
            return rule;
        }
    }
}