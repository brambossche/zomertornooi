using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PCL.OutputLogging;


namespace LogViewer
{
    public partial class Form1 : Form
    {
        PCL.OutputLogging.LogFile lf;


        public Form1()
        {
            InitializeComponent();
            dgv.CellDoubleClick += new DataGridViewCellEventHandler(dgv_CellDoubleClick);    
        
        }

        

        void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogLine l = (LogLine)dgv.Rows[e.RowIndex].DataBoundItem;
                MessageBox.Show(l.getExecption().ToString());
            }
            catch (Exception ee)
            { }
        }

        public void openfile(string fname)
        {
            
            lf = new PCL.OutputLogging.LogFile(fname);
            fillDG();
        
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = opn.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                string fname = opn.FileName;
                lf = new PCL.OutputLogging.LogFile(fname);
                fillDG();

            }

        }

        private void fillDG()
        {

            dgv.DataSource = lf.logLineList;
            dgv.Refresh();
            dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);


            /*
            for (int i = 0; i < lf.logLineList.Count ; i++)
            {
                
            }*/
        
        }
    }
}
