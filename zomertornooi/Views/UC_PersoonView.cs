using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using Views;
using System.Threading.Tasks;
using System.Windows.Forms;
using Factory;

namespace structures.Views
{

    

    public partial class UC_PersoonView : UserControl
    {
        private ActiveBindingList<Persoon> _PersoonList;


        public UC_PersoonView(ActiveBindingList<Persoon> PersoonList)
        {
            InitializeComponent();
            _PersoonList = PersoonList;
            Userview<Persoon> _PersoonView = new Userview<Persoon>(PersoonList, false) { Name = "PersoonView" };
            _PersoonView.Dock = DockStyle.Fill;
            panel1.Controls.Add(_PersoonView);



        }

        private void button1_Click(object sender, EventArgs e)
        {
            _PersoonList.Add(new Persoon() {Voornaam = txt_Voornaam.Text, Naam = txt_Naam.Text, 
                Straat = txt_Straat.Text, Nr = txt_Nr.Text, 
                Woonplaats = txt_Woonplaats.Text, Postcode = txt_Postcode.Text, 
                Land = txt_Land.Text, TelNr = txt_TelNr.Text, GSMNr = txt_GSM.Text,Email = txt_email.Text
            });
        }
    }
}
