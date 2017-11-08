using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telkom.Pirsa.VPA.Engine.Helper
{
    public class Validator
    {
        public static bool IsNumber(string text, out double result)
        { 
            result = double.Epsilon;
            return double.TryParse(text, out result) && result > double.Epsilon;
        }

        public static bool IsInteger(string text, out int result)
        {
            result = -1;
            return int.TryParse(text, out result) && result > 0;
        }
    }
}
