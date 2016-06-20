using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomAttributes;
using FluentNHibernate.Mapping;
using ProgramDefinitions;

namespace structures
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Ploeg : Structurebase
    {
        public virtual int ID
        {
            get;
            set;
        }

        private string _Ploegnaam = "";

        /// <summary>
        /// Give the 
        /// </summary>       
        public virtual string Ploegnaam
        {
            get { return _Ploegnaam; }
            set
            {
                _Ploegnaam = value;
                this.NotifyPropertyChanged(ID);
            }
        }


        private List<Status> _StatusList = new List<Status>();

        [Browsable (false)]
        public List<Status> StatusList
        {
            get { return _StatusList; }
            set { _StatusList = value; }
        }


        private int _AantalxScheids = 0;
        [Browsable(false)]
        public int AantalxScheids
        {
            get { return _AantalxScheids; }
            set { _AantalxScheids = value; }
        }

        private int _AantalxGespeeld = 0;
        [Browsable(false)]
        public int AantalxGespeeld
        {
            get { return _AantalxGespeeld; }
            set { _AantalxGespeeld = value; }
        }

        private int _AantalxRust = 0;
        [Browsable(false)]
        public int AantalxRust
        {
            get { return _AantalxRust; }
            set { _AantalxRust = value; }
        }

        private int _AantalxNaElkaarGespeeld = 0;
        [Browsable(false)]
        public int AantalxNaElkaarGespeeld
        {
            get { return _AantalxNaElkaarGespeeld; }
            set { _AantalxNaElkaarGespeeld = value; }
        }

        private int _AantalxNaElkaarScheids = 0;
        [Browsable(false)]
        public int AantalxNaElkaarScheids
        {
            get { return _AantalxNaElkaarScheids; }
            set { _AantalxNaElkaarScheids = value; }
        }

        private int _AantalxNaElkaarRust = 0;
        [Browsable(false)]
        public int AantalxNaElkaarRust
        {
            get { return _AantalxNaElkaarRust; }
            set { _AantalxNaElkaarRust = value; }
        }

        private bool _Betaald = false;        
        public virtual bool Betaald
        {
            get { return _Betaald; }
            set { _Betaald = value; this.NotifyPropertyChanged(ID); }
        }

        private bool _Aangemeld = false;

        
        public virtual bool Aangemeld
        {
            get { return _Aangemeld; }
            set { _Aangemeld = value; this.NotifyPropertyChanged(ID); }
        }

        private Persoon _Contactpersoon = new Persoon();
        [PropertyGridViewer(ShowInPropertyGrid = true)]
        [TypeConverter(typeof(ExpandableObjectConverter))] //make it expandable in the propertygrid
        public virtual Persoon Contactpersoon
        {
            get { return _Contactpersoon; }
            set
            {
                _Contactpersoon = value;
                this.NotifyPropertyChanged(ID);
            }
        }


        private Category _SubscribedCategory = new Category();
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [PropertyGridViewer(ShowInPropertyGrid = false)]
        public virtual Category SubscribedCategory
        {
            get { return _SubscribedCategory; }
            set { _SubscribedCategory = value; this.NotifyPropertyChanged(ID); }
        }

        private Category _Category = new Category();
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [PropertyGridViewer(ShowInPropertyGrid = false)]
        public virtual Category Category
        {
            get { return _Category; }
            set { _Category = value; this.NotifyPropertyChanged(ID); }
        }

        private string _Reeksnaam = "";

        public string Reeksnaam
        {
            get { return _Reeksnaam; }
            set { _Reeksnaam = value; this.NotifyPropertyChanged(ID); }
        } 


         public override string ToString()
         {
             return this.Ploegnaam;
         }

    }


    /// <summary>
    /// Mapping to NHibernate
    /// 
    /// </summary>
    class PloegMapping : ClassMap<Ploeg>
    {
        public PloegMapping()
        {
            Id(x => x.ID)
                .GeneratedBy.Increment()
                .Not.Nullable();

            Map(x => x.Ploegnaam)
                .Unique();

            Component(x => x.Category, c =>
                      {
                          c.Map(x => x.Geslacht);
                          c.Map(x => x.Niveau);
                      });

            Component(x => x.SubscribedCategory, c =>
            {
                c.Map(x => x.Geslacht, "SubscribedGeslacht");
                c.Map(x => x.Niveau, "SubscribedNiveau");
            });


           References(x => x.Contactpersoon).Not.LazyLoad();

            Map(x => x.Betaald);
            Map(x => x.Aangemeld);
            Map(x => x.Reeksnaam);
            Map(x => x.AantalxNaElkaarScheids);
            Map(x => x.AantalxScheids);
            Table(typeof(Ploeg).Name);
        }
    }

}
