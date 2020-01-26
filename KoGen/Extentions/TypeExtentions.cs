using KoGen.Models.DatabaseModels;
using KoGen.Models.DataModels;
using KoGen.Models.DataModels.Predefined;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace KoGen.Extentions
{
    public static class TypeExtentions
    {
        public static Type FromDBType(string dbType, Size size = null, bool nullable = false)
        {
            switch (dbType)
            {
                case "bigint": return (nullable) ? typeof(long?) : typeof(long);
                case "char": return typeof(string);
                case "varchar": return typeof(string);
                case "bit": return (nullable) ? typeof(bool?) : typeof(bool);
                case "boolean": return (nullable) ? typeof(bool?) : typeof(bool);
                case "date": return (nullable) ? typeof(DateTime?) : typeof(DateTime);
                case "integer": return (nullable) ? typeof(int?) : typeof(int);
                case "smallint": return (nullable) ? typeof(short?) : typeof(short);
                case "numeric":
                    if (size.Min.HasValue && size.Max.HasValue) { 
                        return (nullable) ? typeof(double?) : typeof(double);
                    }
                    else
                    {
                        if (size.Max < 5)
                            return (nullable) ? typeof(short?) : typeof(short);
                        if (size.Max < 10)
                            return (nullable) ? typeof(int?) : typeof(int);
                        return (nullable) ? typeof(long?) : typeof(long);
                    }
                default:
                    return null;
            }
        }
        private static Dictionary<Type, Class> _javaTypeDictionary = new Dictionary<Type, Class>
        {
            {typeof(bool), PredefinedClasses.JavaBoolean},
            {typeof(bool?), PredefinedClasses.JavaBooleanPrimitive},

            {typeof(char), PredefinedClasses.JavaCharPrimitive },
            {typeof(char?), PredefinedClasses.JavaCharacter },

            {typeof(string), PredefinedClasses.JavaString },

            {typeof(short), PredefinedClasses.JavaShortPrimitive },
            {typeof(short?), PredefinedClasses.JavaShort },

            {typeof(int), PredefinedClasses.JavaIntPrimitive },
            {typeof(int?), PredefinedClasses.JavaInteger },


            {typeof(long), PredefinedClasses.JavaLongPrimitive },
            {typeof(long?), PredefinedClasses.JavaLong },

            {typeof(double), PredefinedClasses.JavaDoublePrimitive },
            {typeof(double?), PredefinedClasses.JavaDouble },

            {typeof(DateTime), PredefinedClasses.JavaDate },
            {typeof(DateTime?), PredefinedClasses.JavaDate },

            {typeof(List<>), PredefinedClasses.JavaList},

        };
        public static Class ToJavaType(this Type type)
        {
            return _javaTypeDictionary.ContainsKey(type) ? _javaTypeDictionary[type] : throw new Exception("");
        }
    }


}
