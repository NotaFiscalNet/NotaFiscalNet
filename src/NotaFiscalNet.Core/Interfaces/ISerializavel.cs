using System.Xml;

namespace NotaFiscalNet.Core.Interfaces
{
    public interface ISerializavel
    {
        void Serializar(XmlWriter writer, NFe nfe);
        //void Deserialize(XmlReader reader);
    }
}