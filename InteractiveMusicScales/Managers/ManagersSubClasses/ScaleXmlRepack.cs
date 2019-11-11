using System.Xml.Serialization;

namespace InteractiveMusicScales.Managers
{
    public class ScaleXmlRepack
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public int Keynote { get; set; }

        [XmlAttribute]
        public int Sound { get; set; }
    }
}