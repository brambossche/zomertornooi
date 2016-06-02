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
using TournamentCalculation;

namespace structures.Views
{
    public partial class UC_FinalRounds : UserControl
    {
        private ActiveBindingList<Wedstrijd> _wedstrijdlist;
        private List<string> _ReeksList = new List<string>();
        RoundRobinGenerator rrg = new RoundRobinGenerator();


        public UC_FinalRounds(ActiveBindingList<Wedstrijd> wedstrijdlist, ActiveBindingList<Ploeg> ploegList)
        {
            _wedstrijdlist = wedstrijdlist;
            _wedstrijdlist.ListChanged += _wedstrijdlist_ListChanged;
            InitializeComponent();
        }

        void _wedstrijdlist_ListChanged(object sender, ListChangedEventArgs e)
        {

        }

        private void nc_aantalReeksen_ValueChanged(object sender, EventArgs e)
        {
            switch ((int)nc_aantalReeksen.Value)
            {
                case 1:
                    cmb_Reeks1.Enabled = true;
                    cmb_Reeks2.Enabled = false;
                    cmb_Reeks3.Enabled = false;
                    cmb_Reeks4.Enabled = false;
                    break;
                case 2:
                    cmb_Reeks1.Enabled = true;
                    cmb_Reeks2.Enabled = true;
                    cmb_Reeks3.Enabled = false;
                    cmb_Reeks4.Enabled = false;
                    break;
                case 3:
                    cmb_Reeks1.Enabled = true;
                    cmb_Reeks2.Enabled = true;
                    cmb_Reeks3.Enabled = true;
                    cmb_Reeks4.Enabled = false;
                    break;
                case 4:
                    cmb_Reeks1.Enabled = true;
                    cmb_Reeks2.Enabled = true;
                    cmb_Reeks3.Enabled = true;
                    cmb_Reeks4.Enabled = true;
                    break;
            }



        }

        private void btn_SimulateFinals_Click(object sender, EventArgs e)
        {







        }


        public void GetKlassement()
        {

        }


        public void GetReeksen()
        {
            _ReeksList.Clear();
            foreach (Wedstrijd w in _wedstrijdlist)
            {
                if (!_ReeksList.Contains(w.ReeksNaam))
                {
                    _ReeksList.Add(w.ReeksNaam);
                }
            }
        }





    }
}
