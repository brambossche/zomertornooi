namespace OutputLogging
{
    partial class Filter
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
            this.txtdat = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.lblcounter = new System.Windows.Forms.ToolStripLabel();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.txtfind = new System.Windows.Forms.ToolStripTextBox();
            this.lblfindresult = new System.Windows.Forms.ToolStripLabel();
            this.lblcurrentocc = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtdat
            // 
            this.txtdat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtdat.Location = new System.Drawing.Point(0, 0);
            this.txtdat.Multiline = true;
            this.txtdat.Name = "txtdat";
            this.txtdat.ReadOnly = true;
            this.txtdat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtdat.Size = new System.Drawing.Size(663, 275);
            this.txtdat.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblcounter,
            this.btnSearch,
            this.txtfind,
            this.lblfindresult,
            this.lblcurrentocc});
            this.toolStrip1.Location = new System.Drawing.Point(0, 275);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(663, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // lblcounter
            // 
            this.lblcounter.Name = "lblcounter";
            this.lblcounter.Size = new System.Drawing.Size(92, 22);
            this.lblcounter.Text = "_________________";
            // 
            // btnSearch
            // 
            this.btnSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSearch.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(34, 22);
            this.btnSearch.Text = "Find";
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtfind
            // 
            this.txtfind.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.txtfind.Name = "txtfind";
            this.txtfind.Size = new System.Drawing.Size(100, 25);
            this.txtfind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtfind_KeyPress);
            this.txtfind.TextChanged += new System.EventHandler(this.txtfind_TextChanged);
            // 
            // lblfindresult
            // 
            this.lblfindresult.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblfindresult.Name = "lblfindresult";
            this.lblfindresult.Size = new System.Drawing.Size(12, 22);
            this.lblfindresult.Text = "_";
            // 
            // lblcurrentocc
            // 
            this.lblcurrentocc.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblcurrentocc.Name = "lblcurrentocc";
            this.lblcurrentocc.Size = new System.Drawing.Size(17, 22);
            this.lblcurrentocc.Text = "_/";
            // 
            // Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 300);
            this.Controls.Add(this.txtdat);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Filter";
            this.Text = "Filter";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

      

    

        #endregion

        private System.Windows.Forms.TextBox txtdat;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel lblcounter;
        private System.Windows.Forms.ToolStripButton btnSearch;
        private System.Windows.Forms.ToolStripTextBox txtfind;
        private System.Windows.Forms.ToolStripLabel lblfindresult;
        private System.Windows.Forms.ToolStripLabel lblcurrentocc;
    }
}