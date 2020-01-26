using KoGen.Extentions;
using KoGen.Models.DatabaseModels;
using static KoGen.Models.DataModels.ReferenceValue;
using static KoGen.Extentions.NullableExtentions;
using static KoGen.Models.DataModels.ClassMember;
using KoGen.Models.DatabaseModels.ConstraintModels;
using KoGen.Models.DataModels;
using KoGen.Models.DataModels.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoGen
{
    public class UniqueConstraintClass
    {
        public ReferenceValue Name { get; set; }
        public List<ReferenceValue> ColumnList { get; set; }
    }
    public class EntityConstraintsClass : Class
    {
        public Table Table { get; set; }
        public string TableName => Table.SafeName;
        public string TableSchema => Table.Schema;
        public ReferenceValue TableNameRef => FromStaticMember(this, "TABLE_NAME");
        public ReferenceValue TableSequenceRef => FromStaticMember(this, "TABLE_SEQ_NAME");
        public List<UniqueConstraintClass> UniqueConstraintsRef => Table.UniqueContraints.Select(x => new UniqueConstraintClass { Name = FromStaticMemberByValue(this, $"{x.Name}"), ColumnList = x.Columns.Select(z => GetColumnByValue($"{z.Name}")).ToList() }).ToList();
        public ReferenceValue GetColumnByValue(string value) => FromStaticMemberByValue(this, value);
        public ReferenceValue GetColumnByName(string name) => FromStaticMember(this, name);
        public ReferenceValue GetColumnSizeMax(Column column) => GetColumnByName($"{column.Name}_SIZE_MAX");
        public ReferenceValue GetColumnSizeMin(Column column) => GetColumnByName($"{column.Name}_SIZE_MIN");
        public ReferenceValue GetColumnName(Column column) => GetColumnByName($"COLUMN_{column.Name}");
        public EntityConstraintsClass(Table table, string module)
        {
            Table = table;
            Name = Table.SafeName.ToPascalCase() + "Constraints";
            Package = $@"havelsan.kovan.{module}.common.constraints";
            NonAccessModifiers = new List<NonAccessModifier> { NonAccessModifier.Abstract };

            ClassMembers.Add(CreatePublicStaticFinalString("TABLE_NAME", Table.Name));
            IfPresent(Table.Sequence, ts => CreatePublicStaticFinalString("SEQ_NAME", ts.Name));

            ClassMembers
                .AddRange(Table.Columns.Select(x => CreatePublicStaticFinalString($@"COLUMN_{x.Name}", x.Name)))
                .AddRange(Table.UniqueContraints.Select(x => CreatePublicStaticFinalString($@"{(x.Name.StartsWith("UX_") ? x.Name : "UX_" + x.Name)}", x.Name)))
                .AddRange(Table.Columns.Where(x => x.Size != null).SelectMany(x =>
                {
                    var list = new List<ClassMember>();
                    x.Size.Min.IfPresent(min => list.Add(CreatePublicStaticFinalInt($@"{x.Name}_SIZE_MIN", min)));
                    x.Size.Max.IfPresent(max => list.Add(CreatePublicStaticFinalInt($@"{x.Name}_SIZE_MAX", max)));
                    return list;
                }));

        }


    }
}
