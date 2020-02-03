using KoGen.Extentions;
using static KoGen.Models.DataModels.Predefined.PredefinedClasses;
using static KoGen.Extentions.StringExtentions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KoGen.Models.DataModels.Enum;
using static KoGen.Models.DataModels.Enum.AccessModifier;
using static KoGen.Models.DataModels.Enum.NonAccessModifier;
using System;

namespace KoGen.Models.DataModels
{
    [Serializable]
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
            Type = type.Clone;
            Type.GenericList = Type.GenericList.Select(x => x.Clone).ToList();
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
            .Aggregate(x => x.ToString(), NewLineTab, "", NewLineTab);

        private string GetAccessModifierString =>
            AccessModifier
            .ToString()
            .ToLower();

        private string GetNonAccessModifiersString =>
            NonAccessModifiers
            .Aggregate(x => x.ToString().ToLower(), " ", " ");

        private string GetAssignString =>
            (Value != Type.DefaultValue ? $@" = {AssignString()}" : "");

        private string GenericString =>
          Type.GenericList
          .Aggregate(x => x.Name, ", ", "<", ">");

        public string GetDeclaration()
            => GetAnnotationsString
                + $@"{GetAccessModifierString}{GetNonAccessModifiersString} {Type.Name}{GenericString} {Name}{GetAssignString};";

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

  
        public override int GetHashCode()
        {
            var hashCode = 1806618034;
            hashCode = hashCode * -1521134295 + AccessModifier.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<NonAccessModifier>>.Default.GetHashCode(NonAccessModifiers);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Annotation>>.Default.GetHashCode(Annotations);
            hashCode = hashCode * -1521134295 + Initialized.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<Class>.Default.GetHashCode(Owner);
            hashCode = hashCode * -1521134295 + EqualityComparer<Class>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(Value);
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(DefaultValue);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Comment);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GetAnnotationsString);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GetAccessModifierString);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GetNonAccessModifiersString);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GetAssignString);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GenericString);
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return obj is ClassMember member && Name == member.Name;
        }

        public static bool operator ==(ClassMember cm1, ClassMember cm2)
        {
            return cm1.Owner == cm2.Owner && cm1.Name == cm2.Name;
        }
        public static bool operator !=(ClassMember cm1, ClassMember cm2)
        {
            return !(cm1 == cm2);
        }
    }
}
