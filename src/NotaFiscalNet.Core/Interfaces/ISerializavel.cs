using System.Xml;

namespace NotaFiscalNet.Core.Interfaces
{
    public interface ISerializavel
    {
        void Serializar(XmlWriter writer, INFe nfe);

        //void Deserialize(XmlReader reader);
    }
}