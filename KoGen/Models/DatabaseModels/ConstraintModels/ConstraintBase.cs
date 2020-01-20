namespace KoGen.Models.DatabaseModels.ConstraintModels
{
    public abstract class ConstraintBase
    {
        public string Name { get; set; }
        public virtual Table Table { get; set; }

    }

}
