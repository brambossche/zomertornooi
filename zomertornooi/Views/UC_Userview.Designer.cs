using structures.Views;
namespace Views
{
    public partial class Userview<T>
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
            this.btn_refreshlist = new System.Windows.Forms.Button();
            this.extendDataGridView1 = new Marb.ExtendToolboxCtrl.ExtendDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.extendDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_refreshlist
            // 
            this.btn_refreshlist.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_refreshlist.Location = new System.Drawing.Point(0, 0);
            this.btn_refreshlist.Name = "btn_refreshlist";
            this.btn_refreshlist.Size = new System.Drawing.Size(329, 23);
            this.btn_refreshlist.TabIndex = 1;
            this.btn_refreshlist.Text = "Refresh";
            this.btn_refreshlist.UseVisualStyleBackColor = true;
            this.btn_refreshlist.Click += new System.EventHandler(this.btn_refreshlist_Click);
            // 
            // extendDataGridView1
            // 
            this.extendDataGridView1.AllowUserToAddRows = false;
            this.extendDataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.extendDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extendDataGridView1.Location = new System.Drawing.Point(0, 0);
            this.extendDataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.extendDataGridView1.Name = "extendDataGridView1";
            this.extendDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.extendDataGridView1.Size = new System.Drawing.Size(1360, 446);
            this.extendDataGridView1.TabIndex = 0;
            // 
            // Userview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.extendDataGridView1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Userview";
            this.Size = new System.Drawing.Size(1360, 446);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Enter += new System.EventHandler(this.Userview_Enter);
            this.Leave += new System.EventHandler(this.Userview_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.extendDataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        //private System.Windows.Forms.SplitContainer splitContainer1;
        //private System.Windows.Forms.PropertyGrid PG_details;
        public Marb.ExtendToolboxCtrl.ExtendDataGridView extendDataGridView1;
        private System.Windows.Forms.Button btn_refreshlist;


    }
}

