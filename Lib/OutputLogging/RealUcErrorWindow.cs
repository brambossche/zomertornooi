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
    public partial class RealUcErrorWindow : UserControl
    {
        List<LogLine> logLineList = new List<LogLine>();


        public RealUcErrorWindow()
        {
            InitializeComponent();

           // this.Icon = PCL.lib.Properties.Resources.close;
        }

        public void logError(LogLine l)
        {
            logLineList.Add(l);
            updateDVG();

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

                this.Refresh();


            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
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
    }
}
