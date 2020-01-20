using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoGen.Models.DataModels
{
    public class DataModel
    {
        private object value;

        public AccessModifier AccessModifier { get; set; }
        public List<NonAccessModifier> NonAccessModifiers { get; set; } = new List<NonAccessModifier>();
        public List<Annotation> Annotations { get; set; }
        public DataType DataType { get; set; }
        public string Name { get; set; }

        public object Value { 
            get => value; 
            set {
                DataType = new DataType(value.GetType());
                this.value = value;
            } 
        }

        public static DataModel CreatePublicStaticFinalString(string name, string value = null) => new DataModel(name, typeof(string), value, AccessModifier.Public, NonAccessModifier.Static, NonAccessModifier.Final);
        public static DataModel CreatePublicStaticFinalInt(string name, int value) => new DataModel(name, typeof(int), value, AccessModifier.Public, NonAccessModifier.Static, NonAccessModifier.Final);

        public DataModel(string name, Type type, AccessModifier accessModifier = AccessModifier.Private, params NonAccessModifier[] nonAccessModifiers)
        {
            Name = name;
            DataType = type;
            AccessModifier = accessModifier;

            if (nonAccessModifiers != null)
                NonAccessModifiers.AddRange(nonAccessModifiers);
        }

        public DataModel(string name, Type type, string value = null, AccessModifier accessModifier = AccessModifier.Private, params NonAccessModifier[] nonAccessModifiers) : this(name, type, accessModifier, nonAccessModifiers)
        {
            if (!DataType.Nullable && value == null)
                throw new ArgumentNullException(nameof(value));
            if (!DataType.Assignable(value))
                throw new InvalidCastException(nameof(value));

            Value = value;
        }

        public DataModel(string name, Type type, int value, AccessModifier accessModifier = AccessModifier.Private, params NonAccessModifier[] nonAccessModifiers) : this(name, type, accessModifier, nonAccessModifiers)
        {
            if (!DataType.Assignable(value))
                throw new InvalidCastException(nameof(value));

            Value = value;
        }

        public DataModel(string name, Type type, bool value, AccessModifier accessModifier = AccessModifier.Private, params NonAccessModifier[] nonAccessModifiers) : this(name, type, accessModifier, nonAccessModifiers)
        {
            if (!DataType.Assignable(value))
                throw new InvalidCastException(nameof(value));

            Value = value;
        }

        public string GetDeclaration()
        {
            var res = $@"{AccessModifier.ToString().ToLower()} {NonAccessModifiers.Select(x => x.ToString().ToLower()).Aggregate((x, y) => x + " " + y)} {DataType} {Name}{(Value != null ? $@" = {DataType.AssingString(Value)}" : "")};";
            return res;
        }

        public string AssingString()
        {
            return DataType.AssingString(Value);
        }
    }
}
