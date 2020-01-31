using KoGen.Extentions;
using KoGen.Models.DatabaseModels;
using KoGen.Models.DatabaseModels.ConstraintModels;
using KoGen.Models.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoGen.Extentions.StringExtentions;

namespace KoGen
{
    public class DBFile
    {
        public List<Table> TableList { get; set; }
        public Dictionary<string, EntityConstraintsClass> EntityConstraintDic { get; set; }
        public Dictionary<string, EntityClass> EntityDic { get; set; }
        public Dictionary<string, ModelClass> ModelDic { get; set; }
        public Dictionary<string, ConverterClass> ConverterDic { get; set; }


        public DBFile(string name, string module)
        {
            TableList = new List<Table>();


            using (var sr = new StreamReader(name))
            {
                var str = sr.ReadToEnd();
                foreach (var item in str.GetSlices("/* Create", "/* Create"))
                {
                    if (item.Contains("/* Create Tables */"))
                    {
                        GetTable(item);
                    }
                    else if (item.Contains("/* Create Primary Keys, Indexes, Uniques, Checks */"))
                    {
                        foreach (var constraint in item.GetSlices("ALTER TABLE", $";{NewLine}"))
                        {
                            GetPrimaryKeyAndIndex(constraint);
                        }
                    }
                    else if (item.Contains("/* Create Foreign Key Constraints */"))
                    {
                        foreach (var constraint in item.GetSlices("ALTER TABLE", $";{NewLine}"))
                        {
                            GetForeignKey(constraint);
                        }
                    }
                    else if (item.Contains("/* Create Table Comments, Sequences for Autonumber Columns */"))
                    {
                        foreach (var sequence in item.GetSlices("CREATE SEQUENCE", ";"))
                        {
                            GetSequence(sequence);
                        }
                        foreach (var constraint in item.GetSlices("COMMENT ON", $";{NewLine}"))
                        {
                            if (constraint.Trim().StartsWith("COMMENT ON TABLE"))
                            {
                                GetTableComment(constraint);

                            }
                            else if (constraint.Trim().StartsWith("COMMENT ON COLUMN"))
                            {
                                GetColumnComment(constraint);
                            }
                        }
                    }
                }

            }


            EntityConstraintDic = new Dictionary<string, EntityConstraintsClass>();
            EntityDic = new Dictionary<string, EntityClass>();
            foreach (var table in TableList)
            {
                table.Schema = "KOVAN_" + module.ToUpperEn();
                var moduleName = module.ToLowerEn();
                var ec = new EntityConstraintsClass(table, "workshop");
                EntityConstraintDic.Add(ec.Name, ec);

                var entity = new EntityClass(ec, "workshop");
                EntityDic.Add(entity.Name, entity);
            }


            foreach (var table in TableList)
            {
                table.Columns.Any(x => x.Constraints.Count > 0);
                foreach (var col in table.Columns)
                {
                    if (col.Constraints.Any(x => x is ForeignKey))
                    {
                        var entityClass = EntityDic.Values.First(x => x.EntityConstraints.Table.Name == col.Table.Name);
                        var fkEntityClass = EntityDic.Values.First(x => x.EntityConstraints.Table.Name == (col.Constraints.First(y => y is ForeignKey) as ForeignKey).ReferenceColumn.Table.Name);
                        if (entityClass.Name == fkEntityClass.Name)
                            continue;
                        var cm = entityClass.ClassMembers.ClassMembers.First(x => x.Name == col.Name.ToCamelCase());

                        var entityClassMember = new ClassMember(fkEntityClass.Name.Replace("Entity", "").ToLowerFirstCharacter(), fkEntityClass, null, Models.DataModels.Enum.AccessModifier.Private);
                        entityClass.ClassMembers.Replace(col.Name.ToCamelCase(), entityClassMember);

                        var listClass = Models.DataModels.Predefined.PredefinedClasses.JavaList;
                        listClass.GenericList.Add(entityClass);
                        var fkEntityClassMember = new ClassMember(entityClass.Name.Replace("Entity", "") + "List", listClass, null, Models.DataModels.Enum.AccessModifier.Private);
                        if (col.Constraints.Any(x => x is Unique) && (col.Constraints.FirstOrDefault(x => x is Unique) as Unique).Columns.Count > 1)
                        {
                            fkEntityClass.ClassMembers.Add(fkEntityClassMember);
                            fkEntityClassMember.Annotations.Add(Models.DataModels.Predefined.PredefinedAnnotations.OneToMany()
                                .SetParameter("cascade", new ReferenceValue("CascadeType.ALL"))
                                .SetParameter("mappedBy", entityClassMember.Name));

                            entityClassMember.Annotations.Add(Models.DataModels.Predefined.PredefinedAnnotations.ManyToOne());
                            entityClassMember.Annotations.Add(Models.DataModels.Predefined.PredefinedAnnotations.JoinColumn()
                                .SetParameter("referencedColumnName", fkEntityClass.EntityConstraints.GetStaticMemberByValue((col.Constraints.FirstOrDefault(x => x is ForeignKey) as ForeignKey).Column.Name))
                                .SetParameter("name", KoGen.Models.DataModels.ReferenceValue.FromStaticMemberByValue(entityClass.EntityConstraints, col.Name))
                                .SetParameter("foreignKey", Models.DataModels.Predefined.PredefinedAnnotations.ForeignKey()
                                    .SetParameter("name", (col.Constraints.FirstOrDefault(x => x is ForeignKey) as ForeignKey).Name)));



                        }
                        else
                        {
                            entityClassMember.Annotations.Add(Models.DataModels.Predefined.PredefinedAnnotations.OneToOne());
                        }


                    }
                }
            }

            ModelDic = new Dictionary<string, ModelClass>();
            foreach (var entityClass in EntityDic.Values)
            {
                var m = new ModelClass(entityClass, "workshop");
                ModelDic.Add(m.Name, m);
            }

            foreach (var modelClass in ModelDic.Values)
            {

            }

            ConverterDic = new Dictionary<string, ConverterClass>();
            foreach (var modelClass in ModelDic.Values)
            {
                var m = new ConverterClass(modelClass, "workshop");
                ConverterDic.Add(m.Name, m);
            }
            foreach (var converterClass in ConverterDic.Values)
            {
                foreach (var expression in converterClass.Functions.First(x => x.Name == "doConvertToEntity").Expressions.Where(x => x is SetterAssignExpression).Select(x => x as SetterAssignExpression))
                {
                    if(expression.DestinationClassMember.Type != expression.SourceClassMember.Type)
                    {
                        Console.WriteLine();
                    }
                }
            }
        }

        private void GetTable(string item)
        {
            foreach (var table in item.GetSlices("CREATE TABLE", $"){NewLine};"))
            {
                var t = new Table();
                string tName, tAlias;
                GetTableDefinition(table, "CREATE TABLE", $"{NewLine}(", out tName, out tAlias);

                t.Name = tName;
                t.Alias = tAlias;

                foreach (var column in table.Substring(table.IndexOf($"{NewLine}(") + $"{NewLine}(".Length).Split(new string[] { NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()))
                {
                    GetTableColumn(t, column);
                }
                TableList.Add(t);
            }

        }

        private static void GetTableColumn(Table t, string column)
        {
            var c = new Column();
            c.Name = column.GetSlice("\"", "\"");

            var type = column.GetSlice(" ", " ", column.IndexOf(c.Name) + c.Name.Length);
            Size size = null;
            if (type.Contains("("))
            {
                var sizeStr = type.GetSlice("(", ")");
                var minMaxStr = sizeStr.Split(',');
                if (minMaxStr.Length == 1)
                    size = new Size { Max = int.Parse(sizeStr) };
                else
                    size = new Size { Min = int.Parse(minMaxStr[0]), Max = int.Parse(minMaxStr[1]) };

                type = type.Substring(0, type.IndexOf("("));
            }
            c.Nullable = !column.Contains("NOT NULL");
            c.Size = size;
            c.Type = TypeExtentions.FromDBType(type, size, c.Nullable);

            if (column.Contains("DEFAULT NEXTVAL"))
            {
                var s = new Sequence();
                s.Name = column.GetSlice("DEFAULT NEXTVAL(('\"", "\"'");
                s.Column = c;
                c.Sequence = s;
            }

            t.Columns.Add(c);
            c.Table = t;
        }

        private void GetPrimaryKeyAndIndex(string constraint)
        {
            string tName, tAlias;
            GetTableDefinition(constraint, "\"", "\"", out tName, out tAlias);
            var constraintName = constraint.GetSlice(" ADD CONSTRAINT \"", "\"");
            var columns = new List<string>();
            ConstraintBase c = null;
            var table = TableList.FirstOrDefault(x => x.Name == tName);

            if (constraint.Contains("PRIMARY KEY"))
            {
                c = GetPrimaryKey(constraint, out columns, table);
            }
            else if (constraint.Contains("UNIQUE"))
            {
                c = GetUniqueIndex(constraint, out columns, table);
            }
            c.Name = constraintName;
            c.Table = table;
        }

        private static ConstraintBase GetPrimaryKey(string constraint, out List<string> columns, Table table)
        {
            columns = constraint.GetSlice("PRIMARY KEY (\"", "\")").Replace("\"", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            ConstraintBase c = new PrimaryKey();
            (c as PrimaryKey).Columns.AddRange(table.Columns.Join(columns, x => x.Name, y => y, (a, b) =>
            {

                a.Constraints.Add(c);
                return a;
            }).ToList());
            return c;
        }

        private static ConstraintBase GetUniqueIndex(string constraint, out List<string> columns, Table table)
        {
            columns = constraint.GetSlice("UNIQUE (\"", "\")").Replace("\"", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            ConstraintBase c = new Unique();
            (c as Unique).Columns.AddRange(table.Columns.Join(columns, x => x.Name, y => y, (a, b) =>
            {
                a.Constraints.Add(c);
                return a;
            }).ToList());
            return c;
        }

        private void GetForeignKey(string constraint)
        {
            if (constraint.Contains("FOREIGN KEY ()") || constraint.Contains("REFERENCES  ()"))
                return;

            string tName, tAlias;
            GetTableDefinition(constraint, "\"", "\"", out tName, out tAlias);
            var constraintName = constraint.GetSlice(" ADD CONSTRAINT \"", "\"");

            ConstraintBase c = null;
            var table1 = TableList.FirstOrDefault(x => x.Name == tName);

            var column1 = constraint.GetSlice("FOREIGN KEY (\"", "\")");
            c = new ForeignKey();
            (c as ForeignKey).Column = table1.Columns.FirstOrDefault(x => x.Name == column1);
            (c as ForeignKey).Column.Constraints.Add(c);


            string tName2, tAlias2;
            GetTableDefinition(constraint, "REFERENCES \"", "\"", out tName2, out tAlias2);
            var table2FullName = string.IsNullOrEmpty(tAlias2) ? tName2 : tName2 + " (" + tAlias2 + ")";
            var column2 = constraint.GetSlice($"REFERENCES \"{table2FullName}\" (\"", "\")");
            var table2 = TableList.FirstOrDefault(x => x.Name == tName2);
            (c as ForeignKey).ReferenceColumn = table2.Columns.FirstOrDefault(x => x.Name == column2);
            (c as ForeignKey).ReferenceColumn.Constraints.Add(c);


            c.Name = constraintName;
            c.Table = table1;
        }

        private void GetSequence(string sequence)
        {
            var sqname = sequence.GetSlice("CREATE SEQUENCE ", " INCREMENT").Replace("\"", "");
            var sqParts = sequence.GetSlice("INCREMENT", "\r").Replace("\"", "").Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
            foreach (var t in TableList)
            {
                if (t.Columns.Any(x => x.Sequence != null && x.Sequence.Name == sqname))
                {
                    var column = t.Columns.FirstOrDefault(x => x.Sequence != null && x.Sequence.Name == sqname);
                    column.Sequence.Increment = int.Parse(sqParts[0]);
                    column.Sequence.Start = int.Parse(sqParts[2]);
                }
            }
        }

        private void GetColumnComment(string constraint)
        {
            var parts = constraint.GetSlices("\"", "\"");
            var tableName = (" " + parts[0]).GetSlice(" ", "(").Replace("\"", "").Trim();
            var table = TableList.FirstOrDefault(x => x.Name == tableName);
            var columnName = parts[2].Replace("\"", "").Trim();
            var column = table.Columns.FirstOrDefault(x => x.Name == columnName);
            var comment = constraint.GetSlice("\'", "\'");
            column.Comment = comment;
        }

        private void GetTableComment(string constraint)
        {
            var tableName = (" " + constraint.GetSlice("\"", "\"")).GetSlice(" ", "(").Replace("\"", "").Trim();
            TableList.FirstOrDefault(x => x.Name == tableName).Comment = constraint.GetSlice("\'", "\'");
        }

        private static void GetTableDefinition(string value, string start, string end, out string tName, out string tAlias)
        {
            tName = value.GetSlice(start, end).Replace("\"", "").Trim();
            tAlias = tName.GetSlice("(", ")");
            if (tName != tAlias)
            {
                tAlias = tAlias.Replace("(", "").Replace(")", "").Trim();
                tName = tName.Replace(tAlias, "").Replace("(", "").Replace(")", "").Trim();
            }
            else
            {
                tAlias = "";
            }
        }
    }
}
