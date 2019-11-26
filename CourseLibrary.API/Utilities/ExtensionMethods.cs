using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Utilities
{
    public static class ExtensionMethods
    {
        public static int GetCurrentAge(this DateTimeOffset dateTimeOffset, DateTimeOffset referenceDate)
        {
            int age = referenceDate.Year - dateTimeOffset.Year;
            if (referenceDate < dateTimeOffset.AddYears(age))
            {
                age--;
            }
            return age;
        }
    }
}
