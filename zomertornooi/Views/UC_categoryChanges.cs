using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace structures.Views
{

    public delegate void del_categorychanged(Category oldcategory, Category newcategory);
    public delegate void del_setbackcategory();
    public partial class UC_categoryChanges : UserControl
    {
        private Category _input = new Category();
        private Category _output = new Category();

        public event del_categorychanged categorychanged;
        public event del_setbackcategory setbackcategory;
        public UC_categoryChanges()
        {
            InitializeComponent();
            lstbx_categoryinput.DataSource = Category.Categories.ToList();
            lstbx_outpucategory.DataSource = Category.Categories.ToList();
        }

        private void lstbx_categoryinput_SelectedIndexChanged(object sender, EventArgs e)
        {
           Category input = (Category)(lstbx_categoryinput.SelectedItem);

           lstbx_outpucategory.DataSource = Category.Categories.Where(x => x.Geslacht == input.Geslacht).Where (x=>x.Niveau != input.Niveau).ToList();
        }

        private void lstbx_outpucategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_changecategory_Click(object sender, EventArgs e)
        {

             DialogResult result  =  MessageBox.Show("This action cannot be undone. Are you sure to change categorie from " + lstbx_categoryinput.SelectedItem + " to " + lstbx_outpucategory.SelectedItem + " ?"
                , "Changing categories", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

             if (result == DialogResult.Yes)
             {
                 if (categorychanged != null)
                 {
                     categorychanged.Invoke((Category)(lstbx_categoryinput.SelectedItem), (Category)(lstbx_outpucategory.SelectedItem));
                 }
             }
        }

        private void bnt_setbackcategory_Click(object sender, EventArgs e)
        {
            
             DialogResult result  =  MessageBox.Show("This action cannot be undone. Are you sure to set the categories back to their original state ? " 
                , "Changing categories", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

             if (result == DialogResult.Yes)
             {
                 if (setbackcategory != null)
                 {
                     setbackcategory.Invoke();
                 }
             }
        }
    }
}
