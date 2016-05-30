using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace structures
{
    public class WedstrijdDefinition
    {

        public virtual int ID
        {
            get;
            set;
        }

        private int _MaxNaElkaarSpelen = 2;

        public virtual int MaxNaElkaarSpelen
        {
            get { return _MaxNaElkaarSpelen; }
            set { _MaxNaElkaarSpelen = value; }
        }



        private int _AantalRoundRobin = 1;

        public virtual int AantalRoundRobin
        {
            get { return _AantalRoundRobin; }
            set { _AantalRoundRobin = value; }
        }

        private int _AantalrondesZaterdag = 10;

        public virtual int AantalrondesZaterdag
        {
            get { return _AantalrondesZaterdag; }
            set { _AantalrondesZaterdag = value; }
        }

        private DateTime _AanvangsuurZat = new DateTime(2016, 8, 20, 10, 30, 0);

        public virtual DateTime AanvangsuurZat
        {
            get { return _AanvangsuurZat; }
            set { _AanvangsuurZat = value; }
        }

        private DateTime _AanvangsuurZon = new DateTime(2016, 8, 21, 10, 00, 0);

        public virtual DateTime AanvangsuurZon
        {
            get { return _AanvangsuurZon; }
            set { _AanvangsuurZon = value; }
        }

        private int _Wedstrijdduur = 50;

        public virtual int Wedstrijdduur
        {
            get { return _Wedstrijdduur; }
            set { _Wedstrijdduur = value; }
        }

        private List<DateTime> _StartingTimes = new List<DateTime>();

        public List<DateTime> StartingTimes
        {
            get { return _StartingTimes; }
            set { _StartingTimes = value; }
        }

    }

    class WedstrijdDefinitionMapping : ClassMap<WedstrijdDefinition>
    {
        public WedstrijdDefinitionMapping()
        {
            Id(x => x.ID)
                .GeneratedBy.Increment()
                .Not.Nullable();

            Map(x => x.AantalrondesZaterdag);
            Map(x => x.AantalRoundRobin);
            Map(x => x.AanvangsuurZat);
            Map(x => x.AanvangsuurZon);
            Map(x => x.MaxNaElkaarSpelen);
            Map(x => x.Wedstrijdduur);            
        }
    }
}
