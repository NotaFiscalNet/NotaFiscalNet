using System.Collections.Generic;

namespace NotaFiscalNet.Core.Validacao
{
    internal sealed class ValidationContext : IEnumerable<KeyValuePair<string, ErroValidacao>>
    {
        internal ValidationContext()
        {
        }

        private CodePath _path = new CodePath();
        private Dictionary<string, ErroValidacao> _errors = new Dictionary<string, ErroValidacao>();

        public CodePath Path => _path;

        public Dictionary<string, ErroValidacao> Errors => _errors;

        public void Add(ErroValidacao error)
        {
            Errors.Add(_path.ToString(), error);
        }

        public IEnumerator<KeyValuePair<string, ErroValidacao>> GetEnumerator()
        {
            return Errors.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Errors.GetEnumerator();
        }

        internal void ValidateDefaultValue<T>(T value, T defaultValue, string path, ChaveErroValidacao key)
        {
            if (Equals(value, defaultValue))
            {
                ErroValidacao erro = ErroValidacao.Create(key);
                Path.Append(path);
                Add(erro);
                Path.Remove();
            }
        }

        internal void ValidateDefaultValue<T>(T value, string path, ChaveErroValidacao key)
        {
            ValidateDefaultValue<T>(value, default(T), path, key);
        }

        internal void ValidateField<TObject, TValue>(TObject obj, TValue value, string fieldName, ChaveErroValidacao errorKey, ValidationCondiction<TObject, TValue> condiction)
        {
            if (condiction(obj, value, fieldName) == false)
            {
                Path.Append(fieldName);
                Add(ErroValidacao.Create(errorKey));
                Path.Remove();
            }
        }
    }
}