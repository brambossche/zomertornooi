namespace structures.Views
{
    partial class UC_TornooiAdministratie
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
            this.dgv_Klassement = new System.Windows.Forms.DataGridView();
            this.dgv_Wedstrijden = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_lock = new System.Windows.Forms.Button();
            this.cmb_ReeksNaam = new System.Windows.Forms.ComboBox();
            this.dgv_Terreinen = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmb_Aanvangsuur = new System.Windows.Forms.ComboBox();
            this.btn_previousRound = new System.Windows.Forms.Button();
            this.btn_nextRound = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Klassement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Wedstrijden)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Terreinen)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Controls.Add(this.dgv_Klassement, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgv_Wedstrijden, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.99999F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(820, 519);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dgv_Klassement
            // 
            this.dgv_Klassement.AllowUserToAddRows = false;
            this.dgv_Klassement.AllowUserToResizeColumns = false;
            this.dgv_Klassement.AllowUserToResizeRows = false;
            this.dgv_Klassement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Klassement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Klassement.Location = new System.Drawing.Point(167, 3);
            this.dgv_Klassement.Name = "dgv_Klassement";
            this.dgv_Klassement.ReadOnly = true;
            this.dgv_Klassement.RowTemplate.Height = 24;
            this.dgv_Klassement.Size = new System.Drawing.Size(650, 233);
            this.dgv_Klassement.TabIndex = 4;
            // 
            // dgv_Wedstrijden
            // 
            this.dgv_Wedstrijden.AllowUserToAddRows = false;
            this.dgv_Wedstrijden.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dgv_Wedstrijden, 2);
            this.dgv_Wedstrijden.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Wedstrijden.Location = new System.Drawing.Point(3, 281);
            this.dgv_Wedstrijden.Name = "dgv_Wedstrijden";
            this.dgv_Wedstrijden.Size = new System.Drawing.Size(814, 235);
            this.dgv_Wedstrijden.TabIndex = 1;
            this.dgv_Wedstrijden.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Wedstrijden_CellContentClick);
            this.dgv_Wedstrijden.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Wedstrijden_CellValueChanged);
            this.dgv_Wedstrijden.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_Wedstrijden_RowHeaderMouseClick);
            this.dgv_Wedstrijden.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgv_Wedstrijden_RowPostPaint);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btn_lock, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cmb_ReeksNaam, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dgv_Terreinen, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(158, 233);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // btn_lock
            // 
            this.btn_lock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_lock.Location = new System.Drawing.Point(3, 33);
            this.btn_lock.Name = "btn_lock";
            this.btn_lock.Size = new System.Drawing.Size(152, 23);
            this.btn_lock.TabIndex = 1;
            this.btn_lock.Text = "Lock reeks";
            this.btn_lock.UseVisualStyleBackColor = true;
            this.btn_lock.Click += new System.EventHandler(this.btn_lock_Click);
            // 
            // cmb_ReeksNaam
            // 
            this.cmb_ReeksNaam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmb_ReeksNaam.FormattingEnabled = true;
            this.cmb_ReeksNaam.Location = new System.Drawing.Point(3, 3);
            this.cmb_ReeksNaam.Name = "cmb_ReeksNaam";
            this.cmb_ReeksNaam.Size = new System.Drawing.Size(152, 24);
            this.cmb_ReeksNaam.TabIndex = 0;
            this.cmb_ReeksNaam.SelectedIndexChanged += new System.EventHandler(this.cmb_ReeksNaam_SelectedIndexChanged);
            // 
            // dgv_Terreinen
            // 
            this.dgv_Terreinen.AllowUserToAddRows = false;
            this.dgv_Terreinen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Terreinen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Terreinen.Location = new System.Drawing.Point(3, 62);
            this.dgv_Terreinen.Name = "dgv_Terreinen";
            this.dgv_Terreinen.RowTemplate.Height = 24;
            this.dgv_Terreinen.Size = new System.Drawing.Size(152, 168);
            this.dgv_Terreinen.TabIndex = 2;
            //this.dgv_Terreinen.DataSourceChanged += new System.EventHandler(this.dgv_Terreinen_DataSourceChanged);
            this.dgv_Terreinen.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Terreinen_CellValueChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.cmb_Aanvangsuur);
            this.flowLayoutPanel1.Controls.Add(this.btn_previousRound);
            this.flowLayoutPanel1.Controls.Add(this.btn_nextRound);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 242);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(814, 33);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // cmb_Aanvangsuur
            // 
            this.cmb_Aanvangsuur.FormattingEnabled = true;
            this.cmb_Aanvangsuur.Location = new System.Drawing.Point(3, 3);
            this.cmb_Aanvangsuur.Name = "cmb_Aanvangsuur";
            this.cmb_Aanvangsuur.Size = new System.Drawing.Size(126, 24);
            this.cmb_Aanvangsuur.TabIndex = 1;
            this.cmb_Aanvangsuur.SelectedIndexChanged += new System.EventHandler(this.cmb_Aanvangsuur_SelectedIndexChanged);
            // 
            // btn_previousRound
            // 
            this.btn_previousRound.Enabled = false;
            this.btn_previousRound.Location = new System.Drawing.Point(135, 3);
            this.btn_previousRound.Name = "btn_previousRound";
            this.btn_previousRound.Size = new System.Drawing.Size(129, 27);
            this.btn_previousRound.TabIndex = 3;
            this.btn_previousRound.Text = "Vorige Ronde";
            this.btn_previousRound.UseVisualStyleBackColor = true;
            this.btn_previousRound.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btn_nextRound
            // 
            this.btn_nextRound.Location = new System.Drawing.Point(270, 3);
            this.btn_nextRound.Name = "btn_nextRound";
            this.btn_nextRound.Size = new System.Drawing.Size(129, 27);
            this.btn_nextRound.TabIndex = 2;
            this.btn_nextRound.Text = "Volgende Ronde";
            this.btn_nextRound.UseVisualStyleBackColor = true;
            this.btn_nextRound.Click += new System.EventHandler(this.button1_Click);
            // 
            // UC_TornooiAdministratie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UC_TornooiAdministratie";
            this.Size = new System.Drawing.Size(820, 519);
            this.Load += new System.EventHandler(this.UC_TornooiAdministratie_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Klassement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Wedstrijden)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Terreinen)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgv_Wedstrijden;
        private System.Windows.Forms.ComboBox cmb_Aanvangsuur;
        private System.Windows.Forms.Button btn_nextRound;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.DataGridView dgv_Klassement;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btn_lock;
        private System.Windows.Forms.ComboBox cmb_ReeksNaam;
        private System.Windows.Forms.DataGridView dgv_Terreinen;
        private System.Windows.Forms.Button btn_previousRound;
    }
}
