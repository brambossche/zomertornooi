namespace OutputLogging
{
    partial class UcLogWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcLogWindow));
            this.txtCons = new System.Windows.Forms.TextBox();
            this.chkClear = new System.Windows.Forms.CheckBox();
            this.chkTime = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.CB_Enabletsacktrace = new System.Windows.Forms.CheckBox();
            this.btn_savetofile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtCons
            // 
            this.txtCons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCons.Location = new System.Drawing.Point(0, 33);
            this.txtCons.Multiline = true;
            this.txtCons.Name = "txtCons";
            this.txtCons.ReadOnly = true;
            this.txtCons.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCons.Size = new System.Drawing.Size(741, 270);
            this.txtCons.TabIndex = 0;
            // 
            // chkClear
            // 
            this.chkClear.Checked = true;
            this.chkClear.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClear.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkClear.Location = new System.Drawing.Point(0, 0);
            this.chkClear.Margin = new System.Windows.Forms.Padding(3, 3, 3, 30);
            this.chkClear.Name = "chkClear";
            this.chkClear.Size = new System.Drawing.Size(741, 33);
            this.chkClear.TabIndex = 2;
            this.chkClear.Text = "AutoClear";
            this.chkClear.UseVisualStyleBackColor = true;
            this.chkClear.CheckedChanged += new System.EventHandler(this.chkClear_CheckedChanged);
            // 
            // chkTime
            // 
            this.chkTime.AutoSize = true;
            this.chkTime.Location = new System.Drawing.Point(78, 8);
            this.chkTime.Name = "chkTime";
            this.chkTime.Size = new System.Drawing.Size(79, 17);
            this.chkTime.TabIndex = 3;
            this.chkTime.Text = "TimeStamp";
            this.chkTime.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(333, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(73, 25);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // CB_Enabletsacktrace
            // 
            this.CB_Enabletsacktrace.AutoSize = true;
            this.CB_Enabletsacktrace.Location = new System.Drawing.Point(163, 8);
            this.CB_Enabletsacktrace.Name = "CB_Enabletsacktrace";
            this.CB_Enabletsacktrace.Size = new System.Drawing.Size(79, 17);
            this.CB_Enabletsacktrace.TabIndex = 6;
            this.CB_Enabletsacktrace.Text = "show caller";
            this.CB_Enabletsacktrace.UseVisualStyleBackColor = true;
            this.CB_Enabletsacktrace.CheckedChanged += new System.EventHandler(this.CB_Enabletsacktrace_CheckedChanged);
            // 
            // btn_savetofile
            // 
            this.btn_savetofile.Location = new System.Drawing.Point(412, 3);
            this.btn_savetofile.Name = "btn_savetofile";
            this.btn_savetofile.Size = new System.Drawing.Size(73, 25);
            this.btn_savetofile.TabIndex = 7;
            this.btn_savetofile.Text = "Save to file";
            this.btn_savetofile.UseVisualStyleBackColor = true;
            this.btn_savetofile.Click += new System.EventHandler(this.btn_savetofile_Click);
            // 
            // UcLogWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 303);
            this.Controls.Add(this.btn_savetofile);
            this.Controls.Add(this.CB_Enabletsacktrace);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.chkTime);
            this.Controls.Add(this.txtCons);
            this.Controls.Add(this.chkClear);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UcLogWindow";
            this.TabText = "";
            this.Text = "Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UcLogWindow_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCons;
        private System.Windows.Forms.CheckBox chkClear;
        private System.Windows.Forms.CheckBox chkTime;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox CB_Enabletsacktrace;
        private System.Windows.Forms.Button btn_savetofile;

    }
}
