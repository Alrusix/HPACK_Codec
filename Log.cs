using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPACK_Codec
{
    public delegate void LogInformation(string message);
    public delegate void LogWarning(string message);
    public delegate void LogError(string message);
    public class Log
    {
       
        public static LogInformation Info = new(_ => { });
        public static LogWarning Warning = new(_ => { });
        public static LogError Error = new(_ => { });

    }
}
