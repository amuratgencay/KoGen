﻿using KoGen.Extentions;
using KoGen.Models.DatabaseModels;
using KoGen.Models.DatabaseModels.ConstraintModels;
using KoGen.Models.DataModels;
using KoGen.Models.DataModels.PredefinedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoGen
{
    public class EntityClass : Class
    {
        public Table Table { get; set; }
        public EntityClass(Table table, string module, EntityConstraintsClass entityConstraints)
        {
            Table = table;

            Name = Table.SafeName.ToPascalCase() + "Entity";
            Package = $@"havelsan.kovan.{module}.service.entity";
            AccessModifier = AccessModifier.Public;

            BaseClass = PredefinedClass.HvlEntity;

            Annotations.Add(DataAnnotations.Entity());

            var tableAnnotation = DataAnnotations.Table();
            tableAnnotation.Parameters["name"].Value = new ReferenceValue(entityConstraints.ClassMembers.FirstOrDefault(x => x.Name.Equals("TABLE_NAME")).Name);
            tableAnnotation.Parameters["schema"].Value = table.Schema;
            tableAnnotation.Parameters["indexes"].Value = table.UniqueContraints.Select(u =>
            {
                var indexAnnotation = DataAnnotations.Index();
                indexAnnotation.Parameters["name"].Value = new ReferenceValue(entityConstraints.ClassMembers.FirstOrDefault(x => x.Value.ToString() == u.Name).Name);
                indexAnnotation.Parameters["columnList"].Value = new ReferenceValue("{" + u.Columns.Select(x => entityConstraints.ClassMembers.FirstOrDefault(y => y.Value.ToString() == x.Name).Name).Aggregate((x, y) => x + ", " + y) + "}");
                indexAnnotation.Parameters["unique"].Value = true;
                return indexAnnotation;
            }).ToList();
            Annotations.Add(tableAnnotation);

            if (entityConstraints.ClassMembers.Any(x => x.Name.StartsWith("SEQ_")))
            {
                var sequenceAnnatation = DataAnnotations.HvlEntitySequence();
                sequenceAnnatation.Parameters["name"].Value = new ReferenceValue(entityConstraints.ClassMembers.FirstOrDefault(x => x.Name.StartsWith("SEQ_")).Name);
                Annotations.Add(sequenceAnnatation);
            }
            var str = ToJavaFile();
        }
    }

    public class Entity
    {
        public string Name => Table.SafeName.ToPascalCase() + "Entity";
        public Table Table { get; set; }
        public string Package => $@"package havelsan.kovan.{Table.Schema.ToLower(System.Globalization.CultureInfo.InvariantCulture).Replace("kovan_", "")}.service.entity;";

        public string GetImports()
        {
            var res = "import javax.persistence.*;\r\n";
            res += "import tr.com.havelsan.nf.domain.model.entity.HvlEntity;\r\n";
            if (Table.Columns.Any(x => x.Sequence != null))
                res += "import tr.com.havelsan.nf.hibernate.annotations.HvlEntitySequence;\r\n";
            if (Table.Columns.Any(x => x.Size != null))
                res += "import javax.validation.constraints.Size;\r\n";
            if (Table.Columns.Any(x => !x.Nullable))
                res += "import javax.validation.constraints.NotNull;\r\n";

            res += $@"import havelsan.kovan.{Table.Schema.ToLower(System.Globalization.CultureInfo.InvariantCulture).Replace("kovan_", "")}.common.constraints.{Table.SafeName.ToPascalCase()}Constraints.*;" + "\r\n";

            if (Table.Columns.Any(x => x.Constraints.Any(y => y is ForeignKey)))
                foreach (var fk in Table.Columns.SelectMany(x => x.Constraints.Where(y => y is ForeignKey && (y as ForeignKey).Column.Name == x.Name).Select(z => z as ForeignKey)))
                {
                    res += $@"import havelsan.kovan.{Table.Schema.ToLower(System.Globalization.CultureInfo.InvariantCulture).Replace("kovan_", "")}.common.constraints.{fk.ReferenceColumn.Table.SafeName.ToPascalCase()}Constraints;" + "\r\n";
                }
            return res;
        }
        public string GetColumns()
        {
            var res = "";
            var decList = new List<Tuple<string, string>>();
            foreach (var col in Table.Columns.Where(x => !x.Constraints.Any(y => (y is ForeignKey) && ((y as ForeignKey).Column.Name == x.Name))))
            {
                var column = "\t" + $@"@Column(name = COLUMN_{col.Name})" + "\r\n";
                var constraints = "";
                var declaration = "\t" + $@"private " + col.Type.ToJavaType() + " " + col.Name.ToCamelCase() + ";\r\n";
                decList.Add(new Tuple<string, string>(col.Type.ToJavaType(), col.Name));

                if (col.Size != null)
                    constraints += "\t@Size(" + (col.Size.Min.HasValue ? "min = " + col.Name + "_SIZE_MIN" + (col.Size.Max.HasValue ? ", " : "") : "") + (col.Size.Max.HasValue ? " max = " + col.Name + "_SIZE_MAX" : "") + ")\r\n";
                if (!col.Nullable)
                    constraints += "\t@NotNull\r\n";

                res += column + constraints + declaration + "\r\n";

            }

            foreach (var col in Table.Columns.Where(x => x.Constraints.Any(y => (y is ForeignKey) && ((y as ForeignKey).Column.Name == x.Name))))
            {



                var column = "\t" + $@"@Column(name = COLUMN_{col.Name})" + "\r\n";
                var constraints = "";
                var declaration = "\t" + $@"private " + col.Type.ToJavaType() + " " + col.Name.ToCamelCase() + ";\r\n";
                decList.Add(new Tuple<string, string>(col.Type.ToJavaType(), col.Name));

                if (col.Size != null)
                    constraints += "\t@Size(" + (col.Size.Min.HasValue ? "min = " + col.Name + "_SIZE_MIN" + (col.Size.Max.HasValue ? ", " : "") : "") + (col.Size.Max.HasValue ? " max = " + col.Name + "_SIZE_MAX" : "") + ")\r\n";

                if (!col.Nullable)
                    constraints += "\t@NotNull\r\n";

                res += column + constraints + declaration + "\r\n";

            }

            res += "\r\n";


            foreach (var decl in decList)
            {
                res += "\tpublic " + decl.Item1 + " get" + decl.Item2.ToPascalCase() + $@"() {{ return {decl.Item2.ToCamelCase()}; }}" + "\r\n";
                res += "\tpublic " + decl.Item1 + " set" + decl.Item2.ToPascalCase() + $@"({decl.Item1} {decl.Item2.ToCamelCase()}) {{ this.{decl.Item2.ToCamelCase()} = {decl.Item2.ToCamelCase()}; }}" + "\r\n";
                res += "\r\n";
            }
            return res;
        }
        //Table.Columns.Select(x => "\t" + $@"private " + x.Type.ToJavaType() + " " + x.Name.ToCamelCase() + ";\r\n").Aggregate((x, y) => x + y)
        public override string ToString()
        {
            var result = "";
            result += GetImports();

            result += $@"
@Entity
@Table(schema = ""{Table.Schema}"", name = TABLE_NAME){ (Table.Columns.Any(x => x.Sequence != null) ? "\r\n@HvlEntitySequence(name = TABLE_SEQ_NAME)" : "")}
public class {Table.SafeName.ToPascalCase()}Entity extends HvlEntity {{

{GetColumns()}        


}}
";
            return result;
        }
    }
}