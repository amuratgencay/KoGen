﻿using KoGen.Extentions;
using KoGen.Models.DatabaseModels;
using static KoGen.Models.DataModels.ReferenceValue;
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
        public ReferenceValue TableSequenceRef => ClassMembers.Any(x => x.Name == "TABLE_SEQ_NAME") ? FromStaticMember(this, "TABLE_SEQ_NAME") : null;
        public List<UniqueConstraintClass> UniqueConstraintsRef => Table.UniqueContraints.Select(x => new UniqueConstraintClass { Name = FromStaticMemberByValue(this, $"{x.Name}"), ColumnList = x.Columns.Select(z => GetColumn($"{z.Name}")).ToList() }).ToList();
        public ReferenceValue GetColumn(string name) => FromStaticMemberByValue(this, name);

        public EntityConstraintsClass(Table table, string module)
        {
            Table = table;
            Name = Table.SafeName.ToPascalCase() + "Constraints";
            Package = $@"havelsan.kovan.{module}.common.constraints";
            NonAccessModifiers = new List<NonAccessModifier> { NonAccessModifier.Abstract };

            ClassMembers.Add(ClassMember.CreatePublicStaticFinalString("TABLE_NAME", Table.Name));

            if (Table.Sequence != null)
                ClassMembers.Add(ClassMember.CreatePublicStaticFinalString("SEQ_NAME", Table.Sequence.Name));

            ClassMembers.AddRange(Table.Columns.Select(x => ClassMember.CreatePublicStaticFinalString($@"COLUMN_{x.Name}", x.Name)));
            ClassMembers.AddRange(Table.UniqueContraints.Select(x => ClassMember.CreatePublicStaticFinalString($@"{(x.Name.StartsWith("UX_") ? x.Name : "UX_" + x.Name)}", x.Name)));
            ClassMembers.AddRange(Table.Columns.Where(x => x.Size != null).SelectMany(x =>
            {
                var list = new List<ClassMember>();
                if (x.Size.Min.HasValue)
                    list.Add(ClassMember.CreatePublicStaticFinalInt($@"{x.Name}_SIZE_MIN", x.Size.Min.Value));
                if (x.Size.Max.HasValue)
                    list.Add(ClassMember.CreatePublicStaticFinalInt($@"{x.Name}_SIZE_MAX", x.Size.Max.Value));
                return list;
            }));

        }


    }
}
