using KoGen.Extentions;
using KoGen.Models.ClassMembers;
using static KoGen.Models.DataModels.Predefined.PredefinedClasses;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KoGen.Models.DataModels.Enum;
using static KoGen.Models.DataModels.Enum.AccessModifier;
using static KoGen.Models.DataModels.Enum.NonAccessModifier;

namespace KoGen.Models.DataModels
{
    public class ClassMember
    {
        public AccessModifier AccessModifier { get; set; } = Private;
        public List<NonAccessModifier> NonAccessModifiers { get; set; } = new List<NonAccessModifier>();
        public List<Annotation> Annotations { get; set; } = new List<Annotation>();
        public bool Initialized { get; private set; }
        public string Name { get; set; }
        public Class Owner { get; set; }
        public Class Type { get; set; }
        public object Value { get; set; }
        public readonly object DefaultValue;
        public string Comment { get; set; }

        public ClassMember(string name, Class type, object value = null, AccessModifier accessModifier = Public, params NonAccessModifier[] nonAccessModifiers)
        {
            Name = name;
            Type = type;
            AccessModifier = accessModifier;
            NonAccessModifiers = nonAccessModifiers?.ToList() ?? new List<NonAccessModifier>();

            if (value == null && !type.Nullable)
            {
                Value = type.DefaultValue;
                Initialized = true;
            }
            else if (value != null)
            {
                Value = value;
                Initialized = true;
            }
            DefaultValue = Value;
        }

        private string GetAnnotationsString =>
            Annotations
            .Aggregate(x => x.ToString(), "\r\n\t", "", "\r\n\t");

        private string GetAccessModifierString =>
            AccessModifier
            .ToString()
            .ToLower();

        private string GetNonAccessModifiersString =>
            NonAccessModifiers
            .Aggregate(x => x.ToString().ToLower(), " ", " ");

        private string GetAssignString =>
            (Value != Type.DefaultValue ? $@" = {AssignString()}" : "");

        public string GetDeclaration()
            => GetAnnotationsString
                + $@"{GetAccessModifierString}{GetNonAccessModifiersString} {Type.Name} {Name}{GetAssignString};";

        public string AssignString()
        {
            if (Value == null)
                Value = Type.DefaultValue;
            else if (Value is ReferenceValue refValue)
                return refValue.Value;
            
            return Type.ToStringFunction(Value);
        }
        public static ClassMember CreatePublicStaticFinalString(string name, string value) =>
            new ClassMember(name, JavaString, value, Public, Static, Final);

        public static ClassMember CreatePublicStaticFinalInt(string name, int value) =>
            new ClassMember(name, JavaIntPrimitive, value, Public, Static, Final);
    }
}
