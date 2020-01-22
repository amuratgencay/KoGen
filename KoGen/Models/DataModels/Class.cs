using KoGen.Extentions;
using KoGen.Models.DataModels.Predefined;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KoGen.Models.DataModels
{
    public class ClassMember
    {
        public AccessModifier AccessModifier { get; set; }
        public List<NonAccessModifier> NonAccessModifiers { get; set; } = new List<NonAccessModifier>();
        public List<Annotation> Annotations { get; set; }
        public bool Initialized { get; private set; }
        public string Name { get; set; }
        public Class Owner { get; set; }
        public Class Type { get; set; }
        public object Value { get; set; }
        public string Comment { get; set; }

        public string GetDeclaration()
        {
            var res = $@"{AccessModifier.ToString().ToLower()} {NonAccessModifiers.Select(x => x.ToString().ToLower()).Aggregate((x, y) => x + " " + y)} {Type.Name} {Name}{(Value != null ? $@" = {AssingString(Value)}" : "")};";
            return res;
        }

        private string AssingString(object value)
        {
            if (Type == PredefinedClasses.JavaString)
                return $@"""{value.ToString()}""";
            else if (Type == PredefinedClasses.JavaBoolean || Type == PredefinedClasses.JavaBooleanPrimitive)
                return ((bool)value) ? "true" : "false";
            else if (Type == PredefinedClasses.JavaList)
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

        public static ClassMember CreatePublicStaticFinalString(string name, string value)
        {
            var res = new ClassMember();
            res.Name = name;
            res.Type = PredefinedClasses.JavaString;
            res.Value = value;
            res.AccessModifier = AccessModifier.Public;
            res.NonAccessModifiers = new List<NonAccessModifier> { NonAccessModifier.Static, NonAccessModifier.Final };
            res.Initialized = true;            
            return res;
        }
    }
    public class Class
    {
        public Class BaseClass { get; set; } = PredefinedClasses.JavaObject;
        public Package Package { get; set; }
        public bool Nullable { get; set; } = true;
        public object DefaultValue { get; set; } = null;
        public AccessModifier AccessModifier { get; set; } = AccessModifier.Public;
        public List<NonAccessModifier> NonAccessModifiers { get; set; } = new List<NonAccessModifier>();
        public string Name { get; set; }
        public string Comment { get; set; }

        public List<ClassMember> ClassMembers { get; set; } = new List<ClassMember>();
        public List<Annotation> Annotations { get; set; } = new List<Annotation>();

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
            imports += Annotations.Select(x => $"import {x.Package};" + "\r\n").Aggregate((x, y) => x + y);
            if (imports.Length > 4)
                imports = imports.Remove(imports.Length - 2);
            var res = $@"package {Package};{imports}
{(Annotations.Count > 0 ? "\r\n" + Annotations.Select(x => x.ToString()).Aggregate((x, y) => x + "\r\n" + y) : "")}
{AccessModifier.ToString().ToLower()}{(NonAccessModifiers.Count == 0 ? "" : " " + NonAccessModifiers.Select(x => x.ToString().ToLower()).Aggregate((x, y) => x + " " + y))} class {Name} {(BaseClass != null ? "extends " + BaseClass.Name + " " : "")}{{
    
{(ClassMembers.Count > 0 ? ClassMembers.Select(x => "\t" + x.GetDeclaration() + "\r\n").Aggregate((x, y) => x + y) : "")}

}}";
            return res;
        }

        public static bool operator ==(Class c1, Class c2)
        {
            return c1.Name == c2.Name;
        }

        public static bool operator !=(Class c1, Class c2)
        {
            return c1.Name != c2.Name;
        }
    }
}
