namespace structures.Views.Final_Rounds
{
    partial class UC_Brackets
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Home = new System.Windows.Forms.Label();
            this.lbl_Away = new System.Windows.Forms.Label();
            this.lbl_Winner = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackgroundImage = global::structures.Properties.Resources.Brackets1;
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.44444F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11112F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.44444F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_Home, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_Away, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbl_Winner, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1136, 327);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // lbl_Home
            // 
            this.lbl_Home.AutoSize = true;
            this.lbl_Home.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Home.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Home.Location = new System.Drawing.Point(3, 0);
            this.lbl_Home.Name = "lbl_Home";
            this.lbl_Home.Size = new System.Drawing.Size(498, 108);
            this.lbl_Home.TabIndex = 0;
            this.lbl_Home.Text = "label1";
            this.lbl_Home.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Away
            // 
            this.lbl_Away.AutoSize = true;
            this.lbl_Away.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Away.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Away.Location = new System.Drawing.Point(3, 216);
            this.lbl_Away.Name = "lbl_Away";
            this.lbl_Away.Size = new System.Drawing.Size(498, 111);
            this.lbl_Away.TabIndex = 1;
            this.lbl_Away.Text = "label2";
            this.lbl_Away.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Winner
            // 
            this.lbl_Winner.AutoSize = true;
            this.lbl_Winner.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Winner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Winner.Location = new System.Drawing.Point(633, 108);
            this.lbl_Winner.Name = "lbl_Winner";
            this.lbl_Winner.Size = new System.Drawing.Size(500, 108);
            this.lbl_Winner.TabIndex = 2;
            this.lbl_Winner.Text = "label3";
            this.lbl_Winner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UC_Brackets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UC_Brackets";
            this.Size = new System.Drawing.Size(1136, 327);
            this.Load += new System.EventHandler(this.UC_Brackets_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Home;

        public System.Windows.Forms.Label Lbl_Home
        {
            get { return lbl_Home; }
            set { lbl_Home = value; }
        }
        private System.Windows.Forms.Label lbl_Away;

        public System.Windows.Forms.Label Lbl_Away
        {
            get { return lbl_Away; }
            set { lbl_Away = value; }
        }
        private System.Windows.Forms.Label lbl_Winner;

        public System.Windows.Forms.Label Lbl_Winner
        {
            get { return lbl_Winner; }
            set { lbl_Winner = value; }
        }
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

    }
}
