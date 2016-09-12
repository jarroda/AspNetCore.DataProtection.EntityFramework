
using System;

namespace AspNetCore.DataProtection.EntityFramework
{
    public static class Extensions
    {
        public static DateTime? ParseDate(this string dateString)
        {
            DateTime date;

            if(!string.IsNullOrEmpty(dateString) && DateTime.TryParse(dateString, out date))
            {
                return date;
            }

            return null;
        } 
    }
}