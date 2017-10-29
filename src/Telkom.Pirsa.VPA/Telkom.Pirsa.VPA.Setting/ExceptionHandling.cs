using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telkom.Pirsa.VPA.Setting
{
    public class ExceptionHandling
    {
        public static Exception GenerateException(string message, Exception inner = null)
        {
            return new Exception(message, inner);
        }

       
    }
}
