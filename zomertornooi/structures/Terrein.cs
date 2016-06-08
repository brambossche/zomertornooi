using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using System.ComponentModel;

namespace structures
{
    public class Terrein : Structurebase
    {
        [Browsable(false)]
        public virtual int ID { get; set; }

        private int _TerreinNr = 0;

        public virtual int TerreinNr
        {
            get { return _TerreinNr; }
            set { _TerreinNr = value; this.NotifyPropertyChanged(ID); }
        }

        private string _ReeksNaam = "";

        [ReadOnly(true)]
        [Browsable(false)]
        public virtual string ReeksNaam
        {
            get { return _ReeksNaam; }
            set { _ReeksNaam = value; }
        }

        private bool _status = true;
        [ReadOnly(true)]
        public virtual bool Status
        {
            get { return _status; }
            set { _status = value; this.NotifyPropertyChanged(ID);}
        }


        public override string ToString()
        {
            return TerreinNr.ToString();
        }


        private IList<Wedstrijd> _wedstrijden =  new List<Wedstrijd>();

        public virtual IList<Wedstrijd> wedstrijden
        {
            get { return _wedstrijden; }
            set { _wedstrijden = value; }
        }


        public class TerreingMapping : ClassMap<Terrein>
        {

            public TerreingMapping()
            {
                Id(x => x.ID)
                .GeneratedBy.Increment()
                .Not.Nullable();
                Map(x => x.Status);
                Map(x => x.ReeksNaam);
                Map(x => x.TerreinNr);
                //HasMany(x => x.wedstrijden).Cascade.Delete();
            }
        }
    }
}
