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

namespace structures.Views.Final_Rounds
{
    public partial class UC_AllBrackets : UserControl
    {
        private List<UC_Brackets> _FinalGames = new List<UC_Brackets>();
        private ActiveBindingList<Wedstrijd> _WedstrijdList;

        public List<UC_Brackets> FinalGames
        {
            get { return _FinalGames; }
            set { _FinalGames = value; }
        }


        public UC_AllBrackets()
        {
            InitializeComponent();
        }


        public UC_AllBrackets(int AantalRonden, int AantalWedstrijdenPerRonde)
        {
            this.Dock = DockStyle.Fill;
            InitializeComponent();
            tbl_Brackets.ColumnCount = 2 * AantalRonden - 1;
            tbl_Brackets.RowCount = AantalWedstrijdenPerRonde;
            for (int i = 0; i < AantalRonden * AantalWedstrijdenPerRonde; i++)
            {
                UC_Brackets ucb = new UC_Brackets();
                ucb.Dock = DockStyle.Fill;
                _FinalGames.Add(ucb);
            }

            int index = 0;
            tbl_Brackets.RowStyles.Clear();
            tbl_Brackets.ColumnStyles.Clear();

            for (int i = 0; i < AantalWedstrijdenPerRonde; i++)
            {
                tbl_Brackets.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, Convert.ToSingle(100 / AantalWedstrijdenPerRonde)));
                for (int j = 0; j < 2 * AantalRonden - 1; j++)
                {
                    if (j % 2 == 0)
                    {
                        tbl_Brackets.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, Convert.ToSingle((100 - ((AantalRonden-1)*5)) / AantalRonden)));
                        tbl_Brackets.Controls.Add(_FinalGames[index], j, i);
                        index++;
                    }
                    else
                    {
                        tbl_Brackets.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, Convert.ToSingle(5)));
                    }

                }
            }
        }

    }
}
