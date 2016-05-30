using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using structures;

namespace TestProjectNhibernate.TestStructure
{
    public class TopClass : Structurebase
    {
        public virtual int ID { get; set; }


        private string _Name;
        public virtual string Name 
        {
            get {return _Name;}
            set { _Name = value; this.NotifyPropertyChanged(ID); }
        }


        private SubClass _subclass;
        public virtual SubClass subclass 
        {
            get { return _subclass; }
            set { _subclass = value; this.NotifyPropertyChanged(ID); }
        }

        public string Voornaam
        {
            get 
            {
                if (subclass != null) return subclass.Voornaam;
                else return "";
            }
            set 
            {
                if (subclass == null)
                {
                    subclass = new SubClass();
                }
                subclass.Voornaam = value;
                subclass.TopClasslist.Add(this);
            }
        }

        public class TopClassMapping : ClassMap<TopClass>
        {
            public TopClassMapping()
            {
                Id(x => x.ID)
                    .GeneratedBy.Increment()
                    .Not.Nullable();
                Map(x => x.Name);
                References(x => x.subclass).Cascade.SaveUpdate ();
            }
        }
    }


}
