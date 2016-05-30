 using System;
using System.Collections.Generic;
using System.ComponentModel;
using FluentNHibernate.Mapping;
using System.Linq;
using ProgramDefinitions;
using System.Data;

namespace structures
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Serializable]
    public class Reeks : Structurebase,INotifyPropertyChanged
    {

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }

        #endregion

        //[Browsable(false)]
        public virtual int ID
        {
            get;
            set;
        }

        // Field
        private string _ReeksNaam = "";
        public virtual string ReeksNaam
        {
            get { return _ReeksNaam; }
            set { _ReeksNaam = value; this.NotifyPropertyChanged(ID); }
        }


        private  List<Ploeg> _Ploegen = new List<Ploeg>();

        [Browsable(false)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual List<Ploeg> Ploegen
        {
            get { return _Ploegen; }
            set { _Ploegen = value; this.NotifyPropertyChanged(ID); }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Ploeg Ploegnamen
        {
            get {
                
                return _Ploegen.First();}
        }

        private List<Ploeg> _FreeTeams = new List<Ploeg>();

        public List<Ploeg> FreeTeams
        {
            get { return _FreeTeams; }
            set { _FreeTeams = value; }
        }


        #region Optimization results
        private int _AantalRondes = 0;

        public int AantalRondes
        {
            get { return _AantalRondes; }
            set { _AantalRondes = value; }
        }

        private int _MaxScheids = 0;

        public int MaxScheids
        {
            get { return _MaxScheids; }
            set { _MaxScheids = value; }
        }

        private int _MaxRust = 0;

        public int MaxRust
        {
            get { return _MaxRust; }
            set { _MaxRust = value; }
        }

        private bool _DubbelScheids = false;

        public bool DubbelScheids
        {
            get { return _DubbelScheids; }
            set { _DubbelScheids = value; }
        }

        private bool _Vrijescheids = false;

        public bool Vrijescheids
        {
            get { return _Vrijescheids; }
            set { _Vrijescheids = value; }
        }


        #endregion


        private List<Wedstrijd> _RoundRobin = new List<Wedstrijd>();

        public List<Wedstrijd> RoundRobin
        {
            get { return _RoundRobin; }
            set { _RoundRobin = value; }
        }

        private List<Wedstrijd> _TimeSchedule = new List<Wedstrijd>();

        public List<Wedstrijd> TimeSchedule
        {
            get { return _TimeSchedule; }
            set { _TimeSchedule = value; }
        }

        private List<Wedstrijd> _TimeScheduleHulp = new List<Wedstrijd>();

        public List<Wedstrijd> TimeScheduleHulp
        {
            get { return _TimeScheduleHulp; }
            set { _TimeScheduleHulp = value; }
        }

        private TornooiFormule _TornooiFormule = TornooiFormule.RoundRobin;

        public TornooiFormule TornooiFormule
        {
            get { return _TornooiFormule; }
            set { _TornooiFormule = value; }
        }

        private WedstrijdDefinition _WedstrijdDefinition = new WedstrijdDefinition();

        public WedstrijdDefinition WedstrijdDefinition
        {
            get { return _WedstrijdDefinition; }
            set { _WedstrijdDefinition = value; }
        }



        private List<Terrein> _Terreinen = new List<Terrein>();

        public List<Terrein> Terreinen
        {
            get { return _Terreinen; }
            set { _Terreinen = value; }

        }
    }

    public class ReeksMapping : ClassMap<Reeks>
    {
        public ReeksMapping()
        {
            Id(x => x.ID)
                .GeneratedBy.Increment()
                .Not.Nullable();

            Map(x => x.ReeksNaam)
                .Unique();
            HasMany(x => x.Ploegen).KeyColumn("PloegID");
        }
    }

}
