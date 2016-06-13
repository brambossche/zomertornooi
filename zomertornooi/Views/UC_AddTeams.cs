using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Factory;

namespace structures.Views
{
    public partial class UC_AddTeams : UserControl
    {
        ActiveBindingList<Ploeg> _PloegList;
        ActiveBindingList<Persoon> _PersoonList;


        public UC_AddTeams(ActiveBindingList<Ploeg> PloegList, ActiveBindingList<Persoon> PersoonList)
        {
            _PloegList = PloegList;
            _PloegList.ListChanged += _PloegList_ListChanged;
            _PersoonList = PersoonList;
            _PersoonList.ListChanged += _PersoonList_ListChanged;
            InitializeComponent();
            UpdateComboboxes();
            cmb_category.SelectedIndex = 0;
        }

        void _PersoonList_ListChanged(object sender, ListChangedEventArgs e)
        {
            UpdateComboboxes();
        }

        void _PloegList_ListChanged(object sender, ListChangedEventArgs e)
        {
            UpdateComboboxes();
        }


        public void UpdateComboboxes()
        {
            cmb_category.Items.Clear();
            cmb_category.Items.AddRange(Category.Categories.ToArray());

            cmb_ContactPersoon.Items.Clear();
            foreach (Persoon p in _PersoonList)
            {
                cmb_ContactPersoon.Items.Add(p);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Persoon p;
            if (cmb_ContactPersoon.SelectedIndex >= 0)
            {
                p = (Persoon)cmb_ContactPersoon.SelectedItem;
            }
            else
            {
                p = null;
            }

            Category c = (Category)cmb_category.SelectedItem; 
            
            Ploeg pl = new Ploeg()
            {
                Ploegnaam = txt_PloegNaam.Text,
                Betaald = chb_Betaald.Checked,
                Contactpersoon = p,
                SubscribedCategory = c,
                Category = c
            };

            _PloegList.Add(pl);

            txt_PloegNaam.Clear();
            chb_Betaald.Checked = false;
            cmb_category.SelectedIndex = 0;
            cmb_ContactPersoon.SelectedItem = 0;


        }



    }
}
