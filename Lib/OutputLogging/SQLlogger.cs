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
    public partial class UcSQLLogger : WeifenLuo.WinFormsUI.Docking.DockContent
    {

        public UcSQLLogger()
        {
            InitializeComponent();
          //  this.Icon =   (Icon) PCL.lib.Properties.Resources.warning_icon;
        }

        public ListView GetListviewHandler()
        {
            return this.lvErrors;
        }
    }
}
