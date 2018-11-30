using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelotonData
{
    public static class Util
    {
        public static DateTime DateTimeFromEpochSeconds(int seconds)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            return dateTime.AddSeconds(seconds);
        }
    }
}
