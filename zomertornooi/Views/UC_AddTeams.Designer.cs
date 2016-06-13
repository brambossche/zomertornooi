namespace structures.Views
{
    partial class UC_AddTeams
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
            this.txt_PloegNaam = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chb_Betaald = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_ContactPersoon = new System.Windows.Forms.ComboBox();
            this.cmb_category = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_PloegNaam
            // 
            this.txt_PloegNaam.Location = new System.Drawing.Point(31, 72);
            this.txt_PloegNaam.Name = "txt_PloegNaam";
            this.txt_PloegNaam.Size = new System.Drawing.Size(270, 22);
            this.txt_PloegNaam.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "PloegNaam";
            // 
            // chb_Betaald
            // 
            this.chb_Betaald.AutoSize = true;
            this.chb_Betaald.Location = new System.Drawing.Point(31, 215);
            this.chb_Betaald.Name = "chb_Betaald";
            this.chb_Betaald.Size = new System.Drawing.Size(78, 21);
            this.chb_Betaald.TabIndex = 4;
            this.chb_Betaald.Text = "Betaald";
            this.chb_Betaald.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "ContactPersoon";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Categorie";
            // 
            // cmb_ContactPersoon
            // 
            this.cmb_ContactPersoon.FormattingEnabled = true;
            this.cmb_ContactPersoon.Location = new System.Drawing.Point(31, 123);
            this.cmb_ContactPersoon.Name = "cmb_ContactPersoon";
            this.cmb_ContactPersoon.Size = new System.Drawing.Size(270, 24);
            this.cmb_ContactPersoon.TabIndex = 2;
            // 
            // cmb_category
            // 
            this.cmb_category.FormattingEnabled = true;
            this.cmb_category.Location = new System.Drawing.Point(31, 176);
            this.cmb_category.Name = "cmb_category";
            this.cmb_category.Size = new System.Drawing.Size(268, 24);
            this.cmb_category.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(31, 242);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(268, 31);
            this.button1.TabIndex = 5;
            this.button1.Text = "Toevoegen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // UC_AddTeams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmb_category);
            this.Controls.Add(this.cmb_ContactPersoon);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chb_Betaald);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_PloegNaam);
            this.Name = "UC_AddTeams";
            this.Size = new System.Drawing.Size(342, 691);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_PloegNaam;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chb_Betaald;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_ContactPersoon;
        private System.Windows.Forms.ComboBox cmb_category;
        private System.Windows.Forms.Button button1;
    }
}
