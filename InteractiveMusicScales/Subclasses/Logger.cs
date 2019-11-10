﻿using System;
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
            AppendToLog(errorMessage);
        }

        public static void LogTheException(Exception exception)
        {
            if (exception == null)
            { 
                AppendToLog($"Logger.AppendToLog: something is trying to write a null Exception to log");
                return;
            }

            AppendToLog(exception.GetType().ToString());

            if(!string.IsNullOrEmpty(exception.Message))
                AppendToLog(exception.Message);

            if (!string.IsNullOrEmpty(exception.StackTrace))
                AppendToLog(exception.StackTrace);

            if(exception.InnerException != null)
            {
                AppendToLog($"Inner exception: {exception.InnerException.GetType().ToString()}");

                if (!string.IsNullOrEmpty(exception.InnerException.Message))
                    AppendToLog(exception.InnerException.Message);
            }
        }

        static void AppendToLog(string line)
        {
            if(string.IsNullOrEmpty(line))
            {
                AppendToLog($"Logger.AppendToLog: something is trying to write an empty or null line to log");
                return;
            }

            string fullLine = $"[{DateTime.Now.ToString()}] {line}{Environment.NewLine}";

            try
            {
                File.AppendAllText(errorLogName, fullLine);
            }
            catch (Exception)
            {
                /*Do nothing*/
            }
        }
    }
}
