using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
