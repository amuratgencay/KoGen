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
            ClassMembers.AddRange(Table.UniqueContraints.Select(x => DataModel.CreatePublicStaticFinalString($@"{(x.Name.StartsWith("UX_") ? x.Name : "UX_" + x.Name)}", x.Name)));
            ClassMembers.AddRange(Table.Columns.Where(x => x.Size != null).SelectMany(x =>
            {
                var list = new List<DataModel>();
                if (x.Size.Min.HasValue)
                    list.Add(DataModel.CreatePublicStaticFinalInt($@"{x.Name}_SIZE_MIN", x.Size.Min.Value));
                if (x.Size.Max.HasValue)
                    list.Add(DataModel.CreatePublicStaticFinalInt($@"{x.Name}_SIZE_MAX", x.Size.Max.Value));
                return list;
            }));

        }


    }
    //    public class EntityConstraints
    //    {
    //        public string Name => Table.SafeName.ToPascalCase() + "Constraints";
    //        public Table Table { get; set; }
    //        public string Package => $@"havelsan.kovan.{Table.Schema.ToLower(System.Globalization.CultureInfo.InvariantCulture).Replace("kovan_", "")}.common.constraints";
    //        public override string ToString()
    //        {
    //            var unique = "";
    //            if (Table.Columns.Any(x => x.Constraints.Any(y => y is Unique)))
    //            {
    //                unique = Table.Columns.SelectMany(x => x.Constraints.Where(y => y is Unique).Select(z => "\tpublic static final String " + (z as Unique).Name + $@" = ""{(z as Unique).Name}"";" + "\r\n")).Distinct().Aggregate((x, y) => x + y);
    //            }
    //            var result = $@"
    //package {Package};

    //public abstract class {Table.SafeName.ToPascalCase()}Constraints {{

    //            public static final String TABLE_NAME = ""{Table.Name}"";
    //            public static final String TABLE_SEQ_NAME = ""{Table.Columns.FirstOrDefault(x => x.Sequence != null)?.Sequence.Name ?? Table.Name}_SEQ"";

    //{Table.Columns.Select(x => "\tpublic static final String COLUMN_" + x.Name + $@" = ""{x.Name}"";" + "\r\n").Aggregate((x, y) => x + y)}

    //{Table.Columns.Where(x => x.Size.HasValue).Select(x => "\tpublic static final int " + x.Name + $@"_SIZE_MAX = {x.Size.Value};" + "\r\n").Aggregate((x, y) => x + y)}

    //{unique}

    //}}
    //";
    //            return result;
    //        }
    //    }
}
