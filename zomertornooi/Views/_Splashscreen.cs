using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Views;

namespace structures.Views
{
    public partial class _Splashscreen : Form
    {
        int count = 0, buffer = 0;
        private UserLevel _UserLevel;


        public _Splashscreen(UserLevel userLevel)
        {
            InitializeComponent();
            _UserLevel = userLevel;
            Opacity = 0;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Opacity == 1 )
            {
                timer2.Start();
                timer1.Stop();
            }
            else
            {
                count++;
                Opacity = count * 0.01;
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (buffer == 3)
            {
                timer3.Start();
                timer2.Stop();
            }
            else
            {
                buffer++;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (Opacity == 0)
            {
                //BaseForm Tornooi = new BaseForm(_UserLevel);
                //Tornooi.ShowDialog();
                timer1.Stop();
                timer2.Stop();
                timer3.Stop();
                Dispose();


            }
            else
            {
                count--;
                Opacity = count * 0.01;
            }
        }

    }
}
