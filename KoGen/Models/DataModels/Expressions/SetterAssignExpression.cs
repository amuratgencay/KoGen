using KoGen.Models.DataModels.Functions;
using System;
using System.Linq;

namespace KoGen.Models.DataModels.Expressions
{
    [Serializable]
    public class SetterAssignExpression : Expression
    {
        public FunctionParameter Source { get; set; }
        public ClassMember SourceClassMember { get; set; }
        public FunctionParameter Destination { get; set; }
        public ClassMember DestinationClassMember { get; set; }


        public override string ToString()
        {
            
            return $"{Destination.Name}.{DestinationClassMember.SetterFunction.Name}({Source.Name}.{SourceClassMember.GetterFunction.Name}());";
        }
    }
}
