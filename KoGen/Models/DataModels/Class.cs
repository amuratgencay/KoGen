using KoGen.Extentions;
using static KoGen.Models.DataModels.Predefined.PredefinedClasses;
using static KoGen.Extentions.StringExtentions;
using System;
using System.Collections.Generic;
using System.Linq;
using KoGen.Models.DataModels.Enum;
using static KoGen.Models.DataModels.Enum.AccessModifier;
using static KoGen.Models.DataModels.Enum.NonAccessModifier;
using KoGen.Models.DataModels.Functions;

namespace KoGen.Models.DataModels
{

    [Serializable]
    public class Class : Wrapper
    {

        #region Properties

        public Class BaseClass { get; set; }
        public bool Nullable { get; set; }
        public object DefaultValue { get; set; }
        public AccessModifier AccessModifier { get; set; }
        public List<NonAccessModifier> NonAccessModifiers { get; set; }
        public ClassMemberCollection ClassMembers { get; set; }
        public List<Annotation> Annotations { get; set; }

        public Func<object, string> ToStringFunction;
        public List<Class> GenericList { get; set; }
        public List<Function> Functions { get; set; }

        #endregion

        #region Functions

        public ClassMember GetStaticMember(string name) => ClassMembers.GetStaticMember(name);
        public ClassMember GetStaticMemberByValue(object value) => ClassMembers.GetStaticMemberByValue(value);
        public List<Wrapper> Relations =>
            new List<Wrapper>()
                .AddIfTrue(BaseClass != null, BaseClass)
                .AddList(Annotations)
                .AddList(Annotations.SelectMany(x=>x.Parameters.Values.Select(y=>y.Type)))
                .AddList(ClassMembers.Relations)
                .AddList(ClassMembers.ClassMembers.SelectMany(x=>x.Annotations.SelectMany(y => y.Parameters.Values.Select(z => (z.Value is ReferenceValue referenceValue) ? referenceValue.Clazz : z.Type))))
                .AddList(GenericList.SelectMany(x => x.Relations));

        public List<Package> Packages =>
            Relations
            .Where(x => x.Package != Package.DefaultPackage && x.Package != Package)
            .Select(x => x.Package)
            .ToList();

        #endregion

        #region FileGeneration

        private string PackageString =>
            $"package {Package};{DoubleNewLine}";
        private string ImportsString =>
            Packages
            .AggregateDistinct(x => $"import {x};", NewLine, "", NewLine);

        private string AnnotationsString =>
            Annotations
            .Aggregate(x => x.ToString(), NewLine, NewLine, NewLine, false, "\r\n");
        private string GenericString =>
            GenericList
            .Aggregate(x => x.Name, ", ", "<", ">");
        private string AccessModifierString =>
            AccessModifier
            .ToString()
            .ToLower();
        private string NonAccessModifiersString =>
            NonAccessModifiers
            .Aggregate(x => x.ToString().ToLower(), " ", " ");
        private string BaseClassString =>
            (BaseClass != null && BaseClass != JavaObject) ? $"extends {BaseClass.Name} " : "";
        private string GetterAndSetterString()
        {
            var res = "";
            if (ClassMembers.GetterFunctions.Count > 0)
            {
                res += NewLineTab;
                for (int i = 0; i < ClassMembers.GetterFunctions.Count; i++)
                {
                    res += ClassMembers.GetterFunctions[i].ToString() + NewLineTab;
                    res += ClassMembers.SetterFunctions[i].ToString() + NewLineTab;
                }
                res = res.TrimEnd() + NewLine;
            }
            return res;
        }
        public Class Clone => (Class) this.MemberwiseClone();
        private string FunctionString =>
            Functions.Aggregate(x => x.ToString(), NewLineTab, DoubleNewLineTab, NewLineTab);
        

        public virtual string ToJavaFile()
        {
            return ($"{PackageString}"
                    + $"{ImportsString}"
                    + $"{AnnotationsString}"
                    + $"{AccessModifierString}{NonAccessModifiersString} class {Name}{GenericString} {BaseClassString}{{"
                    + $"{ClassMembers.ClassMembersString}"
                    + $"{GetterAndSetterString()}"
                    + $"{FunctionString}"
                    + $"{NewLine}}}");
        }

        #endregion

        #region Constructors

        public Class()
        {
            BaseClass = JavaObject;
            Package = Package.DefaultPackage;
            Nullable = true;
            DefaultValue = null;
            AccessModifier = Public;
            NonAccessModifiers = new List<NonAccessModifier>();
            ClassMembers = new ClassMemberCollection(this);
            Annotations = new List<Annotation>();
            ToStringFunction = x => x.ToString();
            GenericList = new List<Class>();
            Functions = new List<Function>();
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
            hashCode = hashCode * -1521134295 + EqualityComparer<ClassMemberCollection>.Default.GetHashCode(ClassMembers);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Annotation>>.Default.GetHashCode(Annotations);
            return hashCode;
        }

        public static bool operator ==(Class c1, Class c2)
        {
            return c1.Name == c2?.Name;
        }

        public static bool operator !=(Class c1, Class c2)
        {
            return c1?.Name != c2?.Name;
        }

        #endregion
    }
}
