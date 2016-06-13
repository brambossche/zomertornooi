namespace structures.Views
{
    partial class UC_PloegOverView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_PloegOverView));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tstcmb_categoryfilter = new System.Windows.Forms.ToolStripComboBox();
            this.tstbtn_Filter = new System.Windows.Forms.ToolStripButton();
            this.tstbtn_nofilter = new System.Windows.Forms.ToolStripButton();
            this.tsb_AddTeam = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tstbtn_overview = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tstbtn_changecategory = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstcmb_categoryfilter,
            this.tstbtn_Filter,
            this.tstbtn_nofilter,
            this.tsb_AddTeam,
            this.toolStripSeparator1,
            this.tstbtn_overview,
            this.toolStripSeparator2,
            this.tstbtn_changecategory});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1020, 28);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tstcmb_categoryfilter
            // 
            this.tstcmb_categoryfilter.Name = "tstcmb_categoryfilter";
            this.tstcmb_categoryfilter.Size = new System.Drawing.Size(160, 28);
            this.tstcmb_categoryfilter.Click += new System.EventHandler(this.tstcmb_categoryfilter_Click);
            this.tstcmb_categoryfilter.TextChanged += new System.EventHandler(this.tstcmb_categoryfilter_TextChanged);
            // 
            // tstbtn_Filter
            // 
            this.tstbtn_Filter.CheckOnClick = true;
            this.tstbtn_Filter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tstbtn_Filter.Image = ((System.Drawing.Image)(resources.GetObject("tstbtn_Filter.Image")));
            this.tstbtn_Filter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstbtn_Filter.Name = "tstbtn_Filter";
            this.tstbtn_Filter.Size = new System.Drawing.Size(24, 25);
            this.tstbtn_Filter.Text = "Filter Aan";
            this.tstbtn_Filter.Click += new System.EventHandler(this.tstbtn_Filter_Click);
            // 
            // tstbtn_nofilter
            // 
            this.tstbtn_nofilter.Checked = true;
            this.tstbtn_nofilter.CheckOnClick = true;
            this.tstbtn_nofilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tstbtn_nofilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tstbtn_nofilter.Image = ((System.Drawing.Image)(resources.GetObject("tstbtn_nofilter.Image")));
            this.tstbtn_nofilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstbtn_nofilter.Name = "tstbtn_nofilter";
            this.tstbtn_nofilter.Size = new System.Drawing.Size(24, 25);
            this.tstbtn_nofilter.Text = "Filter Uit";
            this.tstbtn_nofilter.Click += new System.EventHandler(this.tstbtn_nofilter_Click);
            // 
            // tsb_AddTeam
            // 
            this.tsb_AddTeam.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_AddTeam.Image = global::structures.Properties.Resources.Add_sign;
            this.tsb_AddTeam.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_AddTeam.Name = "tsb_AddTeam";
            this.tsb_AddTeam.Size = new System.Drawing.Size(24, 25);
            this.tsb_AddTeam.Text = "Ploegen Toevoegen";
            this.tsb_AddTeam.Click += new System.EventHandler(this.tsb_AddTeam_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // tstbtn_overview
            // 
            this.tstbtn_overview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tstbtn_overview.Image = ((System.Drawing.Image)(resources.GetObject("tstbtn_overview.Image")));
            this.tstbtn_overview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstbtn_overview.Name = "tstbtn_overview";
            this.tstbtn_overview.Size = new System.Drawing.Size(24, 25);
            this.tstbtn_overview.Text = "Aantal Ploegen";
            this.tstbtn_overview.Click += new System.EventHandler(this.tstbtn_overview_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // tstbtn_changecategory
            // 
            this.tstbtn_changecategory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tstbtn_changecategory.Image = ((System.Drawing.Image)(resources.GetObject("tstbtn_changecategory.Image")));
            this.tstbtn_changecategory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstbtn_changecategory.Name = "tstbtn_changecategory";
            this.tstbtn_changecategory.Size = new System.Drawing.Size(24, 25);
            this.tstbtn_changecategory.Text = "Wijzig categorie";
            this.tstbtn_changecategory.Click += new System.EventHandler(this.tstbtn_changecategory_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(1020, 534);
            this.splitContainer1.SplitterDistance = 758;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // UC_PloegOverView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UC_PloegOverView";
            this.Size = new System.Drawing.Size(1020, 562);
            this.Load += new System.EventHandler(this.UC_PloegOverView_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tstbtn_Filter;
        private System.Windows.Forms.ToolStripComboBox tstcmb_categoryfilter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tstbtn_overview;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tstbtn_changecategory;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton tstbtn_nofilter;
        private System.Windows.Forms.ToolStripButton tsb_AddTeam;
    }
}
