﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KoGen.Extentions
{
    public static class ListExtentions
    {
        public static List<T> AddList<T>(this List<T> list, IEnumerable<T> collection)
        {
            if (collection.Count() > 0)
                list.AddRange(collection);
            return list;
        }

        public static List<T> AddIfTrue<T>(this List<T> list, bool condition, T item)
        {
            if (condition)
                list.Add(item);
            return list;
        }

        public static string Aggregate<TSource>(this List<TSource> source, Func<TSource, string> selector = null, string infix = "", string prefix = "", string suffix = "", bool addPrefixAndSuffix = false, string emptyValue = "")
        {
            return source.Count > 0 ? prefix + source.Select(x => selector != null ? selector(x) : x.ToString()).Aggregate((x, y) => x + infix + y) + suffix : (addPrefixAndSuffix ? (prefix.Trim().Length > 0 ? prefix.Trim() : prefix) + (suffix.Trim().Length > 0 ? suffix.Trim() : suffix) : emptyValue);
        }

        public static string AggregateDistinct<TSource>(this List<TSource> source, Func<TSource, string> selector = null, string infix = "", string prefix = "", string suffix = "", bool addPrefixAndSuffix = false)
        {
            return source.Count > 0 ? prefix + source.Select(x => selector != null ? selector(x) : x.ToString()).Distinct().Aggregate((x, y) => x + infix + y) + suffix : (addPrefixAndSuffix ? prefix.Trim() + suffix.Trim() : "");
        }

        public static List<object> ConvertToList(IList list)
        {
            var res = new List<object>();
            foreach (var item in list)
                res.Add(item);

            return res;
        }

        private static List<T> CloneListAs<T>(IList<object> source)
        {
            return source.Cast<T>().ToList();
        }
    }


}
