using KoGen.Extentions;
using static KoGen.Models.DataModels.Predefined.PredefinedClasses;
using static KoGen.Extentions.StringExtentions;
using System;
using KoGen.Models.DataModels.Expressions;

namespace KoGen.Models.DataModels.Functions
{
    [Serializable]
    public class SetterFunction : Function
    {
        public ClassMember ClassMember { get; set; }
        public SetterFunction(ClassMember classMember) : base(classMember.Owner, $"set{classMember.Name.ToUpperFirstCharacter()}", JavaVoid)
        {
            ClassMember = classMember;
            var fp = new FunctionParameter(classMember.Name, ClassMember.Type);
            FunctionParameters.Add(fp);
            Expressions.Add(new AssignExpression { Destination = classMember, Source = fp });
        }
    }
}
