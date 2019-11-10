using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace InteractiveMusicScales.Managers
{
    [XmlRoot("ScalesContainer")]
    public class ScalesXmlContainer
    {
        [XmlArray("Scales")]
        [XmlArrayItem("Scale")]
        public ScaleXmlRepack[] Scales {get; set;}
    }
}
