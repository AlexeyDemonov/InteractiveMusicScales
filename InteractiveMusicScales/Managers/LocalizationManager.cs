using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteractiveMusicScales.Managers;

namespace InteractiveMusicScales
{
    class LocalizationManager : AbstractLoadSaveRequester
    {
        [Obsolete(message:"This functionality is not implemented at this class", error:true)]
        public new event Action<string, object> Request_Save { add {/*Do nothing*/} remove {/*Do nothing*/} }

        const string folderName = "Localization";
        readonly string defaultCultureName;

        public LocalizationManager()
        {
            defaultCultureName = null;
        }

        /// <summary>
        /// Provide default culture in 'en-US'/'ru-RU' format
        /// </summary>
        /// <param name="defaultCultureName"></param>
        public LocalizationManager(string defaultCultureName)
        {
            this.defaultCultureName = defaultCultureName;
        }


        /// <summary>
        /// Returns localization based on current culture
        /// <para>Returns null if default culture was provided at class constructor and it is equals to current one</para>
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,string> Handle_LoadLocalizationRequest()
        {
            string currentCultureName = CultureInfo.CurrentCulture.Name;//Expecting it to be en-US or ru-RU etc.

            //In case we do not need localization
            if(defaultCultureName != null && defaultCultureName == currentCultureName)
                return null;

            string fileName = $"{folderName}\\{currentCultureName}.xml";

            var container = base.InvokeLoadRequest(fileName, typeof(LocalizationXmlRepack));

            if(container != null && container is LocalizationXmlRepack)
            {
                var packedDictionary = (LocalizationXmlRepack)container;

                var unpackedDictionary = new Dictionary<string,string>();

                foreach (var pair in packedDictionary.Entries)
                {
                    unpackedDictionary[pair.Key] = pair.Value;
                }

                return unpackedDictionary;
            }
            else
                return null;
        }
    }
}
