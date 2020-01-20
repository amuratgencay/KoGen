using KoGen.Extentions;
using KoGen.Models.DatabaseModels;
using KoGen.Models.DatabaseModels.ConstraintModels;
using KoGen.Models.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KoGen
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        List<Table> _tableList;
        Dictionary<string, EntityConstraintsClass> entityConstraintDic;
        Dictionary<string, Entity> entityDic;
        private void Form1_Load(object sender, EventArgs e)
        {
            _tableList = new List<Table>();

            using (var sr = new StreamReader("table.sql"))
            {
                var str = sr.ReadToEnd();
                foreach (var item in str.GetSlices("/* Create", "/* Create"))
                {
                    if (item.Contains("/* Create Tables */"))
                    {

                        foreach (var table in item.GetSlices("CREATE TABLE", ")\r\n;"))
                        {
                            var t = new Table();
                            string tName, tAlias;
                            GetTable(table, "CREATE TABLE", "\r\n(", out tName, out tAlias);

                            t.Name = tName;
                            t.Alias = tAlias;

                            foreach (var column in table.Substring(table.IndexOf("\r\n(") + "\r\n(".Length).Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()))
                            {
                                var c = new Column();
                                c.Name = column.GetSlice("\"", "\"");

                                var type = column.GetSlice(" ", " ", column.IndexOf(c.Name) + c.Name.Length);
                                Size size = null;
                                if (type.Contains("("))
                                {
                                    var sizeStr = type.GetSlice("(", ")");
                                    var minMaxStr = sizeStr.Split(',');
                                    if(minMaxStr.Length==1)
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
                            _tableList.Add(t);

                        }
                    }
                    else if (item.Contains("/* Create Primary Keys, Indexes, Uniques, Checks */"))
                    {
                        foreach (var constraint in item.GetSlices("ALTER TABLE", ";\r\n"))
                        {
                            string tName, tAlias;
                            GetTable(constraint, "\"", "\"", out tName, out tAlias);
                            var constraintName = constraint.GetSlice(" ADD CONSTRAINT \"", "\"");
                            var columns = new List<string>();
                            ConstraintBase c = null;
                            var table = _tableList.FirstOrDefault(x => x.Name == tName);

                            if (constraint.Contains("PRIMARY KEY"))
                            {
                                columns = constraint.GetSlice("PRIMARY KEY (\"", "\")").Replace("\"", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                c = new PrimaryKey();
                                (c as PrimaryKey).Columns.AddRange(table.Columns.Join(columns, x => x.Name, y => y, (a, b) =>
                                {
                                    a.Constraints.Add(c);
                                    return a;
                                }).ToList());

                            }
                            else if (constraint.Contains("UNIQUE"))
                            {
                                columns = constraint.GetSlice("UNIQUE (\"", "\")").Replace("\"", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                c = new Unique();
                                (c as Unique).Columns.AddRange(table.Columns.Join(columns, x => x.Name, y => y, (a, b) =>
                                {
                                    a.Constraints.Add(c);
                                    return a;
                                }).ToList());
                            }
                            c.Name = constraintName;
                            c.Table = table;
                        }
                    }
                    else if (item.Contains("/* Create Foreign Key Constraints */"))
                    {
                        foreach (var constraint in item.GetSlices("ALTER TABLE", ";\r\n"))
                        {
                            if (constraint.Contains("FOREIGN KEY ()") || constraint.Contains("REFERENCES  ()"))
                                continue;

                            string tName, tAlias;
                            GetTable(constraint, "\"", "\"", out tName, out tAlias);
                            var constraintName = constraint.GetSlice(" ADD CONSTRAINT \"", "\"");

                            ConstraintBase c = null;
                            var table1 = _tableList.FirstOrDefault(x => x.Name == tName);

                            var column1 = constraint.GetSlice("FOREIGN KEY (\"", "\")");
                            c = new ForeignKey();
                            (c as ForeignKey).Column = table1.Columns.FirstOrDefault(x => x.Name == column1);
                            (c as ForeignKey).Column.Constraints.Add(c);


                            string tName2, tAlias2;
                            GetTable(constraint, "REFERENCES \"", "\"", out tName2, out tAlias2);
                            var table2FullName = string.IsNullOrEmpty(tAlias2) ? tName2 : tName2 + " (" + tAlias2 + ")";
                            var column2 = constraint.GetSlice($"REFERENCES \"{table2FullName}\" (\"", "\")");
                            var table2 = _tableList.FirstOrDefault(x => x.Name == tName2);
                            (c as ForeignKey).ReferenceColumn = table2.Columns.FirstOrDefault(x => x.Name == column2);
                            (c as ForeignKey).ReferenceColumn.Constraints.Add(c);


                            c.Name = constraintName;
                            c.Table = table1;
                        }
                    }
                    else if (item.Contains("/* Create Table Comments, Sequences for Autonumber Columns */"))
                    {
                        foreach (var sequence in item.GetSlices("CREATE SEQUENCE", ";"))
                        {
                            var sqname = sequence.GetSlice("CREATE SEQUENCE ", " INCREMENT").Replace("\"", "");
                            var sqParts = sequence.GetSlice("INCREMENT", "\r").Replace("\"", "").Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
                            foreach (var t in _tableList)
                            {
                                if (t.Columns.Any(x => x.Sequence != null && x.Sequence.Name == sqname))
                                {
                                    var column = t.Columns.FirstOrDefault(x => x.Sequence != null && x.Sequence.Name == sqname);
                                    column.Sequence.Increment = int.Parse(sqParts[0]);
                                    column.Sequence.Start = int.Parse(sqParts[2]);
                                }
                            }
                        }
                        foreach (var constraint in item.GetSlices("COMMENT ON", ";\r\n"))
                        {
                            if (constraint.Trim().StartsWith("COMMENT ON TABLE"))
                            {
                                var tableName = (" " + constraint.GetSlice("\"", "\"")).GetSlice(" ", "(").Replace("\"", "").Trim();
                                var table = _tableList.FirstOrDefault(x => x.Name == tableName);
                                var comment = constraint.GetSlice("\'", "\'");
                                table.Comment = comment;

                            }
                            else if (constraint.Trim().StartsWith("COMMENT ON COLUMN"))
                            {
                                var parts = constraint.GetSlices("\"", "\"");
                                var tableName = (" " + parts[0]).GetSlice(" ", "(").Replace("\"", "").Trim();
                                var table = _tableList.FirstOrDefault(x => x.Name == tableName);
                                var columnName = parts[2].Replace("\"", "").Trim();
                                var column = table.Columns.FirstOrDefault(x => x.Name == columnName);
                                var comment = constraint.GetSlice("\'", "\'");
                                column.Comment = comment;
                            }
                        }
                    }
                }

            }


        }

        private static void GetTable(string value, string start, string end, out string tName, out string tAlias)
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

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            entityConstraintDic = new Dictionary<string, EntityConstraintsClass>();
            entityDic = new Dictionary<string, Entity>();
            foreach (var table in _tableList)
            {
                table.Schema = "KOVAN_" + txtModuleName.Text.ToUpper(System.Globalization.CultureInfo.InvariantCulture);
                var ec = new EntityConstraintsClass(table, "workshop");
                entityConstraintDic.Add(ec.Name, ec);
                lstConstraints.Items.Add(ec.Name);

                var entity = new Entity { Table = table };
                var entityClass = new EntityClass(table, "workshop", ec);
                entityDic.Add(entity.Name, entity);
                lstEntities.Items.Add(entity.Name);
            }
        }

        private void lstConstraints_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtbContraints.Text = entityConstraintDic[lstConstraints.Text].ToJavaFile();
        }

        private void lstEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtbEntities.Text = entityDic[lstEntities.Text].ToString();
        }

    }
}
