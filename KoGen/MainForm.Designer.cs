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
            this.txtModuleName = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbCodes = new System.Windows.Forms.TabControl();
            this.tbConstraints = new System.Windows.Forms.TabPage();
            this.tbEntities = new System.Windows.Forms.TabPage();
            this.lstConstraints = new System.Windows.Forms.ListBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.rtbContraints = new System.Windows.Forms.RichTextBox();
            this.lstEntities = new System.Windows.Forms.ListBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.rtbEntities = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.tbCodes.SuspendLayout();
            this.tbConstraints.SuspendLayout();
            this.tbEntities.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtModuleName
            // 
            this.txtModuleName.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtModuleName.Location = new System.Drawing.Point(154, 17);
            this.txtModuleName.Name = "txtModuleName";
            this.txtModuleName.Size = new System.Drawing.Size(229, 32);
            this.txtModuleName.TabIndex = 0;
            this.txtModuleName.Text = "workshop";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Calibri", 12F);
            this.btnGenerate.Location = new System.Drawing.Point(405, 17);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(107, 32);
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
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 30);
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
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 65);
            this.panel1.TabIndex = 4;
            // 
            // tbCodes
            // 
            this.tbCodes.Controls.Add(this.tbConstraints);
            this.tbCodes.Controls.Add(this.tbEntities);
            this.tbCodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCodes.Location = new System.Drawing.Point(0, 65);
            this.tbCodes.Name = "tbCodes";
            this.tbCodes.SelectedIndex = 0;
            this.tbCodes.Size = new System.Drawing.Size(800, 385);
            this.tbCodes.TabIndex = 5;
            // 
            // tbConstraints
            // 
            this.tbConstraints.Controls.Add(this.rtbContraints);
            this.tbConstraints.Controls.Add(this.splitter1);
            this.tbConstraints.Controls.Add(this.lstConstraints);
            this.tbConstraints.Location = new System.Drawing.Point(4, 25);
            this.tbConstraints.Name = "tbConstraints";
            this.tbConstraints.Padding = new System.Windows.Forms.Padding(3);
            this.tbConstraints.Size = new System.Drawing.Size(792, 356);
            this.tbConstraints.TabIndex = 0;
            this.tbConstraints.Text = "Constraints";
            this.tbConstraints.UseVisualStyleBackColor = true;
            // 
            // tbEntities
            // 
            this.tbEntities.Controls.Add(this.rtbEntities);
            this.tbEntities.Controls.Add(this.splitter2);
            this.tbEntities.Controls.Add(this.lstEntities);
            this.tbEntities.Location = new System.Drawing.Point(4, 25);
            this.tbEntities.Name = "tbEntities";
            this.tbEntities.Padding = new System.Windows.Forms.Padding(3);
            this.tbEntities.Size = new System.Drawing.Size(792, 356);
            this.tbEntities.TabIndex = 1;
            this.tbEntities.Text = "Entities";
            this.tbEntities.UseVisualStyleBackColor = true;
            // 
            // lstConstraints
            // 
            this.lstConstraints.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstConstraints.FormattingEnabled = true;
            this.lstConstraints.ItemHeight = 16;
            this.lstConstraints.Location = new System.Drawing.Point(3, 3);
            this.lstConstraints.Name = "lstConstraints";
            this.lstConstraints.Size = new System.Drawing.Size(120, 350);
            this.lstConstraints.TabIndex = 3;
            this.lstConstraints.SelectedIndexChanged += new System.EventHandler(this.lstConstraints_SelectedIndexChanged);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(123, 3);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 350);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // rtbContraints
            // 
            this.rtbContraints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbContraints.Location = new System.Drawing.Point(126, 3);
            this.rtbContraints.Name = "rtbContraints";
            this.rtbContraints.Size = new System.Drawing.Size(663, 350);
            this.rtbContraints.TabIndex = 5;
            this.rtbContraints.Text = "";
            // 
            // lstEntities
            // 
            this.lstEntities.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstEntities.FormattingEnabled = true;
            this.lstEntities.ItemHeight = 16;
            this.lstEntities.Location = new System.Drawing.Point(3, 3);
            this.lstEntities.Name = "lstEntities";
            this.lstEntities.Size = new System.Drawing.Size(120, 350);
            this.lstEntities.TabIndex = 6;
            this.lstEntities.SelectedIndexChanged += new System.EventHandler(this.lstEntities_SelectedIndexChanged);
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(123, 3);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 350);
            this.splitter2.TabIndex = 7;
            this.splitter2.TabStop = false;
            // 
            // rtbEntities
            // 
            this.rtbEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbEntities.Location = new System.Drawing.Point(126, 3);
            this.rtbEntities.Name = "rtbEntities";
            this.rtbEntities.Size = new System.Drawing.Size(663, 350);
            this.rtbEntities.TabIndex = 8;
            this.rtbEntities.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbCodes);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tbCodes.ResumeLayout(false);
            this.tbConstraints.ResumeLayout(false);
            this.tbEntities.ResumeLayout(false);
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
        private System.Windows.Forms.RichTextBox rtbContraints;
        private System.Windows.Forms.RichTextBox rtbEntities;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.ListBox lstEntities;
    }

}

