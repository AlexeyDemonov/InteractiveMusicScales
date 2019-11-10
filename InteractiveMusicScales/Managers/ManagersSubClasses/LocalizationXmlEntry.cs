using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace InteractiveMusicScales.Managers
{
    public class LocalizationXmlEntry
    {
        [XmlElement]
        public string Key { get; set; }

        [XmlElement]
        public string Value { get; set; }
    }
}
