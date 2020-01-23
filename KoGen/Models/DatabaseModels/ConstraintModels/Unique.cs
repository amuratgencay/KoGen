using System.Collections.Generic;

namespace KoGen.Models.DatabaseModels.ConstraintModels
{
    public class Unique : ConstraintBase { 
        public List<Column> Columns { get; set; } = new List<Column>();
        public override bool Equals(object obj)
        {
            if (obj is Unique)
                return (obj as Unique).Name == Name;

            return false;
        }

        public override int GetHashCode()
        {
            return -1952516548 + EqualityComparer<List<Column>>.Default.GetHashCode(Columns);
        }
    }

}
