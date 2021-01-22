using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace sample_service.Helpers
{
    public static class Extensions
    {
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        public static IQueryable<TSource> TakeIf<TSource>(this IQueryable<TSource> source, bool condition, int count)
        {
            if (condition)
                return source.Take(count);
            else
                return source;
        }


        public static string ToMD5(this string value)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashData = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(value));
            StringBuilder build = new StringBuilder();
            for (int i = 0; i < hashData.Length; i++)
            {
                build.Append(hashData[i].ToString("x2"));
            }
            return build.ToString();
        }
        public static int ToInt(this string value)
        {
            if (string.IsNullOrEmpty(value)) return 0;
            return Convert.ToInt32(value);
        }
        public static long ToLong(this string value)
        {
            if (string.IsNullOrEmpty(value)) return 0;
            return Convert.ToInt64(value);
        }


        public static decimal ToDecimal(this string value)
        {
            if (string.IsNullOrEmpty(value)) return 0;
            return Convert.ToDecimal(value);
        }

        public static int ToInt(this double value)
        {

            return Convert.ToInt32(value);
        }
    }
}
