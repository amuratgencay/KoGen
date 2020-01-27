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


        }


        private void btnGenerate_Click(object sender, EventArgs e)
        {
            _dbFile = new DBFile("table.sql", txtModuleName.Text);

            lstConstraints.Items.AddRange(_dbFile.EntityConstraintDic.Values.Select(x => x.Name).ToArray<object>());
            lstEntities.Items.AddRange(_dbFile.EntityDic.Values.Select(x => x.Name).ToArray<object>());
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
