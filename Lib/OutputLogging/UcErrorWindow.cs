using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OutputLogging
{
    public partial class UcErrorWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        List<LogLine> logLineList = new List<LogLine>();

        /// <summary>
        /// eventhandling used to send to the Output controller
        /// http://beqbrgbrg1nb763:1024/redmine/issues/142
        /// </summary>
        public event Createreport Requestreport;
        public delegate void Createreport();

        public UcErrorWindow()
        {
            InitializeComponent();
            btn_Redminereport.Enabled = false;
           // this.Icon = PCL.lib.Properties.Resources.close;
        }

        public void logError(LogLine l)
        {
            logLineList.Add(l);
            updateDVG();

            if (logLineList.Count > 0)
            {
                btn_Redminereport.Enabled = true;
            }
        }

        private delegate void updateDVGdel();

        private void updateDVG()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new updateDVGdel(updateDVG));

            }
            else 
            {
                this.Text = "Error (" + logLineList.Count.ToString() + ")";
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = logLineList;
                dataGridView1.Refresh();
                CurrencyManager cm = (CurrencyManager)this.dataGridView1.BindingContext[logLineList];
                if (cm != null)
                {
                    cm.Refresh();
                }
                this.Refresh();
            }
        }

        private void tst_clearerror_Click(object sender, EventArgs e)
        {

            ClearErrorWindow();
        }

        public void ClearErrorWindow()
        {
            logLineList = new List<LogLine>();
            updateDVG();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogLine ll = (LogLine)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                switch (e.ColumnIndex)
                {
                    case 0:
                        {
                            MessageBox.Show(ll.DT.ToString(), "Information on error message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case 1:
                        {
                            MessageBox.Show(ll.LogType.ToString(), "Information on error message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case 2:
                        {
                            MessageBox.Show(ll.Message.ToString(), "Information on error message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case 3:
                        {
                            MessageBox.Show(ll.Source.ToString(), "Information on error message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        break;
                    case 4:
                        {
                            MessageBox.Show(ll.ObjectType.ToString(), "Information on error message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    default:
                        {
                            MessageBox.Show(ll.getExecption().ToString(), "Information on error message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                }
            }
            catch(Exception ee)
            {}
        }

        /// <summary>
        /// Button clicked means users wants to create output in Redmine
        /// eventhandling used to send to the Output controller
        /// http://beqbrgbrg1nb763:1024/redmine/issues/142
        /// </summary>
        private void btn_Redminereport_Click(object sender, EventArgs e)
        {
            Requestreport();
        }
    }
}
