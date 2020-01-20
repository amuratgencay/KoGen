using System.Collections.Generic;

namespace KoGen.Models.DatabaseModels.ConstraintModels
{
    public class PrimaryKey : ConstraintBase { public List<Column> Columns { get; set; } = new List<Column>(); }

}
