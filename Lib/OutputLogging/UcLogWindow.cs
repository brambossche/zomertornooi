using System;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;



namespace OutputLogging
{
    public partial class UcLogWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private string  templogfile = @"C:\temp\templog.log";
        private int Lineswriten = 0;
        private string messagebuffer = "";
        private const int maxlines = 10;
        public bool diagmode = true;
        private delegate void dAppendText(string txt);

        private bool _LowgiwndowWriteToFileOnly = false;
        public bool LowgiwndowWriteToFileOnly
        {
            get
            {
                return _LowgiwndowWriteToFileOnly;
            }
            set
            {
                _LowgiwndowWriteToFileOnly = value;

            }
        }

        public bool LogwindowEnableTimestamp
        {
            get
            {
                return this.chkTime.Checked;
            }
            set
            {
                this.chkTime.Checked = value;
            }
        }

        public bool LogwindowEanableCaller
        {
            get
            {
                return this.CB_Enabletsacktrace.Checked;
            }
            set
            {
                this.CB_Enabletsacktrace.Checked = value;
            }
        }

        public UcLogWindow()
        {
            InitializeComponent();

         //   this.Icon = PCL.lib.Properties.Resources.information_icon;
            txtCons.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Load += UcLogWindow_Load;

        }

        void UcLogWindow_Load(object sender, EventArgs e)
        {
            /*_UserConfigurations.Add(_filesave, "Save log file");
            _UserConfigurations.Add(chkClear, "Options");
            _UserConfigurations.Add(chkTime, "Options");
            _UserConfigurations.Add(CB_EnableI2CLog, "Options");
            _UserConfigurations.Add(CB_Enabletsacktrace, "Options");*/
        }

        public delegate void cleardel();
        public void clear()
        {

            if (this.InvokeRequired)
            {
                this.Invoke(new cleardel(clear));
            }
            else
            {

                btnClear_Click(null, null);

                if (_LowgiwndowWriteToFileOnly)
                {
                    messagebuffer = "";
                    using (FileStream fs = new FileStream(templogfile, FileMode.Truncate, FileAccess.Write))
                    {
                        fs.SetLength(0);
                    }
                }
            }
        
        }


        public void write(string Message)
        {
            if (txtCons.InvokeRequired)
            {
                txtCons.BeginInvoke(new dAppendText(write), new object[] { Message });
            }
            else
            {
                if (chkTime.Checked)
                {
                    Message = DateTime.Now.ToString() + " : " + Message;
                }
                if (!(_LowgiwndowWriteToFileOnly))
                {
                    if (txtCons.Lines.Length > 500 && chkClear.Checked)
                    {
                        txtCons.Clear();
                    }
                    txtCons.AppendText(Message);
                    txtCons.Refresh();
                    this.Text = "Log (" + (txtCons.Lines.Count() - 1).ToString() + ")"; //count -1 because an empty string was count in
                }
                else
                {
                    this.Text = "Loggings are routed to log file and not printed in text box";
                    messagebuffer = messagebuffer + Message;
                    Lineswriten ++;
                    if (Lineswriten > maxlines)
                    {
                        
                        WriteFilestream(messagebuffer);
                        messagebuffer = "";
                        Lineswriten = 0;
                    }
                }
            }
        }
        
        private void WriteFilestream (string message)
        {
            try
            {
                using (FileStream fs = new FileStream(templogfile, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(message);
                    sw.Flush();
                }              
            }
            
            catch (Exception e)
            {
                txtCons.Text = "Could not create or append to file " + e.ToString();
            }
        }

        public void writeLine(string Message)
        {
            if (diagmode)
            {
                StackTrace st = new StackTrace(true);
   
                StackFrame sf = st.GetFrame(2);
                Message = "<" + (sf.GetMethod().ReflectedType.Name + ">").PadRight(30) + Message;
            }

            write(Message + Environment.NewLine);
        }

        public string GetLogText()        
        {
            string temp ="";

            if (!(_LowgiwndowWriteToFileOnly))
            {
                temp = txtCons.Text;
            }
            else
            {

                try
                {
                    WriteFilestream(messagebuffer);
                    using (FileStream fileStream = File.Open(templogfile, FileMode.Open, FileAccess.Read)) //append mode creates file if does not exists
                    {
                        using (StreamReader sr = new StreamReader(fileStream))
                        {
                            try
                            {
                                temp = sr.ReadToEnd();
                            }
                            catch (OutOfMemoryException oom)
                            {
                                txtCons.Text = "Reading log file returns memory exception" + oom.ToString();
                            }
                            catch (Exception e)
                            {
                                txtCons.Text = "Getogtext creates exception" + e.ToString();
                            }
                        }
                    }
                }
                catch (FileNotFoundException fnf)
                {
                    txtCons.Text = "log file not found " + fnf.ToString();
                }
            }

            return temp;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCons.Clear();
            this.Text = "Log";
        }

        private void CB_Enabletsacktrace_CheckedChanged(object sender, EventArgs e)
        {
            diagmode = CB_Enabletsacktrace.Checked;            
        }

        private void btn_savetofile_Click(object sender, EventArgs e)
        {
            SaveFileDialog _filesave = new SaveFileDialog();
            _filesave.FileName = "logging";
            _filesave.Filter = "log file (*.log)|*.*";
            _filesave.FilterIndex = 1;
            _filesave.DefaultExt = "log";
            _filesave.AddExtension = true;
            if (_filesave.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter sw = System.IO.File.CreateText(_filesave.FileName);
                try
                {
                    sw.Write(txtCons.Text);
                }
                catch (Exception)
                {
                    //debug.dump("cannot write to file");
                }
                finally
                {
                    sw.Close();
                }                
            }
        }

        private void UcLogWindow_FormClosing(object sender, FormClosingEventArgs e)
        {            
        }

        private void chkClear_CheckedChanged(object sender, EventArgs e)
        {            
        }

    }
}
