namespace structures.Views.Final_Rounds
{
    partial class UC_AllBrackets
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
            this.tbl_Brackets = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // tbl_Brackets
            // 
            this.tbl_Brackets.ColumnCount = 2;
            this.tbl_Brackets.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_Brackets.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_Brackets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_Brackets.Location = new System.Drawing.Point(0, 0);
            this.tbl_Brackets.Name = "tbl_Brackets";
            this.tbl_Brackets.RowCount = 2;
            this.tbl_Brackets.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_Brackets.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_Brackets.Size = new System.Drawing.Size(845, 553);
            this.tbl_Brackets.TabIndex = 0;
            // 
            // UC_AllBrackets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.tbl_Brackets);
            this.Name = "UC_AllBrackets";
            this.Size = new System.Drawing.Size(845, 553);
            this.Load += new System.EventHandler(this.UC_AllBrackets_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbl_Brackets;
    }
}
