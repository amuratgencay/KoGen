using KoGen.Extentions;
using KoGen.Models.DatabaseModels;
using KoGen.Models.DatabaseModels.ConstraintModels;
using KoGen.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoGen
{
    public class EntityConstraintsClass : Class
    {
        public Table Table { get; set; }

        public EntityConstraintsClass(Table table, string module)
        {
            Table = table;

            Name = Table.SafeName.ToPascalCase() + "Constraints";
            Package = $@"havelsan.kovan.{module}.common.constraints";
            AccessModifier = AccessModifier.Public;
            NonAccessModifiers = new List<NonAccessModifier> { NonAccessModifier.Abstract };


            ClassMembers.Add(DataModel.CreatePublicStaticFinalString("TABLE_NAME", Table.Name));

            if (Table.Sequence != null)
                ClassMembers.Add(DataModel.CreatePublicStaticFinalString("SEQ_NAME", Table.Sequence.Name));

            ClassMembers.AddRange(Table.Columns.Select(x => DataModel.CreatePublicStaticFinalString($@"COLUMN_{x.Name}", x.Name)));
            ClassMembers.AddRange(Table.UniqueContraints.Select(x => DataModel.CreatePublicStaticFinalString($@"UX_{x.Name}", x.Name)));
            ClassMembers.AddRange(Table.Columns.Where(x => x.Size.HasValue).Select(x => DataModel.CreatePublicStaticFinalInt($@"{x.Name}_SIZE_MAX", x.Size.Value)));

        }
    }
}
