using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace structures.structures
{
    public class Klassement
    {
        private DataTable _Ranking = new DataTable();

        public DataTable Ranking
        {
            get { return _Ranking; }
            set { _Ranking = value; }
        }

        private string _ReeksNaam = "";

        public string ReeksNaam
        {
            get { return _ReeksNaam; }
            set { _ReeksNaam = value; }
        }





    }
}
