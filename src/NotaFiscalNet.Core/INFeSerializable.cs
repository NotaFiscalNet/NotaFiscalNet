using System.Xml;

namespace NotaFiscalNet.Core
{
    public interface INFeSerializable
    {
        void Serialize(XmlWriter writer, NFe nfe);
        //void Deserialize(XmlReader reader);
    }
}
