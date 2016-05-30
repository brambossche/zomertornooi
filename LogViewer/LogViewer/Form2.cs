using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LogViewer
{
    public partial class Form2 : Form
    {
        public Form2()
        {

            InitializeComponent();
            Icon i = new System.Drawing.Icon(LogViewer.Properties.Resources.Actions_configure1, new System.Drawing.Size(48, 48));
            button1.Image = i.ToBitmap();

        }
    }
}
