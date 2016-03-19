using NotaFiscalNet.Core.Interfaces;
using System.Text;
using System.Xml;

namespace NotaFiscalNet.Core.Tests.Comum
{
    public class Serializador
    {
        private readonly ISerializavel _serializavel;
        private readonly INFe _nfe;

        public Serializador(ISerializavel serializavel, INFe nfe)
        {
            _serializavel = serializavel;
            _nfe = nfe;
        }

        public string Serializar()
        {
            var resultado = new StringBuilder();
            using (var writer = XmlWriter.Create(resultado, new XmlWriterSettings() { OmitXmlDeclaration = true }))
            {
                _serializavel.Serializar(writer, _nfe);
                writer.Flush();
            }
            return resultado.ToString();
        }
    }
}