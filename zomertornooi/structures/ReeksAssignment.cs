using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace structures
{

    public class ReeksAssignment
    {
        public virtual int ID
        {
            get;
            set;
        }
        private Category _Category;
        [ReadOnly(true)]
        public Category Category
        {
            get { return _Category; }
            set { _Category = value; }
        }

        private int _AantalPloegen = 0;

        [ReadOnly(true)]
        public int AantalPloegen
        {
            get { return _AantalPloegen; }
            set { _AantalPloegen = value; }
        }

        private int _AangemeldePloegen = 0;
        [ReadOnly(true)]
        public int AangemeldePloegen
        {
            get { return _AangemeldePloegen; }
            set { _AangemeldePloegen = value; }
        }

        private int _NrOfReeksen = 0;
        public int NrOfReeksen
        {
            get { return _NrOfReeksen; }
            set { _NrOfReeksen = value; }
        }
    }
}


