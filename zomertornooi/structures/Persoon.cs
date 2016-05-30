using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CustomAttributes;
using FluentNHibernate.Mapping;

namespace structures
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Persoon : Structurebase
    {

        void Datachanged()
        {
            this.NotifyPropertyChanged(ID);
        }


        [Browsable(false)]
        public virtual int ID
        {
            get;
            set;
        }


        public virtual int Version { get; set; }  


        private string _Naam = "";

        public virtual string Naam
        {
            get { return _Naam; }
            set { _Naam = value; this.NotifyPropertyChanged(ID); }
        }


        private string _Voornaam = "";

        public virtual string Voornaam
        {
            get { return _Voornaam; }
            set { _Voornaam = value; this.NotifyPropertyChanged(ID); }
        }

        private string _Straat = "";

        public virtual string Straat
        {
            get { return _Straat; }
            set { _Straat = value; this.NotifyPropertyChanged(ID); }
        }

        private string _Nr = "";

        public virtual string Nr
        {
            get { return _Nr; }
            set { _Nr = value; this.NotifyPropertyChanged(ID); }
        }

        private string _Woonplaats = "";

        public virtual string Woonplaats
        {
            get { return _Woonplaats; }
            set { _Woonplaats = value; this.NotifyPropertyChanged(ID); }
        }

        private string _Postcode = "";

        public virtual string Postcode
        {
            get { return _Postcode; }
            set { _Postcode = value; this.NotifyPropertyChanged(ID); }
        }

        private string _Land = "Belgie";

        public virtual string Land
        {
            get { return _Land; }
            set { _Land = value; this.NotifyPropertyChanged(ID); }
        }

        private string _TelNr = "";

        public virtual string TelNr
        {
            get { return _TelNr; }
            set { _TelNr = value; this.NotifyPropertyChanged(ID); }
        }

        private string _GSMNr = "";

        public virtual string GSMNr
        {
            get { return _GSMNr; }
            set { _GSMNr = value; this.NotifyPropertyChanged(ID); }
        }

        private string _Email = "";

        public virtual string Email
        {
            get { return _Email; }
            set { _Email = value; this.NotifyPropertyChanged(ID); }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public override string ToString()
        {
            /*string temp = "";
            PropertyInfo[] properties = typeof(Persoon).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(Address))
                {
                    //temp += ((Address)property.GetValue(this)).ToString();
                }
                else if (property.PropertyType == typeof(Contact))
                {
                    //temp += ((Contact)property.GetValue(this)).ToString();
                }
                else
                {
                    try
                    {
                       // temp += property.Name + " : " + property.GetValue(this) + "\r\n";
                    }
                    catch
                    {
                    }
                }
            }*/

            
            return Voornaam + " " + Naam;
        }
    }


    public delegate void del_datachanged();

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Address 
    {
        public event del_datachanged datachanged;

        private void InvokeDataChanged()
        {
            if (datachanged != null)
            {
                datachanged.Invoke();
            }
        }

        private string _Straat = "";

        public virtual string Straat
        {
            get { return _Straat; }
            set { _Straat = value; InvokeDataChanged(); }
        }

        private  string _Nr = "";

        public virtual string Nr
        {
            get { return _Nr; }
            set { _Nr = value; InvokeDataChanged(); }
        }

        private string _Woonplaats = "";

        public virtual string Woonplaats
        {
            get { return _Woonplaats; }
            set { _Woonplaats = value; InvokeDataChanged(); }
        }

        private string _Postcode = "";

        public virtual string Postcode
        {
            get { return _Postcode; }
            set { _Postcode = value; InvokeDataChanged(); }
        }

        private  string _Land = "Belgie";

        public virtual string Land
        {
            get { return _Land; }
            set { _Land = value; InvokeDataChanged(); }
        }
        [Browsable(false)]
        public override string ToString()
        {
            /*string temp = "";
            PropertyInfo[] properties = typeof(Address).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                try
                {
                    temp += property.Name + " : " + property.GetValue(this) + "\r\n";
                }
                catch
                { }
            }
            return temp;*/
            return Straat + " " + Nr;
        }
    }
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Contact
    {
        public event del_datachanged datachanged;

        private void InvokeDataChanged()
        {
            if (datachanged != null)
            {
                datachanged.Invoke();
            }
        }


        private string _TelNr = "";

        public virtual string TelNr
        {
            get { return _TelNr; }
            set { _TelNr = value; InvokeDataChanged(); }
        }

        private string _GSMNr = "";

        public virtual string GSMNr
        {
            get { return _GSMNr; }
            set { _GSMNr = value; InvokeDataChanged(); }
        }

        private string _Email = "";

        public virtual string Email
        {
            get { return _Email; }
            set { _Email = value; InvokeDataChanged(); }
        }
        [Browsable(false)]
        public override string ToString()
        {
            /*string temp = "";
            PropertyInfo[] properties = typeof(Contact).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                try
                {
                    temp += property.Name + " : " + property.GetValue(this) + "\r\n";
                }
                catch
                {

                }
            }
            return temp;*/

            return _GSMNr;
        }
    }

    public class PersoonMapping : ClassMap<Persoon>
    {
        public PersoonMapping()
        {
            Id(x => x.ID)
            .GeneratedBy.Increment()
            .Not.Nullable();

            /*Version(x => x.Version)
                .Generated.Always()
                .Not.Nullable();
                //.UnsavedValue("0");*/

            Map(x => x.Naam).Not.Nullable();
            Map(x => x.Voornaam).Not.Nullable();
            
            Map(x => x.Straat);
            Map(x => x.Nr);
            Map(x => x.Woonplaats);
            Map(x => x.Postcode);
            Map(x => x.Land);
            Map(x => x.TelNr);
            Map(x => x.GSMNr);
            Map(x => x.Email);         

            //DynamicUpdate();
            //OptimisticLock.Version();
            ///add specific Table name - otherwise function to get Table Name will return "[<TableName>]" iso "<TableName>"
            Table(typeof(Persoon).Name);

        }
        
    }


}
