using System;
using System.Text;

namespace CoDUO_FoV_Changer.Util
{
    internal static class EmergencyLogger
    {

        // An emergency logger for when CurtLog cannot be used (yet?)

        // WriteError for example will simply append to an "error.log" file if the size isn't too big.

        [ThreadStatic]
        private static StringBuilder _sb = new StringBuilder();

        public static void WriteError(string message)
        {
            try
            {
                var filePath = "error.log";
                var maxFileSize = 1024 * 32768; // 32 MB

                if (System.IO.File.Exists(filePath) && new System.IO.FileInfo(filePath).Length > maxFileSize)
                {
                    System.IO.File.Delete(filePath);
                }

                System.IO.File.AppendAllText(filePath, _sb
                    .Clear()
                    .Append(DateTime.Now)
                    .Append(": ")
                    .Append(message)
                    .Append(Environment.NewLine)
                    .ToString());
            }
            catch(Exception ex) { Console.WriteLine(ex.ToString()); }
        }

        public static void WriteException(Exception ex) => WriteError(ex.ToString());
    }
}
