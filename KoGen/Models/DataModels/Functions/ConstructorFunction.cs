using static KoGen.Models.DataModels.Predefined.PredefinedClasses;
using static KoGen.Extentions.StringExtentions;
using System;
using KoGen.Models.DataModels.Expressions;

namespace KoGen.Models.DataModels.Functions
{
    [Serializable]
    public class ConstructorFunction : Function
    {
        public Class Class { get; set; }

        public ConstructorFunction(Class clazz) : base(clazz, $"{clazz.Name}", JavaVoid)
        {
            Class = clazz;
            Class.ClassMembers.ClassMembers.ForEach(x =>
            {
                var fp = new FunctionParameter(x.Name, x.Type);
                FunctionParameters.Add(fp);
                Expressions.Add(new AssignExpression { Destination = x, Source = fp });
            });
            
        }

        public override string ToString()
        {
            return $"{AnnotationsString}{AccessModifierString}{GenericString} {Name}({ParametersString})"
                + $"{NewLineTab}{{"
                + $"{BodyString}"
                + $"{NewLineTab}}}"
                + $"{NewLineTab}";
        }
    }
}
