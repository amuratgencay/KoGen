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
        private DBFile _dbFile;
        private void Form1_Load(object sender, EventArgs e)
        {
            _dbFile = new DBFile("table.sql");

        }


        private void btnGenerate_Click(object sender, EventArgs e)
        {
            _dbFile.EntityConstraintDic = new Dictionary<string, EntityConstraintsClass>();
            _dbFile.EntityDic = new Dictionary<string, EntityClass>();
            foreach (var table in _dbFile.TableList)
            {
                table.Schema = "KOVAN_" + txtModuleName.Text.ToUpperEn();
                var module = txtModuleName.Text.ToLowerEn();
                var ec = new EntityConstraintsClass(table, "workshop");
                _dbFile.EntityConstraintDic.Add(ec.Name, ec);
                lstConstraints.Items.Add(ec.Name);

                var entity = new EntityClass(ec, "workshop");
                _dbFile.EntityDic.Add(entity.Name, entity);
                lstEntities.Items.Add(entity.Name);
            }



            foreach (var table in _dbFile.TableList)
            {
                table.Columns.Any(x => x.Constraints.Count > 0);
                foreach (var col in table.Columns)
                {
                    if((col.Constraints.Any(x=>x is Unique) && col.Constraints.Any(x => x is ForeignKey)))
                    {
                        var entityClass = _dbFile.EntityDic.Values.First(x => x.EntityConstraints.Table.Name == col.Table.Name);
                        var fkEntityClass = _dbFile.EntityDic.Values.First(x => x.EntityConstraints.Table.Name == (col.Constraints.First(y => y is ForeignKey) as ForeignKey).ReferenceColumn.Table.Name);

                        entityClass.ClassMembers.ClassMembers.First(x => x.Name == col.Name.ToCamelCase()).Type = fkEntityClass;
                         
                        Console.WriteLine();
                    }
                }
            }

            Console.WriteLine();
        }

        private void lstConstraints_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtbContraints.Text = _dbFile.EntityConstraintDic[lstConstraints.Text].ToJavaFile();
        }

        private void lstEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtbEntities.Text = _dbFile.EntityDic[lstEntities.Text].ToJavaFile();
        }

    }
}
