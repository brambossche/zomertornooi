namespace structures.Views
{
    partial class UC_Reader
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
            this.dgv_Wedstrijden = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Wedstrijden)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_Wedstrijden
            // 
            this.dgv_Wedstrijden.AllowUserToAddRows = false;
            this.dgv_Wedstrijden.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Wedstrijden.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Wedstrijden.Location = new System.Drawing.Point(0, 0);
            this.dgv_Wedstrijden.Name = "dgv_Wedstrijden";
            this.dgv_Wedstrijden.Size = new System.Drawing.Size(1058, 588);
            this.dgv_Wedstrijden.TabIndex = 2;
            this.dgv_Wedstrijden.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Wedstrijden_CellContentClick);
            // 
            // UC_Reader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv_Wedstrijden);
            this.Name = "UC_Reader";
            this.Size = new System.Drawing.Size(1058, 588);
            this.Load += new System.EventHandler(this.UC_Reader_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Wedstrijden)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Wedstrijden;

    }
}
