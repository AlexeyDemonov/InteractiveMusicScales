using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteractiveMusicScales.Managers;

namespace InteractiveMusicScales
{
    class SettingsManager : AbstractLoadSaveRequester
    {
        const string settingsFileName = "Settings.xml";

        //==============================================================
        //Handlers
        public SettingsRequestEventArgs Handle_LoadSettingsRequest()
        {
            var container = base.InvokeLoadRequest(settingsFileName, typeof(SettingsXmlRepack));

            if(container != null && container is SettingsXmlRepack)
            {
                var loadedSettings = (SettingsXmlRepack)container;

                int length = loadedSettings.FretboardStrings.Length;
                var unpackedNotes = new Note[length];
                for (int i = 0; i < length; i++)
                {
                    unpackedNotes[i] = new Note((Sound)loadedSettings.FretboardStrings[i]);
                }

                return new SettingsRequestEventArgs
                (
                    pianorollSemitone: (Semitone)loadedSettings.PianorollSemitone,
                    fretboardSemitone: (Semitone)loadedSettings.FretboardSemitone,
                    circleSemitone: (Semitone)loadedSettings.CircleSemitone,
                    fretboardStrings: unpackedNotes,
                    lastVisibleString: loadedSettings.LastVisibleString
                );
            }
            else
                return null;
        }

        public void Handle_SaveSettingsRequest(SettingsRequestEventArgs args)
        {
            int length = args.FretboardStrings.Length;
            var packedNotes = new int[length];
            for (int i = 0; i < length; i++)
            {
                packedNotes[i] = (int)args.FretboardStrings[i].Sound;
            }

            var container = new SettingsXmlRepack()
            {
                PianorollSemitone = (int)args.PianorollSemitone,
                FretboardSemitone = (int)args.FretboardSemitone,
                CircleSemitone = (int)args.CircleSemitone,
                FretboardStrings = packedNotes,
                LastVisibleString = args.LastVisibleString
            };

            base.InvokeSaveRequest(settingsFileName, container);
        }
    }
}
