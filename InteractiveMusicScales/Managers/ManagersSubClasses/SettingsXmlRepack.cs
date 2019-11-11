using System.Xml.Serialization;

namespace InteractiveMusicScales.Managers
{
    [XmlRoot("Settings")]
    public class SettingsXmlRepack
    {
        [XmlElement]
        public int PianorollSemitone { get; set; }

        [XmlElement]
        public int FretboardSemitone { get; set; }

        [XmlElement]
        public int CircleSemitone { get; set; }

        [XmlArray("StringRootNotes")]
        [XmlArrayItem("StringRootNote")]
        public int[] FretboardStrings { get; set; }

        [XmlElement]
        public int LastVisibleString { get; set; }
    }
}