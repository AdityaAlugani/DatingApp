using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateOnly dob)
        {
            DateOnly today=DateOnly.FromDateTime(DateTime.UtcNow);
            int age=today.Year-dob.Year;
            DateOnly some=today.AddYears(-age);
            if(some<dob)
            age--;


            return age;
        }
    }
}