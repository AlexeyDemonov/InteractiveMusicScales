using System.Xml.Serialization;

namespace InteractiveMusicScales.Managers
{
    [XmlRoot("ScalesContainer")]
    public class ScalesXmlContainer
    {
        [XmlArray("Scales")]
        [XmlArrayItem("Scale")]
        public ScaleXmlRepack[] Scales { get; set; }
    }
}