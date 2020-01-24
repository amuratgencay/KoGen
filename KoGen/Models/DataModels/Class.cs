using KoGen.Extentions;
using KoGen.Models.ClassMembers;
using static KoGen.Models.DataModels.Predefined.PredefinedClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KoGen.Models.DataModels.Enum;

namespace KoGen.Models.DataModels
{
    public class ClassMember
    {
        public ClassMember(string name, Class type, object value = null, AccessModifier accessModifier = AccessModifier.Public, params NonAccessModifier[] nonAccessModifiers)
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
        
        public AccessModifier AccessModifier { get; set; } = AccessModifier.Private;
        public List<NonAccessModifier> NonAccessModifiers { get; set; } = new List<NonAccessModifier>();
        public List<Annotation> Annotations { get; set; } = new List<Annotation>();
        public bool Initialized { get; private set; }
        public string Name { get; set; }
        public Class Owner { get; set; }
        public Class Type { get; set; }
        public object Value { get; set; }
        public readonly object DefaultValue;
        public string Comment { get; set; }

        public string GetDeclaration()
        {
            var res = "";
            foreach (var item in Annotations)
            {
                res += item + "\r\n\t";
            }
            res += $@"{AccessModifier.ToString().ToLower()}{(NonAccessModifiers.Count > 0 ? " " + NonAccessModifiers.Select(x => x.ToString().ToLower()).Aggregate((x, y) => x + " " + y) : "")} {Type.Name} {Name}{(Value != Type.DefaultValue ? $@" = {AssingString()}" : "")};";

            return res;
        }

        public string AssingString()
        {
            Class type = Type;
            if (Value is ReferenceValue)
            {
                var refValue = Value as ReferenceValue;
                return refValue.Value;
            }
            if (type == JavaString)
                return $@"""{Value.ToString()}""";
            else if (type == JavaBoolean )
                return  Value!= null ? (((bool)Value) ? "true" : "false") : "";
            else if (type == JavaBooleanPrimitive)
                return ((bool)Value) ? "true" : "false";
            else if (type == JavaList)
            {
                var ilist = (IList)Value;
                if (ilist.Count > 0)
                {
                    var res = "{ ";
                    foreach (var item in ilist)
                    {
                        res += item.ToString() + ", ";
                    }
                    res = res.Remove(res.Length - 2) + " }";
                    return res.Replace("  ", " ");
                }
                return "{}";
            }

            return Value.ToString();
        }
        public static ClassMember CreatePublicStaticFinalString(string name, string value) =>
            new ClassMember(name, JavaString, value, AccessModifier.Public, NonAccessModifier.Static, NonAccessModifier.Final);

        public static ClassMember CreatePublicStaticFinalInt(string name, int value) =>
            new ClassMember(name, JavaIntPrimitive, value, AccessModifier.Public, NonAccessModifier.Static, NonAccessModifier.Final);
    }

    public class Class
    {
        public Class BaseClass { get; set; } = JavaObject;
        public Package Package { get; set; }
        public bool Nullable { get; set; } = true;
        public object DefaultValue { get; set; } = null;
        public AccessModifier AccessModifier { get; set; } = AccessModifier.Public;
        public List<NonAccessModifier> NonAccessModifiers { get; set; } = new List<NonAccessModifier>();
        public string Name { get; set; }
        public string Comment { get; set; }

        public List<ClassMember> ClassMembers { get; set; } = new List<ClassMember>();
        public List<Annotation> Annotations { get; set; } = new List<Annotation>();

        public ClassMember GetStaticMember(string name)
        {
            return ClassMembers.FirstOrDefault(x => x.NonAccessModifiers.Contains(NonAccessModifier.Static) && x.Name == name);
        }

        public ClassMember GetStaticMemberByValue(object value)
        {
            return ClassMembers.FirstOrDefault(x => x.NonAccessModifiers.Contains(NonAccessModifier.Static) && x.Value == value);
        }


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

        public virtual string ToJavaFile()
        {
            var imports = "\r\n\r\n";
            if (BaseClass != null && BaseClass.Package.ToString() != Package.ToString())
                imports += $"import {BaseClass.Package};" + "\r\n";
            imports += Annotations.Count > 0 ? Annotations.Select(x => $"import {x.Package};" + "\r\n").Aggregate((x, y) => x + y) : "";

            if (imports.Contains(Package.DefaultPackage.ToString()))
            {
                imports = imports.Replace($"import {Package.DefaultPackage};", "");
            }


            var res = $@"package {Package};{imports}
{(Annotations.Count > 0 ? "\r\n" + Annotations.Select(x => x.ToString()).Aggregate((x, y) => x + "\r\n" + y) : "")}
{AccessModifier.ToString().ToLower()}{(NonAccessModifiers.Count == 0 ? "" : " " + NonAccessModifiers.Select(x => x.ToString().ToLower()).Aggregate((x, y) => x + " " + y))} class {Name} {(BaseClass != null && BaseClass != JavaObject ? "extends " + BaseClass.Name + " " : "")}{{
    
{(ClassMembers.Count > 0 ? ClassMembers.Select(x => "\t" + x.GetDeclaration() + "\r\n").Aggregate((x, y) => x + y) : "")}

}}";
            return res.Replace("\n\r\n\r", "\n\r");
        }


        public static bool operator ==(Class c1, Class c2)
        {
            return c1.Name == c2?.Name;
        }

        public static bool operator !=(Class c1, Class c2)
        {
            return c1.Name != c2?.Name;
        }
    }
}
