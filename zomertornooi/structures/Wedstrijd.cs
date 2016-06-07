using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace structures
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Wedstrijd : Structurebase
    {
        [Browsable(false)]
        public virtual int ID
        {
            get;
            set;
        }

        private DateTime _Aanvangsuur = new DateTime();

        public virtual DateTime Aanvangsuur
        {
            get { return _Aanvangsuur; }
            set { _Aanvangsuur = value; this.NotifyPropertyChanged(ID); }
        }
        
        private Ploeg _Home = new Ploeg();
        [ReadOnly(true)]
        public virtual Ploeg Home
        {
            get { return _Home; }
            set { _Home = value; }
        }

        private Ploeg _Away = new Ploeg();
        [ReadOnly(true)]
        public virtual Ploeg Away
        {
            get { return _Away; }
            set { _Away = value; }
        }

        private Terrein _Terrein = new Terrein();
        [ReadOnly(true)]
        public virtual Terrein Terrein
        {
            get { return _Terrein; }
            set { _Terrein = value; }
        }



        private Ploeg _Scheidsrechter = new Ploeg();
        [ReadOnly(true)]
        public virtual Ploeg Scheidsrechter
        {
            get { return _Scheidsrechter; }
            set { _Scheidsrechter = value; }
        }


        private string _ReeksNaam = "";
        [ReadOnly(true)]
        public virtual string ReeksNaam
        {
            get { return _ReeksNaam; }
            set { _ReeksNaam = value; }
        }


        private Reeks _Reeks = new Reeks();
        [ReadOnly(true)]
        [Browsable(false)]
        public virtual Reeks Reeks
        {
            get { return _Reeks; }
            set { _Reeks = value; }
        }


        private int _Set1Home = 0;

        public virtual int Set1Home
        {
            get { return _Set1Home; }
            set { _Set1Home = value; this.NotifyPropertyChanged(ID); }
        }

        private int _Set1Away = 0;

        public virtual int Set1Away
        {
            get { return _Set1Away; }
            set { _Set1Away = value; this.NotifyPropertyChanged(ID); }
        }

        private int _Set2Home = 0;

        public virtual int Set2Home
        {
            get { return _Set2Home; }
            set { _Set2Home = value; this.NotifyPropertyChanged(ID); }
        }

        private int _Set2Away = 0;

        public int Set2Away
        {
            get { return _Set2Away; }
            set { _Set2Away = value; this.NotifyPropertyChanged(ID); }
        }

        private int _Set3Home = 0;

        public virtual int Set3Home
        {
            get { return _Set3Home; }
            set { _Set3Home = value; this.NotifyPropertyChanged(ID); }
        }


        private int _Set3Away = 0;

        public int Set3Away
        {
            get { return _Set3Away; }
            set { _Set3Away = value; this.NotifyPropertyChanged(ID); }
        }


        private bool _IsBusy = false;

        public virtual bool IsBusy
        {
            get { return _IsBusy; }
            set { _IsBusy = value; this.NotifyPropertyChanged(ID); if (_IsBusy && !_Isplayed) { _Terrein.Status = false; } else { _Terrein.Status = true; } }
        }

        private bool _IsStarted = false;

        public virtual bool IsStarted
        {
            get { return _IsStarted; }
            set { _IsStarted = value; this.NotifyPropertyChanged(ID); if (_IsBusy && !_Isplayed) { _Terrein.Status = false; } else { _Terrein.Status = true; } }
        }
        private bool _Isplayed = false;

        public virtual bool Isplayed
        {
            get { return _Isplayed; }
            set { _Isplayed = value; this.NotifyPropertyChanged(ID); if (_IsBusy && !_Isplayed) { _Terrein.Status = false; } else { _Terrein.Status = true; } }
        }


    }

    /*
     * 
     * http://stackoverflow.com/questions/4017901/fluent-nhibernate-cascade-delete-not-working
       none - do not do any cascades, let the users handles them by themselves.
    save-update - when the object is saved/updated, check the associations and save/update any object that require it (including save/update the associations in many-to-many scenario).
    delete - when the object is deleted, delete all the objects in the association.
    delete-orphan - when the object is deleted, delete all the objects in the association. In addition to that, when an object is removed from the association and not associated with another object (orphaned), also delete it.
    all - when an object is save/update/delete, check the associations and save/update/delete all the objects found.
    all-delete-orphan - when an object is save/update/delete, check the associations and save/update/delete all the objects found. In additional to that, when an object is removed from the association and not associated with another object (orphaned), also delete it.           
     * 
     */


    public class WedstrijdMapping : ClassMap<Wedstrijd>
    {
        public WedstrijdMapping()
        {
            Id(x => x.ID)
                .GeneratedBy.Increment()
                .Not.Nullable();
            Map(x => x.Set1Away);
            Map(x => x.Set1Home);
            Map(x => x.Set2Away);
            Map(x => x.Set2Home);
            Map(x => x.Set3Away);
            Map(x => x.Set3Home);
            Map(x => x.IsBusy);
            Map(x => x.IsStarted);
            Map(x => x.Isplayed);
            Map(x => x.ReeksNaam);
            Map(x => x.Aanvangsuur);
            References(x => x.Away);
            References(x => x.Home);
            //References(x => x.Terrein).Cascade.SaveUpdate();
            References(x => x.Terrein);
            References(x => x.Scheidsrechter);
         
        }
    }
}
