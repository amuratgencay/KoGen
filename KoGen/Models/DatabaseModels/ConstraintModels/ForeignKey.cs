namespace KoGen.Models.DatabaseModels.ConstraintModels
{
    public class ForeignKey : ConstraintBase
    {
        public Column Column { get; set; }
        public Column ReferenceColumn { get; set; }
    }

}
