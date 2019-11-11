using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteractiveMusicScales.Managers;

namespace InteractiveMusicScales
{
    class ScalesManager : AbstractLoadSaveRequester
    {
        const string scalesFileName = "AdditionalScales.xml";


        public Scale[] Handle_LoadAdditionalScalesRequest()
        {
            var container = base.InvokeLoadRequest(scalesFileName, typeof(ScalesXmlContainer));

            if(container != null && container is ScalesXmlContainer)
            {
                var loadedScalesContainer = (ScalesXmlContainer)container;

                var packedScales = loadedScalesContainer.Scales;
                int length = packedScales.Length;
                Scale[] unpackedScales = new Scale[length];

                for (int i = 0; i < length; i++)
                {
                    var packedScale = packedScales[i];
                    unpackedScales[i] = new Scale(packedScale.Name, (Sound)packedScale.Keynote, (Sound)packedScale.Sound);
                }

                return unpackedScales;
            }
            else
                return null;
        }

        public void Handle_SaveAdditionalScalesRequest(Scale[] scales)
        {
            _ = SaveAdditionalScalesAsync(scales);
        }

        public Task Handle_SaveAdditionalScalesRequestAwaitable(Scale[] scales)
        {
            return SaveAdditionalScalesAsync(scales);
        }

        async Task SaveAdditionalScalesAsync(Scale[] scales)
        {
            await Task.Run( ()=>SaveAdditionalScales(scales) );
        }

        void SaveAdditionalScales(Scale[] scales)
        {
            ScaleXmlRepack[] packedScales = new ScaleXmlRepack[scales.Length];

            for (int i = 0; i < scales.Length; i++)
            {
                var scale = scales[i];
                packedScales[i] = new ScaleXmlRepack() { Name = scale.Name, Sound = (int)scale.Sound, Keynote = (int)scale.KeynoteSound };
            }

            var container = new ScalesXmlContainer() { Scales = packedScales };

            base.InvokeSaveRequest(scalesFileName, container);
        }
    }
}
