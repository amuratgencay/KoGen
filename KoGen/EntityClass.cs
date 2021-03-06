﻿using KoGen.Extentions;
using static KoGen.Models.DataModels.Predefined.PredefinedAnnotations;
using static KoGen.Models.DataModels.Predefined.ValidationAnnotations;
using static KoGen.Extentions.NullableExtentions;
using KoGen.Models.DatabaseModels;
using KoGen.Models.DataModels;
using KoGen.Models.DataModels.Predefined;
using System.Linq;
using KoGen.Models.DataModels.Enum;
using System;

namespace KoGen
{
    [Serializable]
    public class EntityClass : Class
    {
        public EntityConstraintsClass EntityConstraints { get; set; }
        public EntityClass(EntityConstraintsClass eConsts, string module)
        {
            EntityConstraints = eConsts;
            Name = eConsts.TableName.ToPascalCase() + "Entity";
            Package = $@"havelsan.kovan.{module}.service.entity";

            BaseClass = PredefinedClasses.HvlEntity;

            Annotations.Add(Entity());

            Annotations.Add(Table()
                .SetParameter("name", eConsts.TableNameRef)
                .SetParameter("schema", eConsts.TableSchema)
                .SetParameter("indexes", eConsts.UniqueConstraintsRef.Select(u =>
                    Index()
                        .SetParameter("name", u.Name)
                        .SetParameter("unique", true)
                        .SetParameter("columnList", u.ColumnList)).ToList()

                ));

            IfPresent(eConsts.TableSequenceRef, ts => Annotations.Add(HvlEntitySequence().SetParameter("name", ts)));
            
            ClassMembers.AddRange(eConsts.Table.Columns.Select(col =>
            {
                var cm = new ClassMember(col.Name.ToCamelCase(), col.Type.ToJavaType(), null, AccessModifier.Private);
                cm.Annotations.Add(Column().SetParameter("name", eConsts.GetColumnName(col)));
                cm.Annotations.AddIfTrue(!col.Nullable, NotNull());
                
                IfPresent(col.Size, size =>
                {
                    if (cm.Type.BaseClass == PredefinedClasses.JavaNumber)
                    {
                        var da = Digits();
                        size.Max.IfPresent(max => da.SetParameter("integer", eConsts.GetColumnSizeMax(col)));
                        size.Min.IfPresent(min => da.SetParameter("fraction", eConsts.GetColumnSizeMin(col)));
                        cm.Annotations.Add(da);
                    }
                    else
                    {
                        var sa = Size();
                        size.Min.IfPresent(min => sa.SetParameter("min", eConsts.GetColumnSizeMin(col)));
                        size.Max.IfPresent(max => sa.SetParameter("max", eConsts.GetColumnSizeMax(col)));
                        cm.Annotations.Add(sa);
                    }
                });
                return cm;
            }));
        }
    }

    //    public class Entity
    //    {
    //        public string Name => Table.SafeName.ToPascalCase() + "Entity";
    //        public Table Table { get; set; }
    //        public string Package => $@"package havelsan.kovan.{Table.Schema.ToLowerEn().Replace("kovan_", "")}.service.entity;";

    //        public string GetImports()
    //        {
    //            var res = "import javax.persistence.*;\r\n";
    //            res += "import tr.com.havelsan.nf.domain.model.entity.HvlEntity;\r\n";
    //            if (Table.Columns.Any(x => x.Sequence != null))
    //                res += "import tr.com.havelsan.nf.hibernate.annotations.HvlEntitySequence;\r\n";
    //            if (Table.Columns.Any(x => x.Size != null))
    //                res += "import javax.validation.constraints.Size;\r\n";
    //            if (Table.Columns.Any(x => !x.Nullable))
    //                res += "import javax.validation.constraints.NotNull;\r\n";

    //            res += $@"import havelsan.kovan.{Table.Schema.ToLowerEn().Replace("kovan_", "")}.common.constraints.{Table.SafeName.ToPascalCase()}Constraints.*;" + "\r\n";

    //            if (Table.Columns.Any(x => x.Constraints.Any(y => y is ForeignKey)))
    //                foreach (var fk in Table.Columns.SelectMany(x => x.Constraints.Where(y => y is ForeignKey && (y as ForeignKey).Column.Name == x.Name).Select(z => z as ForeignKey)))
    //                {
    //                    res += $@"import havelsan.kovan.{Table.Schema.ToLowerEn().Replace("kovan_", "")}.common.constraints.{fk.ReferenceColumn.Table.SafeName.ToPascalCase()}Constraints;" + "\r\n";
    //                }
    //            return res;
    //        }
    //        public string GetColumns()
    //        {
    //            var res = "";
    //            var decList = new List<Tuple<string, string>>();
    //            foreach (var col in Table.Columns.Where(x => !x.Constraints.Any(y => (y is ForeignKey) && ((y as ForeignKey).Column.Name == x.Name))))
    //            {
    //                var column = "\t" + $@"@Column(name = COLUMN_{col.Name})" + "\r\n";
    //                var constraints = "";
    //                var declaration = "\t" + $@"private " + col.Type.ToJavaType() + " " + col.Name.ToCamelCase() + ";\r\n";
    //                decList.Add(new Tuple<string, string>(col.Type.ToJavaType(), col.Name));

    //                if (col.Size != null)
    //                    constraints += "\t@Size(" + (col.Size.Min.HasValue ? "min = " + col.Name + "_SIZE_MIN" + (col.Size.Max.HasValue ? ", " : "") : "") + (col.Size.Max.HasValue ? " max = " + col.Name + "_SIZE_MAX" : "") + ")\r\n";
    //                if (!col.Nullable)
    //                    constraints += "\t@NotNull\r\n";

    //                res += column + constraints + declaration + "\r\n";

    //            }

    //            foreach (var col in Table.Columns.Where(x => x.Constraints.Any(y => (y is ForeignKey) && ((y as ForeignKey).Column.Name == x.Name))))
    //            {



    //                var column = "\t" + $@"@Column(name = COLUMN_{col.Name})" + "\r\n";
    //                var constraints = "";
    //                var declaration = "\t" + $@"private " + col.Type.ToJavaType() + " " + col.Name.ToCamelCase() + ";\r\n";
    //                decList.Add(new Tuple<string, string>(col.Type.ToJavaType(), col.Name));

    //                if (col.Size != null)
    //                    constraints += "\t@Size(" + (col.Size.Min.HasValue ? "min = " + col.Name + "_SIZE_MIN" + (col.Size.Max.HasValue ? ", " : "") : "") + (col.Size.Max.HasValue ? " max = " + col.Name + "_SIZE_MAX" : "") + ")\r\n";

    //                if (!col.Nullable)
    //                    constraints += "\t@NotNull\r\n";

    //                res += column + constraints + declaration + "\r\n";

    //            }

    //            res += "\r\n";


    //            foreach (var decl in decList)
    //            {
    //                res += "\tpublic " + decl.Item1 + " get" + decl.Item2.ToPascalCase() + $@"() {{ return {decl.Item2.ToCamelCase()}; }}" + "\r\n";
    //                res += "\tpublic " + decl.Item1 + " set" + decl.Item2.ToPascalCase() + $@"({decl.Item1} {decl.Item2.ToCamelCase()}) {{ this.{decl.Item2.ToCamelCase()} = {decl.Item2.ToCamelCase()}; }}" + "\r\n";
    //                res += "\r\n";
    //            }
    //            return res;
    //        }
    //        //Table.Columns.Select(x => "\t" + $@"private " + x.Type.ToJavaType() + " " + x.Name.ToCamelCase() + ";\r\n").Aggregate((x, y) => x + y)
    //        public override string ToString()
    //        {
    //            var result = "";
    //            result += GetImports();

    //            result += $@"
    //@Entity
    //@Table(schema = ""{Table.Schema}"", name = TABLE_NAME){ (Table.Columns.Any(x => x.Sequence != null) ? "\r\n@HvlEntitySequence(name = TABLE_SEQ_NAME)" : "")}
    //public class {Table.SafeName.ToPascalCase()}Entity extends HvlEntity {{

    //{GetColumns()}        


    //}}
    //";
    //            return result;
    //        }
    //    }
}
