using System.Collections.Generic;
using System.Text;

namespace NotaFiscalNet.Core.Validacao
{
    internal sealed class CodePath
    {
        internal CodePath()
        {
        }

        private Stack<string> _pilha = new Stack<string>();

        public string Remove()
        {
            if (_pilha.Count > 0)
                return _pilha.Pop();
            else
                return null;
        }

        public void Append(string value)
        {
            _pilha.Push(value);
        }

        public void Append(string format, params object[] args)
        {
            _pilha.Push(string.Format(format, args));
        }

        public override string ToString()
        {
            string[] array = _pilha.ToArray();

            StringBuilder ret = new StringBuilder();

            for (int i = array.Length - 1; i >= 0; i--)
            {
                ret.Append(array[i]);
                ret.Append(".");
            }
            ret.Length--;
            return ret.ToString().Replace(".[", "["); // nos casos onde há listas (indexador), troca o '.[0].' por '[0].'.
        }
    }
}