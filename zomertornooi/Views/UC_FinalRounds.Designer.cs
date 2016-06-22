namespace structures.Views
{
    partial class UC_FinalRounds
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_reeksnaam = new System.Windows.Forms.TextBox();
            this.btn_AddFinals = new System.Windows.Forms.Button();
            this.btn_SimulateFinals = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nc_WedstrijdDuur = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nc_aantalReeksen = new System.Windows.Forms.NumericUpDown();
            this.cmb_Reeks4 = new System.Windows.Forms.ComboBox();
            this.cmb_Reeks3 = new System.Windows.Forms.ComboBox();
            this.dtp_Finals = new System.Windows.Forms.DateTimePicker();
            this.cmb_Reeks2 = new System.Windows.Forms.ComboBox();
            this.cmb_Reeks1 = new System.Windows.Forms.ComboBox();
            this.dgv_wedstrijden = new System.Windows.Forms.DataGridView();
            this.PanelWedstrijden = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nc_WedstrijdDuur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nc_aantalReeksen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_wedstrijden)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgv_wedstrijden, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.PanelWedstrijden, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1210, 573);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txt_reeksnaam);
            this.panel1.Controls.Add(this.btn_AddFinals);
            this.panel1.Controls.Add(this.btn_SimulateFinals);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.nc_WedstrijdDuur);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.nc_aantalReeksen);
            this.panel1.Controls.Add(this.cmb_Reeks4);
            this.panel1.Controls.Add(this.cmb_Reeks3);
            this.panel1.Controls.Add(this.dtp_Finals);
            this.panel1.Controls.Add(this.cmb_Reeks2);
            this.panel1.Controls.Add(this.cmb_Reeks1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(243, 567);
            this.panel1.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 225);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 17);
            this.label8.TabIndex = 33;
            this.label8.Text = "Reeksnaam:";
            // 
            // txt_reeksnaam
            // 
            this.txt_reeksnaam.Location = new System.Drawing.Point(3, 245);
            this.txt_reeksnaam.Name = "txt_reeksnaam";
            this.txt_reeksnaam.Size = new System.Drawing.Size(235, 22);
            this.txt_reeksnaam.TabIndex = 32;
            // 
            // btn_AddFinals
            // 
            this.btn_AddFinals.Location = new System.Drawing.Point(3, 521);
            this.btn_AddFinals.Name = "btn_AddFinals";
            this.btn_AddFinals.Size = new System.Drawing.Size(235, 23);
            this.btn_AddFinals.TabIndex = 31;
            this.btn_AddFinals.Text = "Toevoegen aan database";
            this.btn_AddFinals.UseVisualStyleBackColor = true;
            this.btn_AddFinals.Click += new System.EventHandler(this.btn_AddFinals_Click);
            // 
            // btn_SimulateFinals
            // 
            this.btn_SimulateFinals.Location = new System.Drawing.Point(3, 483);
            this.btn_SimulateFinals.Name = "btn_SimulateFinals";
            this.btn_SimulateFinals.Size = new System.Drawing.Size(235, 23);
            this.btn_SimulateFinals.TabIndex = 30;
            this.btn_SimulateFinals.Text = "Simuleren";
            this.btn_SimulateFinals.UseVisualStyleBackColor = true;
            this.btn_SimulateFinals.Click += new System.EventHandler(this.btn_SimulateFinals_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 132);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 17);
            this.label4.TabIndex = 24;
            this.label4.Text = "Wedstrijdduur (min.)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 423);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 17);
            this.label7.TabIndex = 29;
            this.label7.Text = "Reeks 4";
            // 
            // nc_WedstrijdDuur
            // 
            this.nc_WedstrijdDuur.Location = new System.Drawing.Point(17, 151);
            this.nc_WedstrijdDuur.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nc_WedstrijdDuur.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nc_WedstrijdDuur.Name = "nc_WedstrijdDuur";
            this.nc_WedstrijdDuur.Size = new System.Drawing.Size(120, 22);
            this.nc_WedstrijdDuur.TabIndex = 25;
            this.nc_WedstrijdDuur.Value = new decimal(new int[] {
            45,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 372);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 17);
            this.label6.TabIndex = 28;
            this.label6.Text = "Reeks 3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 17);
            this.label2.TabIndex = 23;
            this.label2.Text = "Aanvangsuur finales";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 321);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 17);
            this.label5.TabIndex = 27;
            this.label5.Text = "Reeks 2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 17);
            this.label1.TabIndex = 22;
            this.label1.Text = "Aantal Reeksen";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 270);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 26;
            this.label3.Text = "Reeks 1";
            // 
            // nc_aantalReeksen
            // 
            this.nc_aantalReeksen.Location = new System.Drawing.Point(17, 29);
            this.nc_aantalReeksen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nc_aantalReeksen.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nc_aantalReeksen.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nc_aantalReeksen.Name = "nc_aantalReeksen";
            this.nc_aantalReeksen.Size = new System.Drawing.Size(120, 22);
            this.nc_aantalReeksen.TabIndex = 21;
            this.nc_aantalReeksen.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nc_aantalReeksen.ValueChanged += new System.EventHandler(this.nc_aantalReeksen_ValueChanged);
            // 
            // cmb_Reeks4
            // 
            this.cmb_Reeks4.Enabled = false;
            this.cmb_Reeks4.FormattingEnabled = true;
            this.cmb_Reeks4.Location = new System.Drawing.Point(3, 443);
            this.cmb_Reeks4.Name = "cmb_Reeks4";
            this.cmb_Reeks4.Size = new System.Drawing.Size(235, 24);
            this.cmb_Reeks4.TabIndex = 3;
            this.cmb_Reeks4.SelectedIndexChanged += new System.EventHandler(this.cmb_Reeks4_SelectedIndexChanged);
            // 
            // cmb_Reeks3
            // 
            this.cmb_Reeks3.Enabled = false;
            this.cmb_Reeks3.FormattingEnabled = true;
            this.cmb_Reeks3.Location = new System.Drawing.Point(3, 392);
            this.cmb_Reeks3.Name = "cmb_Reeks3";
            this.cmb_Reeks3.Size = new System.Drawing.Size(235, 24);
            this.cmb_Reeks3.TabIndex = 2;
            this.cmb_Reeks3.SelectedIndexChanged += new System.EventHandler(this.cmb_Reeks3_SelectedIndexChanged);
            // 
            // dtp_Finals
            // 
            this.dtp_Finals.CustomFormat = "HH:mm";
            this.dtp_Finals.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_Finals.Location = new System.Drawing.Point(17, 90);
            this.dtp_Finals.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtp_Finals.Name = "dtp_Finals";
            this.dtp_Finals.Size = new System.Drawing.Size(200, 22);
            this.dtp_Finals.TabIndex = 19;
            this.dtp_Finals.Value = new System.DateTime(2016, 8, 20, 10, 30, 0, 0);
            // 
            // cmb_Reeks2
            // 
            this.cmb_Reeks2.Enabled = false;
            this.cmb_Reeks2.FormattingEnabled = true;
            this.cmb_Reeks2.Location = new System.Drawing.Point(3, 341);
            this.cmb_Reeks2.Name = "cmb_Reeks2";
            this.cmb_Reeks2.Size = new System.Drawing.Size(235, 24);
            this.cmb_Reeks2.TabIndex = 1;
            this.cmb_Reeks2.SelectedIndexChanged += new System.EventHandler(this.cmb_Reeks2_SelectedIndexChanged);
            // 
            // cmb_Reeks1
            // 
            this.cmb_Reeks1.FormattingEnabled = true;
            this.cmb_Reeks1.Location = new System.Drawing.Point(3, 290);
            this.cmb_Reeks1.Name = "cmb_Reeks1";
            this.cmb_Reeks1.Size = new System.Drawing.Size(235, 24);
            this.cmb_Reeks1.TabIndex = 0;
            this.cmb_Reeks1.SelectedIndexChanged += new System.EventHandler(this.cmb_Reeks1_SelectedIndexChanged);
            // 
            // dgv_wedstrijden
            // 
            this.dgv_wedstrijden.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_wedstrijden.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_wedstrijden.Location = new System.Drawing.Point(252, 404);
            this.dgv_wedstrijden.Name = "dgv_wedstrijden";
            this.dgv_wedstrijden.RowTemplate.Height = 24;
            this.dgv_wedstrijden.Size = new System.Drawing.Size(955, 166);
            this.dgv_wedstrijden.TabIndex = 2;
            // 
            // PanelWedstrijden
            // 
            this.PanelWedstrijden.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelWedstrijden.Location = new System.Drawing.Point(252, 3);
            this.PanelWedstrijden.Name = "PanelWedstrijden";
            this.PanelWedstrijden.Size = new System.Drawing.Size(955, 395);
            this.PanelWedstrijden.TabIndex = 3;
            // 
            // UC_FinalRounds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UC_FinalRounds";
            this.Size = new System.Drawing.Size(1210, 573);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nc_WedstrijdDuur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nc_aantalReeksen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_wedstrijden)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nc_aantalReeksen;
        private System.Windows.Forms.DateTimePicker dtp_Finals;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nc_WedstrijdDuur;
        private System.Windows.Forms.ComboBox cmb_Reeks4;
        private System.Windows.Forms.ComboBox cmb_Reeks3;
        private System.Windows.Forms.ComboBox cmb_Reeks2;
        private System.Windows.Forms.ComboBox cmb_Reeks1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_SimulateFinals;
        private System.Windows.Forms.DataGridView dgv_wedstrijden;
        private System.Windows.Forms.Button btn_AddFinals;
        private System.Windows.Forms.Panel PanelWedstrijden;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_reeksnaam;
    }
}
