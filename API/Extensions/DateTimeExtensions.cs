using System;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        //tinh tuoi
        //dob: data of birth
        public static int CalculateAge(this DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year -dob.Year;
            if(dob.Date > today.AddYears(-age)){
                age--;
            }
            return age;
        }
    }
}