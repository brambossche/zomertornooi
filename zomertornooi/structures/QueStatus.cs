using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Marb.Extender.Invoke;

namespace structures.structures
{
    public partial class QueStatus : UserControl
    {
        public QueStatus()
        {
            InitializeComponent();
        }

        public bool Questatus 
        {
            set { pictureBox1.Invoke(() => pictureBox1.Visible = value); }
            get { return pictureBox1.Visible; }
        }
    }
}
