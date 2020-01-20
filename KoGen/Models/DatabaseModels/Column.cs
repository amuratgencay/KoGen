using KoGen.Models.DatabaseModels.ConstraintModels;
using System;
using System.Collections.Generic;

namespace KoGen.Models.DatabaseModels
{
    public class Size
    {
        public int? Min { get; set; }
        public int? Max { get; set; }
    }
    public class Column
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public Type Type { get; set; }
        public Size Size { get; set; }
        public bool Nullable { get; set; }
        public Sequence Sequence { get; set; }
        public virtual Table Table { get; set; }
        public virtual List<ConstraintBase> Constraints { get; set; } = new List<ConstraintBase>();
    }

}
