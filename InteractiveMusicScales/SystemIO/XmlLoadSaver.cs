using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Threading.Tasks;

namespace InteractiveMusicScales
{
    class XmlLoadSaver
    {
        bool catchAndLogExceptions;

        public XmlLoadSaver(bool catchAndLogExceptions = true)
        {
            this.catchAndLogExceptions = catchAndLogExceptions;
        }

        //==============================================================
        //Handlers
        public void Handle_SaveRequest(string filename, object instance)
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
                if(catchAndLogExceptions)
                {
                    Logger.LogTheError($"XmlFileLoadSaver.Handle_SaveRequest: Error while saving '{filename}' file");
                    Logger.LogTheException(ex);
                }
                else
                {
                    throw;//Rethrow exception
                }
            }
        }

        public object Handle_LoadRequest(string filename, Type type)
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
                if(catchAndLogExceptions)
                {
                    Logger.LogTheError($"XmlFileLoadSaver.Handle_LoadRequest: Error while loading '{filename}' file");
                    Logger.LogTheException(ex);
                    result = null;
                }
                else
                {
                    throw;//Rethrow exception
                }

            }

            return result;
        }
    }
}
