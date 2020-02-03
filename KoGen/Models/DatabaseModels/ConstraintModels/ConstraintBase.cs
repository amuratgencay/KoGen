using System;

namespace KoGen.Models.DatabaseModels.ConstraintModels
{
    [Serializable]
    public abstract class ConstraintBase
    {
        public string Name { get; set; }
        public virtual Table Table { get; set; }

    }

}
