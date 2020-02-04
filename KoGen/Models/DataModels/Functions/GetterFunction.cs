using KoGen.Extentions;
using static KoGen.Extentions.StringExtentions;
using System;
using KoGen.Models.DataModels.Expressions;

namespace KoGen.Models.DataModels.Functions
{
    [Serializable]
    public class GetterFunction : Function
    {
        public ClassMember ClassMember { get; set; }

        public GetterFunction(ClassMember classMember) : base(classMember.Owner, $"get{classMember.Name.ToUpperFirstCharacter()}", classMember.Type)
        {
            ClassMember = classMember;
            Expressions.Add(new ReturnExpression { ClassMember = classMember });
        }
    }
}
