using KoGen.Models.DataModels.Functions;
using System;

namespace KoGen.Models.DataModels.Expressions
{
    [Serializable]
    public class AssignExpression : Expression
    {
        public FunctionParameter Source { get; set; }
        public ClassMember Destination { get; set; }

        public override string ToString()
        {
            return $"this.{Destination.Name} = {Source.Name};";
        }
    }
}
