using System.Collections.Generic;
using System.Linq;

namespace NotaFiscalNet.Core.Tests.Dados
{
    public abstract class Repositorio<T> where T : class
    {
        public Dictionary<string, T> Referencias { get; } = new Dictionary<string, T>();

        protected Repositorio()
        {
            CriarElementos()
                .Select((elemento, indice) => new 
                {
                    Indice = indice + 1,
                    Elemento = elemento
                })
                .ToList()
                .ForEach((elemento) =>
                {
                    Referencias.Add(elemento.Indice.ToString(), elemento.Elemento);
                });
        }

        public abstract List<T> CriarElementos();
    }
}