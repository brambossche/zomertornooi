using System;
using System.Windows.Forms;

namespace OutputLogging
{
    public partial class Filter :  WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public string filter = "";

        public Filter()
        {
            InitializeComponent();
        }

        public void init(string filterline,string[] prevlines)
        {
            filter = filterline;

            for (int i = 0; i < prevlines.Length; i++)
            {
                addline(prevlines[i]);
            }
            this.Text = filterline;
        }

        public delegate void addlinedel(string txt);


        public void addline(string txt)
        {
            if (txt.ToLower().Contains(filter.ToLower()))
            {

                if (this.InvokeRequired)
                {
                    this.Invoke(new addlinedel(addline), new object[] { txt });

                    return;
                }
                txtdat.AppendText(txt + Environment.NewLine);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            highlightnext(txtfind.Text);
        }

        int lastindex = 0;
        int currentocc = 0;

        private void highlightnext(string findstring)
        {
            findstring = findstring.ToLower();

            lastindex = txtdat.Text.ToLower().IndexOf(findstring, lastindex);
            if (lastindex == -1 || lastindex > txtdat.Text.Length)
            {
                lastindex = 0;
                currentocc = 0;


            }
            else
            {
                txtdat.Select(lastindex, findstring.Length);
                lastindex = lastindex + findstring.Length;
                currentocc++;
                lblcurrentocc.Text = currentocc.ToString() + " / ";
                txtdat.ScrollToCaret();

            }


        }

        private int countOccurences(string needle, string haystack)
        {

            if (needle == "")
            {
                return 0;

            }

            return (haystack.Length - haystack.Replace(needle, "").Length) / needle.Length;
        }

        void txtfind_TextChanged(object sender, System.EventArgs e)
        {
            string findstring = txtfind.Text;
            int count = countOccurences(findstring.ToLower(), txtdat.Text.ToLower());
            lblfindresult.Text = count.ToString() + " Occurences found";
            lastindex = 0;

            if (count > 0)
            {

                highlightnext(findstring);
                btnSearch.Text = "next";

            }
        }

        void txtfind_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            switch ((Keys)e.KeyChar)
            {
                case Keys.Enter:

                    highlightnext(txtfind.Text);


                    break;
                default:
                    break;
            }
        }

        private void txtfind_Click(object sender, EventArgs e)
        {

        }

    }
}
