using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io.ark.core
{
    public class Slot
    {
        private static DateTime beginEpoch = new DateTime(2017, 3, 21, 13, 00, 0, DateTimeKind.Utc);

        public static int GetTime()
        {
            //DateTime date = DateTime.Now;

            return Convert.ToInt32((DateTime.UtcNow - beginEpoch).TotalMilliseconds / 1000);
        }
    }
}
