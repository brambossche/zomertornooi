namespace OutputLogging
{
    partial class UcErrorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcErrorWindow));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tst_clearerror = new System.Windows.Forms.ToolStripButton();
            this.btn_Redminereport = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tst_clearerror,
            this.btn_Redminereport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(628, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tst_clearerror
            // 
            this.tst_clearerror.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tst_clearerror.Image = ((System.Drawing.Image)(resources.GetObject("tst_clearerror.Image")));
            this.tst_clearerror.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tst_clearerror.Name = "tst_clearerror";
            this.tst_clearerror.Size = new System.Drawing.Size(38, 22);
            this.tst_clearerror.Text = "Clear";
            this.tst_clearerror.Click += new System.EventHandler(this.tst_clearerror_Click);
            // 
            // btn_Redminereport
            // 
            this.btn_Redminereport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_Redminereport.Image = ((System.Drawing.Image)(resources.GetObject("btn_Redminereport.Image")));
            this.btn_Redminereport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Redminereport.Name = "btn_Redminereport";
            this.btn_Redminereport.Size = new System.Drawing.Size(72, 22);
            this.btn_Redminereport.Text = "Send report";
            this.btn_Redminereport.Click += new System.EventHandler(this.btn_Redminereport_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(628, 122);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // UcErrorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 147);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UcErrorWindow";
            this.Text = "Error";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tst_clearerror;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripButton btn_Redminereport;
    }
}
