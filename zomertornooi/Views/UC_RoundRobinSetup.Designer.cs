namespace structures.Views
{
    partial class UC_RoundRobinSetup
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_calculateRoundRobin = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_OptimizeTerrein = new System.Windows.Forms.Button();
            this.comboBxReeksen = new System.Windows.Forms.ComboBox();
            this.dtp_Zaterdag = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtp_Zondag = new System.Windows.Forms.DateTimePicker();
            this.nc_aantalRoundRobin = new System.Windows.Forms.NumericUpDown();
            this.nc_MaxNaElkaar = new System.Windows.Forms.NumericUpDown();
            this.nc_WedstrijdDuur = new System.Windows.Forms.NumericUpDown();
            this.nc_AantalRondesZaterdag = new System.Windows.Forms.NumericUpDown();
            this.nc_aantalTerreinen = new System.Windows.Forms.NumericUpDown();
            this.dtv_Wedstrijden = new System.Windows.Forms.DataGridView();
            this.dtv_StatusList = new System.Windows.Forms.DataGridView();
            this.btn_Simulate = new System.Windows.Forms.Button();
            this.dtv_optimisation = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dtv_Terreinen = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_DeleteAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nc_aantalRoundRobin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nc_MaxNaElkaar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nc_WedstrijdDuur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nc_AantalRondesZaterdag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nc_aantalTerreinen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtv_Wedstrijden)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtv_StatusList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtv_optimisation)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtv_Terreinen)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "AantalRoundRobin";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 81);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Maximum na elkaar spelen ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 305);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Aantal rondes op zaterdag";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 249);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Wedstrijdduur (min.)";
            // 
            // btn_calculateRoundRobin
            // 
            this.btn_calculateRoundRobin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_calculateRoundRobin.Location = new System.Drawing.Point(0, 714);
            this.btn_calculateRoundRobin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_calculateRoundRobin.Name = "btn_calculateRoundRobin";
            this.btn_calculateRoundRobin.Size = new System.Drawing.Size(1761, 26);
            this.btn_calculateRoundRobin.TabIndex = 9;
            this.btn_calculateRoundRobin.Text = "Store All Games to database";
            this.btn_calculateRoundRobin.UseVisualStyleBackColor = true;
            this.btn_calculateRoundRobin.Click += new System.EventHandler(this.btn_calculateRoundRobin_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 354);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Aantal terreinen";
            // 
            // btn_OptimizeTerrein
            // 
            this.btn_OptimizeTerrein.Location = new System.Drawing.Point(149, 373);
            this.btn_OptimizeTerrein.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_OptimizeTerrein.Name = "btn_OptimizeTerrein";
            this.btn_OptimizeTerrein.Size = new System.Drawing.Size(100, 26);
            this.btn_OptimizeTerrein.TabIndex = 12;
            this.btn_OptimizeTerrein.Text = "Calculate...";
            this.btn_OptimizeTerrein.UseVisualStyleBackColor = true;
            this.btn_OptimizeTerrein.Click += new System.EventHandler(this.btn_OptimizeTerrein_Click);
            // 
            // comboBxReeksen
            // 
            this.comboBxReeksen.FormattingEnabled = true;
            this.comboBxReeksen.Location = new System.Drawing.Point(3, 2);
            this.comboBxReeksen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBxReeksen.Name = "comboBxReeksen";
            this.comboBxReeksen.Size = new System.Drawing.Size(283, 24);
            this.comboBxReeksen.TabIndex = 13;
            this.comboBxReeksen.SelectedIndexChanged += new System.EventHandler(this.comboBxReeksen_SelectedIndexChanged);
            // 
            // dtp_Zaterdag
            // 
            this.dtp_Zaterdag.CustomFormat = "HH:mm";
            this.dtp_Zaterdag.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_Zaterdag.Location = new System.Drawing.Point(11, 155);
            this.dtp_Zaterdag.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtp_Zaterdag.Name = "dtp_Zaterdag";
            this.dtp_Zaterdag.Size = new System.Drawing.Size(200, 22);
            this.dtp_Zaterdag.TabIndex = 14;
            this.dtp_Zaterdag.Value = new System.DateTime(2016, 8, 20, 10, 30, 0, 0);
            this.dtp_Zaterdag.ValueChanged += new System.EventHandler(this.dtp_Zaterdag_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 186);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Aanvangsuur zondag";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 135);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(152, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "Aanvangsuur zaterdag";
            // 
            // dtp_Zondag
            // 
            this.dtp_Zondag.CustomFormat = "HH:mm";
            this.dtp_Zondag.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_Zondag.Location = new System.Drawing.Point(11, 206);
            this.dtp_Zondag.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtp_Zondag.Name = "dtp_Zondag";
            this.dtp_Zondag.Size = new System.Drawing.Size(200, 22);
            this.dtp_Zondag.TabIndex = 17;
            this.dtp_Zondag.Value = new System.DateTime(2016, 8, 21, 10, 0, 0, 0);
            // 
            // nc_aantalRoundRobin
            // 
            this.nc_aantalRoundRobin.Location = new System.Drawing.Point(5, 49);
            this.nc_aantalRoundRobin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nc_aantalRoundRobin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nc_aantalRoundRobin.Name = "nc_aantalRoundRobin";
            this.nc_aantalRoundRobin.Size = new System.Drawing.Size(120, 22);
            this.nc_aantalRoundRobin.TabIndex = 18;
            this.nc_aantalRoundRobin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nc_MaxNaElkaar
            // 
            this.nc_MaxNaElkaar.Location = new System.Drawing.Point(7, 101);
            this.nc_MaxNaElkaar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nc_MaxNaElkaar.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nc_MaxNaElkaar.Name = "nc_MaxNaElkaar";
            this.nc_MaxNaElkaar.Size = new System.Drawing.Size(120, 22);
            this.nc_MaxNaElkaar.TabIndex = 19;
            this.nc_MaxNaElkaar.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nc_WedstrijdDuur
            // 
            this.nc_WedstrijdDuur.Location = new System.Drawing.Point(9, 268);
            this.nc_WedstrijdDuur.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nc_WedstrijdDuur.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nc_WedstrijdDuur.Name = "nc_WedstrijdDuur";
            this.nc_WedstrijdDuur.Size = new System.Drawing.Size(120, 22);
            this.nc_WedstrijdDuur.TabIndex = 20;
            this.nc_WedstrijdDuur.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nc_AantalRondesZaterdag
            // 
            this.nc_AantalRondesZaterdag.Location = new System.Drawing.Point(9, 325);
            this.nc_AantalRondesZaterdag.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nc_AantalRondesZaterdag.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nc_AantalRondesZaterdag.Name = "nc_AantalRondesZaterdag";
            this.nc_AantalRondesZaterdag.Size = new System.Drawing.Size(120, 22);
            this.nc_AantalRondesZaterdag.TabIndex = 21;
            this.nc_AantalRondesZaterdag.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nc_aantalTerreinen
            // 
            this.nc_aantalTerreinen.Location = new System.Drawing.Point(9, 374);
            this.nc_aantalTerreinen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nc_aantalTerreinen.Name = "nc_aantalTerreinen";
            this.nc_aantalTerreinen.Size = new System.Drawing.Size(120, 22);
            this.nc_aantalTerreinen.TabIndex = 22;
            this.nc_aantalTerreinen.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nc_aantalTerreinen.ValueChanged += new System.EventHandler(this.nc_aantalTerreinen_ValueChanged);
            // 
            // dtv_Wedstrijden
            // 
            this.dtv_Wedstrijden.AllowUserToAddRows = false;
            this.dtv_Wedstrijden.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtv_Wedstrijden.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtv_Wedstrijden.Location = new System.Drawing.Point(1175, 2);
            this.dtv_Wedstrijden.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtv_Wedstrijden.Name = "dtv_Wedstrijden";
            this.dtv_Wedstrijden.ReadOnly = true;
            this.tableLayoutPanel1.SetRowSpan(this.dtv_Wedstrijden, 3);
            this.dtv_Wedstrijden.RowTemplate.Height = 24;
            this.dtv_Wedstrijden.Size = new System.Drawing.Size(583, 710);
            this.dtv_Wedstrijden.TabIndex = 23;
            // 
            // dtv_StatusList
            // 
            this.dtv_StatusList.AllowUserToAddRows = false;
            this.dtv_StatusList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtv_StatusList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtv_StatusList.Location = new System.Drawing.Point(293, 235);
            this.dtv_StatusList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtv_StatusList.Name = "dtv_StatusList";
            this.dtv_StatusList.ReadOnly = true;
            this.dtv_StatusList.RowTemplate.Height = 24;
            this.dtv_StatusList.Size = new System.Drawing.Size(876, 477);
            this.dtv_StatusList.TabIndex = 24;
            this.dtv_StatusList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dtv_StatusList_CellFormatting);
            // 
            // btn_Simulate
            // 
            this.btn_Simulate.Location = new System.Drawing.Point(11, 569);
            this.btn_Simulate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Simulate.Name = "btn_Simulate";
            this.btn_Simulate.Size = new System.Drawing.Size(240, 28);
            this.btn_Simulate.TabIndex = 25;
            this.btn_Simulate.Text = "Simulate Games";
            this.btn_Simulate.UseVisualStyleBackColor = true;
            this.btn_Simulate.Click += new System.EventHandler(this.btn_Simulate_Click);
            // 
            // dtv_optimisation
            // 
            this.dtv_optimisation.AllowUserToAddRows = false;
            this.dtv_optimisation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtv_optimisation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtv_optimisation.Location = new System.Drawing.Point(293, 30);
            this.dtv_optimisation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtv_optimisation.Name = "dtv_optimisation";
            this.dtv_optimisation.ReadOnly = true;
            this.dtv_optimisation.RowTemplate.Height = 24;
            this.dtv_optimisation.Size = new System.Drawing.Size(876, 201);
            this.dtv_optimisation.TabIndex = 26;
            this.dtv_optimisation.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dtv_optimisation_CellFormatting);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.dtv_Terreinen);
            this.groupBox1.Controls.Add(this.nc_MaxNaElkaar);
            this.groupBox1.Controls.Add(this.btn_Simulate);
            this.groupBox1.Controls.Add(this.nc_aantalRoundRobin);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.dtp_Zaterdag);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.nc_WedstrijdDuur);
            this.groupBox1.Controls.Add(this.nc_aantalTerreinen);
            this.groupBox1.Controls.Add(this.btn_OptimizeTerrein);
            this.groupBox1.Controls.Add(this.nc_AantalRondesZaterdag);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtp_Zondag);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 30);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.SetRowSpan(this.groupBox1, 2);
            this.groupBox1.Size = new System.Drawing.Size(284, 682);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(11, 633);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(240, 28);
            this.button2.TabIndex = 28;
            this.button2.Text = "Delete from Database";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 601);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(240, 28);
            this.button1.TabIndex = 27;
            this.button1.Text = "Store to Database";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dtv_Terreinen
            // 
            this.dtv_Terreinen.AllowUserToAddRows = false;
            this.dtv_Terreinen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtv_Terreinen.Location = new System.Drawing.Point(9, 402);
            this.dtv_Terreinen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtv_Terreinen.Name = "dtv_Terreinen";
            this.dtv_Terreinen.RowTemplate.Height = 24;
            this.dtv_Terreinen.Size = new System.Drawing.Size(241, 150);
            this.dtv_Terreinen.TabIndex = 26;
            this.dtv_Terreinen.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtv_Terreinen_CellValueChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dtv_Wedstrijden, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtv_optimisation, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dtv_StatusList, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.comboBxReeksen, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1761, 714);
            this.tableLayoutPanel1.TabIndex = 28;
            // 
            // btn_DeleteAll
            // 
            this.btn_DeleteAll.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_DeleteAll.Location = new System.Drawing.Point(0, 740);
            this.btn_DeleteAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_DeleteAll.Name = "btn_DeleteAll";
            this.btn_DeleteAll.Size = new System.Drawing.Size(1761, 30);
            this.btn_DeleteAll.TabIndex = 29;
            this.btn_DeleteAll.Text = "Delete All Games from Database";
            this.btn_DeleteAll.UseVisualStyleBackColor = true;
            this.btn_DeleteAll.Click += new System.EventHandler(this.btn_DeleteAll_Click);
            // 
            // UC_RoundRobinSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btn_calculateRoundRobin);
            this.Controls.Add(this.btn_DeleteAll);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UC_RoundRobinSetup";
            this.Size = new System.Drawing.Size(1761, 770);
            this.Load += new System.EventHandler(this.UC_RoundRobinSetup_Load);
            this.Enter += new System.EventHandler(this.UC_RoundRobinSetup_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.nc_aantalRoundRobin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nc_MaxNaElkaar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nc_WedstrijdDuur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nc_AantalRondesZaterdag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nc_aantalTerreinen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtv_Wedstrijden)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtv_StatusList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtv_optimisation)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtv_Terreinen)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_calculateRoundRobin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_OptimizeTerrein;
        private System.Windows.Forms.ComboBox comboBxReeksen;
        private System.Windows.Forms.DateTimePicker dtp_Zaterdag;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtp_Zondag;
        private System.Windows.Forms.NumericUpDown nc_aantalRoundRobin;
        private System.Windows.Forms.NumericUpDown nc_MaxNaElkaar;
        private System.Windows.Forms.NumericUpDown nc_WedstrijdDuur;
        private System.Windows.Forms.NumericUpDown nc_AantalRondesZaterdag;
        private System.Windows.Forms.NumericUpDown nc_aantalTerreinen;
        private System.Windows.Forms.DataGridView dtv_Wedstrijden;
        private System.Windows.Forms.DataGridView dtv_StatusList;
        private System.Windows.Forms.Button btn_Simulate;
        private System.Windows.Forms.DataGridView dtv_optimisation;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dtv_Terreinen;
        private System.Windows.Forms.Button btn_DeleteAll;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}
