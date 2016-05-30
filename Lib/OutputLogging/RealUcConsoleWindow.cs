using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;



namespace OutputLogging
{
    public partial class RealUcConsoleWindow : UserControl
    {
        
        private List<Filter> filterlist = new List<Filter>();
        private List<string> databuff = new List<string>();
        private delegate void dAppendText(string txt);

        
        public RealUcConsoleWindow()
        {
            InitializeComponent();
            txtCons.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
                if (txtCons.Lines.Length > 500 && chkClear.Checked )
                {
                    txtCons.Clear();
                }

                if (!chkautoscroll.Checked)
                {
                 // fill buffer 
                    databuff.Add( Message);


                }
                else 
                {
                    txtCons.AppendText(Message);
                }
                
              



               // txtCons.Refresh();
                this.Text = "Console (" + txtCons.Lines.Count().ToString() + ")";

            }
        }

        private delegate void writelinedel(string msg);

        public void writeLine(string Message)
        {
     
            if (this.InvokeRequired)
            {
                this.Invoke(new writelinedel(writeLine), new object[] { Message });
            }
            else
            {               

                if (chkTime.Checked)
                {
                    Message = DateTime.Now.ToString() + " : " + Message;

                }
                if (txtCons.Lines.Length > 500 && chkClear.Checked)
                {
                    txtCons.Clear();
                }
       
                this.Text = "Console (" + txtCons.Lines.Count().ToString() + ")";

                if (!chkautoscroll.Checked)
                {
                  //fill buffer
                  databuff.Add(   Message );
                }
                else
                {
                    txtCons.AppendText(Message + Environment.NewLine);
                }

                passtofilter(Message);

              
            }    
        }


      
        private void passtofilter(string Message)
        {
            for (int i = 0; i < filterlist.Count; i++)
            {
                filterlist[i].addline(Message);
            }
        
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCons.Clear();
        }

        private void chkautoscroll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkautoscroll.Checked)
            { 
                //add buffer to the window again
                List<string> emptybuff = databuff;
                for (int i = 0; i < emptybuff.Count; i++)
                {
                    writeLine(emptybuff[i]);

                }

                databuff = new List<string>();
       

            }
        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
          //  txtFilter.Enabled = chkFilter.Checked;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Filter f = new Filter();
            f.init(txtFilter.Text, txtCons.Lines);
           // f.Show(this.DockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Float);
           
           
            f.FormClosing += new FormClosingEventHandler(f_FormClosing);

            filterlist.Add(f);

        }

        void f_FormClosing(object sender, FormClosingEventArgs e)
        {
            Filter thesender = (Filter)sender;
            if (filterlist.Contains(thesender))
            {
                
                filterlist.Remove(thesender);

            }
        }

        private void txtCons_TextChanged(object sender, EventArgs e)
        {

        }
   
    }
}
