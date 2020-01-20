using KoGen.Extentions;
using System;
using System.Collections;

namespace KoGen.Models.DataModels
{
    public class DataType
    {
        public string Name { get; set; }
        public bool Nullable { get; set; }

        public DataType() { }
        public DataType(Type type)
        {
            bool nullable;
            Name = type.ToJavaType(out nullable);
            Nullable = nullable;
        }

        public bool Assignable(object value) => Name == value.GetType().ToJavaType();

        public static implicit operator DataType(Type type) => new DataType(type);

        public override string ToString()
        {
            return Name;
        }

        public string AssingString(object value)
        {
            if (Name == "String")
                return $@"""{value.ToString()}""";
            else if (Name == "Boolean")
                return ((bool)value) ? "true" : "false";
            else if (Name == "List`1")
            {
                var res = "{ ";
                foreach (var item in ((IList)value))
                {
                    res += item.ToString() + ", ";
                }
                res = res.Remove(res.Length - 2) + " }";
                return res.Replace("  ", " ");
            }
            
            return value.ToString();
        }
    }

    public class ReferenceValue
    {
        public ReferenceValue(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
        public override string ToString() => Value;
    }
}
