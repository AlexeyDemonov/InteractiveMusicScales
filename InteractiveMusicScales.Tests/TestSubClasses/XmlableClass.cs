using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
