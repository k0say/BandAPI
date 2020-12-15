using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI.Helpers
{
    //come extension method deve essere static
    public static class FoundedYearsAgo
    {
        public static int GetYearsAgo(this DateTime dateTime)
        {
            var currentDate = DateTime.Now;
            int yearsAgo = currentDate.Year - dateTime.Year;

            return yearsAgo;
        }
    }
}
