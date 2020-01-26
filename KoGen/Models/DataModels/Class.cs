using KoGen.Extentions;
using KoGen.Models.ClassMembers;
using static KoGen.Models.DataModels.Predefined.PredefinedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using KoGen.Models.DataModels.Enum;
using static KoGen.Models.DataModels.Enum.AccessModifier;
using static KoGen.Models.DataModels.Enum.NonAccessModifier;

namespace KoGen.Models.DataModels
{
    public abstract class Wrapper
    {
        public Package Package { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }

    }
    public class Class : Wrapper
    {
        public Class BaseClass { get; set; }
        public bool Nullable { get; set; }
        public object DefaultValue { get; set; }
        public AccessModifier AccessModifier { get; set; }
        public List<NonAccessModifier> NonAccessModifiers { get; set; }
        public List<ClassMember> ClassMembers { get; set; }
        public List<Annotation> Annotations { get; set; }

        public Func<object, string> ToStringFunction;
        public ClassMember GetStaticMember(string name)
        {
            return ClassMembers.FirstOrDefault(x => x.NonAccessModifiers.Contains(Static) && x.Name == name);
        }

        public ClassMember GetStaticMemberByValue(object value)
        {
            return ClassMembers.FirstOrDefault(x => x.NonAccessModifiers.Contains(Static) && x.Value == value);
        }
        private List<Wrapper> GetRelations => 
            new List<Wrapper>()
                .AddIfTrue((BaseClass != null && BaseClass.Package.ToString() != Package.ToString()), BaseClass)
                .AddList(Annotations)
                .AddList(ClassMembers.SelectMany(x => x.Annotations))
                .AddList(ClassMembers.Select(x => x.Type));
        
        private List<Package> GetPackages =>
            GetRelations
            .Where(x => x.Package != Package.DefaultPackage)
            .Select(x => x.Package)
            .ToList();
        private string PackageString =>
            $"package {Package};\r\n\r\n";
        private string GetImportsString => 
            GetPackages
            .AggregateDistinct(x => $"import {x};", "\r\n", "\r\n") + "\r\n";

        private string GetAnnotationsString =>
            Annotations
            .Aggregate(x => x.ToString(), "\r\n", "\r\n", "\r\n");

        private string GetAccessModifierString =>
            AccessModifier
            .ToString()
            .ToLower();
        private string GetNonAccessModifiersString =>
            NonAccessModifiers
            .Aggregate(x => x.ToString().ToLower(), " ", " ");
        private string BaseClassString =>
            (BaseClass != null && BaseClass != JavaObject) ? $"extends {BaseClass.Name} " : "";

        private string ClassMembersString =>
            ClassMembers.Aggregate(x => "\t" + x.GetDeclaration(), "\r\n\r\n", "\r\n\r\n") + "\r\n\r\n";

        public virtual string ToJavaFile()
        {
            return ($"{PackageString}"
                    + $"{GetImportsString}"
                    + $"{GetAnnotationsString}"
                    + $"{GetAccessModifierString}{GetNonAccessModifiersString} class {Name} {BaseClassString}{{"
                    + $"{ClassMembersString}"
                    + $"}}").ReplaceAll("\n\r\n\r", "\n\r");
        }

        #region Constructors

        public Class()
        {
            BaseClass = JavaObject;
            Package = Package.DefaultPackage;
            Nullable = true;
            DefaultValue = null;
            AccessModifier = Public;
            NonAccessModifiers = new List<NonAccessModifier>();
            ClassMembers = new List<ClassMember>();
            Annotations = new List<Annotation>();
            ToStringFunction = x => x.ToString();
        }

        public Class(string name) : this()
        {
            Name = name;
        }

        public Class(string name, Package package) : this()
        {
            Name = name;
            Package = package;
        }

        public Class(string name, params NonAccessModifier[] nonAccessModifiers) : this()
        {
            Name = name;
            NonAccessModifiers = nonAccessModifiers.ToList();
        }

        public Class(string name, Class baseClass, params NonAccessModifier[] nonAccessModifiers) : this()
        {
            Name = name;
            BaseClass = baseClass;
            NonAccessModifiers = nonAccessModifiers.ToList();
        }

        public Class(string name, object defaultValue, bool nullable, params NonAccessModifier[] nonAccessModifiers) : this()
        {
            Name = name;
            DefaultValue = defaultValue;
            Nullable = nullable;
            NonAccessModifiers = nonAccessModifiers.ToList();
        }

        public Class(string name, Class baseClass, object defaultValue, bool nullable, params NonAccessModifier[] nonAccessModifiers) : this()
        {
            Name = name;
            BaseClass = baseClass;
            DefaultValue = defaultValue;
            Nullable = nullable;
            NonAccessModifiers = nonAccessModifiers.ToList();
        }

        public Class(string name, Func<object, string> toStringFunction, params NonAccessModifier[] nonAccessModifiers) : this()
        {
            Name = name;
            ToStringFunction = toStringFunction;
            NonAccessModifiers = nonAccessModifiers.ToList();
        }

        public Class(string name, object defaultValue, bool nullable, Func<object, string> toStringFunction, params NonAccessModifier[] nonAccessModifiers) : this(name, defaultValue, nullable, nonAccessModifiers)
        {
            Name = name;
            DefaultValue = defaultValue;
            Nullable = nullable;
            ToStringFunction = toStringFunction;
            NonAccessModifiers = nonAccessModifiers.ToList();
        }

        public Class(string name, Package package, Func<object, string> toStringFunction) : this()
        {
            Name = name;
            Package = package;
            ToStringFunction = toStringFunction;
        }

        #endregion

        #region Operators

        public override bool Equals(object obj)
        {
            return obj is Class @class && @class.Name == Name;
        }

        public override int GetHashCode()
        {
            var hashCode = 217421047;
            hashCode = hashCode * -1521134295 + EqualityComparer<Class>.Default.GetHashCode(BaseClass);
            hashCode = hashCode * -1521134295 + EqualityComparer<Package>.Default.GetHashCode(Package);
            hashCode = hashCode * -1521134295 + Nullable.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(DefaultValue);
            hashCode = hashCode * -1521134295 + AccessModifier.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<NonAccessModifier>>.Default.GetHashCode(NonAccessModifiers);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Comment);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<ClassMember>>.Default.GetHashCode(ClassMembers);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Annotation>>.Default.GetHashCode(Annotations);
            return hashCode;
        }

        public static bool operator ==(Class c1, Class c2)
        {
            return c1.Name == c2?.Name;
        }

        public static bool operator !=(Class c1, Class c2)
        {
            return c1.Name != c2?.Name;
        }

        #endregion
    }
}
