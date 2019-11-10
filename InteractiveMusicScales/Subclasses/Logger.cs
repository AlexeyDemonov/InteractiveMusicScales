using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace InteractiveMusicScales
{
    class Logger
    {
        static string errorLogName = "errors.log";

        public static void LogTheError(string errorMessage)
        {
            string toWrite = $"[{DateTime.Now.ToString()}] {errorMessage}";

            try
            {
                File.AppendAllText(errorLogName, errorMessage);
            }
            catch (Exception)
            {
                /*Do nothing*/
            }
        }
    }
}
