using System.Collections.Generic;

namespace InteractiveMusicScales
{
    internal class Localizer
    {
        private Dictionary<string, string> localization;

        public Localizer(Dictionary<string, string> localization)
        {
            this.localization = localization;
        }

        public string this[string key]
        {
            get
            {
                if (this.localization != null && this.localization.TryGetValue(key, out string localization) == true)
                    return localization;
                else
                    return key;
            }
        }
    }
}