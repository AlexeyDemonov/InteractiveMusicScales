using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
