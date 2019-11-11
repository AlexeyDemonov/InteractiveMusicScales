using System.Xml.Serialization;

namespace InteractiveMusicScales.Managers
{
    [XmlRoot("Localization")]
    public class LocalizationXmlRepack
    {
        [XmlArray("Entries")]
        [XmlArrayItem("Entry")]
        public LocalizationXmlEntry[] Entries { get; set; }
    }
}