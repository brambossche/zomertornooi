using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using structures;

namespace TestProjectNhibernate.TestStructure
{
    public class SubClass : Structurebase
    {
        public virtual int ID { get; set; }

        private string _Voornaam;

        public virtual string Voornaam 
        {
            get { return _Voornaam; }
            set { _Voornaam = value;  this.NotifyPropertyChanged(ID); }
        }

        private IList<TopClass> _TopClasslist = new List<TopClass>();
        public virtual IList<TopClass> TopClasslist { get { return _TopClasslist; } set { _TopClasslist = value; } }

        public override string ToString()
        {
            return Voornaam;
        }

        public class  SubClassMap : ClassMap<SubClass>
        {
            public SubClassMap()
            {
                Id(x => x.ID)
                    .GeneratedBy.Increment()
                    .Not.Nullable();
                Map(x => x.Voornaam);
                HasMany<TopClass>(x => x.ID).Cascade.AllDeleteOrphan().Inverse();
            }
        }
    }
}
