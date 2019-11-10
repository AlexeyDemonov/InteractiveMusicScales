using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Threading.Tasks;

namespace InteractiveMusicScales
{
    class XmlFileLoadSaver
    {
        //==============================================================
        //Handlers
        public void Handle_SaveFileRequest(string filename, dynamic instance)
        {
            try
            {
                using (var stream = File.Create(filename))
                {
                    var serializer = new XmlSerializer( instance.GetType() );
                    serializer.Serialize(stream, instance);
                }
            }
            catch (Exception ex)
            {
                Logger.LogTheError($"XmlFileLoadSaver.Handle_SaveFileRequest: Error while saving '{filename}' file: {ex.Message}");
                DeleteFile(filename);
            }
        }

        public object Handle_LoadFileRequest(string filename, Type type)
        {
            object result = null;

            try
            {
                using (var stream = File.OpenRead(filename))
                {
                    var serializer = new XmlSerializer( type );
                    result = serializer.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Logger.LogTheError($"XmlFileLoadSaver.Handle_LoadFileRequest: Error while loading '{filename}' file: {ex.Message}");
                DeleteFile(filename);
                result = null;
            }

            return result;
        }

        //==============================================================
        //Methods
        void DeleteFile(string filename)
        {
            if (File.Exists(filename))
            {
                try
                {
                    File.Delete(filename);
                }
                catch (Exception ex)
                {
                    Logger.LogTheError($"XmlFileLoadSaver.DeleteCorruptedFile: Error while trying to delete '{filename}': {ex.Message}");
                }
            }
        }
    }
}
