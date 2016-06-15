using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace structures.Views.Final_Rounds
{
    public partial class UC_Brackets : UserControl
    {
        public UC_Brackets()
        {
            InitializeComponent();


            

        }


        public UC_Brackets(string h, string a)
        {
            InitializeComponent();
            lbl_Home.Text = h;
            lbl_Away.Text = a;

        }



        private void UC_Brackets_Load(object sender, EventArgs e)
        {

        }
    }
}
