namespace structures.Views
{
    partial class UC_reeksAssignment
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new UC_reeksAssignment.CustomGrid();
            this.pnl_listview = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pnl_listassignment = new System.Windows.Forms.Panel();
            this.btn_allbacktoinputlist = new System.Windows.Forms.Button();
            this.btn_assigntolists = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pnl_listview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.SuspendLayout();
            this.pnl_listassignment.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnl_listview);
            this.splitContainer1.Panel2.Controls.Add(this.pnl_listassignment);
            this.splitContainer1.Size = new System.Drawing.Size(1013, 454);
            this.splitContainer1.SplitterDistance = 337;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 454);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(337, 0);
            this.panel1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(337, 454);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            // 
            // pnl_listview
            // 
            this.pnl_listview.Controls.Add(this.splitContainer2);
            this.pnl_listview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_listview.Location = new System.Drawing.Point(0, 0);
            this.pnl_listview.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnl_listview.Name = "pnl_listview";
            this.pnl_listview.Size = new System.Drawing.Size(671, 396);
            this.pnl_listview.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Size = new System.Drawing.Size(671, 396);
            this.splitContainer2.SplitterDistance = 222;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 0;
            // 
            // pnl_listassignment
            // 
            this.pnl_listassignment.AutoSize = true;
            this.pnl_listassignment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_listassignment.Controls.Add(this.btn_allbacktoinputlist);
            this.pnl_listassignment.Controls.Add(this.btn_assigntolists);
            this.pnl_listassignment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_listassignment.Location = new System.Drawing.Point(0, 396);
            this.pnl_listassignment.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnl_listassignment.Name = "pnl_listassignment";
            this.pnl_listassignment.Size = new System.Drawing.Size(671, 58);
            this.pnl_listassignment.TabIndex = 1;
            // 
            // btn_allbacktoinputlist
            // 
            this.btn_allbacktoinputlist.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_allbacktoinputlist.Location = new System.Drawing.Point(0, 28);
            this.btn_allbacktoinputlist.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_allbacktoinputlist.Name = "btn_allbacktoinputlist";
            this.btn_allbacktoinputlist.Size = new System.Drawing.Size(669, 28);
            this.btn_allbacktoinputlist.TabIndex = 1;
            this.btn_allbacktoinputlist.Text = "Set all itms back to inputlist";
            this.btn_allbacktoinputlist.UseVisualStyleBackColor = true;
            this.btn_allbacktoinputlist.Click += new System.EventHandler(this.btn_allbacktoinputlist_Click);
            // 
            // btn_assigntolists
            // 
            this.btn_assigntolists.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_assigntolists.Location = new System.Drawing.Point(0, 0);
            this.btn_assigntolists.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_assigntolists.Name = "btn_assigntolists";
            this.btn_assigntolists.Size = new System.Drawing.Size(669, 28);
            this.btn_assigntolists.TabIndex = 0;
            this.btn_assigntolists.Text = "Divide inputs over outputlists";
            this.btn_assigntolists.UseVisualStyleBackColor = true;
            this.btn_assigntolists.Click += new System.EventHandler(this.btn_assigntolists_Click);
            // 
            // UC_reeksAssignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UC_reeksAssignment";
            this.Size = new System.Drawing.Size(1013, 454);
            this.Enter += new System.EventHandler(this.UC_reeksAssignment_Enter);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.pnl_listview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.pnl_listassignment.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnl_listassignment;
        private System.Windows.Forms.Button btn_allbacktoinputlist;
        private System.Windows.Forms.Button btn_assigntolists;
        private System.Windows.Forms.Panel pnl_listview;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private UC_reeksAssignment.CustomGrid dataGridView1;
    }
}
