using KoGen.Models.DatabaseModels;
using System;

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
                case "numeric":
                    if (size.Max < 5)
                        return (nullable) ? typeof(short?) : typeof(short);
                    if (size.Max < 10)
                        return (nullable) ? typeof(int?) : typeof(int);
                    return (nullable) ? typeof(long?) : typeof(long);
                default:
                    return null;
            }
        }


        public static string ToJavaType(this Type type, out bool nullable)
        {
            nullable = true;
            if (type == typeof(char) || type == typeof(char?))
            {
                return "Character";
            }
            else if (type == typeof(int))
            {
                nullable = false;
                return "int";
            }
            else if (type == typeof(int?) || type == typeof(Int32) || type == typeof(Int32?))
            {
                nullable = true;
                return "Integer";
            }
            else if (type == typeof(short) || type == typeof(short?) || type == typeof(Int16) || type == typeof(Int16?))
            {
                nullable = true;
                return "Short";
            }
            else if (type == typeof(Int64) || type == typeof(Int64?))
            {
                nullable = true;
                return "Long";
            }
            else if (type == typeof(string) || type == typeof(String))
            {
                return "String";
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return "Date";
            }
            else
            {
                return type.Name;
            }
        }
        public static string ToJavaType(this Type type)
        {
            bool nullable;
            return ToJavaType(type, out nullable);
        }

    }

}
