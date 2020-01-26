using KoGen.Models.DatabaseModels.ConstraintModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoGen.Models.DatabaseModels
{
    public class Table
    {
        public string Schema { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Comment { get; set; }
        public List<Column> Columns { get; set; } = new List<Column>();
        public string Prefix => Name.Substring(0, Name.IndexOf("_"));
        public string SafeName => Name.Substring(Name.IndexOf("_"));
        public Sequence Sequence => Columns.FirstOrDefault(x => x.Sequence != null)?.Sequence ?? null;
        public List<ConstraintBase> Contraints => Columns.Any(x => x.Constraints.Any()) ? Columns.SelectMany(x => x.Constraints).ToList() : new List<ConstraintBase>();
        public List<Unique> UniqueContraints => Contraints?.Where(x => x is Unique).Select(x => x as Unique).Distinct().ToList() ?? new List<Unique>();
        public List<ForeignKey> ForeingKeyContraints => Contraints?.Where(x => x is ForeignKey).Select(x => x as ForeignKey).ToList() ?? new List<ForeignKey>();
        public PrimaryKey PrimaryKeyContraints => Contraints?.Where(x => x is PrimaryKey).Select(x => x as PrimaryKey).FirstOrDefault();
    }

}
