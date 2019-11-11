using System.Xml.Serialization;

namespace InteractiveMusicScales.Tests
{
    [XmlRoot("Sample")]
    public class XmlableClass
    {
        [XmlElement]
        public string StringValue { get; set; }

        [XmlElement]
        public int IntValue { get; set; }
    }
}