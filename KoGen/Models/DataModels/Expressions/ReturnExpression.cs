using System;

namespace KoGen.Models.DataModels.Expressions
{
    [Serializable]
    public class ReturnExpression : Expression
    {
        public ClassMember ClassMember { get; set; }
        public override string ToString()
        {
            return $"return this.{ClassMember.Name};";
        }
    }
}
