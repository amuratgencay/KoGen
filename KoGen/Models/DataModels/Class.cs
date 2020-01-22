﻿using System.Collections.Generic;
using System.Linq;

namespace KoGen.Models.DataModels
{
    public class ClassInstance
    {
        public AccessModifier AccessModifier { get; set; }
        public List<NonAccessModifier> NonAccessModifiers { get; set; } = new List<NonAccessModifier>();
        public List<Annotation> Annotations { get; set; }
        public string Name { get; set; }
        public Class Owner { get; set; }
        public Class Type { get; set; }
        public object Value { get; set; }

        public string GetDeclaration()
        {
            var res = $@"{AccessModifier.ToString().ToLower()} {NonAccessModifiers.Select(x => x.ToString().ToLower()).Aggregate((x, y) => x + " " + y)} {Type} {Name}{(Value != null ? $@" = {DataType.AssingString(Value)}" : "")};";
            return res;
        }
    }
    public class Class
    {
        public Class BaseClass { get; set; } = PredefinedClasses.PredefinedClass.JavaObject; 
        public Package Package { get; set; }
        public bool Nullable { get; set; } = true;
        public AccessModifier AccessModifier { get; set; } = AccessModifier.Public;
        public List<NonAccessModifier> NonAccessModifiers { get; set; } = new List<NonAccessModifier>();
        public string Name { get; set; }
        public List<ClassInstance> ClassMembers { get; set; } = new List<ClassInstance>();
        public bool Instantiated { get; private set; }
        public List<Annotation> Annotations { get; set; } = new List<Annotation>();
        public virtual string ToJavaFile()
        {
            var imports = "\r\n\r\n";
            if(BaseClass!=null && BaseClass.Package.ToString() != Package.ToString())
                imports += $"import {BaseClass.Package};" + "\r\n" ;
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
    }
}
