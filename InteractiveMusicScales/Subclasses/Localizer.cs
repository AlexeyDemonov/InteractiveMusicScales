using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveMusicScales
{
    class Localizer
    {
        Dictionary<string,string> localization;

        public Localizer(Dictionary<string,string> localization)
        {
            this.localization = localization;
        }

        public string this[string key]
        {
            get
            {
                if(this.localization != null && this.localization.TryGetValue(key, out string localization) == true)
                    return localization;
                else
                    return key;
            }
        }
    }
}
