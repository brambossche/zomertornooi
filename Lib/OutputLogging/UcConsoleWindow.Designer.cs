namespace OutputLogging
{
    partial class UcConsoleWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcConsoleWindow));
            this.txtCons = new System.Windows.Forms.TextBox();
            this.chkClear = new System.Windows.Forms.CheckBox();
            this.chkTime = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.chkautoscroll = new System.Windows.Forms.CheckBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtCons
            // 
            this.txtCons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCons.Location = new System.Drawing.Point(0, 34);
            this.txtCons.Multiline = true;
            this.txtCons.Name = "txtCons";
            this.txtCons.ReadOnly = true;
            this.txtCons.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCons.Size = new System.Drawing.Size(693, 128);
            this.txtCons.TabIndex = 0;
            // 
            // chkClear
            // 
            this.chkClear.Checked = true;
            this.chkClear.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClear.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkClear.Location = new System.Drawing.Point(0, 0);
            this.chkClear.Name = "chkClear";
            this.chkClear.Size = new System.Drawing.Size(693, 34);
            this.chkClear.TabIndex = 1;
            this.chkClear.Text = "AutoClear";
            this.chkClear.UseVisualStyleBackColor = true;
            // 
            // chkTime
            // 
            this.chkTime.AutoSize = true;
            this.chkTime.Location = new System.Drawing.Point(76, 9);
            this.chkTime.Name = "chkTime";
            this.chkTime.Size = new System.Drawing.Size(79, 17);
            this.chkTime.TabIndex = 2;
            this.chkTime.Text = "TimeStamp";
            this.chkTime.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(250, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(73, 25);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Location = new System.Drawing.Point(486, 7);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(132, 20);
            this.txtFilter.TabIndex = 6;
            // 
            // chkautoscroll
            // 
            this.chkautoscroll.AutoSize = true;
            this.chkautoscroll.Checked = true;
            this.chkautoscroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkautoscroll.Location = new System.Drawing.Point(162, 9);
            this.chkautoscroll.Name = "chkautoscroll";
            this.chkautoscroll.Size = new System.Drawing.Size(71, 17);
            this.chkautoscroll.TabIndex = 8;
            this.chkautoscroll.Text = "autoscroll";
            this.chkautoscroll.UseVisualStyleBackColor = true;
            this.chkautoscroll.CheckedChanged += new System.EventHandler(this.chkautoscroll_CheckedChanged);
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilter.Location = new System.Drawing.Point(624, 5);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(65, 23);
            this.btnFilter.TabIndex = 9;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // UcConsoleWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 162);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.chkautoscroll);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.chkTime);
            this.Controls.Add(this.txtCons);
            this.Controls.Add(this.chkClear);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UcConsoleWindow";
            this.TabText = "";
            this.Text = "Console";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UcConsoleWindow_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCons;
        private System.Windows.Forms.CheckBox chkClear;
        private System.Windows.Forms.CheckBox chkTime;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.CheckBox chkautoscroll;
        private System.Windows.Forms.Button btnFilter;

    }
}
