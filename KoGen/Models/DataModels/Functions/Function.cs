using KoGen.Extentions;
using static KoGen.Models.DataModels.Predefined.PredefinedClasses;
using static KoGen.Extentions.StringExtentions;
using System;
using System.Collections.Generic;
using System.Linq;
using KoGen.Models.DataModels.Enum;
using static KoGen.Models.DataModels.Enum.AccessModifier;
using KoGen.Models.DataModels.Expressions;

namespace KoGen.Models.DataModels.Functions
{
    [Serializable]
    public class Function
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public AccessModifier AccessModifier { get; set; } = Private;
        public List<NonAccessModifier> NonAccessModifiers { get; set; } = new List<NonAccessModifier>();
        public List<Annotation> Annotations { get; set; } = new List<Annotation>();
        public Class ReturnType { get; set; }
        public Class Owner { get; set; }
        public List<FunctionParameter> FunctionParameters { get; set; }
        public List<Expression> Expressions { get; set; }
        public Function(Class owner, string name, Class returnType, AccessModifier accessModifier = Public, params NonAccessModifier[] nonAccessModifiers)
        {
            Owner = owner;
            Name = name;
            ReturnType = returnType ?? JavaVoid;
            AccessModifier = accessModifier;
            NonAccessModifiers = nonAccessModifiers?.ToList() ?? new List<NonAccessModifier>();
            FunctionParameters = new List<FunctionParameter>();
            Expressions = new List<Expression>();
        }

        protected string AnnotationsString =>
            Annotations
            .Aggregate(x => x.ToString(), NewLineTab, "", NewLineTab);

        protected string AccessModifierString =>
            AccessModifier
            .ToString()
            .ToLower();

        protected string NonAccessModifiersString =>
            NonAccessModifiers
            .Aggregate(x => x.ToString().ToLower(), " ", " ");

        protected string ParametersString =>
            FunctionParameters.Aggregate(x => x.ToString(), ", ");

        protected string GenericString =>
            ReturnType.GenericList
            .Aggregate(x => x.Name, ", ", "<", ">");
        protected string BodyString => Expressions.Aggregate(x => $"{x}", NewLineDoubleTab, NewLineDoubleTab);
        public override string ToString()
        {
            return $"{AnnotationsString}{AccessModifierString}{NonAccessModifiersString} {ReturnType.Name}{GenericString} {Name}({ParametersString})"
                + $"{NewLineTab}{{"
                + $"{BodyString}"
                + $"{NewLineTab}}}"
                + $"{NewLineTab}";
        }
    }
}
