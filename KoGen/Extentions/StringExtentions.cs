using System;
using System.Collections.Generic;
using System.Linq;

namespace KoGen.Extentions
{
    public static class StringExtentions
    {
        public static string ToPascalCase(this string value)
        {
            return value.ToUpper(System.Globalization.CultureInfo.InvariantCulture).Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x[0] + x.Substring(1).ToLower(System.Globalization.CultureInfo.InvariantCulture))
                .Aggregate((x, y) => x + y);
        }

        public static string ToCamelCase(this string value)
        {
            int index = 0;
            var result = "";
            foreach (var item in value.ToUpper(System.Globalization.CultureInfo.InvariantCulture).Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (index > 0)
                {
                    result += item[0] + item.Substring(1).ToLower(System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    result += item.ToLower(System.Globalization.CultureInfo.InvariantCulture);
                }
                index++;
            };

            return result;
        }
        public static string GetSlice(this string value, string start, string end, int index = -1)
        {
            if (index == -1)
                index = 0;

            var indx1 = value.IndexOf(start, index);
            if (indx1 == -1)
                return value;

            var indx2 = value.IndexOf(end, indx1 + start.Length);
            if (indx2 == -1)
                return value.Substring(indx1);

            return value.Substring(indx1 + start.Length, indx2 - indx1 - start.Length);
        }

        public static string[] GetSlices(this string value, string start, string end, int index = -1)
        {
            if (!value.Contains(start))
                return new string[0];

            var res = new List<string>();
            var str = value;
            string part;
            do
            {
                part = GetSlice(str, start, end, index);
                if (part != str)
                {
                    part = start + part;
                    res.Add(part);
                    str = str.Substring(str.IndexOf(part) + part.Length);
                }
                else if (part.StartsWith(start))
                {
                    res.Add(part);
                }
            } while (part != str);

            return res.ToArray();
        }

        public static string ReplaceAll(this string str, string oldValue, string newValue)
        {
            while (str.Contains(oldValue))
            {
                str = str.Replace(oldValue, newValue);
            }
            return str;
        }
    }

}
