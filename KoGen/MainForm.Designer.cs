namespace KoGen
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtModuleName = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbCodes = new System.Windows.Forms.TabControl();
            this.tbConstraints = new System.Windows.Forms.TabPage();
            this.rtbContraints = new KoGen.Components.CodeEditor(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.lstConstraints = new System.Windows.Forms.ListBox();
            this.tbEntities = new System.Windows.Forms.TabPage();
            this.rtbEntities = new KoGen.Components.CodeEditor(this.components);
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.lstEntities = new System.Windows.Forms.ListBox();
            this.tbModels = new System.Windows.Forms.TabPage();
            this.rtbModels = new KoGen.Components.CodeEditor(this.components);
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.lstModels = new System.Windows.Forms.ListBox();
            this.tbConverters = new System.Windows.Forms.TabPage();
            this.rtbConverters = new KoGen.Components.CodeEditor(this.components);
            this.splitter4 = new System.Windows.Forms.Splitter();
            this.lstConverters = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.tbCodes.SuspendLayout();
            this.tbConstraints.SuspendLayout();
            this.tbEntities.SuspendLayout();
            this.tbModels.SuspendLayout();
            this.tbConverters.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtModuleName
            // 
            this.txtModuleName.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtModuleName.Location = new System.Drawing.Point(116, 14);
            this.txtModuleName.Margin = new System.Windows.Forms.Padding(2);
            this.txtModuleName.Name = "txtModuleName";
            this.txtModuleName.Size = new System.Drawing.Size(173, 27);
            this.txtModuleName.TabIndex = 0;
            this.txtModuleName.Text = "workshop";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Calibri", 12F);
            this.btnGenerate.Location = new System.Drawing.Point(304, 14);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(80, 26);
            this.btnGenerate.TabIndex = 1;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseCompatibleTextRendering = true;
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F);
            this.label1.Location = new System.Drawing.Point(2, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Module Name:";
            this.label1.UseCompatibleTextRendering = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtModuleName);
            this.panel1.Controls.Add(this.btnGenerate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 53);
            this.panel1.TabIndex = 4;
            // 
            // tbCodes
            // 
            this.tbCodes.Controls.Add(this.tbConstraints);
            this.tbCodes.Controls.Add(this.tbEntities);
            this.tbCodes.Controls.Add(this.tbModels);
            this.tbCodes.Controls.Add(this.tbConverters);
            this.tbCodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCodes.Location = new System.Drawing.Point(0, 53);
            this.tbCodes.Margin = new System.Windows.Forms.Padding(2);
            this.tbCodes.Name = "tbCodes";
            this.tbCodes.SelectedIndex = 0;
            this.tbCodes.Size = new System.Drawing.Size(600, 313);
            this.tbCodes.TabIndex = 5;
            // 
            // tbConstraints
            // 
            this.tbConstraints.Controls.Add(this.rtbContraints);
            this.tbConstraints.Controls.Add(this.splitter1);
            this.tbConstraints.Controls.Add(this.lstConstraints);
            this.tbConstraints.Location = new System.Drawing.Point(4, 22);
            this.tbConstraints.Margin = new System.Windows.Forms.Padding(2);
            this.tbConstraints.Name = "tbConstraints";
            this.tbConstraints.Padding = new System.Windows.Forms.Padding(2);
            this.tbConstraints.Size = new System.Drawing.Size(592, 287);
            this.tbConstraints.TabIndex = 0;
            this.tbConstraints.Text = "Constraints";
            this.tbConstraints.UseVisualStyleBackColor = true;
            // 
            // rtbContraints
            // 
            this.rtbContraints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbContraints.EdgeMode = ScintillaNET.EdgeMode.Line;
            this.rtbContraints.FontQuality = ScintillaNET.FontQuality.AntiAliased;
            this.rtbContraints.Location = new System.Drawing.Point(95, 2);
            this.rtbContraints.Margin = new System.Windows.Forms.Padding(2);
            this.rtbContraints.Name = "rtbContraints";
            this.rtbContraints.Size = new System.Drawing.Size(495, 283);
            this.rtbContraints.TabIndex = 5;
            this.rtbContraints.TabWidth = 2;
            this.rtbContraints.WrapMode = ScintillaNET.WrapMode.Word;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(93, 2);
            this.splitter1.Margin = new System.Windows.Forms.Padding(2);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(2, 283);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // lstConstraints
            // 
            this.lstConstraints.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstConstraints.FormattingEnabled = true;
            this.lstConstraints.Location = new System.Drawing.Point(2, 2);
            this.lstConstraints.Margin = new System.Windows.Forms.Padding(2);
            this.lstConstraints.Name = "lstConstraints";
            this.lstConstraints.Size = new System.Drawing.Size(91, 283);
            this.lstConstraints.TabIndex = 3;
            this.lstConstraints.SelectedIndexChanged += new System.EventHandler(this.lstConstraints_SelectedIndexChanged);
            // 
            // tbEntities
            // 
            this.tbEntities.Controls.Add(this.rtbEntities);
            this.tbEntities.Controls.Add(this.splitter2);
            this.tbEntities.Controls.Add(this.lstEntities);
            this.tbEntities.Location = new System.Drawing.Point(4, 22);
            this.tbEntities.Margin = new System.Windows.Forms.Padding(2);
            this.tbEntities.Name = "tbEntities";
            this.tbEntities.Padding = new System.Windows.Forms.Padding(2);
            this.tbEntities.Size = new System.Drawing.Size(592, 287);
            this.tbEntities.TabIndex = 1;
            this.tbEntities.Text = "Entities";
            this.tbEntities.UseVisualStyleBackColor = true;
            // 
            // rtbEntities
            // 
            this.rtbEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbEntities.Location = new System.Drawing.Point(87, 2);
            this.rtbEntities.Margin = new System.Windows.Forms.Padding(2);
            this.rtbEntities.Name = "rtbEntities";
            this.rtbEntities.Size = new System.Drawing.Size(503, 283);
            this.rtbEntities.TabIndex = 8;
            this.rtbEntities.TabWidth = 2;
            this.rtbEntities.WrapMode = ScintillaNET.WrapMode.Word;
            this.rtbEntities.EdgeMode = ScintillaNET.EdgeMode.Line;
            this.rtbEntities.FontQuality = ScintillaNET.FontQuality.AntiAliased;
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(84, 2);
            this.splitter2.Margin = new System.Windows.Forms.Padding(2);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 283);
            this.splitter2.TabIndex = 7;
            this.splitter2.TabStop = false;
            // 
            // lstEntities
            // 
            this.lstEntities.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstEntities.FormattingEnabled = true;
            this.lstEntities.Location = new System.Drawing.Point(2, 2);
            this.lstEntities.Margin = new System.Windows.Forms.Padding(2);
            this.lstEntities.Name = "lstEntities";
            this.lstEntities.Size = new System.Drawing.Size(82, 283);
            this.lstEntities.TabIndex = 6;
            this.lstEntities.SelectedIndexChanged += new System.EventHandler(this.lstEntities_SelectedIndexChanged);
            // 
            // tbModels
            // 
            this.tbModels.Controls.Add(this.rtbModels);
            this.tbModels.Controls.Add(this.splitter3);
            this.tbModels.Controls.Add(this.lstModels);
            this.tbModels.Location = new System.Drawing.Point(4, 22);
            this.tbModels.Name = "tbModels";
            this.tbModels.Padding = new System.Windows.Forms.Padding(3);
            this.tbModels.Size = new System.Drawing.Size(592, 287);
            this.tbModels.TabIndex = 2;
            this.tbModels.Text = "Models";
            this.tbModels.UseVisualStyleBackColor = true;
            // 
            // rtbModels
            // 
            this.rtbModels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbModels.Location = new System.Drawing.Point(97, 3);
            this.rtbModels.Margin = new System.Windows.Forms.Padding(2);
            this.rtbModels.Name = "rtbModels";
            this.rtbModels.Size = new System.Drawing.Size(492, 281);
            this.rtbModels.TabIndex = 11;
            this.rtbModels.TabWidth = 2;
            this.rtbModels.WrapMode = ScintillaNET.WrapMode.Word;
            this.rtbModels.EdgeMode = ScintillaNET.EdgeMode.Line;
            this.rtbModels.FontQuality = ScintillaNET.FontQuality.AntiAliased;
            // 
            // splitter3
            // 
            this.splitter3.Location = new System.Drawing.Point(94, 3);
            this.splitter3.Margin = new System.Windows.Forms.Padding(2);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(3, 281);
            this.splitter3.TabIndex = 10;
            this.splitter3.TabStop = false;
            // 
            // lstModels
            // 
            this.lstModels.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstModels.FormattingEnabled = true;
            this.lstModels.Location = new System.Drawing.Point(3, 3);
            this.lstModels.Margin = new System.Windows.Forms.Padding(2);
            this.lstModels.Name = "lstModels";
            this.lstModels.Size = new System.Drawing.Size(91, 281);
            this.lstModels.TabIndex = 9;
            this.lstModels.SelectedIndexChanged += new System.EventHandler(this.lstModels_SelectedIndexChanged);
            // 
            // tbConverters
            // 
            this.tbConverters.Controls.Add(this.rtbConverters);
            this.tbConverters.Controls.Add(this.splitter4);
            this.tbConverters.Controls.Add(this.lstConverters);
            this.tbConverters.Location = new System.Drawing.Point(4, 22);
            this.tbConverters.Name = "tbConverters";
            this.tbConverters.Padding = new System.Windows.Forms.Padding(3);
            this.tbConverters.Size = new System.Drawing.Size(592, 287);
            this.tbConverters.TabIndex = 3;
            this.tbConverters.Text = "Converters";
            this.tbConverters.UseVisualStyleBackColor = true;
            // 
            // rtbConverters
            // 
            this.rtbConverters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbConverters.Location = new System.Drawing.Point(97, 3);
            this.rtbConverters.Margin = new System.Windows.Forms.Padding(2);
            this.rtbConverters.Name = "rtbConverters";
            this.rtbConverters.Size = new System.Drawing.Size(492, 281);
            this.rtbConverters.TabIndex = 11; 
            this.rtbConverters.TabWidth = 2;
            this.rtbConverters.WrapMode = ScintillaNET.WrapMode.Word;
            this.rtbConverters.EdgeMode = ScintillaNET.EdgeMode.Line;
            this.rtbConverters.FontQuality = ScintillaNET.FontQuality.AntiAliased;
            // 
            // splitter4
            // 
            this.splitter4.Location = new System.Drawing.Point(94, 3);
            this.splitter4.Margin = new System.Windows.Forms.Padding(2);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(3, 281);
            this.splitter4.TabIndex = 10;
            this.splitter4.TabStop = false;
            // 
            // lstConverters
            // 
            this.lstConverters.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstConverters.FormattingEnabled = true;
            this.lstConverters.Location = new System.Drawing.Point(3, 3);
            this.lstConverters.Margin = new System.Windows.Forms.Padding(2);
            this.lstConverters.Name = "lstConverters";
            this.lstConverters.Size = new System.Drawing.Size(91, 281);
            this.lstConverters.TabIndex = 9;
            this.lstConverters.SelectedIndexChanged += new System.EventHandler(this.lstConverters_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.tbCodes);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tbCodes.ResumeLayout(false);
            this.tbConstraints.ResumeLayout(false);
            this.tbEntities.ResumeLayout(false);
            this.tbModels.ResumeLayout(false);
            this.tbConverters.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtModuleName;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tbCodes;
        private System.Windows.Forms.TabPage tbConstraints;
        private System.Windows.Forms.TabPage tbEntities;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ListBox lstConstraints;
        private Components.CodeEditor rtbContraints;
        private Components.CodeEditor rtbEntities;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.ListBox lstEntities;
        private System.Windows.Forms.TabPage tbModels;
        private System.Windows.Forms.TabPage tbConverters;
        private Components.CodeEditor rtbModels;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.ListBox lstModels;
        private System.Windows.Forms.ListBox lstConverters;
        private System.Windows.Forms.Splitter splitter4;
        private Components.CodeEditor rtbConverters;



    }

}

