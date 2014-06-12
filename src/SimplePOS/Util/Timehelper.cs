using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePOS.Util
{
    public class Timehelper
    {
        private Timehelper() {/*empty*/}



        public static long getTimestampOfDateTime(DateTime date)
        {
            DateTime refTime = new DateTime(1970, 1, 1);  //Refernzdatum (festgelegt)
            TimeSpan ts = new TimeSpan(date.Ticks - refTime.Ticks);  // das Delta ermitteln
            // Das Delta als gesammtzahl der sekunden ist der Timestamp
            return (Convert.ToInt64(ts.TotalSeconds));
        }

        public static DateTime getDateTimeOfTimestamp(long timestamp)
        {
            DateTime refTime = new DateTime(1970, 1, 1);  //Refernzdatum (festgelegt)
            refTime.AddSeconds(timestamp);  // Timestamp aufaddieren
            return refTime;
        }
    }
}
